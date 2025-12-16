Prueba Técnica Carsales – BFF .NET + Angular

Este repositorio contiene la solución desarrollada para la prueba técnica de Carsales. La solución está compuesta por un Backend For Frontend (BFF) desarrollado en .NET y un frontend en Angular. El frontend consume exclusivamente los endpoints expuestos por el BFF, el cual a su vez utiliza la API pública de Rick and Morty como fuente de datos.

La rama utilizada para la evaluación y entrega del proyecto es master.


Descripción general de la solución

La arquitectura implementada sigue el patrón Backend For Frontend (BFF). El objetivo principal de este enfoque es desacoplar el frontend de la API externa, centralizando en el backend la comunicación con servicios externos, la lógica de negocio, el manejo de errores y la configuración por entorno.

El frontend no consume directamente la API de Rick and Morty, sino que se comunica únicamente con el BFF, lo que permite un mayor control sobre los datos expuestos, una estructura más mantenible y una mejor separación de responsabilidades.


Estructura del repositorio

La solución se organiza de la siguiente manera:

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

La separación entre backend y frontend es intencional y busca mantener una estructura clara, ordenada y fácil de revisar.


Backend – Carsales.RickAndMorty.BFF

El proyecto Carsales.RickAndMorty.BFF corresponde al Backend For Frontend desarrollado en .NET. Este componente es responsable de consumir la API pública de Rick and Morty, procesar la información obtenida y exponer endpoints propios que son consumidos por el frontend.

Las principales responsabilidades del BFF son:
- Consumir la API externa.
- Adaptar y transformar los datos según las necesidades del frontend.
- Centralizar la lógica de negocio.
- Manejar errores de forma controlada.
- Utilizar configuración externa mediante archivos appsettings.

El backend se organiza en las siguientes carpetas:

Clients: contiene el cliente HTTP encargado de comunicarse con la API externa de Rick and Morty, encapsulando la lógica de consumo del servicio externo.

Services: contiene la lógica de negocio del BFF. Los servicios orquestan las llamadas a los clientes, aplican transformaciones y validaciones, y devuelven la información procesada.

Endpoints: define los endpoints expuestos por el BFF utilizando Minimal APIs. Estos endpoints representan el contrato que consume el frontend.

Models: contiene los modelos y DTOs utilizados dentro del BFF, evitando el acoplamiento directo con los modelos de la API externa.

Middleware: incluye el middleware de manejo centralizado de errores, encargado de capturar excepciones no controladas y devolver respuestas consistentes al frontend.

Program.cs: archivo de entrada de la aplicación, donde se configuran los servicios, middlewares y endpoints del BFF.

La configuración del backend se maneja mediante los archivos appsettings.json y appsettings.Development.json. En ellos se define la URL base de la API externa y otros parámetros dependientes del entorno, evitando valores críticos hardcodeados en el código.


Frontend – Angular

El frontend está desarrollado en Angular y se encuentra completamente unificado en una sola ruta dentro del repositorio: frontend/carsales-rickandmorty-web. Esta carpeta contiene tanto los archivos de configuración del proyecto Angular como el código fuente de la aplicación.

Dentro del proyecto frontend se incluyen los archivos de configuración (angular.json, package.json, tsconfig) y el código fuente ubicado en la carpeta src.

La carpeta src/app contiene los componentes, servicios, interceptores y modelos del frontend, donde se implementa la lógica de presentación y el consumo de datos desde el BFF. La carpeta src/environments define la configuración por entorno del frontend, incluyendo la URL base del BFF. La carpeta public contiene archivos estáticos utilizados por la aplicación.

Las funcionalidades implementadas en el frontend incluyen:
- Consumo de datos a través del BFF.
- Visualización de información en listas.
- Paginación y filtros básicos.
- Manejo de estados de carga.
- Manejo básico de errores en la interfaz.


Manejo de errores

El manejo de errores se centraliza principalmente en el backend mediante un middleware que captura excepciones y devuelve respuestas controladas al frontend. A nivel de frontend, se manejan estados de error y carga para evitar fallos silenciosos y mejorar la experiencia de usuario.


Archivos de configuración

El backend utiliza archivos appsettings para separar la configuración por entorno. El frontend utiliza archivos de configuración de entorno para definir la URL del BFF. En ambos casos se evita el uso de valores críticos hardcodeados directamente en el código.


Cómo ejecutar el proyecto

Para ejecutar el backend es necesario contar con el .NET SDK instalado. Desde la carpeta Carsales.RickAndMorty.BFF se deben ejecutar los siguientes comandos:

dotnet restore  
dotnet run  

Para ejecutar el frontend es necesario contar con Node.js y Angular CLI. Desde la carpeta frontend/carsales-rickandmorty-web se deben ejecutar los siguientes comandos:

npm install  
ng serve  

La aplicación frontend quedará disponible por defecto en http://localhost:4200.


Notas finales

La rama master contiene la versión final del proyecto con el BFF y el frontend correctamente estructurados. Las carpetas generadas automáticamente como bin, obj y node_modules se excluyen del repositorio mediante .gitignore. La solución fue desarrollada priorizando claridad, buenas prácticas, separación de responsabilidades y mantenibilidad.
