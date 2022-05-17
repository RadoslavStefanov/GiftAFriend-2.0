using GAF.Core.Models;
using GAF.Infrastructure.Data;

namespace GAF.Core.Services
{
    public class TransactionsService
    {
        private readonly GAFDbContext repo;
        public TransactionsService(GAFDbContext _repo)
        {repo = _repo;}

        public async Task<List<AllUserTransactionsModel>> getAllTransactions()
        {
            var allTransactions = repo.TransferEvents.ToList();
            var result = new List<AllUserTransactionsModel>();

            foreach (var t in allTransactions)
            {
                result.Add(new AllUserTransactionsModel
                {
                    senderUserName = t.SenderName,
                    recieverUserName = t.RecieverName,
                    Amount = t.Amount,
                    DateTime = t.DateTime,
                });
            }

            return result;
        }
    }
}
