using Application.Commands.Property;
using Application.CommandHandlers.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Types.Property;
using Domain.Utils;
using NSubstitute;
using PropertyEntities = Domain.Entities;
using Domain.Types.User;

namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.CommandHandlers.Property
{
    public class UpdatePropertyCommandHandlerTests
    {
        private readonly IPropertyRepository propertyRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly UpdatePropertyCommandHandler handler;

        public UpdatePropertyCommandHandlerTests()
        {
            propertyRepositoryMock = Substitute.For<IPropertyRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new UpdatePropertyCommandHandler(propertyRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsUpdated()
        {
            // Arrange
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var command = new UpdatePropertyCommand
            {
                Id = mockId,
                Title = "Updated Title",
                Description = "Updated Description",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 150000m,
                Address = "Updated Address",
                Area = 130.5m,
                Rooms = 4,
                Bathrooms = 3,
                ConstructionYear = 2021,
                UserId = mockId
            };

            var existingProperty = new PropertyEntities.Property
            {
                Id = mockId,
                Title = "Old Title",
                Description = "Old Description",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 100000m,
                Address = "Old Address",
                Area = 120.5m,
                Rooms = 3,
                Bathrooms = 2,
                ConstructionYear = 2020,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = mockId,
                User = new PropertyEntities.User { Id = mockId, Username = "SampleUser", Email = "sample@example.com", FirstName = "Sample", LastName = "User", Password = "hashedpassword", Address = "User Address", PhoneNumber = "1234567890", Nationality = "Sample Nationality", CreatedAt = DateTime.UtcNow, Status = UserStatus.ACTIVE, Role = UserRole.CLIENT }
            };

            propertyRepositoryMock.GetByIdAsync(mockId).Returns(Result<PropertyEntities.Property>.Success(existingProperty));
            propertyRepositoryMock.UpdateAsync(existingProperty).Returns(Result.Success());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPropertyIsNotFound()
        {
            // Arrange
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var command = new UpdatePropertyCommand
            {
                Id = mockId,
                Title = "Updated Title",
                Description = "Updated Description",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 150000m,
                Address = "Updated Address",
                Area = 130.5m,
                Rooms = 4,
                Bathrooms = 3,
                ConstructionYear = 2021,
                UserId = mockId
            };

            propertyRepositoryMock.GetByIdAsync(mockId).Returns(Result<PropertyEntities.Property>.Failure("Property not found."));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Property not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenUpdateFails()
        {
            // Arrange
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var command = new UpdatePropertyCommand
            {
                Id = mockId,
                Title = "Updated Title",
                Description = "Updated Description",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 150000m,
                Address = "Updated Address",
                Area = 130.5m,
                Rooms = 4,
                Bathrooms = 3,
                ConstructionYear = 2021,
                UserId = mockId
            };

            var existingProperty = new PropertyEntities.Property
            {
                Id = mockId,
                Title = "Old Title",
                Description = "Old Description",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 100000m,
                Address = "Old Address",
                Area = 120.5m,
                Rooms = 3,
                Bathrooms = 2,
                ConstructionYear = 2020,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = mockId,
                User = new PropertyEntities.User { Id = mockId, Username = "SampleUser", Email = "sample@example.com", FirstName = "Sample", LastName = "User", Password = "hashedpassword", Address = "User Address", PhoneNumber = "1234567890", Nationality = "Sample Nationality", CreatedAt = DateTime.UtcNow, Status = UserStatus.ACTIVE, Role = UserRole.CLIENT }
            };

            propertyRepositoryMock.GetByIdAsync(mockId).Returns(Result<PropertyEntities.Property>.Success(existingProperty));
            propertyRepositoryMock.UpdateAsync(existingProperty).Returns(Result.Failure("Update failed."));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Update failed.", result.ErrorMessage);
        }
    }
}
