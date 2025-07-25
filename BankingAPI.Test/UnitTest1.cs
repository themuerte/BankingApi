using BankingAPI.Api.Data;
using BankingAPI.Api.Models;
using BankingAPI.Api.Services;
using BankingAPI.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Test;

public class UnitTest1
{
    private BankingDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<BankingDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
        var context = new BankingDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task CrearCuentaBancariaTest()
    {
        var context = GetDbContext();
        var service = new BankingService(context);

        var client = new ClientModel { Name = "Test", LastName = "User", BirthDate = DateTime.Now.AddYears(-30), Sex = SexTypeEnum.Male };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var accountDto = new CreateAccountDto { ClientId = client.Id, InitialBalance = 500 };
        var account = await service.CreateAccount(accountDto);

        Assert.NotNull(account);
        Assert.Equal(500, account.Balance);
        Assert.Equal(client.Id, account.ClientId);
    }

    [Fact]
    public async Task DepositoYRetiroTest()
    {
        var context = GetDbContext();
        var service = new BankingService(context);

        var client = new ClientModel { Name = "Test", LastName = "User", BirthDate = DateTime.Now.AddYears(-30), Sex = SexTypeEnum.Male };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var account = await service.CreateAccount(new CreateAccountDto { ClientId = client.Id, InitialBalance = 100 });

        // Depósito
        var deposito = await service.CreateTransaction(account.AccountNumber, new TransactionDto { transactionType = TransactionTypeEnum.Deposit, Amount = 50 });
        Assert.Equal(StatusTransactionEnum.Appoved, deposito.Status);
        Assert.Equal(150, deposito.Balance);

        // Retiro exitoso
        var retiro = await service.CreateTransaction(account.AccountNumber, new TransactionDto { transactionType = TransactionTypeEnum.Removal, Amount = 50 });
        Assert.Equal(StatusTransactionEnum.Appoved, retiro.Status);
        Assert.Equal(100, retiro.Balance);

        // Retiro fallido (sin fondos)
        var retiroFallido = await service.CreateTransaction(account.AccountNumber, new TransactionDto { transactionType = TransactionTypeEnum.Removal, Amount = 200 });
        Assert.Equal(StatusTransactionEnum.Rejected, retiroFallido.Status);
        Assert.Equal(100, retiroFallido.Balance);
    }

    [Fact]
    public async Task ConsultaSaldoYHistorialTest()
    {
        var context = GetDbContext();
        var service = new BankingService(context);

        var client = new ClientModel { Name = "Test", LastName = "User", BirthDate = DateTime.Now.AddYears(-30), Sex = SexTypeEnum.Male };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var account = await service.CreateAccount(new CreateAccountDto { ClientId = client.Id, InitialBalance = 200 });

        await service.CreateTransaction(account.AccountNumber, new TransactionDto { transactionType = TransactionTypeEnum.Deposit, Amount = 100 });
        await service.CreateTransaction(account.AccountNumber, new TransactionDto { transactionType = TransactionTypeEnum.Removal, Amount = 50 });

        var saldo = await service.GetBalance(account.AccountNumber);
        Assert.Equal(250, saldo);

        var historial = await service.GetTransactionsAsync(account.AccountNumber);
        Assert.Equal(2, historial.Count);
        Assert.Contains(historial, t => t.TransactionType == TransactionTypeEnum.Deposit && t.Amount == 100);
        Assert.Contains(historial, t => t.TransactionType == TransactionTypeEnum.Removal && t.Amount == 50);
    }

    [Fact]
    public async Task AplicacionDeInteresesTest()
    {
        var context = GetDbContext();
        var service = new BankingService(context);

        var client = new ClientModel { Name = "Test", LastName = "User", BirthDate = DateTime.Now.AddYears(-30), Sex = SexTypeEnum.Male };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var account = await service.CreateAccount(new CreateAccountDto { ClientId = client.Id, InitialBalance = 1000 });

        // Simula aplicación de interés (ejemplo: 5%)
        var interes = 0.05m;
        account.Balance += account.Balance * interes;
        context.Accounts.Update(account);
        await context.SaveChangesAsync();

        var saldo = await service.GetBalance(account.AccountNumber);
        Assert.Equal(1050, saldo);
    }
}