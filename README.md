# Sistema de Votación Nacional

## 1. Integrantes del grupo

- Fernando Carrillo Castro
- Andy González Jiménez
- Josué Navarro Barrantes
- Raul Castro Acuña

## 2. Repositorio

https://github.com/Raid2021/Practica-3/tree/main

## 3. Especificación del proyecto

### a. Arquitectura del proyecto

Solución en capas compuesta por 4 proyectos .NET 8:

| Proyecto | Tipo | Responsabilidad |
|---|---|---|
| `SistemaVotacion.Core` | Class Library | Entidades de dominio (`Votante`, `PartidoPolitico`, `Voto`) y DTOs (contratos de entrada/salida compartidos entre la API y el MVC). Capa transversal sin dependencias hacia las demás. |
| `SistemaVotacion.Infrastructure` | Class Library | Acceso a datos: `ApplicationDbContext` (EF Core), migraciones y Repositorios (`IVotanteRepository`, `IPartidoRepository`, `IVotoRepository`). Depende de `Core`. |
| `SistemaVotacion.API` | ASP.NET Core Web API | Capa de lógica de negocio y exposición HTTP: Controllers + Services (`IVotanteService`, `IPartidoService`, `IVotoService`) + perfil de AutoMapper. Depende de `Core` e `Infrastructure`. |
| `SistemaVotacion.Web` | ASP.NET Core MVC | Interfaz de usuario. Consume `SistemaVotacion.API` mediante clientes HTTP tipados (`HttpClient` + `IHttpClientFactory`). Depende únicamente de `Core` (para reutilizar DTOs/validaciones). |

Flujo de una petición: `Vista (Razor) → Controller MVC → Servicio HTTP tipado → API REST → Service → Repository → EF Core → SQL Server`.

### b. Librerías / paquetes NuGet utilizados

- `Microsoft.EntityFrameworkCore` / `Microsoft.EntityFrameworkCore.SqlServer` / `Microsoft.EntityFrameworkCore.Tools` — acceso a datos y migraciones (SQL Server).
- `AutoMapper.Extensions.Microsoft.DependencyInjection` — mapeo entidad ↔ DTO en la capa de servicios de la API.
- `Swashbuckle.AspNetCore` — documentación/exploración interactiva de la API (Swagger).
- `SistemaVotacion.Web` no agrega paquetes adicionales: usa `HttpClient`/`IHttpClientFactory` y Razor, incluidos en el SDK `Microsoft.NET.Sdk.Web`.

### c. Principios SOLID y patrones de diseño utilizados

**Patrones:**
- **Repository** — `IVotanteRepository`, `IPartidoRepository`, `IVotoRepository` aíslan el acceso a datos (EF Core) del resto de la aplicación.
- **Service Layer** — `IVotanteService`, `IPartidoService`, `IVotoService` concentran las reglas de negocio (cédula única, no eliminar votante que ya votó, validar partido activo antes de votar, etc.) y son consumidos por los Controllers.
- **DTO (Data Transfer Object)** — `Core/DTOs` desacopla lo que viaja por HTTP de las entidades de EF Core, evitando sobre-exposición de datos y problemas de serialización circular.
- **Result simplificado** — los métodos de escritura de los servicios retornan `string` (vacío = éxito, mensaje = error) en vez de lanzar excepciones para errores de negocio esperados, evitando `try/catch` en los controllers.
- **Typed Client** — `IVotanteApiService`, `IPartidoApiService`, `IVotacionApiService` en `SistemaVotacion.Web` encapsulan el consumo de la API vía `HttpClient` inyectado por `IHttpClientFactory`.
- **Dependency Injection** — todos los repositorios, servicios y clientes HTTP se registran e inyectan vía el contenedor de `Program.cs` (constructor injection) tanto en la API como en el MVC.

**SOLID:**
- **S (Single Responsibility)**: cada capa tiene una única razón de cambio — Controllers solo orquestan HTTP, Services solo contienen reglas de negocio, Repositories solo hacen acceso a datos, y en el MVC los Controllers de presentación delegan toda la lógica de negocio a la API.
- **O (Open/Closed)**: los ViewModels del MVC (`VotanteEditViewModel`, `PartidoEditViewModel`) extienden los DTOs de `Core` en vez de duplicar sus propiedades y validaciones, permitiendo agregar campos propios de la vista sin modificar el DTO original.
- **L (Liskov Substitution)**: cualquier implementación de `IVotanteRepository`, `IPartidoService`, `IVotanteApiService`, etc., es intercambiable sin alterar el comportamiento esperado por quien la consume.
- **I (Interface Segregation)**: interfaces pequeñas y específicas por entidad/caso de uso (`IVotoService.VotarAsync`, `IVotoService.ObtenerResultadosAsync`) en vez de una interfaz genérica y sobrecargada.
- **D (Dependency Inversion)**: los Controllers dependen de abstracciones (`IVotanteService`, `IVotanteApiService`), no de sus implementaciones concretas; estas se inyectan por constructor y se registran en `Program.cs`.

## Funcionalidades

- CRUD completo de votantes (con validación de cédula única y bloqueo de eliminación si ya votó).
- CRUD completo de partidos políticos (con validación de nombre/siglas únicos).
- Pantalla de votación: valida que la cédula esté registrada y que el votante no haya votado antes de registrar el voto.
- Pantalla de resultados: cantidad de votos y porcentaje por partido.

## Instrucciones para ejecutar el sistema

1. Requisitos: .NET 8 SDK y SQL Server (o LocalDB, incluido con Visual Studio).
2. Restaurar dependencias y aplicar migraciones (desde `SistemaVotacion.API`, que es donde está configurado el `DbContext`):
   ```
   dotnet restore
   dotnet ef database update --project SistemaVotacion.Infrastructure --startup-project SistemaVotacion.API
   ```
3. Ejecutar la API (queda disponible en `http://localhost:5114`, Swagger en `/swagger`):
   ```
   dotnet run --project SistemaVotacion.API
   ```
4. En otra terminal, ejecutar el proyecto MVC (queda disponible en `http://localhost:5201`). La URL de la API se configura en `SistemaVotacion.Web/appsettings.json` (`ApiSettings:BaseUrl`) y ya apunta por defecto a `http://localhost:5114/`:
   ```
   dotnet run --project SistemaVotacion.Web
   ```
5. Abrir `http://localhost:5201` en el navegador para usar el sistema (Votantes, Partidos, Votar, Resultados).
