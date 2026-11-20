# Laboratorio 14 PublicaciÃ³n y despliegue

API de productos sobre la soluciÃ³n original `Lab14_MauricioRdgz`.
Se conserva .NET 10, compatible con el requisito del laboratorio de .NET 8 o superior.

## Arquitectura hexagonal

- Domain: Producto y el puerto IProductoRepository; no depende de otras capas.
- Application: comandos y consultas con MediatR; depende de Domain.
- Infrastructure: adaptador de repositorio en memoria; depende de Domain.
- Lab14_MauricioRdgz: API, controladores, Swagger y composiciÃ³n de dependencias.

El controlador llama a MediatR y los casos de uso llaman al puerto del dominio.
Se mantiene el estilo de ImportNet, sin agregar capas ni paquetes innecesarios.
Los productos son datos ficticios guardados en memoria: se reinician al detener o desplegar la API.
Los endpoints CRUD estÃ¡n abiertos para la demostraciÃ³n del laboratorio.

## Ejecutar en Rider

Abre la soluciÃ³n existente y ejecuta el perfil Lab14_MauricioRdgz.
Swagger: http://localhost:5140/swagger

```powershell
dotnet run --project Lab14_MauricioRdgz
```

## Publicar en una carpeta

En Rider, Publish to Folder usa el perfil FolderProfile, configuraciÃ³n Release.
El mismo perfil puede ejecutarse desde la consola:

```powershell
dotnet publish Lab14_MauricioRdgz -c Release /p:PublishProfile=FolderProfile
dotnet published/Lab14_MauricioRdgz.dll --urls http://localhost:5140
python tests/smoke_api.py http://localhost:5140
```

## GitHub Actions y Render

El workflow restaura, compila, publica, prueba la API y guarda los archivos como artifact.
Dockerfile permite desplegar .NET en Render, que no ofrece un runtime nativo para .NET.
Crear un Web Service Docker con este repositorio, plan Free, puerto 10000 y health check /health.
En Render, seleccionar Auto-Deploy: After CI Checks Pass.
Render espera que GitHub Actions termine correctamente antes de desplegar cada commit.
No se necesitan Deploy Hooks ni secretos adicionales.
La URL pública final tendrá /swagger y /health disponibles.
