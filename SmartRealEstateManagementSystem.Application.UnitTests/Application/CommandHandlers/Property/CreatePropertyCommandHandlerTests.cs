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
    public class CreatePropertyCommandHandlerTests
    {
        private readonly IPropertyRepository propertyRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly CreatePropertyCommandHandler handler;

        public CreatePropertyCommandHandlerTests()
        {
            propertyRepositoryMock = Substitute.For<IPropertyRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new CreatePropertyCommandHandler(propertyRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsCreated()
        {
            // Arrange
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var command = new CreatePropertyCommand
            {
                Title = "Sample Title",
                Description = "Sample Description",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 100000m,
                Address = "Sample Address",
                Area = 120.5m,
                Rooms = 3,
                Bathrooms = 2,
                ConstructionYear = 2020,
                UserId = mockId
            };

            var property = new PropertyEntities.Property
            {
                Id = mockId,
                Title = command.Title,
                Description = command.Description,
                Type = command.Type,
                Status = command.Status,
                Price = command.Price,
                Address = command.Address,
                Area = command.Area,
                Rooms = command.Rooms,
                Bathrooms = command.Bathrooms,
                ConstructionYear = command.ConstructionYear,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = command.UserId,
                User = new Domain.Entities.User { Id = mockId, Username = "SampleUser", Email = "sample@example.com", FirstName = "Sample", LastName = "User", Password = "hashedpassword", Address = "User Address", PhoneNumber = "1234567890", Nationality = "Sample Nationality", CreatedAt = DateTime.UtcNow, Status = UserStatus.ACTIVE, Role = UserRole.CLIENT }
            };

            mapperMock.Map<PropertyEntities.Property>(command).Returns(property);
            propertyRepositoryMock.CreateAsync(property).Returns(Result<Guid>.Success(property.Id));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(property.Id, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPropertyCreationFails()
        {
            // Arrange
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var command = new CreatePropertyCommand
            {
                Title = "Sample Title",
                Description = "Sample Description",
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Price = 100000m,
                Address = "Sample Address",
                Area = 120.5m,
                Rooms = 3,
                Bathrooms = 2,
                ConstructionYear = 2020,
                UserId = mockId
            };

            var property = new PropertyEntities.Property
            {
                Id = mockId,
                Title = command.Title,
                Description = command.Description,
                Type = command.Type,
                Status = command.Status,
                Price = command.Price,
                Address = command.Address,
                Area = command.Area,
                Rooms = command.Rooms,
                Bathrooms = command.Bathrooms,
                ConstructionYear = command.ConstructionYear,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = command.UserId,
                User = new Domain.Entities.User { Id = mockId, Username = "SampleUser", Email = "sample@example.com", FirstName = "Sample", LastName = "User", Password = "hashedpassword", Address = "User Address", PhoneNumber = "1234567890", Nationality = "Sample Nationality", CreatedAt = DateTime.UtcNow, Status = UserStatus.ACTIVE, Role = UserRole.CLIENT }
            };

            mapperMock.Map<PropertyEntities.Property>(command).Returns(property);
            propertyRepositoryMock.CreateAsync(property).Returns(Result<Guid>.Failure("Creation failed."));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Creation failed.", result.ErrorMessage);
        }
    }
}
