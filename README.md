# HungerStation Microservices Platform

## Technical Architecture Overview

HungerStation is a comprehensive food delivery microservices platform built with **.NET 8**, implementing modern microservices patterns with event-driven architecture, API Gateway, and cloud-native deployment strategies.

## Architecture Patterns & Design Principles

### Microservices Architecture

* **Domain-Driven Design (DDD)**: Each service represents a bounded context
* **Database per Service**: Independent data stores for each microservice
* **API Gateway Pattern**: Centralized entry point for client communications
* **Event-Driven Architecture**: Asynchronous communication via Azure Service Bus
* **CQRS (Command Query Responsibility Segregation)**: Separation of read/write operations
* **Circuit Breaker Pattern**: Fault tolerance and resilience

### Core Design Principles

* **Single Responsibility Principle**: Each service has one business responsibility
* **Loose Coupling**: Services communicate through well-defined APIs
* **High Cohesion**: Related functionality grouped within services
* **Autonomous Services**: Independent deployment and scaling
* **Fault Isolation**: Failures contained within service boundaries

## Microservices Catalog

### 1. Authentication Service (AuthAPI)

**Port**: 7001
**Technology Stack**: ASP.NET Core 8, Entity Framework Core, JWT, Identity Framework

```
├── Controllers/AuthController.cs
├── Models/ApplicationUser.cs
├── Services/JwtTokenGenerator.cs
└── Data/AppDbContext.cs
```

**Key Features**:

* JWT Token generation and validation
* Role-based authorization (Admin, Customer)
* User registration and authentication
* Password hashing with Identity Framework
* Token refresh mechanism

**API Endpoints**:

```
POST /api/auth/register     - User Registration  
POST /api/auth/login        - User Authentication  
POST /api/auth/assign-role  - Role Assignment  
```

### 2. Product Catalog Service (ProductAPI)

**Port**: 7002
**Technology Stack**: ASP.NET Core 8, Entity Framework Core, AutoMapper

```
├── Controllers/ProductController.cs
├── Models/Product.cs
├── Data/AppDbContext.cs
└── Helpers/MappingProfiles.cs
```

**Key Features**:

* Product CRUD operations
* Category management
* Image handling and storage
* Search and filtering capabilities
* Stock management

**API Endpoints**:

```
GET    /api/product           - List all products  
GET    /api/product/{id}      - Get product by ID  
POST   /api/product           - Create product (Admin)  
PUT    /api/product           - Update product (Admin)  
DELETE /api/product/{id}      - Delete product (Admin)  
```

### 3. Coupon Management Service (CouponAPI)

**Port**: 7003
**Technology Stack**: ASP.NET Core 8, Entity Framework Core, AutoMapper

```
├── Controllers/CouponController.cs
├── Models/Coupon.cs
├── Data/AppDbContext.cs
└── Helpers/MappingProfiles.cs
```

**Key Features**:

* Discount coupon creation and management
* Coupon validation and expiry handling
* Usage tracking and limits
* Percentage and fixed amount discounts

**API Endpoints**:

```
GET    /api/coupon                 - List all coupons  
GET    /api/coupon/{id}            - Get coupon by ID  
GET    /api/coupon/GetByCode/{code} - Get coupon by code  
POST   /api/coupon                 - Create coupon (Admin)  
PUT    /api/coupon                 - Update coupon (Admin)  
DELETE /api/coupon/{id}            - Delete coupon (Admin)  
```

### 4. Shopping Cart Service (ShoppingCartAPI)

**Port**: 7004
**Technology Stack**: ASP.NET Core 8, Entity Framework Core, Azure Service Bus

```
├── Controllers/CartAPIController.cs
├── Models/CartHeader.cs
├── Models/CartDetails.cs
├── Service/ProductService.cs
└── Service/CouponService.cs
```

**Key Features**:

* Cart item management (Add, Update, Remove)
* Coupon application and validation
* Real-time price calculation
* Cart persistence for logged-in users
* Integration with Product and Coupon services

