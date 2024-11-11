//using Application.DTOs;
//using Application.Queries.Payment;
//using Application.QueryHandlers.Payment;
//using AutoMapper;
//using Domain.Repositories;
//using NSubstitute;
//using PaymentEntities = Domain.Entities;

//namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.QueryHandlers.Payment
//{
//    public class GetAllPaymentsQueryHandlerTests
//    {
//        private readonly IPaymentRepository paymentRepositoryMock;
//        private readonly IMapper mapperMock;
//        private readonly GetAllPaymentsQueryHandler handler;

//        public GetAllPaymentsQueryHandlerTests()
//        {
//            paymentRepositoryMock = Substitute.For<IPaymentRepository>();
//            mapperMock = Substitute.For<IMapper>();
//            handler = new GetAllPaymentsQueryHandler(paymentRepositoryMock, mapperMock);
//        }

//        [Fact]
//        public async Task Handle_ShouldReturnSuccessResult_WhenPaymentsExist()
//        {
//            // Arrange
//            var payments = new List<PaymentEntities.Payment> { new PaymentEntities.Payment { Id = Guid.NewGuid(), Price = 100 } };
//            var paymentDTOs = new List<PaymentDTO> { new PaymentDTO { Id = payments[0].Id, Price = 100 } };

//            paymentRepositoryMock.GetAllAsync().Returns(payments);
//            mapperMock.Map<IEnumerable<PaymentDTO>>(payments).Returns(paymentDTOs);

//            var query = new GetAllPaymentsQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.True(result.IsSuccess);
            
//        }

//        [Fact]
//        public async Task Handle_ShouldReturnFailureResult_WhenNoPaymentsExist()
//        {
//            // Arrange
//            paymentRepositoryMock.GetAllAsync().Returns((IEnumerable<PaymentEntities.Payment>)null!);

//            var query = new GetAllPaymentsQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.False(result.IsSuccess);
//            Assert.Equal("No payments found", result.ErrorMessage);
//        }
//    }
//}
