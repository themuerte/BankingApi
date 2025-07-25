using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Api.Models
{
    public class AccountBankModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        [Required]
        public DateTime DateLastUpdate { get; set; }

        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }

        public ICollection<TransactionsModel> Transactions { get; set; } = new List<TransactionsModel>();
    }
}