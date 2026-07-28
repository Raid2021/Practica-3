# Buenas prácticas (resumen)

Este documento recoge recomendaciones prácticas para trabajar con este proyecto ASP.NET Core + EF Core (SQL Server).

- Configuración y secretos
  - Mantener la cadena de conexión fuera del código fuente. Usar `appsettings.Development.json` y/o _user secrets_ (`dotnet user-secrets`) para entorno local.
  - En producción usar variables de entorno o un servicio de secretos (por ejemplo Azure Key Vault).

- Entity Framework Core
  - Registrar el `DbContext` vía DI (Inyección de dependencias) (ya está en `Program.cs`).
  - No hardcodear la cadena en `OnConfiguring`; permitir que DI la inyecte.
  - Usar Microsoft.EntityFrameworkCore.SqlServer cuando se trabaja con SQL Server.
  - Usar migraciones (`dotnet ef migrations add <Nombre>` / `dotnet ef database update`) para evolucionar el esquema. Para SQL Server las alteraciones habituales funcionan normalmente.
  - Mantener las entidades en la carpeta `Entities` y el contexto en `Data`.
  - Utilizar IQueryable para hacer las consultas a la base de datos.
  - Utilizar AsNoTracking() para consultas de solo lectura (mejora rendimiento y evita tracking de EF Core). No usar AsNoTracking() en consultas que vayan a devolver entidades que posteriormente se actualizarán o eliminarán (en esos casos EF Core debe trackear la entidad para aplicar cambios).
  - Tracking predeterminado: por defecto EF Core realiza tracking de las entidades que devuelve en una consulta (son tracked por el DbContext). Si necesita forzar tracking en una consulta que proviene de un IQueryable configurado como NoTracking, puede usar AsTracking(). AsTracking() fuerza que las entidades devueltas sean tracked para permitir actualizaciones posteriores.

- Código y estilo
  - Habilitar `nullable` (ya está en el proyecto). Manejar referencias nulas explícitamente y usar tipos anulables cuando corresponda.
  - Preferir métodos `async` para acceso a datos (`ToListAsync`, `SaveChangesAsync`).
  - Seguir convenciones PascalCase para clases y propiedades.

- Control de versiones y PRs
  - Hacer commits pequeños y con mensajes claros. Abrir PRs para cambios significativos y pedir revisión.
  - Añadir un `.gitignore` apropiado y no commitear binarios, secretos ni bases de datos locales.

- Comandos útiles
  - Paquetes EF Core:
    - `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
    - `dotnet add package Microsoft.EntityFrameworkCore.Design`
  - Herramienta CLI: `dotnet tool install --global dotnet-ef`
  - Scaffold desde SQL Server (genera entidades y contexto):
    - `dotnet ef dbcontext scaffold "Server=.;Database=TuDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --context ApplicationDbContext --context-dir Data --force`
  - Migraciones:
    - `dotnet ef migrations add InitialCreate`
    - `dotnet ef database update`

- Otras recomendaciones
  - Documentar cómo ejecutar el proyecto en desarrollo (`dotnet restore`, `dotnet build`, `dotnet run`).
  - Mantener dependencias actualizadas y planear actualizaciones mayores con pruebas.

Si quieres, puedo añadir ejemplos concretos de `appsettings.json`, plantillas de pruebas o eliminar la clase placeholder `Ejemplo.cs` generada por el scaffold.
