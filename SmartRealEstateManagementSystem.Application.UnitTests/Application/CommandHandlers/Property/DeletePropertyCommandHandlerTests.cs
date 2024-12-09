using Application.CommandHandlers.Property;
using Application.Commands.Property;
using Domain.Repositories;
using Domain.Types.Property;
using Domain.Types.UserInformation;
using Domain.Utils;
using NSubstitute;
using PropertyEntities = Domain.Entities;


namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.CommandHandlers.Property
{
    public class DeletePropertyCommandHandlerTests
    {
        private readonly IPropertyRepository propertyRepositoryMock;
        private readonly DeletePropertyCommandHandler handler;

        public DeletePropertyCommandHandlerTests()
        {
            propertyRepositoryMock = Substitute.For<IPropertyRepository>();
            handler = new DeletePropertyCommandHandler(propertyRepositoryMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsDeleted()
        {
            // Arrange
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            propertyRepositoryMock.GetByIdAsync(mockId).Returns(Result<PropertyEntities.Property>.Success(new Domain.Entities.Property
            {
                Id = mockId,
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
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = mockId,
                User = new Domain.Entities.UserInformation { Id = mockId, Username = "SampleUser", Email = "sample@example.com", FirstName = "Sample", LastName = "User", Address = "User Address", PhoneNumber = "1234567890", Nationality = "Sample Nationality", CreatedAt = DateTime.UtcNow, Status = UserStatus.ACTIVE, Role = UserRole.CLIENT }
            }));
            propertyRepositoryMock.DeleteAsync(mockId).Returns(Result.Success());

            var command = new DeletePropertyCommand { Id = mockId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPropertyDoesNotExist()
        {
            // Arrange
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            propertyRepositoryMock.GetByIdAsync(mockId).Returns(Result<PropertyEntities.Property>.Failure("Property not found."));

            var command = new DeletePropertyCommand { Id = mockId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Property not found.", result.ErrorMessage);
        }
    }
}
