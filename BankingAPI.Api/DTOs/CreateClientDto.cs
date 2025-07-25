using BankingAPI.Api.Models;

namespace BankingAPI.Api.DTOs
{
    public class CreateClientDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public SexTypeEnum Sex { get; set; }
        public decimal Income { get; set; }
    }
}