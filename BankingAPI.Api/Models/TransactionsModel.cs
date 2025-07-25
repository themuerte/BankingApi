using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Api.Models
{

    public class TransactionsModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TransactionTypeEnum TransactionType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public StatusTransactionEnum Status { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        [Required]
        public DateTime DateLastUpdate { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int AccountBankId { get; set; }
        [ForeignKey(nameof(AccountBankId))]
        public AccountBankModel AccountBank { get; set; }
    }
}