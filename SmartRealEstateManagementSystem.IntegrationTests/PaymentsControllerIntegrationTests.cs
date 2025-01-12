using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence; 
using Domain.Entities;          
using Domain.Types.Property;    
using Domain.Types.Payment;    
using Domain.Types.UserInformation; 
using Application.Commands.Payment;  
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Hosting;
using Google;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SmartRealEstateManagementSystem.IntegrationTests
{
    public class PaymentsControllerIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ApplicationDbContext _dbContext;
        private readonly UsersDbContext _dbIdentityContext;

        private const string BaseUrl = "/api/v1/payments";

        private static readonly Guid SellerId = new Guid("11111111-1111-1111-1111-111111111111");
        private static readonly Guid BuyerId = new Guid("22222222-2222-2222-2222-222222222222");
        private static readonly Guid PropertyId = new Guid("33333333-3333-3333-3333-333333333333");

        public PaymentsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // 1. Override normal DbContext with an in-memory database
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test"); // Ensures test-specific behavior
                builder.ConfigureServices(services =>
                {
                    var sp = services.BuildServiceProvider();
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                        var dbIdentity = scopedServices.GetRequiredService<UsersDbContext>();

                        db.Database.EnsureCreated();
                        dbIdentity.Database.EnsureCreated();
                    }
                });

            });

            // 2. Resolve ApplicationDbContext
            var scope = _factory.Services.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _dbIdentityContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
            _dbContext.Database.EnsureCreated();
            _dbIdentityContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GivenValidPaymentData_WhenCreateIsCalled_ThenPaymentIsSavedToDatabase()
        {
            // Arrange
            // Seed the DB with a seller user, buyer user, property referencing seller
            SeedValidUsersAndProperty();

            var client = _factory.CreateClient();

            // Prepare the command to create a payment
            // referencing the existing property and users
            var command = new CreatePaymentCommand
            {
                Type = PaymentType.SALE,
                Date = DateTime.UtcNow,
                Price = 999.99m,
                Status = PaymentStatus.PENDING,
                PaymentMethod = PaymentMethod.CREDIT_CARD,
                PropertyId = PropertyId,   
                SellerId = SellerId,       
                BuyerId = BuyerId         
            };

            // Act
            var response = await client.PostAsJsonAsync(BaseUrl, command);
            response.EnsureSuccessStatusCode();

            // Assert
            // Check the Payment table for the inserted record
            var paymentInDb = await _dbContext.Payments
                .FirstOrDefaultAsync();

            paymentInDb.Should().NotBeNull();
            paymentInDb!.Price.Should().Be(999.99m);
            paymentInDb.SellerId.Should().Be(SellerId);
            paymentInDb.BuyerId.Should().Be(BuyerId);
            paymentInDb.PropertyId.Should().Be(PropertyId);
        }

        public void Dispose()
        {
            // Cleanup after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
        private void SeedValidUsersAndProperty()
        {
            var sellerUser = new UserInformation
            {
                Id = SellerId,
                Username = "SellerUser",
                Email = "seller@example.com",
                FirstName = "Seller",
                LastName = "User",
                Address = "123 Seller St",
                PhoneNumber = "123-456-7890",
                Nationality = "SellerLand",
                CreatedAt = DateTime.UtcNow,
                Status = UserStatus.ACTIVE,
                Role = UserRole.PROFESSIONAL
            };

            var buyerUser = new UserInformation
            {
                Id = BuyerId,
                Username = "BuyerUser",
                Email = "buyer@example.com",
                FirstName = "Buyer",
                LastName = "User",
                Address = "456 Buyer Rd",
                PhoneNumber = "987-654-3210",
                Nationality = "BuyerLand",
                CreatedAt = DateTime.UtcNow,
                Status = UserStatus.ACTIVE,
                Role = UserRole.CLIENT
            };

            var property = new Property
            {
                Id = PropertyId,
                Title = "Test Property",
                Description = "Property belonging to SellerUser",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 10000m,
                Address = "100 Real Estate Way",
                Area = 120.5m,
                Rooms = 3,
                Bathrooms = 2,
                ConstructionYear = 2020,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = sellerUser.Id, 
                User = sellerUser
            };

            _dbContext.Users.Add(sellerUser);
            _dbContext.Users.Add(buyerUser);
            _dbContext.Properties.Add(property);

            _dbContext.SaveChanges();
        }
    }
}
