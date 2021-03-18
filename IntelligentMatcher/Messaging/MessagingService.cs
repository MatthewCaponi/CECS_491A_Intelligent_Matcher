using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using DataAccess.Repositories;
using Models;

namespace Messaging
{


    public class MessagingService : IMessagingService
    {
        private readonly IMessagesRepo _messagesRepo;
        private readonly IChannelsRepo _channelsRepo;
        private readonly IUserChannelsRepo _userChannelsRepo;
        private readonly IUserAccountRepository _userAccountRepository;


        public MessagingService(IMessagesRepo messagesRepo, IChannelsRepo channelsRepo, IUserChannelsRepo userChannelsRepo, IUserAccountRepository userAccountRepository)
        {
            _messagesRepo = messagesRepo;
            _channelsRepo = channelsRepo;
            _userChannelsRepo = userChannelsRepo;
            _userAccountRepository = userAccountRepository;
        }
        public async Task<bool> sendMessageAsync(MessageModel model)
        {

            try
            {
                IEnumerable<MessageModel> models = await _messagesRepo.GetAllMessagesByChannelId(model.ChannelId);

                model.ChannelMessageId = models.Count();
                model.Date = DateTime.Now;
                model.Time = DateTime.Now.ToString().Split(' ')[1];

                UserAccountModel userAccountModel = await _userAccountRepository.GetAccountById(model.UserId);
                model.Username = userAccountModel.Username;
                await _messagesRepo.CreateMessage(model);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public async Task<IEnumerable<MessageModel>> GetAllChannelMessagesAsync(int ChannelId)
        {
            IEnumerable<MessageModel> models = await _messagesRepo.GetAllMessagesByChannelId(ChannelId);

            return models;

        }

        public async Task<bool> CreateChannelAsync(ChannelModel model)
        {

            await _userChannelsRepo.AddUserChannel(model.OwnerId, await _channelsRepo.CreateChannel(model));

            return true;
        }

        public async Task<bool> DeleteChannelAsync(int id)
        {

            await _channelsRepo.DeleteChannelbyId(id);
            await _userChannelsRepo.RemoveChannelUsingChannelId(id);
            return true;
        }

        public async Task<bool> RemoveUserFromChannelAsync(int channelId, int userId)
        {

            await _userChannelsRepo.RemoveUserIdChannelId(userId, channelId);
            return true;
        }

        public async Task<bool> AddUserToChannelAsync(int UserId, int ChannelId)
        {

            await _userChannelsRepo.AddUserChannel(UserId, ChannelId);
            return true;
        }

        public async Task<IEnumerable<UserIdModel>> GetAllUsersInChannelAsync(int channelId)
        {
            IEnumerable<int> userIds = await _userChannelsRepo.GetAllUsersByChannelId(channelId);
            List<UserIdModel> userIdModels = new List<UserIdModel>();
            foreach(int userId in userIds)
            {
                UserIdModel model = new UserIdModel();
                model.UserId = userId;
                UserAccountModel userAcountModel = await _userAccountRepository.GetAccountById(userId);
                model.Username = userAcountModel.Username;
                userIdModels.Add(model);
            }

            IEnumerable<UserIdModel> userIdModelI = userIdModels;

            return userIdModelI;


        }

        public async Task<IEnumerable<ChannelModel>> GetAllUserChannelsAsync(int UserId)
        {
            IEnumerable<int> channelIds = await _userChannelsRepo.GetAllChannelsByUserId(UserId);
            List<ChannelModel> channelModelsList = new List<ChannelModel>();
            foreach (int channelId in channelIds)
            {
                ChannelModel newModel = await _channelsRepo.GetChannelbyId(channelId);
                if(newModel != null)
                {
                    channelModelsList.Add(newModel);

                }
            }

            IEnumerable<ChannelModel> channelModels = channelModelsList;

            return channelModels;


        }

        public async Task<string> GetChannelOwnerAsync(int id)
        {
            return await _channelsRepo.GetChannelOwnerbyId(id);
        }
    }
}
