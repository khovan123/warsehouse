
```
warehouse-api-dotnet
├─ Dockerfile
├─ src
│  └─ WarehouseBusiness
│     ├─ API
│     │  ├─ API.csproj
│     │  ├─ API.http
│     │  ├─ API.json
│     │  ├─ appsettings.json
│     │  ├─ Common
│     │  │  └─ ApiBuilder.cs
│     │  ├─ Controllers
│     │  │  ├─ AuthController.cs
│     │  │  ├─ BinController.cs
│     │  │  ├─ BusinessPartnerController.cs
│     │  │  ├─ CategoryController.cs
│     │  │  ├─ CheckHealthController.cs
│     │  │  ├─ ProductController.cs
│     │  │  └─ WarehouseController.cs
│     │  ├─ Middlewares
│     │  ├─ Program.cs
│     │  └─ Properties
│     │     ├─ launchSettings.json
│     │     └─ PublishProfiles
│     ├─ Application
│     │  ├─ Application.csproj
│     │  ├─ Dtos
│     │  │  ├─ BinDTO.cs
│     │  │  ├─ BusinessPartnerDTO.cs
│     │  │  ├─ CategoryDTO.cs
│     │  │  ├─ LoginDTO.cs
│     │  │  ├─ ProductDTO.cs
│     │  │  └─ WarehouseDTO.cs
│     │  ├─ Interfaces
│     │  │  ├─ IAuthService.cs
│     │  │  ├─ IBinService.cs
│     │  │  ├─ IBusinessPartnerService.cs
│     │  │  ├─ ICategoryService.cs
│     │  │  ├─ IHealthService.cs
│     │  │  ├─ IProductService.cs
│     │  │  └─ IWarehouseService.cs
│     │  └─ Services
│     │     ├─ AuthService.cs
│     │     ├─ BinService.cs
│     │     ├─ BusinessPartnerService.cs
│     │     ├─ CategoryService.cs
│     │     ├─ HealthService.cs
│     │     ├─ ProductService.cs
│     │     └─ WarehouseService.cs
│     ├─ Contract
│     │  ├─ Contract.csproj
│     │  └─ Responses
│     │     ├─ ApiErrorCode.cs
│     │     └─ ApiResponse.cs
│     ├─ Domain
│     │  ├─ Domain.csproj
│     │  ├─ Entities
│     │  │  ├─ Address.cs
│     │  │  ├─ Bin.cs
│     │  │  ├─ BusinessPartner.cs
│     │  │  ├─ Category.cs
│     │  │  ├─ Contact.cs
│     │  │  ├─ Product.cs
│     │  │  ├─ User.cs
│     │  │  ├─ Warehouse.cs
│     │  │  └─ Weak
│     │  │     └─ ProductWithCategory.cs
│     │  ├─ Properties
│     │  └─ Repositories
│     │     ├─ IBaseRepository.cs
│     │     ├─ IBinRepository.cs
│     │     ├─ IBusinessPartnetRepository.cs
│     │     ├─ ICategoryRepository.cs
│     │     ├─ IProductRepository.cs
│     │     ├─ IUserRepository.cs
│     │     └─ IWarehouseRepository.cs
│     └─ Infrastructure
│        ├─ Constants
│        │  └─ MongoCollections.cs
│        ├─ DB
│        │  ├─ MongoDBConfig.cs
│        │  └─ MongoDbContext.cs
│        ├─ Infrastructure.csproj
│        └─ Repositories
│           ├─ BinRepository.cs
│           ├─ BusinessPartnerRepository.cs
│           ├─ CategoryRepository.cs
│           ├─ ProductRepository.cs
│           ├─ UserRepository.cs
│           └─ WarehouseRepository.cs
└─ warehouse-api-dotnet.sln

```