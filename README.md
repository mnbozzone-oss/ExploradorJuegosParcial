# Explorador de juegos

App que hice para el primer parcial de Desarrollo de aplicaciones móviles 2.

Permite cargar una lista de juegos gratuitos y elegir uno para ver su nombre, género, plataforma, descripción y página web.

## Qué usé

- .NET MAUI con .NET 9.
- C# y XAML.
- MVVM con propiedades y comandos.
- HttpClient para consultar la API.
- Shell para navegar al detalle.

## Cómo está organizado

- Models: los datos de cada juego.
- Services: la consulta a la API.
- ViewModels: la carga, los mensajes y la navegación.
- Views: la pantalla de detalle.
- MainPage.xaml: la pantalla principal.

El code-behind solo conecta las pantallas con sus ViewModels.

## Cómo ejecutarla

1. Abrir ExploradorJuegos.sln en Visual Studio 2022 con .NET MAUI y .NET 9 instalados.
2. Elegir Windows Machine y ejecutar.
3. Tocar Cargar juegos y seleccionar uno para ver el detalle.

Para cargar los juegos hace falta internet.

## De dónde salen los datos

Los datos son de [FreeToGame](https://www.freetogame.com).

API: https://www.freetogame.com/api/games

## Qué probé

- Cargar y volver a cargar sin duplicar juegos.
- Abrir el detalle, volver y elegir otro juego.
- Volver a abrir el mismo juego.
- Cargar sin internet y comprobar el mensaje de conexión.
- Consultar temporalmente un juego inexistente y comprobar el mensaje HTTP 404.
- Restaurar la dirección original y volver a cargar correctamente.

La app también incluye errores HTTP 400 y 500, tiempo de espera agotado y datos con formato incorrecto, aunque esos casos no los probé.