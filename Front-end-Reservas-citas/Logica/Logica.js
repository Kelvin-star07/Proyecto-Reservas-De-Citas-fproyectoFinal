const urlLogin = "https://localhost:7067/api/RegistroUsuario/Login-usuario";
const urlRegistro = "https://localhost:7067/api/RegistroUsuario/Registro-usuario";
const urlCreateConfig = "https://localhost:7067/api/ConfiguracionReserva/Crear-configuracion";
const urlUpdateConfig = "https://localhost:7067/api/ConfiguracionReserva/Modificar-configuracion"
const urlGetConfig = "https://localhost:7067/api/ConfiguracionReserva/Obtener-configuracion"
const urlReservarcita = "https://localhost:7067/api/ReservarCita/Registra-Reserva"
const urlVerCita = "https://localhost:7067/api/ReservarCita/Ver-mi-Reserva"
const urlConsultarLog = "https://localhost:7067/api/LeerLogg/consultar-logg"
const urlAddEstacion = "https://localhost:7067/Api/Estacion/Agregar-Estacion"
const urlUpEstacion = "https://localhost:7067/Api/Estacion/Modificar-estacion"
const urlGetEstaciones = "https://localhost:7067/Api/Estacion/Obtener-estaciones"

const token = localStorage.getItem("token");





async function validarLogin() {
    const nombre = document.getElementById("NombreUser").value;
    const contraseña = document.getElementById("ContraseñaLogin").value;

    if (!nombre || !contraseña) {
        alert("Por favor, completa todos los campos");
        return;
    }

    try {
        const res = await fetch(urlLogin, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ nombre, contraseña })
        });

        if (!res.ok) {
            const errorData = await res.json().catch(() => ({}));
            throw new Error(errorData.message || "Usuario o contraseña incorrecta");
        }

        const data = await res.json();

        localStorage.setItem("token", data.token);
        localStorage.setItem("usuario", JSON.stringify(data.usuario));


        const rol = data.usuario.rol;
        if (rol === true) {
            window.location.href = "PanelAdmin.html";
            alert("Usuario registrado correctamente")
        } else if (rol === false) {
            window.location.href = "PanelUsuario.html";
            alert("Usuario registrado correctamente")
        } else {
            alert("Usuario no válido");
        }

    } catch (error) {
        alert(error.message);
    }
}




async function registroUsuario() {
    const usuario = {
        Nombre: document.getElementById("NombreUser").value,
        Apellido: document.getElementById("ApellidoUsuario").value,
        Edad: parseInt(document.getElementById("EdadUsuario").value),
        Cedula: document.getElementById("CedulaUsuario").value,
        Correo: document.getElementById("CorreoUsuario").value,
        Contraseña: document.getElementById("ContraseñaRegistro").value,
        Sexo: document.getElementById("SexoUsuario").value,
        Dia: parseInt(document.getElementById("DiaNacimiento").value),
        Mes: parseInt(document.getElementById("MesNacimiento").value),
        Año: parseInt(document.getElementById("AnoNacimiento").value)
    };


    for (const key in usuario) {
        if (!usuario[key] && usuario[key] !== 0) {
            alert(`El campo ${key} es obligatorio`);
            return;
        }
    }

    try {
        const res = await fetch(urlRegistro, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(usuario)
        });

        if (!res.ok) {
            const errorData = await res.json().catch(() => ({}));
            throw new Error(errorData.message || "Error al registrar usuario");
        }

        const data = await res.json();
        alert(data.message);

        localStorage.setItem("token", data.token);
        window.location.href = "PanelUsuario.html";

        if (res.ok) {
            alert("Usuario registrado correctamente")
        }

    } catch (error) {
        alert(error.message);
    }
}


function init() {
    const btnLogin = document.getElementById("BtnLogin");
    const btnRegistro = document.getElementById("BtnRegistro");

    if (btnLogin) btnLogin.addEventListener("click", validarLogin);
    if (btnRegistro) btnRegistro.addEventListener("click", registroUsuario);
}

document.addEventListener("DOMContentLoaded", () => {
    const btnLogin = document.getElementById("BtnLogin");
    if (btnLogin) btnLogin.addEventListener("click", validarLogin);
});



document.addEventListener("DOMContentLoaded", init);

document.addEventListener("DOMContentLoaded", () => {

    const usuario = JSON.parse(localStorage.getItem("usuario"));

    if (usuario && usuario.nombre) {
        const h2Bienvenida = document.querySelector(".BienvenidaAdmin");
        h2Bienvenida.textContent = `Bienvenido sr.${usuario.nombre}`;
    }
});


