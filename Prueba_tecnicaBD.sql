create table Productos ( 
ProductoID SERIAL PRIMARY KEY,
NombreProducto VARCHAR( 100 ) NOT NULL,
Descripcion TEXT,
Precio DECIMAL ( 10, 2) NOT NULL
);

create table SubProductos (
SubProductoID SERIAL PRIMARY KEY,
NombreSubProducto VARCHAR (100) NOT NULL,
DescripcionSubProducto TEXT,
PrecioSubProducto DECIMAL (10, 2) NOT NULL,
ProductoID INT NOT NULL,
CONSTRAINT fk_producto
FOREIGN KEY (ProductoID) REFERENCES PRODUCTOS(ProductoID)

on delete cascade
);