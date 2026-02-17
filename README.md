# Plataforma de Participación Digital

> **Fortaleciendo la democracia y la inclusión ciudadana en República Dominicana.** 🇩🇴

Este proyecto es una plataforma web desarrollada en **.NET 8 (Blazor Server)** que permite a la ciudadanía participar activamente en la toma de decisiones públicas a través de consultas y encuestas digitales. Alineado con la **Agenda Digital 2030**.

## 🚀 Características Principales

### Para Ciudadanos
*   **Consultas Públicas**: Acceso a encuestas activas sobre temas de interés nacional.
*   **Votación en Tiempo Real**: Sistema de votación interactivo con actualizaciones instantáneas (SignalR).
*   **Comentarios**: Espacio para debatir y proponer ideas (moderado).
*   **Transparencia**: Visualización de resultados en tiempo real y estadísticas claras.

### Para Administradores (Gobierno)
*   **Gestión de Encuestas**: Creación, edición y publicación de nuevas consultas.
*   **Panel de Control**: Dashboard con métricas clave de participación.
*   **Reportes**: Exportación de datos en formato CSV para análisis detallado.
*   **Seguridad**: Gestión de usuarios y roles (Admin/Ciudadano).

---

## 🛠️ Stack Tecnológico

*   **Backend & Frontend**: ASP.NET Core Blazor Server (.NET 8).
*   **Base de Datos**: SQL Server (Entity Framework Core).
*   **Tiempo Real**: SignalR.
*   **Pruebas**: xUnit, Moq, EF Core InMemory.
*   **Seguridad**: ASP.NET Core Identity, OWASP Headers, Rate Limiting.

---

## 🏗️ Arquitectura

El sistema sigue una arquitectura limpia (Clean Architecture) simplificada para prototipado rápido:

```mermaid
graph TD
    User([Usuario]) -->|HTTPS| Web[Capa Web (Blazor)]
    Web -->|SignalR| Hubs[VoteHub (Tiempo Real)]
    Web -->|DI| Services[Servicios de Aplicación]
    Services -->|EF Core| Repo[Infraestructura (Datos)]
    Repo -->|SQL| DB[(SQL Server)]
    
    subgraph "Seguridad"
        Identity[ASP.NET Identity]
        RateLimit[Rate Limiter]
    end
    
    Web -.-> Identity
    Web -.-> RateLimit
```

---

## 📋 Requisitos Previos

*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (o LocalDB)
*   Visual Studio 2022 o VS Code.

---

## ⚙️ Configuración y Ejecución

1.  **Clonar el repositorio** (o descargar el código fuente):
    ```bash
    git clone https://github.com/tu-usuario/participacion-digital.git
    cd ParticipacionDigital
    ```

2.  **Configurar Base de Datos**:
    *   Abre `ParticipacionDigital.Web/appsettings.json`.
    *   Ajusta la cadena de conexión `DefaultConnection` si es necesario.

3.  **Ejecutar Migraciones**:
    ```bash
    dotnet ef database update --project ParticipacionDigital.Infrastructure --startup-project ParticipacionDigital.Web
    ```

4.  **Iniciar la Aplicación**:
    ```bash
    dotnet run --project ParticipacionDigital.Web
    ```

5.  **Abrir en Navegador**:
    *   Navega a `https://localhost:5001`.

---

## 🧪 Ejecución de Pruebas

El proyecto incluye pruebas unitarias e integración para garantizar la calidad.

```bash
dotnet test ParticipacionDigital.Tests/ParticipacionDigital.Tests.csproj
```

---

## 🔒 Seguridad

Se han implementado medidas de hardening siguiendo recomendaciones OWASP:
*   **Cabeceras HTTP Seguras** (HSTS, X-Frame-Options, CSP, etc.).
*   **Rate Limiting** global para prevenir ataques de fuerza bruta.
*   **Resiliencia** en conexión a BD (Retries automáticos).

---

## 📄 Licencia

Este proyecto es de código abierto bajo la licencia MIT.
