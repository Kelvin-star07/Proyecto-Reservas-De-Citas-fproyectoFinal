# Responsabilidades del Proyecto

## Desarrollo Individual
Este proyecto fue desarrollado de manera **individual** por Kelvin Díaz Ramírez. Todo el backend y frontend fue implementado desde cero, incluyendo la planificación, diseño y codificación de cada funcionalidad.

---

## Proceso de desarrollo
1. **Análisis y planificación**
   - Se estudió cuidadosamente el documento de requerimientos del proyecto.
   - Se elaboró un **boceto en Miro** para visualizar cómo el usuario interactuaría con el sistema:
     - Pantallas
     - Formularios
     - Botones, iconos y logo
     - Flujo de interacción entre usuario y sistema

2. **Diseño de la interacción**
   - Pantalla de inicio: logo pequeño, campos de usuario y contraseña, botones de **Ingresar** y **Registrarte**.
   - Panel del administrador:
     - Crear configuración
     - Ver configuración
     - Modificar configuración
     - Ver actividades (acceso de solo lectura al log en archivo TXT)
     - Gestionar estaciones (agregar, modificar y ver estaciones, donde cada estación es solo un nombre)
   - Panel del usuario:
     - Reservar citas (formulario de reserva)
     - Ver última cita realizada

3. **Implementación por funcionalidades**
   - Primero se desarrolló **registro y login**, para permitir la autenticación de usuarios.
   - Luego se implementaron las acciones del **administrador**, ya que las funcionalidades del usuario dependían de la configuración establecida por el admin.
   - Finalmente, se implementaron las funcionalidades del **usuario**, incluyendo la reserva y visualización de citas.

---

## Herramientas y tecnologías utilizadas
- **Backend:** C# con ASP.NET Core Web API, Entity Framework Core, SQL Server, JWT para autenticación y seguimiento de usuarios.
- **Frontend:** HTML, CSS, JavaScript.
- **Otros recursos:** Archivos de log en TXT para registro de actividades, diagramas y bocetos en Miro para planificación.

---

## Observaciones finales
- Todo el sistema fue desarrollado por un único autor, quien se encargó de **todas las etapas del proyecto**, desde el análisis inicial hasta el despliegue.
- El desarrollo incluyó planificación visual, codificación, pruebas y ajustes de funcionalidad, asegurando la correcta interacción entre el backend y el frontend.
- Se priorizó un flujo lógico de desarrollo por funcionalidades, respetando la dependencia entre acciones de administrador y usuario.
