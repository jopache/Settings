# Settings

A service for configuration management that stores settings in a hierarchical manner.  This means that settings cascade down per environment/application to lower levels meaning commonly used settings can be defined once at a higher level and can cascade down to all applications/environments.  This is useful for things such as connection strings, api urls.  If you need to update a connection string or API url, you can do so in one place and all your applications using the service will automatically be updated to the new value (unless they have overridden the setting value).  This means that you dont have to go hunting down all the applications/environments, transform configuration files, update settings in non uniform databases across applications, create new deployments for updating settings, etc. 

## Contents

- [`src/`](./src)
    - The main event - the code for this project!
- [`scripts/`](./scripts/ReadMe.md)
    - Helpful scripts to build/test/run the project!

## Getting started

1. Clone the project
2. Install project requirements
    - [`docker`](https://www.docker.com/)
      - [`mac`](https://www.docker.com/docker-mac)
      - [`windows`](https://www.docker.com/docker-windows)
3. Build and run the project
    - `cd Settings`
    - `./scripts/cibuild`
    - `./scripts/server`
4. View the goods
    - Browse to [localhost:4200](http://localhost:4200)


## Architecture

### Backend
The backend is written in .NET Core 2.0 for easy portability across different operating systems. The project uses Web API to server up data and while Authentication/Authorization is not yet available, it will be available soon. 

#### Database
The project utilizes Entity Framework Core 2.0 therefore it is flexible in its persistence options.  Through updating /src/Settings/appsettings.json DatabaseType key to either "InMemory", "SqlServer", or "Postgres" you can swap out different types of database servers.  I plan on adding MySQL in the future.


### Frontend