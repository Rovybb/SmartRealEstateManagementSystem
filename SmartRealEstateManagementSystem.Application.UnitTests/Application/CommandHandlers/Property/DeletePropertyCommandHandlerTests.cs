using Application.CommandHandlers.Property;
using Application.Commands.Property;
using Domain.Repositories;
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
            var propertyId = Guid.NewGuid();
            propertyRepositoryMock.GetByIdAsync(propertyId).Returns(Result<PropertyEntities.Property>.Success(new PropertyEntities.Property { Id = propertyId }));
            propertyRepositoryMock.DeleteAsync(propertyId).Returns(Result<Guid>.Success(propertyId));

            var command = new DeletePropertyCommand { Id = propertyId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(propertyId, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            propertyRepositoryMock.GetByIdAsync(propertyId).Returns(Result<PropertyEntities.Property>.Failure("Property not found."));

            var command = new DeletePropertyCommand { Id = propertyId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Property not found.", result.ErrorMessage);
        }
    }
}
