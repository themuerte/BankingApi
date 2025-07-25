using BankingAPI.Api.DTOs;
using BankingAPI.Api.Models;
using BankingAPI.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Api.Services
{
    public class BankingService : IBankingService
    {
        private readonly BankingDbContext _context;

        public BankingService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<ClientModel> CreateClient(CreateClientDto data)
        {
            var client = new ClientModel
            {
                Name = data.Name,
                LastName = data.LastName,
                BirthDate = data.BirthDate,
                Sex = data.Sex,
                
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<AccountBankModel> CreateAccount(CreateAccountDto data)
        {
            var random = new Random();
            var accountNumber = 0;
            do
            {
                accountNumber = random.Next(10000000, 99999999);
            }
            while (await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber));

            var account = new AccountBankModel
            {
                AccountNumber = accountNumber,
                Balance = data.InitialBalance,
                ClientId = data.ClientId
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<TransactionsModel> CreateTransaction(long accountNumber, TransactionDto data)
        {
            var account = await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new Exception("La cuenta bancaria no existe.");
            }

            var transactionType = data.transactionType;
            var status = StatusTransactionEnum.Appoved;
            decimal newBalance = account.Balance;

            if (transactionType == TransactionTypeEnum.Deposit)
            {
                newBalance += data.Amount;
            }
            else if (transactionType == TransactionTypeEnum.Removal)
            {
                if (account.Balance < data.Amount)
                {
                    status = StatusTransactionEnum.Rejected;
                }
                else
                {
                    newBalance -= data.Amount;
                }
            }

            var transaction = new TransactionsModel
            {
                TransactionType = transactionType,
                Amount = data.Amount,
                Status = status,
                DateCreation = DateTime.UtcNow,
                Balance = (status == StatusTransactionEnum.Appoved) ? newBalance : account.Balance,
                AccountBankId = account.Id
            };

            account.Transactions.Add(transaction);

            if (status == StatusTransactionEnum.Appoved)
            {
                account.Balance = newBalance;
            }

            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<decimal?> GetBalance(long accountNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            return account?.Balance;
        }

        public async Task<List<TransactionsModel>> GetTransactionsAsync(long accountNumber)
        {
             var account = await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new Exception("La cuenta bancaria no existe."); 
            }

            var transactions = account.Transactions.OrderBy(t => t.DateCreation).ToList();

            return transactions;

        }
    }
}