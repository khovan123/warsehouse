
```
warehouse-api-dotnet
├─ .dockerignore
├─ .editorconfig
├─ certs
├─ docker-compose.yml
├─ Dockerfile
├─ README.md
├─ src
│  └─ WarehouseBusiness
│     ├─ API
│     │  ├─ API.csproj
│     │  ├─ API.http
│     │  ├─ appsettings.json
│     │  ├─ Common
│     │  │  ├─ ApiBuilder.cs
│     │  │  ├─ Constants.cs
│     │  │  └─ Writer.cs
│     │  ├─ Controllers
│     │  │  ├─ AuthController.cs
│     │  │  ├─ BinController.cs
│     │  │  ├─ BusinessPartnerController.cs
│     │  │  ├─ CategoryController.cs
│     │  │  ├─ CheckHealthController.cs
│     │  │  ├─ GoodTransactionController.cs
│     │  │  ├─ InventoryController.cs
│     │  │  ├─ MovementController.cs
│     │  │  ├─ ParetoController.cs
│     │  │  ├─ ProductController.cs
│     │  │  ├─ ReservationController.cs
│     │  │  ├─ SetupsController.cs
│     │  │  ├─ StockController.cs
│     │  │  └─ WarehouseController.cs
│     │  ├─ DependencyInjection
│     │  │  └─ DependencyInjection.cs
│     │  ├─ Extensions
│     │  │  ├─ AuthExtensions.cs
│     │  │  ├─ DatabaseExtentions.cs
│     │  │  ├─ HealthCheckExtensions.cs
│     │  │  └─ RateLimitExtensions.cs
│     │  ├─ Middlewares
│     │  │  ├─ CorrelationCheckMiddleware.cs
│     │  │  └─ ExceptionMiddleware.cs
│     │  ├─ Program.cs
│     │  └─ Properties
│     │     ├─ launchSettings.json
│     │     └─ PublishProfiles
│     ├─ Application
│     │  ├─ Application.csproj
│     │  ├─ DependencyInjection
│     │  │  └─ DependencyInjection.cs
│     │  ├─ Dtos
│     │  │  ├─ BinDTO.cs
│     │  │  ├─ BusinessPartnerDTO.cs
│     │  │  ├─ CategoryDTO.cs
│     │  │  ├─ GoodTractionsDTO.cs
│     │  │  ├─ InventoryDTO.cs
│     │  │  ├─ LoginDTO.cs
│     │  │  ├─ MovementDTO.cs
│     │  │  ├─ ParetoDTO.cs
│     │  │  ├─ ProductDTO.cs
│     │  │  ├─ RefreshTokenDTO.cs
│     │  │  ├─ ReservationDTO.cs
│     │  │  ├─ SetupsDTO.cs
│     │  │  ├─ StockDTO.cs
│     │  │  └─ WarehouseDTO.cs
│     │  ├─ Exceptions
│     │  │  └─ DomainException.cs
│     │  ├─ Helper
│     │  │  ├─ Calculator.cs
│     │  │  ├─ Hash.cs
│     │  │  └─ Options
│     │  │     └─ JwtOptions.cs
│     │  ├─ Interfaces
│     │  │  ├─ IAuthService.cs
│     │  │  ├─ IBinService.cs
│     │  │  ├─ IBusinessPartnerService.cs
│     │  │  ├─ ICategoryService.cs
│     │  │  ├─ IGoodTransactionService.cs
│     │  │  ├─ IHealthService.cs
│     │  │  ├─ IInventoryService.cs
│     │  │  ├─ IMovementService.cs
│     │  │  ├─ IParetoService.cs
│     │  │  ├─ IProductService.cs
│     │  │  ├─ IReservationService.cs
│     │  │  ├─ ISetupsService.cs
│     │  │  ├─ IStockService.cs
│     │  │  ├─ IValidationRunner.cs
│     │  │  └─ IWarehouseService.cs
│     │  ├─ Services
│     │  │  ├─ AuthService.cs
│     │  │  ├─ BaseService.cs
│     │  │  ├─ BinService.cs
│     │  │  ├─ BusinessPartnerService.cs
│     │  │  ├─ CategoryService.cs
│     │  │  ├─ GoodTransactionService.cs
│     │  │  ├─ HealthService.cs
│     │  │  ├─ InventoryService.cs
│     │  │  ├─ MovementService.cs
│     │  │  ├─ ParetoService.cs
│     │  │  ├─ ProductService.cs
│     │  │  ├─ ReservationService.cs
│     │  │  ├─ SetupsService.cs
│     │  │  ├─ StockService.cs
│     │  │  ├─ ValidationRunner.cs
│     │  │  └─ WarehouseService.cs
│     │  └─ Validators
│     │     └─ LoginRequestValidator.cs
│     ├─ Contract
│     │  ├─ Contract.csproj
│     │  ├─ Interfaces
│     │  │  └─ IFlagValidatableRequest.cs
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
│     │  │  ├─ GoodTransaction.cs
│     │  │  ├─ Inventory.cs
│     │  │  ├─ Movement.cs
│     │  │  ├─ Pareto.cs
│     │  │  ├─ Product.cs
│     │  │  ├─ RefreshToken.cs
│     │  │  ├─ Reservation.cs
│     │  │  ├─ Stock.cs
│     │  │  ├─ User.cs
│     │  │  ├─ Warehouse.cs
│     │  │  └─ Weak
│     │  │     ├─ BinDetails.cs
│     │  │     ├─ GoodTransactionDetails.cs
│     │  │     ├─ InventoryDetails.cs
│     │  │     ├─ Movement
│     │  │     │  ├─ MovementReport.cs
│     │  │     │  └─ MovementSummary.cs
│     │  │     ├─ MovementDetails.cs
│     │  │     ├─ ParetoDetails.cs
│     │  │     ├─ ProductDetails.cs
│     │  │     ├─ ReservationDetails.cs
│     │  │     └─ StockDetails.cs
│     │  ├─ Enums
│     │  │  ├─ InventoryType.cs
│     │  │  ├─ ParetoTag.cs
│     │  │  └─ SummaryPeriod.cs
│     │  ├─ Helpers
│     │  │  └─ MongoDateFormats.cs
│     │  ├─ Properties
│     │  └─ Repositories
│     │     ├─ IBaseRepository.cs
│     │     ├─ IBinRepository.cs
│     │     ├─ IBusinessPartnerRepository.cs
│     │     ├─ ICategoryRepository.cs
│     │     ├─ IGoodTransactionRepository.cs
│     │     ├─ IInventoryRepository.cs
│     │     ├─ IMovementRepository.cs
│     │     ├─ IParetoRepository.cs
│     │     ├─ IProductRepository.cs
│     │     ├─ IRefreshTokenRepository.cs
│     │     ├─ IReservationRepository.cs
│     │     ├─ IStockRepository.cs
│     │     ├─ IUserRepository.cs
│     │     └─ IWarehouseRepository.cs
│     └─ Infrastructure
│        ├─ Constants
│        │  ├─ MongoCollections.cs
│        │  └─ MongoFields.cs
│        ├─ DB
│        │  ├─ MongoDBConfig.cs
│        │  ├─ MongoDbContext.cs
│        │  ├─ RedisConfig.cs
│        │  └─ RedisContext.cs
│        ├─ Helpers
│        │  ├─ BsonDocumentExpression.cs
│        │  ├─ DateHelper.cs
│        │  └─ MongoAggregationPipeline.cs
│        ├─ Infrastructure.csproj
│        └─ Repositories
│           ├─ BinRepository.cs
│           ├─ BusinessPartnerRepository.cs
│           ├─ CategoryRepository.cs
│           ├─ GoodTransactionRepository.cs
│           ├─ InventoryRepository.cs
│           ├─ MovementRepository.cs
│           ├─ ParetoRepository.cs
│           ├─ ProductRepository.cs
│           ├─ RefreshTokenRepository.cs
│           ├─ ReservationRepository.cs
│           ├─ StockRepository.cs
│           ├─ UserRepository.cs
│           └─ WarehouseRepository.cs
├─ tests
│  └─ WarehouseBusiness.UnitTests
│     ├─ API.UnitTests
│     │  ├─ API.UnitTests.csproj
│     │  ├─ appsettings.json
│     │  ├─ Program.cs
│     │  └─ Properties
│     │     └─ launchSettings.json
│     └─ Application.UnitTests
│        ├─ Application.UnitTests.csproj
│        ├─ Dtos
│        │  └─ LoginTestDTO.cs
│        ├─ Properties
│        ├─ Services
│        │  ├─ AuthServiceTests.cs
│        │  └─ ValidationRunnerTest.cs
│        └─ Validators
│           └─ LoginRequestValidatorTest.cs
└─ warehouse-api-dotnet.sln

```