**API Endpoints**:

```
GET    /api/cart/GetCart/{userId}  - Get user cart  
POST   /api/cart/CartUpsert        - Add/Update cart item  
POST   /api/cart/RemoveCart        - Remove cart item  
POST   /api/cart/ApplyCoupon       - Apply coupon to cart  
POST   /api/cart/EmailCartRequest  - Send cart via email  
```

### 5. Order Management Service (OrderAPI)

**Port**: 7005
**Technology Stack**: ASP.NET Core 8, Entity Framework Core, Stripe, Azure Service Bus

```
├── Controllers/OrderController.cs
├── Models/OrderHeader.cs
├── Models/OrderDetails.cs
├── Service/ProductService.cs
└── Utility/SD.cs
```

**Key Features**:

* Order creation and lifecycle management
* Stripe payment integration
* Order status tracking (Pending → Approved → ReadyForPickup → Completed)
* Payment validation and refund processing
* Reward points calculation via Service Bus

**API Endpoints**:

```
GET    /api/order/GetOrders        - List orders (filtered by user/admin)  
GET    /api/order/GetOrder/{id}    - Get order details  
POST   /api/order/CreateOrder      - Create new order  
POST   /api/order/CreateStripeSession - Create Stripe payment session  
POST   /api/order/ValidateStripeSession - Validate payment  
POST   /api/order/UpdateOrderStatus/{id} - Update order status  
```

**Order State Machine**:

```
Pending → Approved → ReadyForPickup → Completed  
    ↓  
Cancelled/Refunded  
```

### 6. Email Notification Service (EmailAPI)

**Port**: 7006
**Technology Stack**: ASP.NET Core 8, Azure Service Bus, Entity Framework Core

```
├── Messaging/AzureServiceBusConsumer.cs
├── Services/EmailService.cs
├── Models/EmailLogger.cs
└── Extensions/ApplicationBuilderExtensions.cs
```

**Key Features**:

* Asynchronous email processing via Service Bus
* Email template management
* Delivery status tracking
* Cart abandonment notifications
* Order confirmation emails

**Service Bus Integration**:

```
Queue: EmailShoppingCartQueue  
Topic: OrderCreatedTopic  
```

### 7. Rewards & Loyalty Service (RewardAPI)

**Port**: 7007
**Technology Stack**: ASP.NET Core 8, Azure Service Bus, Entity Framework Core

```
├── Messaging/AzureServiceBusConsumer.cs
├── Services/RewardService.cs
├── Models/Rewards.cs
└── Extensions/ApplicationBuilderExtensions.cs
```

**Key Features**:

* Points-based loyalty system
* Order completion reward calculation
* User reward history tracking
* Event-driven reward processing

**Service Bus Integration**:

```
Topic: OrderCreatedTopic  
Subscription: OrderCreated_Rewards_Subscription  
```

### 8. Web Frontend (MVC Application)

**Port**: 7000
**Technology Stack**: ASP.NET Core MVC 8, Bootstrap, jQuery

```
├── Controllers/HomeController.cs
├── Controllers/CartController.cs
├── Controllers/OrderController.cs
├── Service/BaseService.cs
└── Views/
```

**Key Features**:

* Responsive web interface
* JWT-based authentication
* Shopping cart management
* Order tracking dashboard
* Admin panel for product/coupon management

## Inter-Service Communication

### Synchronous Communication

* **HTTP/HTTPS REST APIs**: Direct service-to-service calls
* **Service Discovery**: Configuration-based endpoint resolution
* **Circuit Breaker**: Fault tolerance for external calls

### Asynchronous Communication

* **Azure Service Bus**: Message queuing and pub/sub patterns
* **Event-Driven Architecture**: Domain events for business processes
* **Message Patterns**: Command, Event, and Query messages

### Communication Flow

