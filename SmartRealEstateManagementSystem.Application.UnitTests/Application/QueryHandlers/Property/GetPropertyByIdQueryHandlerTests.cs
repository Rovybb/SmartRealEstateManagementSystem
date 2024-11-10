using Application.DTOs;
using Application.Queries.Property;
using Application.QueryHandlers.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using NSubstitute;
using PropertyEntities = Domain.Entities;


namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.QueryHandlers.Property
{
    public class GetPropertyByIdQueryHandlerTests
    {
        private readonly IPropertyRepository propertyRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly GetPropertyByIdQueryHandler handler;

        public GetPropertyByIdQueryHandlerTests()
        {
            propertyRepositoryMock = Substitute.For<IPropertyRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new GetPropertyByIdQueryHandler(propertyRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyExists()
        {
            // Arrange
            var property = new PropertyEntities.Property { Id = Guid.NewGuid(), Title = "Test Property" };
            var propertyDTO = new PropertyDTO { Id = property.Id, Title = "Test Property" };

            propertyRepositoryMock.GetByIdAsync(property.Id).Returns(Result<PropertyEntities.Property>.Success(property));
            mapperMock.Map<PropertyDTO>(property).Returns(propertyDTO);

            var query = new GetPropertyByIdQuery { Id = property.Id };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(propertyDTO, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            propertyRepositoryMock.GetByIdAsync(propertyId).Returns(Result<PropertyEntities.Property>.Failure("Property not found."));

            var query = new GetPropertyByIdQuery { Id = propertyId };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Property not found.", result.ErrorMessage);
        }
    }
}
