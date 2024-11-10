using Application.DTOs;
using Application.Queries.Property;
using Application.QueryHandlers.Property;
using AutoMapper;
using Domain.Repositories;
using NSubstitute;
using PropertyEntities = Domain.Entities;

namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.QueryHandlers.Property
{
    public class GetAllPropertiesQueryHandlerTests
    {
        private readonly IPropertyRepository propertyRepositoryMock;
        private readonly IMapper mapperMock;
        private readonly GetAllPropertiesQueryHandler handler;

        public GetAllPropertiesQueryHandlerTests()
        {
            propertyRepositoryMock = Substitute.For<IPropertyRepository>();
            mapperMock = Substitute.For<IMapper>();
            handler = new GetAllPropertiesQueryHandler(propertyRepositoryMock, mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertiesExist()
        {
            // Arrange
            var properties = new List<PropertyEntities.Property> { new PropertyEntities.Property { Id = Guid.NewGuid(), Title = "Test Property" } };
            var propertyDTOs = new List<PropertyDTO> { new PropertyDTO { Id = properties[0].Id, Title = "Test Property" } };

            propertyRepositoryMock.GetAllAsync().Returns(properties);
            mapperMock.Map<IEnumerable<PropertyDTO>>(properties).Returns(propertyDTOs);

            var query = new GetAllPropertiesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(propertyDTOs, result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenNoPropertiesExist()
        {
            // Arrange
            propertyRepositoryMock.GetAllAsync().Returns((IEnumerable<PropertyEntities.Property>)null!);

            var query = new GetAllPropertiesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No properties found.", result.ErrorMessage);
        }
    }
}
