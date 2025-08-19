
# Sistema de Reservas de Citas

## Descripción
El Sistema de Reservas de Citas es una aplicación web diseñada para gestionar de manera eficiente la asignación de citas en instituciones académicas, centros de servicios o entidades públicas. Permite que los usuarios puedan reservar citas según la disponibilidad de recursos (estaciones de atención), y al mismo tiempo ofrece al administrador herramientas para configurar horarios, turnos, duración de citas y cantidad de estaciones disponibles.

El sistema garantiza el control de concurrencia, evita sobrecupos, y notifica a los usuarios mediante correo electrónico al confirmar una reserva.

---

## Objetivos
- Proveer una interfaz web para reservar citas de manera sencilla.
- Permitir al administrador definir fechas habilitadas, turnos, duración de citas y cantidad de estaciones.
- Generar automáticamente horarios con capacidad limitada según las estaciones.
- Bloquear horarios que hayan alcanzado su límite de capacidad.
- Enviar correos de confirmación a los usuarios.
- Registrar logs centralizados de todas las acciones relevantes del sistema (reservas, cancelaciones, login, errores y cambios de configuración).

---

## Arquitectura
El sistema está construido utilizando la **Onion Architecture**, separando la aplicación en cuatro capas bien definidas:

1. **Dominio**: Contiene las entidades, agregados y reglas de negocio centrales.
2. **Aplicación**: Implementa casos de uso, coordina la lógica de negocio y expone servicios como envío de correos y manejo de logs.
3. **Infraestructura**: Contiene la implementación concreta de acceso a datos, persistencia y servicios externos que interactúan con la aplicación.
4. **Presentación (API)**: Contiene los controladores que exponen la API REST, consumiendo los servicios de la capa de Aplicación para procesar solicitudes de usuarios y administradores.

Esta arquitectura asegura modularidad y desacoplamiento, facilitando mantenimiento, escalabilidad y pruebas unitarias.

---

## Tecnologías utilizadas

**Backend:**
- Lenguaje: C#
- Plataforma: ASP.NET Core Web API
- ORM: Entity Framework Core
- Base de datos: SQL Server
- Autenticación y seguridad: JWT
- Patrón de diseño Singleton: para manejo centralizado de logs
- Patrón Repositorio: para desacoplar la lógica de negocio del acceso a datos

**Frontend:**
- HTML, CSS, JavaScript

---

## Patrones de diseño y justificación

1. **Singleton (Logs)**: Se utilizó para garantizar que solo exista una instancia del servicio de logs, evitando inconsistencias y asegurando que todas las acciones relevantes del sistema sean registradas de forma centralizada y confiable.

2. **Repositorio (Repository Pattern)**: Desacopla la capa de acceso a datos del resto de la aplicación. Permite cambiar la implementación del almacenamiento sin afectar la lógica de negocio y facilita la realización de pruebas unitarias.

---

## Funcionalidades principales

**Para el administrador:**
- Configuración de fechas habilitadas para reservas.
- Definición de turnos (matutino/vespertino) y rangos de horas.
- Establecimiento de duración de cada cita.
- Determinación de la cantidad de estaciones disponibles por día y turno.

**Para el usuario:**
- Registro y login seguro.
- Visualización de fechas y turnos disponibles.
- Visualización de horarios con cupos disponibles.
- Reservar citas respetando la capacidad máxima por horario.
- Recepción de correo electrónico de confirmación con fecha, hora, estación y estado de la cita.

**Validaciones y restricciones:**
- No se permiten reservas fuera de las fechas y horarios configurados.
- Cada usuario puede tener solo una cita activa por día.
- El sistema maneja concurrencia para evitar sobrecupos.

---

## Ejemplo de flujo de prueba
1. Configurar el sistema para un lunes, turno matutino, horario de 8:00 a 12:00, citas de 10 minutos y 4 estaciones.
2. Validar que se generen correctamente los slots de tiempo.
3. Reservar 4 citas a las 8:00 AM y verificar que no se permita la quinta reserva.
4. Reservar en otro horario y validar que la reserva sea exitosa.
5. Confirmar que el usuario recibe un correo electrónico con la información de la cita.
6. Verificar que todas las acciones se registren correctamente en el log.

---

## Instrucciones de instalación y despliegue

1. Clonar el repositorio:
```bash
git clone https://github.com/tu_usuario/nombre_repositorio.git


2.Configurar la base de datos en SQL Server y actualizar el `appsettings.json` con la cadena de conexión.

3. Restaurar paquetes de NuGet:
```bash
dotnet restore

4.Ejecutar la aplicación desde Visual Studio o usando CLI:

5.Abrir un navegador y acceder a la API en la URL indicada (por defecto https://localhost:5001) para interactuar con la aplicación.

6.Desde el frontend (HTML/JS/CSS), consumir los endpoints de la API para realizar reservas y administrar la configuración según el rol de usuario.
