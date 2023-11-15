# ADASALES2023
SISTEMA DE GESTIÓN DE VENTAS HECHO CON .NET LA PRUEBA DE INGRESO EN ADA SA

## PREPARAR DATABASE
NOTA: La cadena de conexión está en appsettings.json  en SALES.API ***NO ALTERAR LA CONEXIÓN A AZURE PARA LAS IMÁGENES**

1 - add-migration  NOMBREMIGRACION

2- update-database

para agregar las entidades y los registros iniciales. 

## CUENTAS DE PRUEBA
Se crean dos cuentas por defecto  aunque también es posible hacer registro posterior, la cuenta admin@ será el unico administrador.

USUARIO (perfil de usuario no anonimo)
ema@gmail.com

ADMIN (Perfil de administrador)
admin@gmail.com

** CONTRASEÑA PARA AMBAS CUENTAS ES:   123@Ingreso

* El usuario sin registro puede ver los productos, pero para agregar a carrito de compra se le solicita inicio de sesion o registro.

* Las imágenes de los productos se almacenan en una cuenta de almacenamiento de azure y en la db solo se almacena la ruta generada para cada imagen de producto.

