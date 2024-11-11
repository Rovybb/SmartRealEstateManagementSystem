using Application.CommandHandlers.Payment;
using Application.Commands.Payment;
using Application.DTOs;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using NSubstitute;
using PaymentEntities = Domain.Entities;

namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.CommandHandlers.Payment
{
    public class CreatePaymentCommandHandlerTests
    {
        private readonly IPaymentRepository paymentRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly CreatePaymentCommandHandler handler;

        public CreatePaymentCommandHandlerTests()
        {
            paymentRepositoryMock = Substitute.For<IPaymentRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new CreatePaymentCommandHandler(paymentRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPaymentIsCreated()
        {
            // Arrange
            var payment = new PaymentEntities.Payment { Id = Guid.NewGuid(), Price = 1000 };
            var paymentDTO = new PaymentDTO { Id = payment.Id, Price = 1000 };

            paymentRepositoryMock.CreateAsync(payment).Returns(Result<Guid>.Success(payment.Id));
            mapperMock.Map<PaymentEntities.Payment>(Arg.Any<CreatePaymentCommand>()).Returns(payment);
            mapperMock.Map<PaymentDTO>(payment).Returns(paymentDTO);

            var command = new CreatePaymentCommand { Price = 1000 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(paymentDTO.Id, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPaymentIsNotCreated()
        {
            // Arrange
            paymentRepositoryMock.CreateAsync(Arg.Any<PaymentEntities.Payment>()).Returns(Result<Guid>.Failure("Payment not created."));

            var command = new CreatePaymentCommand { Price = 1000 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Payment not created.", result.ErrorMessage);
        }
    }
}
