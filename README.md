# Prueba Técnica Carsales – BFF .NET + Angular

Este repositorio contiene la solución desarrollada para la prueba técnica de Carsales.
La solución está compuesta por un Backend For Frontend (BFF) desarrollado en .NET y un frontend en Angular.

El frontend consume exclusivamente los endpoints expuestos por el BFF, el cual utiliza la API pública de Rick and Morty como fuente de datos.
La rama utilizada para la evaluación y entrega del proyecto es master.

## Descripción general de la solución

La arquitectura implementada sigue el patrón Backend For Frontend (BFF).
El objetivo principal de este enfoque es desacoplar el frontend de la API externa, centralizando en el backend la comunicación con servicios externos, la lógica de negocio, el manejo de errores y la configuración por entorno.

El frontend no consume directamente la API de Rick and Morty, sino que se comunica únicamente con el BFF.
Esto permite mayor control sobre los datos expuestos, una mejor separación de responsabilidades y una solución más mantenible.

## Estructura del repositorio

```text
.
├── Carsales.RickAndMorty.BFF
│   ├── Clients
│   ├── Endpoints
│   ├── Middleware
│   ├── Models
│   ├── Properties
│   ├── Services
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── frontend
│   └── carsales-rickandmorty-web
│       ├── angular.json
│       ├── package.json
│       ├── package-lock.json
│       ├── proxy.conf.json
│       ├── tsconfig.json
│       ├── tsconfig.app.json
│       ├── tsconfig.spec.json
│       ├── src
│       │   ├── app
│       │   └── environments
│       └── public
│
├── .gitignore
└── README.md
```

El proyecto Carsales.RickAndMorty.BFF corresponde al Backend For Frontend desarrollado en .NET.
Este componente es responsable de consumir la API pública de Rick and Morty, procesar la información obtenida y exponer endpoints propios que son consumidos por el frontend.

### Función del BFF

- Consume la API externa de Rick and Morty.
- Adapta y transforma los datos según las necesidades del frontend.
- Centraliza la lógica de negocio.
- Maneja errores de forma controlada.
- Expone un contrato estable hacia el frontend.

### Organización interna del backend

Clients:
Cliente HTTP para consumo de la API externa.

Services:
Lógica de negocio y orquestación.

Endpoints:
Endpoints expuestos mediante Minimal APIs.

Models:
Modelos y DTOs internos.

Middleware:
Manejo centralizado de errores.

Program.cs:
Configuración de servicios y endpoints.

### Configuración del backend

Configuración mediante appsettings.json y appsettings.Development.json.

## Frontend – Angular

Proyecto Angular ubicado en frontend/carsales-rickandmorty-web.

### Funcionalidades del frontend

- Consumo de datos desde el BFF.
- Listados con paginación y filtros.
- Manejo de carga y errores.

## Cómo ejecutar el proyecto

Backend:
cd Carsales.RickAndMorty.BFF
dotnet restore
dotnet run

Frontend:
cd frontend/carsales-rickandmorty-web
npm install
ng serve
