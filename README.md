# DocuTrack S.A. - Árbol Binario (MVC) - Unidad 1

## Descripción general
Este proyecto implementa un sistema de gestión de archivos y carpetas utilizando un **Árbol Binario de Búsqueda (BST)** bajo el patrón **Modelo-Vista-Controlador (MVC)** en **C# (.NET 6+)**.  
El objetivo es demostrar la manipulación de estructuras de datos dinámicas y su aplicación en un contexto de organización documental simulada.

---

## Integrantes
- Jhonathan Salazar Munnoz - Arquitecto de soluciones y desarrollador 

## Requisitos de compilación y ejecución

1. Tener instalado **.NET SDK 6.0 o superior**.
2. Clonar el repositorio o copiar los archivos.
3. Abrir la carpeta del proyecto en terminal y ejecutar:

```bash
dotnet build
dotnet run
```

---

## Funcionalidades implementadas

- **Inserción** de nodos (archivos y carpetas) con conteo de comparaciones.  
- **Búsqueda** de nodos (6 búsquedas: 4 existentes, 2 inexistentes).  
- **Eliminación** de nodos (casos: hoja, un hijo, dos hijos).  
- **Actualización** (mediante eliminar + insertar).  
- **Recorridos**: Preorden, Inorden, Postorden y Por niveles.  
- **Cálculo de altura** del árbol.  
- **Visualización ASCII** del árbol en consola.  
- **Separación MVC estricta** (sin mezcla de responsabilidades).

---

## Flujo de casos de uso

1. Construcción automática del árbol con **14 nodos** iniciales.  
2. Ejecución de 6 búsquedas (4 exitosas y 2 fallidas).  
3. 3 actualizaciones: hoja, nodo con un hijo, raíz.  
4. 3 eliminaciones: hoja, nodo con un hijo, raíz.  
5. Impresión de recorridos y altura final.  
6. Visualización del árbol tras cada operación.

---

## Licencia

Proyecto académico con fines educativos. Libre de uso y modificación con fines no comerciales.
