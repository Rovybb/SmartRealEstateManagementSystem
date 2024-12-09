//TODO: Uncomment the code below

//using Application.DTOs;
//using Application.Queries.Property;
//using Application.QueryHandlers.Property;
//using AutoMapper;
//using Domain.Repositories;
//using Domain.Types.Property;
//using Domain.Types.UserInformation;
//using NSubstitute;
//using PropertyEntities = Domain.Entities;

//namespace SmartRealEstateManagementSystem.Application.UnitTests.Application.QueryHandlers.Property
//{
//    public class GetAllPropertiesQueryHandlerTests
//    {
//        private readonly IPropertyRepository propertyRepositoryMock;
//        private readonly IMapper mapperMock;
//        private readonly GetAllPropertiesQueryHandler handler;

//        public GetAllPropertiesQueryHandlerTests()
//        {
//            propertyRepositoryMock = Substitute.For<IPropertyRepository>();
//            mapperMock = Substitute.For<IMapper>();
//            handler = new GetAllPropertiesQueryHandler(propertyRepositoryMock, mapperMock);
//        }

//        [Fact]
//        public async Task Handle_ShouldReturnSuccessResult_WhenPropertiesExist()
//        {
//            // Arrange
//            var mockId = Guid.Parse("a026c5ca-a4d4-4b2c-af7f-615c31e4adc1");
//            var properties = new List<PropertyEntities.Property> {
//                    new PropertyEntities.Property{
//                    Id = mockId,
//                    Title = "Sample Title",
//                    Description = "Sample Description",
//                    Type = PropertyType.HOUSE,
//                    Status = PropertyStatus.AVAILABLE,
//                    Price = 100000m,
//                    Address = "Sample Address",
//                    Area = 120.5m,
//                    Rooms = 3,
//                    Bathrooms = 2,
//                    ConstructionYear = 2020,
//                    CreatedAt = DateTime.UtcNow,
//                    UpdatedAt = DateTime.UtcNow,
//                    UserId = mockId,
//                    User = new Domain.Entities.User { Id = mockId, Username = "SampleUser", Email = "sample@example.com", FirstName = "Sample", LastName = "User", Password = "hashedpassword", Address = "User Address", PhoneNumber = "1234567890", Nationality = "Sample Nationality", CreatedAt = DateTime.UtcNow, Status = UserStatus.ACTIVE, Role = UserRole.CLIENT }
//                    } };
//            var propertyDtos = new List<PropertyDto> {
//                    new PropertyDto {
//                        Id = properties[0].Id,
//                        Title = properties[0].Title,
//                        Description = properties[0].Description,
//                        Type = properties[0].Type,
//                        Status = properties[0].Status,
//                        Price = properties[0].Price,
//                        Address = properties[0].Address,
//                        Area = properties[0].Area,
//                        Rooms = properties[0].Rooms,
//                        Bathrooms = properties[0].Bathrooms,
//                        ConstructionYear = properties[0].ConstructionYear,
//                        CreatedAt = properties[0].CreatedAt,
//                        UpdatedAt = properties[0].UpdatedAt,
//                        UserId = properties[0].UserId
//                    }
//                };

//            propertyRepositoryMock.GetAllAsync().Returns(properties);
//            mapperMock.Map<IEnumerable<PropertyDto>>(properties).Returns(propertyDtos);

//            var query = new GetAllPropertiesQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.True(result.IsSuccess);
//            Assert.Equal(propertyDtos, result.Data);
//        }

//        [Fact]
//        public async Task Handle_ShouldReturnFailureResult_WhenNoPropertiesExist()
//        {
//            // Arrange
//            propertyRepositoryMock.GetAllAsync().Returns((IEnumerable<PropertyEntities.Property>)null!);

//            var query = new GetAllPropertiesQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.False(result.IsSuccess);
//            Assert.Equal("No properties found.", result.ErrorMessage);
//        }
//    }
//}