function NOfuncional() {
    alert("Este boton no tiene funcionalidad");

}

//Logica para las funcionalidades del admin
async function crearConfiguracion() {
    try {

        const fechaInput = document.getElementById("fechaConfiguracion").value;
        const turno = document.getElementById("turnoConfiguracion").value;
        const horaInicio = document.getElementById("horaInicio").value;
        const horaFin = document.getElementById("horaFin").value;
        const duracion = parseInt(document.getElementById("duracionCittas").value);
        const estaciones = parseInt(document.getElementById("estacionesConfig").value);


        if (!fechaInput || !turno || !horaInicio || !horaFin || !duracion || !estaciones) {
            alert("Por favor, completa todos los campos");
            return;
        }


        const fecha = fechaInput.includes('/')
            ? fechaInput.split('/').reverse().join('-')
            : fechaInput;


        const config = {
            Fecha: fecha,
            Turno: turno,
            HoraInicio: horaInicio,
            HoraFin: horaFin,
            DuracionCitas: duracion,
            CantidadEstaciones: estaciones
        };


        const token = localStorage.getItem("token");
        const res = await fetch(urlCreateConfig, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + token
            },
            body: JSON.stringify(config)
        });


        const text = await res.text();
        if (!res.ok) throw new Error(text);

        alert("Configuración creada correctamente");

        document.getElementById("fechaConfiguracion").value = "";
        document.getElementById("turnoConfiguracion").value = "";
        document.getElementById("horaInicio").value = "";
        document.getElementById("horaFin").value = "";
        document.getElementById("duracionCittas").value = "";
        document.getElementById("estacionesConfig").value = "";

    } catch (error) {
        alert("Error al crear configuración: " + error.message);
    }
}


// Modificar configuración
async function modificarConfiguracion() {
    const config = getValoresFormulario();
    if (!config) return;

    try {
        const token = localStorage.getItem("token");
        const res = await fetch(urlUpdateConfig, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(config)
        });

        const text = await res.text();
        if (!res.ok) throw new Error(text);

        alert("Configuración modificada correctamente");
    } catch (err) {
        alert("Error al modificar configuración: " + err.message);
    }
}





document.addEventListener("DOMContentLoaded", () => {
    const btnBuscar = document.getElementById("btnBuscar");
    if (btnBuscar) btnBuscar.addEventListener("click", obtenerConfiguracion);
});

async function obtenerConfiguracion() {
    const fechaInput = document.getElementById("fechaBuscar").value;
    const turno = document.getElementById("turnoBuscar").value;

    console.log("Buscar configuración:", { fechaInput, turno, token });

    if (!fechaInput || !turno) {
        alert("Introduce fecha y turno para buscar la configuración");
        return;
    }

    const fecha = fechaInput.includes('/')
        ? fechaInput.split('/').reverse().join('-')
        : fechaInput;

    try {
        const res = await fetch(`${urlGetConfig}?fecha=${fecha}&turno=${turno}`, {
            method: "GET",
            headers: { "Authorization": `Bearer ${token}` }
        });

        console.log("Respuesta fetch:", res);

        if (!res.ok) {
            const texto = await res.text();
            throw new Error(texto || "Error desconocido al obtener la configuración");
        }

        const data = await res.json();
        console.log("Datos recibidos:", data);

        document.getElementById("fechaConfig").value = data.Fecha || data.fecha;
        document.getElementById("turnoConfig").value = data.Turno || data.turno;
        document.getElementById("horaInicio").value = data.HoraInicio || data.horaInicio;
        document.getElementById("horaFin").value = data.HoraFin || data.horaFin;
        document.getElementById("duracionCitas").value = data.DuracionCitas || data.duracionCitas;
        document.getElementById("cantidadEstaciones").value = data.cantidadEstaciones;

    } catch (err) {
        console.error(err);
        alert("Error al obtener configuración: " + err.message);
    }
}




function getValoresFormulario() {
    const fechaInput = document.getElementById("fechaConfiguracion").value;
    const turno = document.getElementById("turnoConfiguracion").value;
    const horaInicio = document.getElementById("horaInicio").value;
    const horaFin = document.getElementById("horaFin").value;
    const duracionCitas = document.getElementById("duracionCittas").value;
    const estaciones = document.getElementById("estacionesConfig").value;

    if (!fechaInput || !turno || !horaInicio || !horaFin || !duracionCitas || !estaciones) {
        alert("Todos los campos son obligatorios");
        return null;
    }


    const fecha = fechaInput.includes('/')
        ? fechaInput.split('/').reverse().join('-')
        : fechaInput;

    return {
        Fecha: fecha,
        Turno: turno,
        HoraInicio: horaInicio,
        HoraFin: horaFin,
        DuracionCitas: Number(duracionCitas),
        CantidadEstaciones: Number(estaciones)
    };
}


