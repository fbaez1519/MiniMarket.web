// ==============================================
// CONFIGURACIÓN
// ==============================================
const API_URL = 'http://localhost:5285'; // Cambia el puerto si el tuyo es otro
let authToken = '';

// ==============================================
// LOGIN
// ==============================================
document.getElementById('loginForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    try {
        const response = await fetch(`${API_URL}/api/Auth/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        });

        if (!response.ok) throw new Error('Credenciales inválidas');

        const data = await response.json();
        authToken = data.token;

        // Mostrar Dashboard
        document.getElementById('loginSection').style.display = 'none';
        document.getElementById('dashboardSection').style.display = 'block';
        document.getElementById('userName').innerHTML = `Bienvenido, <strong>${data.usuario}</strong>`;

        // Cargar datos
        cargarResumen();
        cargarProductos();

    } catch (error) {
        document.getElementById('loginError').style.display = 'block';
        setTimeout(() => document.getElementById('loginError').style.display = 'none', 3000);
    }
});

// ==============================================
// CERRAR SESIÓN
// ==============================================
document.getElementById('logoutBtn').addEventListener('click', () => {
    authToken = '';
    document.getElementById('dashboardSection').style.display = 'none';
    document.getElementById('loginSection').style.display = 'flex';
    document.getElementById('loginForm').reset();
});

// ==============================================
// FUNCIONES DEL DASHBOARD
// ==============================================
async function cargarResumen() {
    try {
        // Productos (para contar total y stock bajo)
        const response = await fetch(`${API_URL}/api/Productos`);
        const productos = await response.json();

        document.getElementById('totalProductos').textContent = productos.length;
        document.getElementById('stockBajo').textContent = productos.filter(p => p.stock < 5).length;

        // Usuarios (para contar activos)
        const userResponse = await fetch(`${API_URL}/api/Usuarios`);
        const usuarios = await userResponse.json();
        document.getElementById('usuariosActivos').textContent = usuarios.length;

        // Ventas (para simular ventas hoy)
        const ventaResponse = await fetch(`${API_URL}/api/Ventas`);
        const ventas = await ventaResponse.json();
        const ventasHoy = ventas.filter(v => new Date(v.fecha).toDateString() === new Date().toDateString());
        const totalVentasHoy = ventasHoy.reduce((sum, v) => sum + v.total, 0);
        document.getElementById('ventasHoy').textContent = `$${totalVentasHoy.toFixed(2)}`;

    } catch (error) {
        console.error('Error al cargar el resumen:', error);
    }
}

async function cargarProductos() {
    try {
        const response = await fetch(`${API_URL}/api/Productos`);
        const productos = await response.json();

        const tbody = document.getElementById('productosTableBody');
        tbody.innerHTML = '';

        productos.forEach(p => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${p.id}</td>
                <td>${p.nombre}</td>
                <td>$${p.precio.toFixed(2)}</td>
                <td>${p.stock}</td>
                <td>${p.categoria}</td>
            `;
            tbody.appendChild(row);
        });

    } catch (error) {
        console.error('Error al cargar productos:', error);
    }
}

// ==============================================
// RECARGAR DATOS CADA 30 SEGUNDOS (Opcional)
// ==============================================
if (document.getElementById('dashboardSection').style.display !== 'none') {
    setInterval(() => {
        cargarResumen();
        cargarProductos();
    }, 30000);
}