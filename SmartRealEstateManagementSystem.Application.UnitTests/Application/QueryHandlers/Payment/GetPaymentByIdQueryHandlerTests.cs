using Application.DTOs;
using Application.Queries.Payment;
using Application.QueryHandlers.Payment;
using AutoMapper;
using Domain.Repositories;
using Domain.Types.Payment;
using Domain.Types.Property;
using Domain.Types.UserInformation;
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
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var payment = new Domain.Entities.Payment
            {
                Id = mockId,
                Type = PaymentType.SALE,
                Date = DateTime.UtcNow,
                Price = 1000m,
                Status = PaymentStatus.COMPLETED,
                PaymentMethod = PaymentMethod.CREDIT_CARD,
                PropertyId = mockId,
                SellerId = mockId,
                BuyerId = mockId,
                Property = new Domain.Entities.Property
                {
                    Id = mockId,
                    Title = "Sample Title",
                    Description = "Sample Description",
                    Type = PropertyType.HOUSE,
                    Status = PropertyStatus.AVAILABLE,
                    Price = 100000.00m,
                    Address = "123 Sample Street",
                    Area = 1500.00m,
                    Rooms = 3,
                    Bathrooms = 2,
                    ConstructionYear = 2020,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = mockId,
                    User = new Domain.Entities.UserInformation
                    {
                        Id = mockId,
                        Username = "sampleuser",
                        Email = "sampleuser@example.com",
                        FirstName = "Sample",
                        LastName = "User",
                        Address = "123 Sample Street",
                        PhoneNumber = "123-456-7890",
                        Nationality = "Sample Nationality",
                        CreatedAt = DateTime.UtcNow,
                        Status = UserStatus.ACTIVE,
                        Role = UserRole.CLIENT,
                    }
                },
                Seller = new Domain.Entities.UserInformation
                {
                    Id = mockId,
                    Username = "selleruser",
                    Email = "selleruser@example.com",
                    FirstName = "Seller",
                    LastName = "User",
                    Address = "456 Seller Street",
                    PhoneNumber = "987-654-3210",
                    Nationality = "Seller Nationality",
                    CreatedAt = DateTime.UtcNow,
                    Status = UserStatus.ACTIVE,
                    Role = UserRole.CLIENT
                },
                Buyer = new Domain.Entities.UserInformation
                {
                    Id = mockId,
                    Username = "buyeruser",
                    Email = "buyeruser@example.com",
                    FirstName = "Buyer",
                    LastName = "User",
                    Address = "789 Buyer Street",
                    PhoneNumber = "321-654-9870",
                    Nationality = "Buyer Nationality",
                    CreatedAt = DateTime.UtcNow,
                    Status = UserStatus.ACTIVE,
                    Role = UserRole.CLIENT
                }
            };

            var paymentDto = new PaymentDto {
                        Id = payment.Id,
                        Type = payment.Type,
                        Date = payment.Date,
                        Price = payment.Price,
                        Status = payment.Status,
                        PaymentMethod = payment.PaymentMethod,
                        PropertyId = payment.PropertyId,
                        SellerId = payment.SellerId,
                        BuyerId = payment.BuyerId,
                };

            paymentRepositoryMock.GetByIdAsync(payment.Id).Returns(Result<PaymentEntities.Payment>.Success(payment));
            mapperMock.Map<PaymentDto>(payment).Returns(paymentDto);

            var query = new GetPaymentByIdQuery { Id = payment.Id };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(paymentDto, result.Data);
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
