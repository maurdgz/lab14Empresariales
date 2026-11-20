# Laboratorio 14 Publicación y despliegue

API de productos sobre la solución original `Lab14_MauricioRdgz`.
Se conserva .NET 10, compatible con el requisito del laboratorio de .NET 8 o superior.

## Arquitectura hexagonal

- Domain: Producto y el puerto IProductoRepository; no depende de otras capas.
- Application: comandos y consultas con MediatR; depende de Domain.
- Infrastructure: adaptador de repositorio en memoria; depende de Domain.
- Lab14_MauricioRdgz: API, controladores, Swagger y composición de dependencias.

El controlador llama a MediatR y los casos de uso llaman al puerto del dominio.
Se mantiene el estilo de ImportNet, sin agregar capas ni paquetes innecesarios.
Los productos son datos ficticios guardados en memoria: se reinician al detener o desplegar la API.
Los endpoints CRUD están abiertos para la demostración del laboratorio.

## Ejecutar en Rider

Abre la solución existente y ejecuta el perfil Lab14_MauricioRdgz.
Swagger: http://localhost:5140/swagger

```powershell
dotnet run --project Lab14_MauricioRdgz
```

## Publicar en una carpeta

En Rider, Publish to Folder usa el perfil FolderProfile, configuración Release.
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
Desactivar Auto-Deploy: el workflow dispara el despliegue solo después de las pruebas.
Guardar el Deploy Hook de Render como secret RENDER_DEPLOY_HOOK en GitHub Actions.
Nunca escribir ese enlace dentro del código o de las capturas.
La URL pública final tendrá /swagger y /health disponibles.

Sin el secret, Actions publica el artifact pero el paso de despliegue informa que falta la configuración.
