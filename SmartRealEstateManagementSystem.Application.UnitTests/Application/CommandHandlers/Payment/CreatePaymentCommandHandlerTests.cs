using Application.CommandHandlers.Payment;
using Application.Commands.Payment;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using NSubstitute;
using Domain.Types.Payment;
using Domain.Types.Property;
using Domain.Types.User;

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
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var createPaymentCommand = new CreatePaymentCommand
            {
                Type = PaymentType.SALE,
                Date = DateTime.UtcNow,
                Price = 1000m,
                Status = PaymentStatus.COMPLETED,
                PaymentMethod = PaymentMethod.CREDIT_CARD,
                PropertyId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                BuyerId = Guid.NewGuid()
            };
            var payment = new Domain.Entities.Payment
            {
                Id = mockId,
                Type = createPaymentCommand.Type,
                Date = createPaymentCommand.Date,
                Price = createPaymentCommand.Price,
                Status = createPaymentCommand.Status,
                PaymentMethod = createPaymentCommand.PaymentMethod,
                PropertyId = createPaymentCommand.PropertyId,
                SellerId = createPaymentCommand.SellerId,
                BuyerId = createPaymentCommand.BuyerId,
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

            paymentRepositoryMock.CreateAsync(Arg.Any< Domain.Entities.Payment >()).Returns(Result<Guid>.Success(mockId));
            mapperMock.Map<Domain.Entities.Payment>(createPaymentCommand).Returns(payment);

            // Act
            var result = await handler.Handle(createPaymentCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(mockId, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPaymentCreationFails()
        {
            // Arrange
            var createPaymentCommand = new CreatePaymentCommand
            {
                Type = PaymentType.SALE,
                Date = DateTime.UtcNow,
                Price = 1000m,
                Status = PaymentStatus.COMPLETED,
                PaymentMethod = PaymentMethod.CREDIT_CARD,
                PropertyId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1"),
                SellerId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1"),
                BuyerId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1")
            };

            paymentRepositoryMock.CreateAsync(Arg.Any<Domain.Entities.Payment>()).Returns(Result<Guid>.Failure("Payment creation failed."));

            // Act
            var result = await handler.Handle(createPaymentCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Payment creation failed.", result.ErrorMessage);
        }
    }
}
