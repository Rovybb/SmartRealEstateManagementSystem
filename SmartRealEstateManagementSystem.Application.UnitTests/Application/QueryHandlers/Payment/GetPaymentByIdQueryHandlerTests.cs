using Application.DTOs;
using Application.Queries.Payment;
using Application.QueryHandlers.Payment;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using NSubstitute;
using PaymentEntities = Domain.Entities;

namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.QueryHandlers.Payment
{
    public class GetPaymentByIdQueryHandlerTests
    {
        private readonly IPaymentRepository paymentRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly GetPaymentByIdQueryHandler handler;

        public GetPaymentByIdQueryHandlerTests()
        {
            paymentRepositoryMock = Substitute.For<IPaymentRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new GetPaymentByIdQueryHandler(paymentRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPaymentExists()
        {
            // Arrange
            var payment = new PaymentEntities.Payment { Id = Guid.NewGuid(), Price = 1000 };
            var paymentDTO = new PaymentDTO { Id = payment.Id, Price = 1000 };

            paymentRepositoryMock.GetByIdAsync(payment.Id).Returns(Result<PaymentEntities.Payment>.Success(payment));
            mapperMock.Map<PaymentDTO>(payment).Returns(paymentDTO);

            var query = new GetPaymentByIdQuery { Id = payment.Id };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(paymentDTO, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPaymentDoesNotExist()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            paymentRepositoryMock.GetByIdAsync(paymentId).Returns(Result<PaymentEntities.Payment>.Failure("Payment not found."));

            var query = new GetPaymentByIdQuery { Id = paymentId };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Payment not found.", result.ErrorMessage);
        }

    }
}
