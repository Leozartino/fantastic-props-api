# E-Commerce Project 💲

This E-commerce project is developed using Angular for the front-end and .NET Core C# for the back-end. It is a personal project created with the aim of applying and showcasing knowledge of these technologies.

## Technologies Used 🔨

- **Front-End:** Angular (not included in this repository)
- **Back-End:** .NET Core C#
- **Database:** SQL Server
- **Architecture:** Hexagonal Architecture
- **Design Pattern:** Generic Repository Pattern
- **Layers:** Three layers - API, Presentation, and Infrastructure
- **Additional Tools:** Migrations, Docker

## Project Structure 📕

The project follows a modular and organized structure, adhering to the principles of hexagonal architecture. The three main layers are:

1. **API Layer:** Contains the API endpoints and serves as the entry point for external communication.

2. **Domain Layer(core):** Represents the entities, the definitions for interfances (repositories and services).

3. **Infrastructure Layer:** Manages the underlying infrastructure, including database interactions, external services, and other low-level concerns.

## Design Patterns 🕸️

The project incorporates the Generic Repository Pattern, enhancing data access by providing a common set of methods for CRUD operations. This promotes code reusability and maintainability.

## Database and Migrations

SQL Server is utilized as the database for this E-commerce project. The project integrates migrations to manage database schema changes efficiently.

## Docker 🐳

Docker is employed for containerization, allowing for a consistent and reproducible environment across different systems. This simplifies deployment and scalability.

## How to Run 🤓

To run the project, follow these steps:

1. **Clone the Repository:**
   ```
   git clone https://github.com/Leozartino/fantastic-props-api
   ```

2. **Navigate to the Project Directory:**
   ```
   set up the startup project with Program.cs file
   ```

4. **Access the Application:**
   Open your browser and go to [http://localhost:yourport](http://localhost:yourport)

Feel free to explore and modify the project according to your needs. Happy coding!
