import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [productos, setProductos] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    const fetchProductos = async () => {
      try {
        const response = await fetch('http://localhost:5284/api/productos')
        if (!response.ok) throw new Error('Error al obtener productos')
        const data = await response.json()
        setProductos(data)
      } catch (err) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }

    fetchProductos()
  }, [])

  return (
    <div className="app-container">
      <h1>Vista previa de productos</h1>

      {loading && <p>Cargando productos...</p>}
      {error && <p style={{ color: 'red' }}>Error: {error}</p>}

      <div className="productos">
        {productos.map((producto) => (
          <div key={producto.productoID} className="producto-card">
            <h2>{producto.nombrePrducto}</h2>
            <p><strong>Descripción:</strong> {producto.descripcion}</p>
            <p><strong>Precio:</strong> ${producto.precio}</p>

            {producto.subProductos?.length > 0 && (
              <>
                <h4>Subproductos:</h4>
                <ul>
                  {producto.subProductos.map((sub) => (
                    <li key={sub.subProductoID}>
                      <strong>{sub.nombreSubPrducto}</strong> - {sub.descripcionSubProducto} (${sub.precioSubProducto})
                    </li>
                  ))}
                </ul>
              </>
            )}
          </div>
        ))}
      </div>
    </div>
  )
}

export default App
