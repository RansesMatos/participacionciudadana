# Manual de Usuario: Plataforma de Participación Digital

Bienvenido a la guía de uso de la plataforma. Este documento explica cómo interactuar con las diferentes funcionalidades del sistema.

## 👤 Rol: Ciudadano

### 1. Registro e Inicio de Sesión
*   **Registro**:
    *   Haz clic en el botón "Registrarse" en la página de inicio o en el menú superior.
    *   Completa el formulario con tu correo electrónico y una contraseña segura.
    *   (Nota: En este prototipo, el correo no requiere confirmación real).
*   **Login**:
    *   Usa el botón "Login" e ingresa tus credenciales.

### 2. Participar en Encuestas
*   **Ver Encuestas**: En la página de inicio o en la sección "Encuestas", verás un listado de consultas activas.
*   **Votar**:
    *   Haz clic en "Ver Detalles" o "Participar" en una encuesta.
    *   Si no has iniciado sesión, el sistema te pedirá hacerlo.
    *   Selecciona una de las opciones disponibles y haz clic en "Enviar Voto".
    *   **Confirmación**: Verás un mensaje de éxito y las barras de resultados se actualizarán en tiempo real.
*   **Resultados**:
    *   Si ya votaste, podrás ver las estadísticas actuales de la votación.

### 3. Comentarios (Próximamente)
*   Podrás dejar opiniones escritas en cada encuesta para debatir con otros ciudadanos.

---

## 🛡️ Rol: Administrador

### 1. Acceso al Panel de Control
*   Inicia sesión con una cuenta que tenga permisos de administrador (configurada previamente en la base de datos).
*   En el menú superior, verás la opción **"Admin"**.

### 2. Gestión de Encuestas
*   **Crear Nueva Encuesta**:
    *   Ve a `Admin > Crear Encuesta`.
    *   Ingresa un **Título** descriptivo y una **Descripción** detallada.
    *   Define la **Fecha de Inicio** y **Fin**.
    *   Agrega las **Opciones de Respuesta** (mínimo 2).
    *   Haz clic en "Crear". La encuesta aparecerá inmediatamente en el listado público.
*   **Editar/Cerrar**: (Funcionalidad disponible en futuras versiones).

### 3. Reportes y Exportación
*   Ve a `Admin > Reportes`.
*   Verás una tabla con todas las encuestas del sistema.
*   **Exportar Data**:
    *   Haz clic en el botón "Exportar CSV" junto a una encuesta.
    *   Se descargará un archivo `.csv` con el desglose de votos, ideal para análisis en Excel.

---

## ❓ Preguntas Frecuentes

**¿Es mi voto anónimo?**
Sí, el sistema registra que participaste para evitar dobles votos, pero tu elección se maneja con privacidad.

**¿Puedo cambiar mi voto?**
No, una vez emitido el voto, es definitivo para garantizar la integridad del proceso.

**¿Qué hago si encuentro un error?**
Contacta al soporte técnico o reporta el problema al administrador del sistema.
