# Survey Application

Kullanıcıların anket oluşturmasına, yönetmesine ve cevaplamasına olanak tanıyan bir web uygulaması geliştirilmesi.


## Kullanılan Teknolojiler

•	.NET 9
•	Clean Architecture prensiplerine uygun katmanlı yapı oluşturuldu.
o	Core Layer: Domain entities, interfaces, business rules
o	Application Layer: Use cases, DTOs, validators, application logic
o	Infrastructure Layer: Data access, external services, ORM implementation
o	API Layer: Controllers, middleware, filters
•	ORM olarak Entity Framework Core kullanıldı.
•	Authentication & Authorization: JWT Token tabanlı kimlik doğrulama yapıldı.
•	RESTful API geliştirildi.



## Proje Yapısı

```text
SurveyApp
│
├── SurveyApp.Application
│   ├── Interfaces
│   ├── Services
│   ├── DTOs
│   └── Features
│
├── SurveyApp.Core
│   ├── Entities
│   ├── Common
│   └── Constants
│
├── SurveyApp.Domain
│   ├── Entities
│   ├── Enums
│   └── Rules
│
├── SurveyApp.Persistance
│   ├── Context
│   ├── Repositories
│   ├── Configurations
│   └── Migrations
│
├── SurveyApp.WebApi
│   ├── Controllers
│   ├── Middleware
│   ├── Extensions
│   └── Program.cs
│
└── README.md
```

---

## Katmanlar

### SurveyApp.Application
Uygulama iş kurallarını içerir.

- Servisler
- DTO yapıları
- Interface tanımları
- CQRS / MediatR işlemleri

---

### SurveyApp.Core
Ortak kullanılan temel yapıları içerir.

- Base entity sınıfları
- Ortak yardımcı sınıflar
- Constants
- Genel abstractions

---

### SurveyApp.Domain
Domain modellerini ve iş kurallarını içerir.

- Entity modelleri
- Domain kuralları
- Enum yapıları

---

### SurveyApp.Persistance
Veritabanı işlemlerini yönetir.

- Entity Framework Core
- DbContext
- Repository Pattern
- Migration işlemleri

---

### SurveyApp.WebApi
API katmanıdır.

- Controller yapıları
- Endpointler
- Middleware
- Swagger
- Dependency Injection yapılandırmaları

## API Endpointleri

| Method | Endpoint | Açıklama |
|---|---|---|
| GET | /api/todos | Tüm todolar |
| GET | /api/todos/{id} | Id’ye göre todo |
| POST | /api/todos | Yeni todo ekle |
| PUT | /api/todos/{id} | Todo güncelle |
| DELETE | /api/todos/{id} | Todo sil |