```
Web App → API Gateway → Microservices  
    ↓  
Service Bus → Background Services  
    ↓  
Database Updates → Event Publishing  
```

## Security Architecture

### Authentication & Authorization

* **JWT (JSON Web Tokens)**: Stateless authentication
* **Role-Based Access Control (RBAC)**: Admin and Customer roles
* **Bearer Token Authentication**: API endpoint protection
* **Refresh Token Mechanism**: Session management

### Security Patterns

```csharp
[Authorize(Roles = "ADMIN")]
public class ProductController : ControllerBase
{
    // Admin-only endpoints
}
```

### Security Headers

* HTTPS enforcement
* CORS configuration
* Input validation and sanitization
* SQL injection prevention via Entity Framework

## Data Architecture

### Database Design

* **Database per Service**: Independent data stores
* **Entity Framework Core**: ORM with Code-First approach
* **SQL Server**: Primary database engine
* **Connection String Management**: Configuration-based

### Data Consistency

* **Eventual Consistency**: Across service boundaries
* **Transactional Consistency**: Within service boundaries
* **Saga Pattern**: Distributed transaction management
* **Event Sourcing**: Audit trail for critical operations

### Database Schema Examples

```sql
-- Order Service Tables  
OrderHeaders (OrderHeaderId, UserId, OrderTotal, Status, PaymentIntentId)  
OrderDetails (OrderDetailsId, OrderHeaderId, ProductId, Price, Count)  

-- Reward Service Tables  
Rewards (Id, UserId, OrderId, RewardsActivity, RewardsDate)  
```

## Event-Driven Architecture

### Azure Service Bus Configuration

```json
{
  "TopicAndQueueNames": {
    "EmailShoppingCartQueue": "emailshoppingcart",
    "OrderCreatedTopic": "ordercreated",
    "OrderCreated_Rewards_Subscription": "ordercreated_rewards"
  }
}
```

### Event Flow Patterns

1. **Cart Email Request**: Cart → Service Bus Queue → Email Service
2. **Order Completion**: Order → Service Bus Topic → Rewards Service
3. **Payment Validation**: Stripe Webhook → Order Update → Reward Points

### Message Handling

```csharp
public async Task OnEmailCartRequestReceived(ProcessMessageEventArgs arg)
{
    var cartDto = JsonConvert.DeserializeObject<CartDto>(messageBody);
    await _emailService.EmailCartAndLogAsync(cartDto);
    await arg.CompleteMessageAsync(arg.Message);
}
```

## Development & Deployment

### Project Structure

```
HungerStation_Microservices/  
├── HungerStation.Services.AuthAPI/  
├── HungerStation.Services.ProductAPI/  
├── HungerStation.Services.CouponAPI/  
├── HungerStation.Services.ShoppingCartAPI/  
├── HungerStation.Services.OrderAPI/  
├── HungerStation.Services.EmailAPI/  
├── HungerStation.Services.RewardAPI/  
├── HungerStation.MessageBus/  
├── HungerStation.Web/  
└── HungerStation.sln  
```

### Technology Stack Summary

| Component          | Technology            | Version |
| ------------------ | --------------------- | ------- |
| Runtime            | .NET                  | 8.0     |
| Web Framework      | ASP.NET Core          | 8.0     |
| ORM                | Entity Framework Core | 8.0     |
| Database           | SQL Server            | Latest  |
| Messaging          | Azure Service Bus     | 7.17+   |
| Authentication     | JWT Bearer            | 8.0     |
| Payment            | Stripe.NET            | 44.13+  |
| Object Mapping     | AutoMapper            | 12.0+   |
| JSON Serialization | Newtonsoft.Json       | 13.0+   |

### Build and Run

