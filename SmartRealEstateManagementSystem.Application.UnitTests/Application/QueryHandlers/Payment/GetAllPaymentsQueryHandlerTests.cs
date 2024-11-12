using Application.DTOs;
using Application.Queries.Payment;
using Application.QueryHandlers.Payment;
using AutoMapper;
using Domain.Repositories;
using Domain.Types.Payment;
using Domain.Types.Property;
using Domain.Types.User;
using NSubstitute;
using PaymentEntities = Domain.Entities;

namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.QueryHandlers.Payment
{
    public class GetAllPaymentsQueryHandlerTests
    {
        private readonly IPaymentRepository paymentRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly GetAllPaymentsQueryHandler handler;

        public GetAllPaymentsQueryHandlerTests()
        {
            paymentRepositoryMock = Substitute.For<IPaymentRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new GetAllPaymentsQueryHandler(paymentRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPaymentsExist()
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
                    User = new Domain.Entities.User
                    {
                        Id = mockId,
                        Username = "sampleuser",
                        Email = "sampleuser@example.com",
                        FirstName = "Sample",
                        LastName = "User",
                        Password = "hashedpassword",
                        Address = "123 Sample Street",
                        PhoneNumber = "123-456-7890",
                        Nationality = "Sample Nationality",
                        CreatedAt = DateTime.UtcNow,
                        Status = UserStatus.ACTIVE,
                        Role = UserRole.CLIENT,
                    }
                },
                Seller = new Domain.Entities.User
                {
                    Id = mockId,
                    Username = "selleruser",
                    Email = "selleruser@example.com",
                    FirstName = "Seller",
                    LastName = "User",
                    Password = "hashedpassword",
                    Address = "456 Seller Street",
                    PhoneNumber = "987-654-3210",
                    Nationality = "Seller Nationality",
                    CreatedAt = DateTime.UtcNow,
                    Status = UserStatus.ACTIVE,
                    Role = UserRole.CLIENT
                },
                Buyer = new Domain.Entities.User
                {
                    Id = mockId,
                    Username = "buyeruser",
                    Email = "buyeruser@example.com",
                    FirstName = "Buyer",
                    LastName = "User",
                    Password = "hashedpassword",
                    Address = "789 Buyer Street",
                    PhoneNumber = "321-654-9870",
                    Nationality = "Buyer Nationality",
                    CreatedAt = DateTime.UtcNow,
                    Status = UserStatus.ACTIVE,
                    Role = UserRole.CLIENT
                }
            };
            var payments = new List<Domain.Entities.Payment> { payment };

            var paymentDtos = new List<PaymentDto> {
                    new PaymentDto {
                        Id = payments[0].Id,
                        Type = payments[0].Type,
                        Date = payments[0].Date,
                        Price = payments[0].Price,
                        Status = payments[0].Status,
                        PaymentMethod = payments[0].PaymentMethod,
                        PropertyId = payments[0].PropertyId,
                        SellerId = payments[0].SellerId,
                        BuyerId = payments[0].BuyerId,
                    }
                };

            paymentRepositoryMock.GetAllAsync().Returns(payments);
            mapperMock.Map<IEnumerable<PaymentDto>>(payments).Returns(paymentDtos);

            var query = new GetAllPaymentsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(paymentDtos, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenNoPaymentsExist()
        {
            // Arrange
            paymentRepositoryMock.GetAllAsync().Returns((IEnumerable<PaymentEntities.Payment>)null!);

            var query = new GetAllPaymentsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No payments found.", result.ErrorMessage);
        }
    }
}
