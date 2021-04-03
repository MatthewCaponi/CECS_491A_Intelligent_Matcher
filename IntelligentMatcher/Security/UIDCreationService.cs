using DataAccess;
using DataAccess.Repositories;
using System;
using System.Threading.Tasks;
using System.Text;
using Models;
namespace Security
{
    public class UIDCreationService : IUIDCreationService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private const int UID_LENGTH = 10;

        public UIDCreationService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<string> CreateUIDAsync()
        {


            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < UID_LENGTH; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            string UID = str_build.ToString();

            //UserAccountModel model = await _userAccountRepository.GetAccountById(UID);

            //if(model == null)
            //{
               // await CreateUIDAsync();
            //}
            return UID;
        }
    }
}