function formatFecha(fecha) {
    return fecha.includes('/') ? fecha.split('/').reverse().join('-') : fecha;
}








function limpiarFormulario() {
    document.getElementById("fechaConfiguracion").value = "";
    document.getElementById("turnoConfiguracion").value = "";
    document.getElementById("horaInicio").value = "";
    document.getElementById("horaFin").value = "";
    document.getElementById("duracionCittas").value = "";
    document.getElementById("estacionesConfig").value = "";
}


function initConfiguracion() {
    const btnConfirmar = document.getElementById("ConfirmarConfig");
    const btnCancelar = document.getElementById("ConcelarConfig");

    if (btnConfirmar) {
        if (window.location.href.includes("CrearConfiguracion.html")) {
            btnConfirmar.addEventListener("click", crearConfiguracion);
        } else if (window.location.href.includes("ModificarConfig.html")) {
            btnConfirmar.addEventListener("click", modificarConfiguracion);
        }
    }

    if (btnCancelar) btnCancelar.addEventListener("click", limpiarFormulario);


    if (window.location.href.includes("verConfiguracion.html")) {
        obtenerConfiguracion();
    }
}

document.addEventListener("DOMContentLoaded", initConfiguracion);


//Logica de reserva de citas
document.addEventListener("DOMContentLoaded", () => {
    const fechaInput = document.getElementById("fechaSeleccionada");
    const turnoInput = document.getElementById("turnoSeleccionado");
    const btnCargarSlots = document.getElementById("btnCargarSlots");
    const slotsContainer = document.getElementById("slotsContainer");
    const btnReservar = document.getElementById("btonReservar");
    const btnCancelar = document.getElementById("btonCancelar");
    const token = localStorage.getItem("token");

    if (!token) {
        alert("No se encontró token de autorización. Por favor, inicie sesión.");
        return;
    }

    let slotSeleccionado = null;
    let slots = [];

    function formatearFecha(fechaRaw) {
        return fechaRaw.includes("/") ? fechaRaw.split("/").reverse().join("-") : fechaRaw;
    }

    async function cargarSlots(fecha, turno) {
        try {
            const response = await fetch(`https://localhost:7067/api/GeneracionSlot/obtener-slots?fecha=${fecha}&turno=${turno}`, {
                method: "GET",
                headers: { "Authorization": `Bearer ${token}` }
            });

            if (!response.ok) throw new Error("No se pudo cargar los slots");

            slots = await response.json();
            renderSlots(slots);
        } catch (error) {
            alert("Error al cargar los horarios: " + error.message);
        }
    }

    function renderSlots(slots) {
        slotsContainer.innerHTML = "";
        slotSeleccionado = null;

        slots.forEach(slot => {
            const btn = document.createElement("button");
            btn.type = "button";
            btn.textContent = `${slot.horaInicio} - ${slot.horaFin} (${slot.cupoDisponible})`;
            btn.classList.add("slot-btn");

            if (slot.cupoDisponible <= 0) {
                btn.disabled = true;
                btn.classList.add("lleno");
            }

            btn.addEventListener("click", () => {

                document.querySelectorAll(".slot-btn").forEach(b => b.classList.remove("seleccionado"));
                btn.classList.add("seleccionado");
                slotSeleccionado = slot.horaInicio.slice(0, 5);
            });

            slotsContainer.appendChild(btn);
        });
    }


    async function reservarCita(reserva) {
        try {
            const res = await fetch(urlReservarcita, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify(reserva)
            });

            if (res.ok) {
                alert("Cita reservada con éxito");
                slotsContainer.innerHTML = "";
                slotSeleccionado = null;
            } else {
                const errorData = await res.json();
                alert(errorData.message || "Error al reservar cita");
            }
        } catch (err) {
            alert("Error de conexión: " + err.message);
        }
    }

    function cancelarSeleccion() {
        slotsContainer.innerHTML = "";
        slotSeleccionado = null;
    }

    // Eventos
    btnCargarSlots.addEventListener("click", () => {
        const fecha = formatearFecha(fechaInput.value);
        const turno = turnoInput.value;

        if (!fecha || !turno) {
            alert("Seleccione una fecha y un turno");
            return;
        }

        cargarSlots(fecha, turno);
    });

    btnReservar.addEventListener("click", () => {
        if (!slotSeleccionado) {
            alert("Debe seleccionar un horario");
            return;
        }

        const reserva = {
            fecha: formatearFecha(fechaInput.value),
            turno: turnoInput.value,
            hora: slotSeleccionado
        };

        reservarCita(reserva);
    });

    btnCancelar.addEventListener("click", cancelarSeleccion);
});


