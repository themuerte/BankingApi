using BankingAPI.Api.DTOs;
using BankingAPI.Api.Models;

namespace BankingAPI.Api.Services
{
    public interface IBankingService
    {
        Task<ClientModel> CreateClient(CreateClientDto data);
        Task<AccountBankModel> CreateAccount(CreateAccountDto data);
        Task<decimal?> GetBalance(long accountNumber);
        Task<TransactionsModel> CreateTransaction(long accountNumber, TransactionDto data);
        Task<List<TransactionsModel>> GetTransactionsAsync(long accountNumber);
    }
}