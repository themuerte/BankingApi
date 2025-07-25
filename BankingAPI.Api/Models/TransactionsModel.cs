using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Api.Models
{
    public class TransactionsModel
    {
        public int Id { get; set; }

        public required TransactionTypeEnum TransactionType { get; set; }

        public required string Description { get; set; }

        public required decimal Amount { get; set; }

        public required StatusTransactionEnum Status { get; set; }

        public required DateTime DateCreation { get; set; }

        public required DateTime DateLastUpdate { get; set; }

        // Account
        public int AccountBankId { get; set; }

        [ForeignKey(nameof(AccountBankId))]
        public AccountBankModel? AccountBank { get; set; }

    }
}
