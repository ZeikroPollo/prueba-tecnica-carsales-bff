# Prueba Técnica Carsales – BFF .NET + Angular

Este repositorio contiene la solución desarrollada para la prueba técnica de Carsales.  
El proyecto implementa un **Backend For Frontend (BFF)** en .NET que actúa como intermediario entre un frontend en Angular y la API pública de Rick and Morty, aplicando buenas prácticas de arquitectura, manejo de errores y configuración.

La rama utilizada para la entrega y revisión es **master**.

---

## Objetivo de la solución

El objetivo principal es desacoplar el frontend de la API externa, centralizando la lógica de negocio y el manejo de errores en un BFF.  
Esto permite:
- Un contrato estable hacia el frontend.
- Mayor control sobre los datos expuestos.
- Manejo consistente de errores.
- Facilidad para aplicar cambios o reglas sin afectar la UI.

---

## Estructura del repositorio

```text
.
├── Carsales.RickAndMorty.BFF/        # Backend BFF en .NET
├── frontend/
│   └── carsales-rickandmorty-web/    # Frontend Angular
└── PruebaTecnicaCarsales.slnx        # Solución de Visual Studio
```

La separación entre backend y frontend es intencional para facilitar la mantención y la revisión del código.

---

## Backend – Carsales.RickAndMorty.BFF

Proyecto .NET que implementa el patrón **Backend For Frontend**.

### Responsabilidades principales

- Consumir la API pública de Rick and Morty.
- Exponer endpoints propios adaptados al frontend.
- Centralizar la lógica de negocio.
- Manejar errores y devolver respuestas controladas.
- Utilizar configuración externa mediante `appsettings`.

### Estructura interna

- **Clients/**  
  Cliente HTTP tipado encargado de comunicarse con la API de Rick and Morty.  
  Permite aislar el acceso externo y facilitar cambios futuros.

- **Services/**  
  Contiene la lógica de negocio.  
  Orquesta las llamadas al cliente, procesa la información y aplica reglas como paginación o validaciones.

- **Endpoints/**  
  Define los endpoints expuestos por el BFF utilizando Minimal APIs.  
  Estos endpoints son el único punto de acceso para el frontend.

- **Models/**  
  Modelos y DTOs propios del BFF, evitando el acoplamiento directo con los modelos de la API externa.

- **Middleware/**  
  Middleware de manejo de errores que captura excepciones y devuelve respuestas consistentes al frontend.

### Configuración

- **appsettings.json / appsettings.Development.json**  
  Contienen la configuración del proyecto, incluyendo la URL base de la API externa.

---

## Frontend – Angular

Aplicación Angular que consume exclusivamente los endpoints del BFF.

### Funcionalidades implementadas

- Consumo de datos desde el BFF.
- Listado de información.
- Paginación y filtros.
- Manejo de estados de carga.
- Manejo básico de errores a nivel de UI.

### Estructura relevante

- **src/app/**  
  Componentes y servicios de la aplicación.
- **src/environments/**  
  Configuración de entornos, incluyendo la URL base del BFF.
- **angular.json**  
  Configuración del proyecto.
- **package.json**  
  Dependencias y scripts.

---

## Manejo de errores (Feedback G3)

- El backend centraliza el manejo de errores mediante un middleware.
- Las excepciones se transforman en respuestas controladas y coherentes para el frontend.
- El frontend maneja estados de error y carga, evitando fallos silenciosos.

---

## Archivos de configuración (Feedback G5)

- El backend utiliza `appsettings.json` y `appsettings.Development.json`.
- El frontend utiliza archivos de entorno (`environment.ts`).
- No existen valores críticos hardcodeados en el código.

---

## Interacciones y funcionalidades extra (Feedback G8)

- Paginación y filtros en el frontend.
- Separación clara de responsabilidades entre componentes y servicios.
- El frontend no consume directamente la API externa, solo el BFF.

---

## Cómo ejecutar el proyecto

### Backend (.NET)

Requisitos:
- .NET SDK

```bash
cd Carsales.RickAndMorty.BFF
dotnet restore
dotnet run
```

El backend se levanta en un puerto local definido en la configuración.

---

### Frontend (Angular)

Requisitos:
- Node.js
- Angular CLI

```bash
cd frontend/carsales-rickandmorty-web
npm install
ng serve
```


## Flujo general

1. El frontend realiza peticiones HTTP al BFF.
2. El BFF consume la API de Rick and Morty.
3. El BFF procesa y adapta los datos.
4. El frontend recibe la información lista para ser presentada.

---

## Notas finales

- La rama **master** contiene la versión final del proyecto.
- Carpetas generadas automáticamente (`bin`, `obj`, `node_modules`) están excluidas mediante `.gitignore`.
- La solución prioriza claridad, separación de responsabilidades y mantenibilidad.