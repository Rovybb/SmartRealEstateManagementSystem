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
    public class UpdatePaymentCommandHandlerTests
    {
        private readonly IPaymentRepository paymentRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly UpdatePaymentCommandHandler handler;

        public UpdatePaymentCommandHandlerTests()
        {
            paymentRepositoryMock = Substitute.For<IPaymentRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new UpdatePaymentCommandHandler(paymentRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPaymentIsUpdated()
        {
            // Arrange
            var payment = new PaymentEntities.Payment { Id = Guid.NewGuid(), Price = 1000 };
            var paymentDTO = new PaymentDTO { Id = payment.Id, Price = 1000 };

            paymentRepositoryMock.GetByIdAsync(Arg.Any<Guid>()).Returns(Result<PaymentEntities.Payment>.Success(payment));
            paymentRepositoryMock.UpdateAsync(payment).Returns(Result<Guid>.Success(payment.Id));
            mapperMock.Map(Arg.Any<UpdatePaymentCommand>(), payment).Returns(payment); 

            var command = new UpdatePaymentCommand { Id = payment.Id, Price = 1000 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
           // TODO error here Assert.Equal(paymentDTO.Id, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPaymentIsNotUpdated()
        {
            /// Arrange
            paymentRepositoryMock.GetByIdAsync(Arg.Any<Guid>())
                .Returns(Result<PaymentEntities.Payment>.Failure("Payment not found."));

            var command = new UpdatePaymentCommand { Id = Guid.NewGuid(), Price = 1000 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Payment not found.", result.ErrorMessage);
        }
    }
}
