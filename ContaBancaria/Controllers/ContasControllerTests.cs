using System;
using System.Collections.Generic;
using System.Linq;
using ContaBancaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
namespace ContaBancaria.Controllers
{
    public class ContasControllerTests
    {
        private readonly AppDbContext _context;

        public ContasControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);

            // Adiciona alguns dados de seed
            _context.Contas.AddRange(new List<Conta>
            {
                new Conta { Titular = "Titular1", Saldo = 100 },
                new Conta { Titular = "Titular2", Saldo = 200 }
            });

            _context.SaveChanges();
        }

        [Fact]
        public void Deposito_Deve_Retornar_Ok_Result()
        {
            // Arrange
            var controller = new ContasController(_context);

            // Act
            var result = controller.Deposito(1, 100);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Saque_Com_Saldo_Suficiente_Deve_Retornar_Ok_Result()
        {
            // Arrange
            var controller = new ContasController(_context);

            // Act
            var result = controller.Saque(1, 50);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Saque_Com_Saldo_Insuficiente_Deve_Retornar_BadRequest_Result()
        {
            // Arrange
            var controller = new ContasController(_context);

            // Act
            var result = controller.Saque(1, 5000);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Transferencia_Com_Saldo_Suficiente_Deve_Retornar_Ok_Result()
        {
            // Arrange
            var controller = new ContasController(_context);

            // Act
            var result = controller.Transferencia(1, 2, 30);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Transferencia_Com_Saldo_Insuficiente_Deve_Retornar_BadRequest_Result()
        {
            // Arrange
            var controller = new ContasController(_context);

            // Act
            var result = controller.Transferencia(1, 2, 1500);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Extrato_Deve_Retornar_Ok_Result()
        {
            // Arrange
            var controller = new ContasController(_context);

            // Act
            var result = controller.Extrato(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
