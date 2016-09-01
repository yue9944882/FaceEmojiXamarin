using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
namespace FaceEmojiClient 
{
    public class AccountService
    {
        private static AccountService instance;

        public static AccountService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountService();
                }

                return instance;
            }
        }

        public string AuthenticationToken { get; set; }
        public Account Account { get; set; }
        public User User { get; set; }

        public bool ReadyToSignIn
        {
            get { return !string.IsNullOrEmpty(AuthenticationToken); }
        }

        private AccountService()
        {
            FetchAuthenticationToken();
        }

        void FetchAuthenticationToken()
        {
            
        }

        public async Task Register(Account account, User user)
        {


        }



        public async Task<bool> Login()
        {

        }

        public async Task<bool> Login(Account account)
        {

        }

        public void SignOut()
        {
        }

        public async Task<bool> DeleteAccount()
        {
            bool result;

            try
            {
                using (var handler = new AuthenticationHandler())
                {
                    using (var client = MobileServiceClientFactory.CreateClient(handler))
                    {
                        // Account
                        var accountTable = client.GetTable<Account>();
                        await accountTable.DeleteAsync(Account);

                        // User
                        var userTable = client.GetTable<User>();
                        await userTable.DeleteAsync(User);

                        // Friendships
                        var friendships = await client.GetTable<Friendship>()
                            .Where(friendship => friendship.UserId == AccountService.Instance.User.Id).Select(friendship => friendship).ToListAsync();

                        friendships.AddRange(await client.GetTable<Friendship>()
                            .Where(friendship => friendship.FriendId == AccountService.Instance.User.Id).Select(friendship => friendship).ToListAsync());

                        var friendshipsTable = client.GetTable<Friendship>();
                        foreach (var friend in friendships)
                        {
                            await friendshipsTable.DeleteAsync(friend);
                        }

                        // Moments
                        var moments = await client.GetTable<Moment>()
                            .Where(moment => moment.SenderUserId == AccountService.Instance.User.Id).Select(moment => moment).ToListAsync();

                        moments.AddRange(await client.GetTable<Moment>()
                            .Where(moment => moment.RecipientUserId == AccountService.Instance.User.Id).Select(moment => moment).ToListAsync());

                        var momentsTable = client.GetTable<Moment>();
                        foreach (var moment in moments)
                        {
                            await momentsTable.DeleteAsync(moment);
                        }
                    }
                }

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        private async Task Insert(Account account, bool isLogin)
        {
            
        }

        private async Task<Account> GetCurrentAccount(Account account)
        {
            
        }

        private async Task<User> GetCurrentUser()
        {

        }
    }
}
*/