# Prueba Técnica Carsales – BFF .NET + Angular

Este repositorio contiene la solución desarrollada para la prueba técnica de Carsales.  
La solución está compuesta por un **Backend For Frontend (BFF)** desarrollado en .NET y un **frontend en Angular**.

El frontend consume exclusivamente los endpoints expuestos por el BFF, el cual utiliza la API pública de Rick and Morty como fuente de datos.

La rama utilizada para la evaluación y entrega del proyecto es **master**.

---

## Descripción general de la solución

La arquitectura implementada sigue el patrón **Backend For Frontend (BFF)**.  
El objetivo principal de este enfoque es desacoplar el frontend de la API externa, centralizando en el backend:

- La comunicación con servicios externos
- La lógica de negocio
- El manejo de errores
- La configuración por entorno

El frontend **no consume directamente** la API de Rick and Morty, sino que se comunica únicamente con el BFF.  
Esto permite mayor control sobre los datos expuestos, una mejor separación de responsabilidades y una solución más mantenible.

---

## Estructura del repositorio

La solución se organiza de la siguiente manera:

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
