using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Api.Models
{
    public class ClientModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public long AccountNumber { get; set; }

        [Required]
        public SexTypeEnum Sex { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        [Required]
        public DateTime DateLastUpdate { get; set; }


        public ICollection<AccountBankModel> Accounts { get; set; } = new List<AccountBankModel>();
    }
}
