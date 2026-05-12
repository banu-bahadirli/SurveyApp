# Survey Application

Kullanıcıların anket oluşturmasına, yönetmesine ve cevaplamasına olanak tanıyan bir web uygulaması geliştirilmesi.


## Kullanılan Teknolojiler
- **.NET 9**
- Clean Architecture prensiplerine uygun katmanlı yapı oluşturuldu.
- Entity Framework Core (ORM) kullanıldı.			- 
-Authentication & Authorization: JWT Token tabanlı kimlik doğrulama yapıldı.
- RESTful API geliştirme
- FluentValidation (request doğrulama)
- MediatR (CQRS ve pipeline behavior)


## Proje Yapısı

```text
SurveyApp
│
├── SurveyApp.Application
│   ├── Behaviors
│   ├── Features
│   ├── Services
│   
│
├── SurveyApp.Core
│   ├── Exceptions
│   ├── Persistance
│   └── Security
│
├── SurveyApp.Domain
│   ├── Entities
│ 
│
├── SurveyApp.Persistance
│   ├── Context
│   ├── EntityConfigurations
│   ├── Migrations
│   └── Repositories
│
├── SurveyApp.WebApi
│   ├── Controllers
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



