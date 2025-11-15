# Bounded Context: TASKS

Este Bounded Context implementa la funcionalidad completa de gestión de tareas (Tasks) para el sistema FruTech Backend API.

## Estructura del Proyecto

```
Tasks/
├── Application/
│   └── Internal/
│       ├── CommandServices/
│       │   └── TaskCommandService.cs
│       └── QueryServices/
│           └── TaskQueryService.cs
├── Domain/
│   ├── Model/
│   │   ├── Aggregate/
│   │   │   └── Task.cs
│   │   ├── Commands/
│   │   │   ├── CreateTaskCommand.cs
│   │   │   ├── EditTaskCommand.cs
│   │   │   └── DeleteTaskCommand.cs
│   │   └── Queries/
│   │       ├── GetAllTasksQuery.cs
│   │       ├── GetTaskByIdQuery.cs
│   │       └── GetTasksByFieldQuery.cs
│   ├── Repositories/
│   │   └── ITaskRepository.cs
│   └── Services/
│       ├── ITaskCommandService.cs
│       └── ITaskQueryService.cs
├── Infrastructure/
│   └── Persistence/
│       └── EFC/
│           ├── Configuration/
│           │   └── Extensions/
│           │       └── ModelBuilderExtensions.cs
│           └── Repositories/
│               └── TaskRepository.cs
└── Interfaces/
    └── REST/
        ├── Resources/
        │   ├── CreateTaskResource.cs
        │   ├── EditTaskResource.cs
        │   └── TaskResource.cs
        ├── Transform/
        │   ├── CreateTaskCommandFromResourceAssembler.cs
        │   ├── EditTaskCommandFromResourceAssembler.cs
        │   └── TaskResourceFromEntityAssembler.cs
        └── TasksController.cs
```

## Modelo de Datos

El modelo `Task` contiene las siguientes propiedades (siguiendo la estructura del db.json):

```csharp
public class Task
{
    public int id { get; set; }
    public string description { get; set; }
    public string due_date { get; set; }
    public string field { get; set; }
}
```

**Nota:** Los nombres de las propiedades están en minúsculas y snake_case para coincidir exactamente con el formato del db.json del frontend, facilitando la integración.

## Endpoints API

### GET /api/Tasks
Obtiene todas las tareas.

**Respuesta:**
```json
[
  {
    "id": 1,
    "description": "Check for aphids and apply neem oil if necessary.",
    "due_date": "10/10",
    "field": "Campo de Granos, Los Grandes"
  }
]
```

### GET /api/Tasks/{id}
Obtiene una tarea por su ID.

**Parámetros:**
- `id` (int): ID de la tarea

**Respuesta:**
```json
{
  "id": 1,
  "description": "Check for aphids and apply neem oil if necessary.",
  "due_date": "10/10",
  "field": "Campo de Granos, Los Grandes"
}
```

### GET /api/Tasks/field/{field}
Obtiene todas las tareas de un campo específico.

**Parámetros:**
- `field` (string): Nombre del campo

**Respuesta:**
```json
[
  {
    "id": 1,
    "description": "Check for aphids and apply neem oil if necessary.",
    "due_date": "10/10",
    "field": "Campo de Granos, Los Grandes"
  }
]
```

### POST /api/Tasks
Crea una nueva tarea.

**Body:**
```json
{
  "description": "Apply nitrogen-rich fertilizer.",
  "due_date": "12/10",
  "field": "Papas del Sol"
}
```

**Respuesta:**
```json
{
  "id": 2,
  "description": "Apply nitrogen-rich fertilizer.",
  "due_date": "12/10",
  "field": "Papas del Sol"
}
```

### PUT /api/Tasks/{id}
Actualiza una tarea existente.

**Parámetros:**
- `id` (int): ID de la tarea

**Body:**
```json
{
  "description": "Apply nitrogen-rich fertilizer (updated).",
  "due_date": "13/10",
  "field": "Papas del Sol"
}
```

**Respuesta:**
```json
{
  "id": 2,
  "description": "Apply nitrogen-rich fertilizer (updated).",
  "due_date": "13/10",
  "field": "Papas del Sol"
}
```

### DELETE /api/Tasks/{id}
Elimina una tarea.

**Parámetros:**
- `id` (int): ID de la tarea

**Respuesta:** 204 No Content

## Arquitectura

### Domain-Driven Design (DDD)
El proyecto sigue los principios de DDD con:
- **Aggregates**: Task como raíz de agregado
- **Commands**: CreateTask, EditTask, DeleteTask
- **Queries**: GetAllTasks, GetTaskById, GetTasksByField
- **Repository Pattern**: ITaskRepository para acceso a datos
- **Service Pattern**: Separación entre CommandService y QueryService

### CQRS (Command Query Responsibility Segregation)
- **Command Services**: Manejan operaciones de escritura (Create, Update, Delete)
- **Query Services**: Manejan operaciones de lectura (Get)

### Inyección de Dependencias
Todos los servicios y repositorios están registrados en `Program.cs`:
```csharp
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskCommandService, TaskCommandService>();
builder.Services.AddScoped<ITaskQueryService, TaskQueryService>();
```

## Base de Datos

El proyecto utiliza **Entity Framework Core** con **InMemoryDatabase** para desarrollo:
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("FruTechDB");
});
```

La configuración de la entidad Task se encuentra en:
`Tasks/Infrastructure/Persistence/EFC/Configuration/Extensions/ModelBuilderExtensions.cs`

## Notas Importantes

1. **Naming Convention**: Se utilizan los mismos nombres de propiedades que en el db.json (id, description, due_date, field) para facilitar la integración con el frontend.

2. **Namespace**: El namespace es `FruTech.Backend.API.Tasks` (con 's' al final) para evitar conflictos con `System.Threading.Tasks.Task`.

3. **Sin CompleteTask**: Como se especificó, no se implementó el comando CompleteTaskCommand.

4. **InMemory Database**: Los datos se pierden al reiniciar la aplicación. Para producción, se debe cambiar a SQL Server, PostgreSQL u otra base de datos persistente.

## Próximos Pasos

Para conectar con una base de datos real:

1. Agregar la cadena de conexión en `appsettings.json`
2. Cambiar `UseInMemoryDatabase` por `UseSqlServer` o el proveedor correspondiente
3. Ejecutar migraciones: `dotnet ef migrations add InitialCreate`
4. Actualizar la base de datos: `dotnet ef database update`

## Ejemplo de Uso

```bash
# Obtener todas las tareas
curl -X GET https://localhost:7xxx/api/Tasks

# Crear una nueva tarea
curl -X POST https://localhost:7xxx/api/Tasks \
  -H "Content-Type: application/json" \
  -d '{
    "description": "Nueva tarea",
    "due_date": "15/10",
    "field": "Campo de Prueba"
  }'

# Actualizar una tarea
curl -X PUT https://localhost:7xxx/api/Tasks/1 \
  -H "Content-Type: application/json" \
  -d '{
    "description": "Tarea actualizada",
    "due_date": "16/10",
    "field": "Campo de Prueba"
  }'

# Eliminar una tarea
curl -X DELETE https://localhost:7xxx/api/Tasks/1
```

