using BankingAPI.Api.Models;

namespace BankingAPI.Api.DTOs
{
    public class TransactionDto
    {
        public TransactionTypeEnum transactionType { get; set; }
        public decimal Amount { get; set; }
    }
}