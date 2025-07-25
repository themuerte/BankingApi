# Banking API

API RESTful para gestión de clientes, cuentas bancarias y transacciones.

## Requisitos

- .NET 8 SDK
- SQLite (se crea automáticamente el archivo de base de datos)

## Ejecución del Proyecto

1. **Restaurar dependencias**
   ```sh
   dotnet restore
   ```

2. **Compilar el proyecto**
   ```sh
   dotnet build
   ```

3. **Ejecutar migraciones**
   ```sh
   dotnet ef database update --project BankingAPI.Api
   ```

4. **Ejecutar la API**
   ```sh
   dotnet run --project BankingAPI.Api
   ```

5. **Acceder a Swagger**
   - Abre tu navegador en [http://localhost:5194/swagger](http://localhost:5194/swagger) (o el puerto configurado).

## Ejecución de Pruebas Unitarias

1. **Ejecutar los tests**
   ```sh
   dotnet test
   ```

   Esto ejecutará todas las pruebas unitarias del proyecto.

## Estructura Principal

- `BankingAPI.Api`: Proyecto principal de la API.
- `BankingAPI.Test`: Proyecto de pruebas unitarias.
- `DTOs`: Objetos de transferencia de datos.
- `Models`: Modelos de entidades.
- `Services`: Lógica de negocio y servicios.
- `Context`: Contexto de base de datos (EF Core).

## Endpoints Principales

- `POST /api/banking/client` - Crear cliente
- `POST /api/banking/account` - Crear cuenta bancaria
- `GET /api/banking/account/{accountNumber}/balance` - Consultar saldo
- `POST /api/banking/account/{accountNumber}/transaction` - Registrar depósito/retiro
- `GET /api/banking/account/{accountNumber}/transactions` - Consultar historial de transacciones

