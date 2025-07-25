using Microsoft.AspNetCore.Mvc;
using BankingAPI.Api.Services;
using BankingAPI.Api.DTOs;
using BankingAPI.Api.Models;

namespace BankingAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly IBankingService _service;

        public BankingController(IBankingService service)
        {
            _service = service;
        }

        [HttpPost("client")]
        public async Task<ActionResult<ClientModel>> CreateClient([FromBody] CreateClientDto dto)
        {
            var client = await _service.CreateClient(dto);
            return CreatedAtAction(nameof(CreateClient), new { id = client.Id }, client);
        }

        [HttpPost("account")]
        public async Task<ActionResult<AccountBankModel>> CreateAccount([FromBody] CreateAccountDto dto)
        {
            var account = await _service.CreateAccount(dto);
            return CreatedAtAction(nameof(GetBalance), new { accountNumber = account.AccountNumber }, account);
        }

        [HttpGet("account/{accountNumber}/balance")]
        public async Task<ActionResult<decimal>> GetBalance(long accountNumber)
        {
            var balance = await _service.GetBalance(accountNumber);
            if (balance == null) return NotFound();
            return Ok(new
            {
                balance
            });
        }

        [HttpPost("account/{accountNumber}/transaction")]
        public async Task<ActionResult<TransactionsModel>> CreateTransaction(long accountNumber, [FromBody] TransactionDto dto)
        {
            var transaction = await _service.CreateTransaction(accountNumber, dto);
            if (transaction == null) return BadRequest("No se pudo registrar la transacción.");
            return Ok(new
            {
                transaction.Id,
                transaction.TransactionType,
                transaction.Amount,
                transaction.Balance,
                transaction.Status,
                transaction.DateCreation
            });
        }

        // En tu controlador
        [HttpGet("account/{accountNumber}/transactions")]
        public async Task<ActionResult<object>> GetTransactions(long accountNumber)
        {
            var transactions = await _service.GetTransactionsAsync(accountNumber);

            var result = transactions.Select(t => new
            {
                t.Id,
                t.TransactionType,
                t.Amount,
                t.Balance,
                t.Status,
                t.DateCreation
            }).ToList();

            var saldoFinal = result.LastOrDefault()?.Balance ?? 0;

            return Ok(new
            {
                saldoFinal,
                transacciones = result
            });
        }
    }
}