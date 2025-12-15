```
warehouse-api-dotnet
├─ Dockerfile
├─ README.md
├─ src
│  └─ WarehouseBusiness
│     ├─ API
│     │  ├─ API.csproj
│     │  ├─ API.http
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
│     │  │  ├─ SetupsController.cs
│     │  │  └─ WarehouseController.cs
│     │  ├─ DependencyInjection
│     │  │  └─ DependencyInjection.cs
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
│     │  │  ├─ RefreshTokenDTO.cs
│     │  │  ├─ SetupsDTO.cs
│     │  │  └─ WarehouseDTO.cs
│     │  ├─ Helper
│     │  │  └─ Hash.cs
│     │  ├─ Interfaces
│     │  │  ├─ IAuthService.cs
│     │  │  ├─ IBinService.cs
│     │  │  ├─ IBusinessPartnerService.cs
│     │  │  ├─ ICategoryService.cs
│     │  │  ├─ IHealthService.cs
│     │  │  ├─ IProductService.cs
│     │  │  ├─ ISetupsService.cs
│     │  │  └─ IWarehouseService.cs
│     │  └─ Services
│     │     ├─ AuthService.cs
│     │     ├─ BinService.cs
│     │     ├─ BusinessPartnerService.cs
│     │     ├─ CategoryService.cs
│     │     ├─ HealthService.cs
│     │     ├─ ProductService.cs
│     │     ├─ SetupsService.cs
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
│     │  │  ├─ RefreshToken.cs
│     │  │  ├─ User.cs
│     │  │  ├─ Warehouse.cs
│     │  │  └─ Weak
│     │  │     └─ ProductWithCategory.cs
│     │  ├─ Properties
│     │  └─ Repositories
│     │     ├─ IBaseRepository.cs
│     │     ├─ IBinRepository.cs
│     │     ├─ IBusinessPartnerRepository.cs
│     │     ├─ ICategoryRepository.cs
│     │     ├─ IProductRepository.cs
│     │     ├─ IRefreshTokenRepository.cs
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
│           ├─ RefreshTokenRepository.cs
│           ├─ UserRepository.cs
│           └─ WarehouseRepository.cs
└─ warehouse-api-dotnet.sln

```
