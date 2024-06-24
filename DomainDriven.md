Presentation (API) -> Application Services -> Domain (eg. `IRepository`) -> Infrastructure -> Database & external systems

- Presentation Layer (closest to consumers)

    - Application Root
    - This could be your API
    - Has Models or Dto definitions of its own with attributes relative to its layer. 
    - If this is an API, then Models/DTO have attributes for formatting or data type validations
    - Maps data between `ApplicationService.Dto` and `Presentation.Dto`
    - Must reference Application Layer to be able to inject services

- Application Service Layer

    - Has Dto definitions of its own to be able to return data without exposing the domain entities.
    - Contains Application Services:
        - Services which are specific to the implementation of a domain model or which have no dependency on the domain model.
        - A classic example of this would be sending and email based upon a state change or action in the domain.
        - This is usually a requirement of the application itself, and is likely not specified by the domain model.
        - This can either be procedurally executed by an application service after a call to the domain service, or as an event raised from the domain service.
    - Bridge between Presentation Layer and Domain Layer.

- Domain Layer

    - Domain Entities
    - May contain Domain Services:
        - Services which exist to enforce the integrity of the domain and facilitate the insertion, creation, deletion, and retrieval of data from the domain. Additionally, domain services can orchestrate higher-level combinations of domain objects into viewmodels.
        - Often, these are facades on top of repositories, working to hide some of the low-level implementation and to provide an interface more in line with the UL (ubiquitous language) to help manage expectations.
    - In some situations when the domain service needs to interact with other Domains (Business-Customer relationships) or external systems, then the:
        - domain service interface is created in the domain layer, defined in words that can be understood by the business, free from technical terms (IE: `IExcelReport`, `IGoogleSheetReport`, `IRepository`)
        - domain service implementation is created in the Infrastructure layer.

- Infrastructure Layer

    - Closest to your database or external services.
    - Database infrastructure (mapping).
    - Excel libraries if you define this layer as infrastructure code.
    - Mail or notification services.
    - PDF output files