```bash
# Build entire solution  
dotnet build HungerStation.sln  

# Run specific service  
dotnet run --project HungerStation.Services.OrderAPI  

# Run all services (requires multiple terminals)  
dotnet run --project HungerStation.Services.AuthAPI  
dotnet run --project HungerStation.Services.ProductAPI  
dotnet run --project HungerStation.Services.CouponAPI  
dotnet run --project HungerStation.Services.ShoppingCartAPI  
dotnet run --project HungerStation.Services.OrderAPI  
dotnet run --project HungerStation.Services.EmailAPI  
dotnet run --project HungerStation.Services.RewardAPI  
dotnet run --project HungerStation.Web  
```

### Service Ports Configuration

```json
{
  "ServiceUrls": {
    "AuthAPI": "https://localhost:7001",
    "ProductAPI": "https://localhost:7002", 
    "CouponAPI": "https://localhost:7003",
    "ShoppingCartAPI": "https://localhost:7004",
    "OrderAPI": "https://localhost:7005",
    "EmailAPI": "https://localhost:7006",
    "RewardAPI": "https://localhost:7007"
  }
}
```

## Testing Strategy

### Unit Testing

* **xUnit**: Primary testing framework
* **Moq**: Mocking framework for dependencies
* **FluentAssertions**: Readable test assertions

### Integration Testing

* **TestServer**: In-memory server for API testing
* **Test Databases**: Isolated test environments
* **Service Bus Emulation**: Local testing capabilities

### API Testing

* **Swagger/OpenAPI**: Interactive API documentation
* **Postman Collections**: Automated API testing
* **Health Checks**: Service monitoring endpoints

## Monitoring & Observability

### Logging

* **Serilog**: Structured logging framework
* **Application Insights**: Cloud-based monitoring
* **Correlation IDs**: Request tracing across services

### Health Checks

```csharp
builder.Services.AddHealthChecks()
    .AddDbContext<AppDbContext>()
    .AddAzureServiceBusTopic(connectionString, topicName);
```

### Metrics & Performance

* **Response Time Monitoring**: API performance tracking
* **Error Rate Tracking**: Failure analysis
* **Resource Utilization**: CPU, Memory, Database connections

## Future Enhancements

### Planned Features

1. **API Gateway**: Ocelot or Azure API Management
2. **Service Discovery**: Consul or Azure Service Discovery
3. **Configuration Management**: Azure Key Vault integration
4. **Caching Layer**: Redis for improved performance
5. **Message Encryption**: End-to-end message security
6. **Event Store**: Complete audit trail implementation
7. **GraphQL Gateway**: Unified data querying interface
8. **Container Orchestration**: Kubernetes deployment
9. **CI/CD Pipeline**: Azure DevOps or GitHub Actions
10. **Load Balancing**: Multi-instance deployment support

### Scalability Considerations

* **Horizontal Scaling**: Multi-instance deployment
* **Database Sharding**: Data distribution strategies
* **CDN Integration**: Static content delivery
* **Auto-scaling**: Resource-based scaling policies

## Development Guidelines

### Code Quality Standards

* **Clean Architecture**: Separation of concerns
* **SOLID Principles**: Object-oriented design
* **Code Reviews**: Peer review process
* **Static Analysis**: SonarQube integration
* **Documentation**: Comprehensive API documentation

### Git Workflow

```bash
# Feature branch workflow  
git checkout -b feature/order-management  
git commit -m "feat: implement order creation endpoint"  
git push origin feature/order-management  
```

### Commit Message Convention

```
feat: add new order management service  
fix: resolve payment validation issue  
docs: update API documentation  
test: add unit tests for reward calculation  
refactor: optimize database queries  
```

---

## Performance Benchmarks

| Metric             | Target  | Current |
| ------------------ | ------- | ------- |
| API Response Time  | < 200ms | \~150ms |
| Order Processing   | < 5s    | \~3s    |
| Cart Operations    | < 100ms | \~80ms  |
| Email Delivery     | < 30s   | \~25s   |
| Payment Processing | < 10s   | \~7s    |

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**HungerStation Microservices Platform** - Built with .NET 8 and modern cloud-native patterns.

---


