# Prueba Técnica Carsales – BFF .NET + Angular

Este repositorio contiene la solución desarrollada para la prueba técnica de Carsales.  
El proyecto implementa un **Backend For Frontend (BFF)** en .NET y un **frontend en Angular**, utilizando la API pública de Rick and Morty como fuente de datos.

La rama utilizada para la entrega y revisión es **master**.

---

## Objetivo de la solución

El objetivo principal de la solución es desacoplar el frontend de la API externa mediante un BFF, centralizando la lógica de negocio, la configuración y el manejo de errores en el backend.

Esto permite:
- Evitar el consumo directo de la API externa desde el frontend.
- Tener un contrato controlado y estable hacia la UI.
- Manejar errores de forma consistente.
- Facilitar la mantención y escalabilidad del proyecto.

---

## Estructura del repositorio

```text
.
├── Carsales.RickAndMorty.BFF/         # Backend For Frontend en .NET
├── frontend/
│   └── carsales-rickandmorty-web/     # Proyecto Angular
├── .gitignore
└── README.md
