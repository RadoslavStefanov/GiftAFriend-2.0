using GAF.Core.Models;
using GAF.Infrastructure.Data;
using GAF.Infrastructure.Data.Models;

namespace GAF.Core.Services
{
    public class UsersService
    {
        private readonly GAFDbContext repo;
        public UsersService(GAFDbContext _repo)
        {
            repo = _repo;
        }
        public bool hasInfo(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            if (repo.UserInfos.FirstOrDefault(ui => ui.UserId == user.Id) == null)
            { return false; }

            else { return true; }
        }

        public async Task generateInfo(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            await repo.UserInfos.AddAsync(new UserInfos
            {
                UserId = user.Id,
                GiftTokens = 0
            });
            await repo.SaveChangesAsync();
        }

        public async Task<decimal> getUserTokens(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            { return repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id).GiftTokens; }
            return 0;
        }

        public async Task<string>? getUserNotifications(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            { return null; }

            var result = "";
            if (!await hasCompleteInfo(user))
            { result+= "-accountSettings"; }

            if (await hasFriendRequests(user))
            { result+="-friends-friendRequests"; }

            return result;
        }

        private async Task<bool> hasCompleteInfo(Microsoft.AspNetCore.Identity.IdentityUser user)
        {
            var info = repo.UserInfos.FirstOrDefault(ui => ui.UserId == user.Id);
            if (info==null || info.MobileNumber == null  || info.MobileNumber.Length < 5)
            { return false; }

            return true;
        }

        private async Task<bool> hasFriendRequests(Microsoft.AspNetCore.Identity.IdentityUser user)
        {return repo.FriendRequests.Any(x => x.RecieverId == user.Id);}

        public async Task<List<UserTransactionsModel>> getUserTransactions(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            var result = new List<UserTransactionsModel>();
            var userTransactions = repo.TransferEvents.Where(te => te.SenderId == user.Id || te.RecieverId==user.Id).Take(20).ToList();

            foreach (var item in userTransactions)
            {

                var type = "";
                var fromToUser = "";
                var fromToUserId = "";
                if (item.SenderId == user.Id) 
                { 
                    type = "sent";
                    fromToUser = repo.Users.FirstOrDefault(u => u.Id == item.RecieverId).UserName ?? "Unknown User";
                    fromToUserId = repo.Users.FirstOrDefault(u => u.Id == item.RecieverId).Id ?? "Unknown User";
                }
                else 
                { 
                    type = "recieved";
                    fromToUser = repo.Users.FirstOrDefault(u => u.Id == item.SenderId).UserName ?? "Unknown User";
                    fromToUserId = repo.Users.FirstOrDefault(u => u.Id == item.SenderId).Id ?? "Unknown User";
                }

                result.Add(new UserTransactionsModel
                {
                    Id = item.Id,
                    DateTime = item.DateTime,
                    Type = type,
                    FromToUser = fromToUser,
                    FromToUserId = fromToUserId,
                    Message = item.Message
                });
            }

            return result.OrderBy(x=>x.DateTime).ToList();
        }

        public async Task<string?> sendGift(string userName, GiftSendPostModel model)
        {
                var user = repo.Users.FirstOrDefault(x => x.UserName == userName);

                if (repo.UserInfos.FirstOrDefault(x => x.MobileNumber == model.PhoneNumber) == null) 
                { return "error-Please fill the fields with the proper values!:"; }

                var recieverId = repo.UserInfos.FirstOrDefault(x => x.MobileNumber == model.PhoneNumber).UserId;

                var userNumber = repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id).MobileNumber;

                if (userNumber == null) { return "error-You cannot send gifts until you provide personal mobile number!:"; }
                if (recieverId == null) { return "error-Unknown MobileNumber!:"; }
                if (!giftModelIsValid(model, user)) { return "error-Please fill the fields with the proper values!:"; }
                return await registerGiftTransaction(model, user, recieverId);
            
        }

        private bool giftModelIsValid(GiftSendPostModel model, Microsoft.AspNetCore.Identity.IdentityUser user)
        {
            var userTokens = repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id).GiftTokens;
            if (model.Amount > userTokens) { return false; }
            if (model.Amount == 0) { return false; }
            return true;
        }

        private async Task<string?> registerGiftTransaction(GiftSendPostModel model, Microsoft.AspNetCore.Identity.IdentityUser user, string recieverId)
        {
            try
            {
                var reciever = repo.Users.FirstOrDefault(x => x.Id == recieverId);

                var senderTokens = repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id).GiftTokens-=model.Amount;
                var recieverTokens = repo.UserInfos.FirstOrDefault(x => x.UserId == reciever.Id).GiftTokens += model.Amount;

                await repo.TransferEvents.AddAsync(new TransferEvents
                {
                    RecieverId = reciever.Id,
                    RecieverName = reciever.UserName,
                    SenderId = user.Id,
                    SenderName = user.UserName,
                    DateTime = DateTime.UtcNow,
                    Message = model.Message ?? "No message was added."
                });
                await repo.SaveChangesAsync();
                return "success-Gift successfully sent!";
            }
            catch (Exception)
            { return "error-Something went wrong! Please try again later!"; }
            
        }

        public async Task<UserInfosModel?> getUserSettings(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            var userInfo = repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id);
            return new UserInfosModel
            {
                userId = user.Id,
                MobileNumber = userInfo.MobileNumber,
                Address = userInfo.Address
            };
        }

        public async Task<string?> applyUserSettings(string userName,UserInfosModel model)
        {
            var result = "";
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            var userInfo = repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id);
            if (userInfo.Address == model.Address && userInfo.MobileNumber == model.MobileNumber)
            { return "error-No changes detected!:"; }

            if (userInfo.Address != model.Address)
            {
                userInfo.Address = model.Address;
                await repo.SaveChangesAsync();
                result += "success-Successfuly changed address!:";
            }

            if (userInfo.MobileNumber == null && model.MobileNumber != null)
            {
                if (repo.UserInfos.Any(x => x.MobileNumber == model.MobileNumber))
                { result += "error-This number already exists!:"; }
                else
                {
                    userInfo.MobileNumber = model.MobileNumber;
                    await repo.SaveChangesAsync();
                    result += "success-Successfuly changed numer!:";
                }
            }

            if (userInfo.MobileNumber != null && model.MobileNumber != userInfo.MobileNumber)
            {result += "error-You cannot change your number, once set!:";}

            return result;
        }

        public async Task giveNewUserBonus(string userName)
        {
            var user = repo.Users.FirstOrDefault(x => x.UserName == userName);
            repo.UserInfos.FirstOrDefault(x => x.UserId == user.Id).GiftTokens += 100 ;
            await repo.SaveChangesAsync();
        }
    }
}

