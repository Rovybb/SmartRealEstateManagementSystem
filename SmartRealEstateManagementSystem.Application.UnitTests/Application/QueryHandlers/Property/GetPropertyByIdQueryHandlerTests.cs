using Application.DTOs;
using Application.Queries.Property;
using Application.QueryHandlers.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Types.Property;
using Domain.Types.User;
using NSubstitute;
using PropertyEntities = Domain.Entities;
using Domain.Utils;

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
            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
            var property = new PropertyEntities.Property
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
                User = new PropertyEntities.User
                {
                    Id = mockId,
                    Username = "SampleUser",
                    Email = "sample@example.com",
                    FirstName = "Sample",
                    LastName = "User",
                    Password = "hashedpassword",
                    Address = "User Address",
                    PhoneNumber = "1234567890",
                    Nationality = "Sample Nationality",
                    CreatedAt = DateTime.UtcNow,
                    Status = UserStatus.ACTIVE,
                    Role = UserRole.CLIENT
                }
            };
            var propertyDTO = new PropertyDto
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Type = property.Type,
                Status = property.Status,
                Price = property.Price,
                Address = property.Address,
                Area = property.Area,
                Rooms = property.Rooms,
                Bathrooms = property.Bathrooms,
                ConstructionYear = property.ConstructionYear,
                CreatedAt = property.CreatedAt,
                UpdatedAt = property.UpdatedAt,
                UserId = property.UserId
            };

            propertyRepositoryMock.GetByIdAsync(property.Id).Returns(Result<PropertyEntities.Property>.Success(property));
            mapperMock.Map<PropertyDto>(property).Returns(propertyDTO);

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
