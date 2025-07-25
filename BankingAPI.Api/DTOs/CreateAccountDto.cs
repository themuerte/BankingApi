namespace BankingAPI.Api.DTOs
{
    public class CreateAccountDto
    {
        public int ClientId { get; set; }
        public decimal InitialBalance { get; set; }
    }
}