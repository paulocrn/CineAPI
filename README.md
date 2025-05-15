
# 🎬 CineAPI - API REST para gestión de cine

Proyecto backend desarrollado en **.NET 9**, con arquitectura en capas (API, Application, Domain, Infrastructure) y acceso a base de datos SQL Server mediante **Entity Framework Core**. Incluye Swagger y un seeder automático de datos para desarrollo.

---

## 🚀 Tecnologías utilizadas

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server (local)
- Entity Framework Core
- AutoMapper
- Swagger
- Arquitectura hexagonal

---

## ⚙️ Requisitos previos

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- SQL Server (instancia local como `localhost\SQLEXPRESS`)

---

## 📦 Clonar el proyecto

```bash
git clone https://github.com/paulocrn/CineAPI.git
cd CineAPI
```

---

## 🔧 Configuración inicial

### 1. Configura tu base de datos en `appsettings.json`

Ubicación: `src/API/appsettings.json`

```json
"ConnectionStrings": {
  "ConexionPrincipal": "Server=localhost\SQLEXPRESS;Database=cinema;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> 💡 Asegúrate de que la base de datos `cinema` no exista si quieres que se cree limpia desde cero.

---

## 🧪 Base de datos y datos de prueba

Al ejecutar la API en modo desarrollo, se realizará lo siguiente automáticamente:

- Se eliminará y recreará la base de datos (`EnsureDeleted` + `EnsureCreated`)
- Se ejecutará el **seeder de prueba** (`DatabaseSeeder`) con:
  - 50 clientes
  - 10 películas
  - 5 salas con asientos
  - 20 funciones (carteleras)
  - Reservas aleatorias (marcando asientos ocupados)

Esto ya está incluido en `Program.cs`.

---

## ▶️ Ejecutar el proyecto

Desde Visual Studio o terminal:

```bash
cd src/API
dotnet run
```

La API estará disponible en:

```
http://localhost:5084
```

## 🌐 Documentación Swagger

Disponible en:

```
http://localhost:5084/swagger
```

---

## 🔗 Endpoints destacados

- `GET /api/cartelera` — Obtener todas las funciones
- `GET /api/cartelera/{id}` — Obtener función por ID
- `POST /api/cartelera` — Crear nueva función
- `PUT /api/cartelera/{id}` — Actualizar función
- `DELETE /api/cartelera/{id}` — Eliminar función
- `POST /api/cartelera/cancelar/{id}` — Cancelar función y sus reservas
- `GET /api/cartelera/reservas-genero?genero=Accion&desde=...&hasta=...` — Reservas por género
- `GET /api/cartelera/estadisticas-asientos?fecha=...` — Estadísticas por sala

---

## 📌 Notas adicionales

- Puedes modificar el seeder en `DatabaseSeeder.cs` para agregar más datos personalizados.
- La base de datos se reinicia solo en entorno de desarrollo.
- Las migraciones no son necesarias porque se usa `EnsureCreated`, pero puedes agregarlas si deseas persistencia real.
- Las dependencias y servicios están registrados automáticamente al inicio en `Program.cs`.
