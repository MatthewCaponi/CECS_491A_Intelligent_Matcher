﻿using System;
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
        public async Task<bool> SendMessageAsync(MessageModel model)
        {

            try
            {
                IEnumerable<MessageModel> models = await _messagesRepo.GetAllMessagesByChannelIdAsync(model.ChannelId);

                model.ChannelMessageId = models.Count();
                model.Date = DateTime.Now;
                model.Time = DateTime.Now.ToString().Split(' ')[1];

                UserAccountModel userAccountModel = await _userAccountRepository.GetAccountById(model.UserId);
                model.Username = userAccountModel.Username;
                await _messagesRepo.CreateMessageAsync(model);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public async Task<IEnumerable<MessageModel>> GetAllChannelMessagesAsync(int channelId)
        {

                IEnumerable<MessageModel> models = await _messagesRepo.GetAllMessagesByChannelIdAsync(channelId);

                return models;


        }

        public async Task<bool> CreateChannelAsync(ChannelModel model)
        {

            await _userChannelsRepo.AddUserChannelAsync(model.OwnerId, await _channelsRepo.CreateChannelAsync(model));

            return true;
        }

        public async Task<bool> DeleteChannelAsync(int id)
        {
            try
            {
                await _channelsRepo.DeleteChannelbyIdAsync(id);
                await _userChannelsRepo.RemoveChannelUsingChannelIdAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveUserFromChannelAsync(int channelId, int userId)
        {
            try
            {
                await _userChannelsRepo.RemoveUserIdChannelIdAsync(userId, channelId);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> DeleteMessageAsync(int messageId)
        {
            try
            {
                await _messagesRepo.DeleteMessageByIdAsync(messageId);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> AddUserToChannelAsync(int userId, int channelId)
        {
            try
            {
                await _userChannelsRepo.AddUserChannelAsync(userId, channelId);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<IEnumerable<UserIdModel>> GetAllUsersInChannelAsync(int channelId)
        {
            IEnumerable<int> userIds = await _userChannelsRepo.GetAllUsersByChannelIdAsync(channelId);
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

        public async Task<IEnumerable<ChannelModel>> GetAllUserChannelsAsync(int userId)
        {
            IEnumerable<int> channelIds = await _userChannelsRepo.GetAllChannelsByUserIdAsync(userId);
            List<ChannelModel> channelModelsList = new List<ChannelModel>();
            foreach (int channelId in channelIds)
            {
                ChannelModel newModel = await _channelsRepo.GetChannelbyIdAsync(channelId);
                if(newModel != null)
                {
                    channelModelsList.Add(newModel);

                }
            }

            IEnumerable<ChannelModel> channelModels = channelModelsList;

            return channelModels;


        }

        public async Task<int> GetChannelOwnerAsync(int id)
        {
            return await _channelsRepo.GetChannelOwnerbyIdAsync(id);
        }
    }
}
