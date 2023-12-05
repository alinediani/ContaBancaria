using System;
using System.Collections.Generic;
using ContaBancaria.Controllers;
using ContaBancaria.Models;
using ContaBancaria.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContaBancaria.Tests.Controllers
{
    public class ContasControllerTests
    {
        private readonly Mock<IContaService> _contaServiceMock;
        private readonly ContasController _contasController;

        public ContasControllerTests()
        {
            _contaServiceMock = new Mock<IContaService>();
            _contasController = new ContasController(_contaServiceMock.Object);
        }

        [Fact]
        public void Deposito_Deve_Retornar_Ok_Result()
        {
            // Arrange
            _contaServiceMock.Setup(x => x.Deposito(It.IsAny<int>(), It.IsAny<decimal>()));

            // Act
            var result = _contasController.Deposito(1, 100);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _contaServiceMock.Verify(x => x.Deposito(1, 100), Times.Once);
        }

        [Fact]
        public void Saque_Com_Saldo_Suficiente_Deve_Retornar_Ok_Result()
        {
            // Arrange
            _contaServiceMock.Setup(x => x.Saque(It.IsAny<int>(), It.IsAny<decimal>()));

            // Act
            var result = _contasController.Saque(1, 50);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _contaServiceMock.Verify(x => x.Saque(1, 50), Times.Once);
        }

        [Fact]
        public void Saque_Com_Saldo_Insuficiente_Deve_Retornar_BadRequest_Result()
        {
            // Arrange
            _contaServiceMock.Setup(x => x.Saque(It.IsAny<int>(), It.IsAny<decimal>()))
                .Throws(new InvalidOperationException("Saldo insuficiente para realizar o saque."));

            // Act
            var result = _contasController.Saque(1, 5000);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Transferencia_Com_Saldo_Suficiente_Deve_Retornar_Ok_Result()
        {
            // Arrange
            _contaServiceMock.Setup(x => x.Transferencia(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()));

            // Act
            var result = _contasController.Transferencia(1, 2, 30);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _contaServiceMock.Verify(x => x.Transferencia(1, 2, 30), Times.Once);
        }

        [Fact]
        public void Transferencia_Com_Saldo_Insuficiente_Deve_Retornar_BadRequest_Result()
        {
            // Arrange
            _contaServiceMock.Setup(x => x.Transferencia(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .Throws(new InvalidOperationException("Saldo insuficiente para realizar a transferência."));

            // Act
            var result = _contasController.Transferencia(1, 2, 1500);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Extrato_Deve_Retornar_Ok_Result()
        {
            // Arrange
            _contaServiceMock.Setup(x => x.Extrato(It.IsAny<int>()))
                .Returns(new List<Lancamento>());

            // Act
            var result = _contasController.Extrato(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _contaServiceMock.Verify(x => x.Extrato(1), Times.Once);
        }
    }
}
