//using Application.CommandHandlers.Payment;
//using Application.Commands.Payment;
//using Domain.Repositories;
//using Domain.Utils;
//using NSubstitute;
//using PaymentEntities = Domain.Entities;

//namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.CommandHandlers.Payment
//{
//    public class DeletePaymentCommandHandlerTests
//    {
//        private readonly IPaymentRepository paymentRepositoryMock;
//        private readonly DeletePaymentCommandHandler handler;

//        public DeletePaymentCommandHandlerTests()
//        {
//            paymentRepositoryMock = Substitute.For<IPaymentRepository>();
//            handler = new DeletePaymentCommandHandler(paymentRepositoryMock);
//        }

//        [Fact]
//        public async Task Handle_ShouldReturnSuccessResult_WhenPaymentIsDeleted()
//        {
//            // Arrange
//            var paymentId = Guid.NewGuid();
//            paymentRepositoryMock.GetByIdAsync(paymentId).Returns(Result<PaymentEntities.Payment>.Success(new PaymentEntities.Payment { Id = paymentId }));
//            paymentRepositoryMock.DeleteAsync(paymentId).Returns(Result<Guid>.Success(paymentId));

//            var command = new DeletePaymentCommand { Id = paymentId };

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.True(result.IsSuccess);

//        }

//        [Fact]
//        public async Task Handle_ShouldReturnFailureResult_WhenPaymentDoesNotExist()
//        {
//            // Arrange
//            var paymentId = Guid.NewGuid();
//            paymentRepositoryMock.GetByIdAsync(paymentId).Returns(Result<PaymentEntities.Payment>.Failure("Payment not found."));

//            var command = new DeletePaymentCommand { Id = paymentId };

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.False(result.IsSuccess);
//            Assert.Equal("Payment not found.", result.ErrorMessage);


//        }
//    }
//}
