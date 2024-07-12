# AlumniNet API

## Overview
The AlumniNet API is built using the ASP.NET Core API template. This API serves both the mobile and web applications of AlumniNet, providing necessary data from SQL Server. The API development was a collaborative effort with Matei Debu, who developed the web part of the AlumniNet application.

## Structure
The structure of this API follows the MVC pattern, clearly separating responsibilities through the use of directories.

### Models
- **DatabaseContext**: Facilitates communication with the database and maps database tables to classes using Entity Framework.

### DTOs (Data Transfer Objects)
- Contains classes tailored for communication with client applications. AutoMapper, a NuGet Package, is used to map these DTOs to the model classes.

### MappingProfiles
- Contains all profiles corresponding to the model classes to facilitate mapping between DTO and model.

### Repository
- Implements the Repository logic along with the Unit of Work design pattern, which will be detailed in a dedicated chapter.

### Services
- Contains functionalities required by multiple endpoints, such as adding a file to AWS S3.

### Controllers
- Contains all controllers with the necessary endpoints to provide data to client applications. Each controller includes basic CRUD operations and other complex operations needed for specific data communication.

## Authentication and Authorization
- Each endpoint requires authorization provided by Firebase, based on the token sent in the API call.

## Data Storage
- For storing posts, a hybrid approach is used. All post data is saved in the database except for images. Instead of the image, the file name uploaded to the cloud using AWS S3 is stored.

## Key Components

### Models
- **DatabaseContext**: Manages database connections and entity configurations.
- **Entity Classes**: Represents database tables as C# classes.

### DTOs
- **Data Transfer Objects**: Simplifies data transfer between client and server.
- **AutoMapper**: Maps DTOs to entity classes and vice versa.

### MappingProfiles
- **AutoMapper Profiles**: Define mappings between DTOs and model classes.

### Repository Pattern
- **Repositories**: Encapsulate data access logic.
- **Unit of Work**: Manages transactions and coordinates changes across repositories.

### Services
- **Business Logic**: Contains methods for complex operations like file uploads to AWS S3.

### Controllers
- **Endpoints**: Provide CRUD operations and complex data retrievals.
- **Authorization**: Ensures secure access to endpoints using Firebase tokens.

## Dependencies
- **ASP.NET Core**: Framework for building the API.
- **Entity Framework Core**: ORM for database operations.
- **AutoMapper**: Object-object mapping.
- **Firebase**: Authentication and authorization.
- **AWS S3**: File storage service.

## Contact

If you have any questions, suggestions, or feedback, feel free to contact us at api-support@university.edu.

---

Thank you for using the AlumniNet API! We hope this API helps you efficiently manage and access alumni data.
