# PLANAZO - Sistema de Gestión de Eventos y Reservas

## 1. Descripción del proyecto

PLANAZO es una aplicación de consola desarrollada en C# con .NET que permite administrar eventos y gestionar reservas de entradas. El sistema permite consultar los eventos disponibles, conocer sus precios, reservar entradas, consultar las reservas realizadas y cancelar reservas.

El proyecto fue desarrollado como parte de una actividad académica de Programación Orientada a Objetos (POO).

## 2. Objetivo

Desarrollar una aplicación que permita gestionar eventos y reservas de manera organizada, aplicando los principios fundamentales de la Programación Orientada a Objetos en C#.

## 3. Tipos de eventos

El sistema cuenta con los siguientes tipos de eventos:

- **Cine:** conserva el precio base de la entrada.
- **Concierto:** aumenta el precio base en un 15 %.
- **Taller:** aplica un descuento del 10 % sobre el precio base.
- **Evento deportivo:** aplica un incremento del 20 % sobre el precio base.

## 4. Funcionalidades

La aplicación permite:

1. Ver los eventos disponibles y sus cupos.
2. Consultar la información de un evento.
3. Reservar entradas para un evento.
4. Consultar las reservas realizadas.
5. Cancelar una reserva y recuperar los cupos.
6. Consultar el resumen de reservas activas.
7. Consultar las reservas de un usuario.
8. Validar que la cantidad de entradas sea válida y que existan cupos disponibles.
9. Evitar la cancelación repetida de una misma reserva.

## 5. Programación Orientada a Objetos

En el desarrollo se aplican los siguientes conceptos:

- **Abstracción:** la clase `Evento` es abstracta porque representa las características comunes de los eventos, pero no define un tipo concreto de evento. Además, declara el método abstracto `CalcularPrecioFinal()`, que cada tipo de evento implementa según sus propias reglas de precio.
- **Encapsulamiento:** los cupos disponibles se modifican mediante métodos que validan las operaciones.
- **Herencia:** las clases `Cine`, `Concierto`, `Taller` y `EventoDeportivo` heredan de `Evento`.
- **Polimorfismo:** cada tipo de evento implementa su propia versión del método `CalcularPrecioFinal()`.
- **Constructores:** permiten inicializar los objetos con sus datos.
- **Asociación entre clases:** una reserva relaciona un usuario con un evento.
- **Colecciones:** se utilizan listas para almacenar eventos, usuarios y reservas.
- **Excepciones:** se utilizan para controlar operaciones no permitidas, como reservar más entradas de las disponibles o cancelar una reserva que ya fue cancelada.

## 6. Tecnologías utilizadas

- Lenguaje: C#.
- Plataforma: .NET.
- Entorno de desarrollo: Visual Studio Code.
- Tipo de aplicación: consola.

## 7. Estructura del proyecto

- `Program.cs`: contiene el menú principal y la interacción con el usuario.
- `Modelos/`: contiene las clases que representan eventos, usuarios y reservas.
- `Servicios/`: contiene la lógica para gestionar eventos y reservas.
- `Capturas/`: contiene las evidencias del funcionamiento de la aplicación.

## 8. Cómo ejecutar el proyecto

1. Instalar el SDK de .NET.
2. Abrir la carpeta del proyecto en Visual Studio Code.
3. Abrir una terminal dentro de la carpeta principal.
4. Ejecutar el siguiente comando:

```bash
dotnet run
```

5. Seleccionar una opción del menú y seguir las instrucciones de la aplicación.

## 9. Evidencias

En la carpeta `Capturas` se encuentran las imágenes que muestran el funcionamiento de las diferentes opciones del sistema.

## 10. Autor

Proyecto académico desarrollado por Rosaura León.

## 11. Conclusión

El desarrollo de PLANAZO permitió aplicar los conceptos de Programación Orientada a Objetos mediante un sistema de gestión de eventos y reservas. La aplicación incorpora validaciones para controlar las reservas y cancelaciones, calcula los precios según el tipo de evento y mantiene un resumen de las reservas activas.
