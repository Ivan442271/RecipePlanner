# Recipe Planner

Recipe Planner is a TUI application for storing recipes and creating shopping lists.

## Subject area

The application is designed for storing cooking recipes, organizing ingredients and generating shopping lists based on selected recipes.

## Features

- create recipes
- edit recipes
- delete recipes
- save ingredients and cooking steps
- search recipes
- filter recipes by category
- sort recipes
- scale portions
- favorites support
- shopping list generation
- application settings

## Technologies

- C#
- .NET 10
- JSON serialization
- NUnit

## Project structure

### RecipePlanner.Domain
Contains models, enums and interfaces.

### RecipePlanner.Application
Contains business logic, repositories and services.

### RecipePlanner.ConsoleUI
Contains terminal user interface.

### RecipePlanner.Tests
Contains unit tests.

## Data storage

Recipes are stored in:

```text
recipes.json
```

Application settings are stored in:

```text
settings.json
```

## Run project

Open solution in Visual Studio and run:

```text
RecipePlanner.ConsoleUI
```

## Tests

Run tests using Test Explorer in Visual Studio.

## Publish

Build self-contained publish version using Visual Studio publish tools.

## Example data

After the first launch the application automatically creates JSON files for recipes and settings storage.