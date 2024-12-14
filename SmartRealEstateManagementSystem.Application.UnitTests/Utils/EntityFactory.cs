using Application.Commands.Payment;
using Application.Commands.Property;
using Application.DTOs;
using Application.Queries.Payment;
using Application.Queries.Property;
using Domain.Entities;
using Domain.Types.Payment;
using Domain.Types.Property;
using Domain.Types.UserInformation;
using Domain.Utils;

namespace SmartRealEstateManagementSystem.Application.UnitTests.Utils
{
    public static class EntityFactory
    {
        public static Payment CreatePayment(Guid mockId)
        {
            return new Payment
            {
                Id = mockId,
                Type = PaymentType.SALE,
                Date = DateTime.UtcNow,
                Price = 1000m,
                Status = PaymentStatus.COMPLETED,
                PaymentMethod = PaymentMethod.CREDIT_CARD,
                PropertyId = mockId,
                SellerId = mockId,
                BuyerId = mockId,
                Property = CreateProperty(mockId),
                Seller = CreateSeller(mockId),
                Buyer = CreateBuyer(mockId)
            };
        }

        public static Property CreateProperty(Guid mockId)
        {
            return new Property
            {
                Id = mockId,
                Title = "Sample Title",
                Description = "Sample Description",
                Price = 100000.00m,
                Address = "123 Sample Street",
                Area = 1500.00m,
                Rooms = 3,
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Bathrooms = 2,
                ConstructionYear = 2020,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = mockId,
                User = CreateUser(mockId, "sampleuser", "sampleuser@example.com", "Sample", "User", "123 Sample Street", "123-456-7890", "Sample Nationality")
            };
        }

        public static UserInformation CreateSeller(Guid mockId)
        {
            return CreateUser(mockId, "selleruser", "selleruser@example.com", "Seller", "User", "456 Seller Street", "987-654-3210", "Seller Nationality");
        }

        public static UserInformation CreateBuyer(Guid mockId)
        {
            return CreateUser(mockId, "buyeruser", "buyeruser@example.com", "Buyer", "User", "789 Buyer Street", "321-654-9870", "Buyer Nationality");
        }

        public static UserInformation CreateUser(Guid mockId, string username, string email, string firstName, string lastName, string address, string phoneNumber, string nationality)
        {
            return new UserInformation
            {
                Id = mockId,
                Username = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Nationality = nationality,
                CreatedAt = DateTime.UtcNow,
                Status = UserStatus.ACTIVE,
                Role = UserRole.CLIENT
            };
        }

        public static CreatePaymentCommand CreatePaymentCommand(Guid mockId)
        {
            return new CreatePaymentCommand
            {
                Type = PaymentType.SALE,
                Date = DateTime.UtcNow,
                Price = 1000m,
                Status = PaymentStatus.COMPLETED,
                PaymentMethod = PaymentMethod.CREDIT_CARD,
                PropertyId = mockId,
                SellerId = mockId,
                BuyerId = mockId
            };
        }

        public static UpdatePaymentCommand CreateUpdatePaymentCommand(Guid mockId)
        {
            return new UpdatePaymentCommand
            {
                Id = mockId,
                Type = PaymentType.SALE,
                Date = DateTime.UtcNow,
                Price = 1000m,
                Status = PaymentStatus.COMPLETED,
                PaymentMethod = PaymentMethod.CREDIT_CARD,
                PropertyId = mockId,
                SellerId = mockId,
                BuyerId = mockId
            };
        }

        public static CreatePropertyCommand CreatePropertyCommand(Guid mockId)
        {
            return new CreatePropertyCommand
            {
                Title = "Sample Title",
                Description = "Sample Description",
                Price = 100000m,
                Address = "Sample Address",
                Area = 120.5m,
                Rooms = 3,
                Bathrooms = 2,
                ConstructionYear = 2020,
                UserId = mockId
            };
        }

        public static DeletePropertyCommand CreateDeletePropertyCommand(Guid mockId)
        {
            return new DeletePropertyCommand
            {
                Id = mockId
            };
        }

        public static UpdatePropertyCommand CreateUpdatePropertyCommand(Guid mockId)
        {
            return new UpdatePropertyCommand
            {
                Id = mockId,
                Title = "Updated Title",
                Description = "Updated Description",
                Price = 150000m,
                Address = "Updated Address",
                Area = 130.5m,
                Rooms = 4,
                Bathrooms = 3,
                ConstructionYear = 2021,
                UserId = mockId
            };
        }

        public static GetAllPaymentsQuery CreateGetAllPaymentsQuery()
        {
            return new GetAllPaymentsQuery();
        }

        public static GetPaymentByIdQuery CreateGetPaymentByIdQuery(Guid mockId)
        {
            return new GetPaymentByIdQuery { Id = mockId };
        }

        public static GetAllPropertiesQuery CreateGetAllPropertiesQuery()
        {
            return new GetAllPropertiesQuery();
        }

        public static GetPropertyByIdQuery CreateGetPropertyByIdQuery(Guid mockId)
        {
            return new GetPropertyByIdQuery { Id = mockId };
        }

        public static PaymentDto CreatePaymentDto(Payment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                Type = payment.Type,
                Date = payment.Date,
                Price = payment.Price,
                Status = payment.Status,
                PaymentMethod = payment.PaymentMethod,
                PropertyId = payment.PropertyId,
                SellerId = payment.SellerId,
                BuyerId = payment.BuyerId
            };
        }

        public static PropertyDto CreatePropertyDto(Property property)
        {
            return new PropertyDto
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                Address = property.Address,
                Type = PropertyType.HOUSE,
                Status = PropertyStatus.AVAILABLE,
                Area = property.Area,
                Rooms = property.Rooms,
                Bathrooms = property.Bathrooms,
                ConstructionYear = property.ConstructionYear,
                CreatedAt = property.CreatedAt,
                UpdatedAt = property.UpdatedAt,
                UserId = property.UserId
            };
        }

        public static PaginatedList<Property> CreatePaginatedProperties(List<Property> properties, int pageNumber, int pageSize)
        {
            return new PaginatedList<Property>(properties, properties.Count, pageNumber, pageSize);
        }
    }
}