//consultar citas
async function consultarCitas() {
    const token = localStorage.getItem("token");
    if (!token) {
        alert("No se encontró token de autorización. Por favor, inicie sesión.");
        return;
    }

    const reservaContainer = document.getElementById("reservaContainer");
    if (!reservaContainer) return;

    try {
        const response = await fetch(urlVerCita, {
            method: "GET",
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!response.ok) throw new Error("No se pudo obtener la reserva");

        const reserva = await response.json();
        reservaContainer.innerHTML = `
            <p>Fecha: ${reserva.fecha}</p>
            <p>Hora: ${reserva.hora}</p>
            <p>Turno: ${reserva.turno}</p>
        `;
    } catch (err) {
        alert("Error al consultar la cita: " + err.message);
    }
}


document.addEventListener("DOMContentLoaded", () => {
    consultarCitas();
});


//Consultar log
document.addEventListener("DOMContentLoaded", () => {
    const container = document.getElementById("ContainerLoggActividades");
    const token = localStorage.getItem("token");

    if (!token) {
        alert("No se encontró token de autorización. Por favor, inicie sesión.");
        return;
    }


    async function mostrarLogs() {
        try {
            const response = await fetch(urlConsultarLog, {
                method: "GET",
                headers: {
                    "Authorization": `Bearer ${token}`,
                    "Accept": "text/plain"
                }
            });

            if (!response.ok) throw new Error("Error al cargar los logs");

            const logs = await response.text();


            container.innerHTML = `<pre>${logs}</pre>`;
        } catch (err) {
            container.innerHTML = `<p style="color:red;">${err.message}</p>`;
        }
    }


    mostrarLogs();
});











// Agregar estación
async function agregarEstacion() {
    const nombre = document.getElementById("nombreAgregar").value;

    try {
        const response = await fetch(urlAddEstacion, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({ nombre })
        });

        if (!response.ok) throw new Error(`Error HTTP ${response.status}`);

        const data = await response.text();
        document.getElementById("resultadoAgregar").innerText = data;
    } catch (error) {
        document.getElementById("resultadoAgregar").innerText = "Error: " + error;
    }
}

// Modificar estación
async function modificarEstacion() {
    const id = document.getElementById("idModificar").value;
    const nombre = document.getElementById("nombreModificar").value;

    try {
        const response = await fetch(urlUpEstacion, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({ id: parseInt(id), nombre })
        });

        if (!response.ok) throw new Error(`Error HTTP ${response.status}`);

        const data = await response.text();
        document.getElementById("resultadoModificar").innerText = data;
    } catch (error) {
        document.getElementById("resultadoModificar").innerText = "Error: " + error;
    }
}

// Ver estaciones
async function verEstaciones() {
    try {
        const response = await fetch(urlGetEstaciones, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (!response.ok) throw new Error(`Error HTTP ${response.status}`);

        let data = [];
        try {
            data = await response.json();
        } catch {
            data = [];
        }

        const lista = document.getElementById("listaEstaciones");
        lista.innerHTML = "";

        if (data.length === 0) {
            lista.textContent = "No hay estaciones registradas.";
            return;
        }

        data.forEach(estacion => {
            const li = document.createElement("li");
            li.textContent = `ID: ${estacion.id}, Nombre: ${estacion.nombre}`;
            lista.appendChild(li);
        });
    } catch (error) {
        alert("Error: " + error);
    }
}

// Conectar botones al DOM
document.addEventListener("DOMContentLoaded", () => {
    const btnAgregar = document.getElementById("btnAgregar");
    if (btnAgregar) btnAgregar.addEventListener("click", agregarEstacion);

    const btnModificar = document.getElementById("btnModificar");
    if (btnModificar) btnModificar.addEventListener("click", modificarEstacion);

    const btnVer = document.getElementById("btnVer");
    if (btnVer) btnVer.addEventListener("click", verEstaciones);
});



document.getElementById("btnBuscar").addEventListener("click", function () {
    document.getElementById("cuadroResultados").style.display = "block";
});

