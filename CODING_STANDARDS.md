# EMS Project Coding Standards

This document outlines the coding standards and best practices to follow when contributing to the EMS Project. Adhering to these guidelines ensures code quality, maintainability, and consistency across the project.

---

## General Guidelines
- Write clean, readable, and well-documented code.
- Use meaningful variable, function, and class names.
- Avoid code duplication; use reusable functions/components.
- Add comments for complex logic and public APIs.
- Keep functions and methods short and focused.
- Remove unused code and imports.

---

## .NET API (C#)
- Follow [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
- Use PascalCase for class, method, and property names.
- Use camelCase for local variables and parameters.
- Organize code into appropriate namespaces and folders (Controllers, Models, Migrations).
- Use async/await for asynchronous operations.
- Validate input data and handle exceptions gracefully.
- Use dependency injection for services.
- Write XML documentation for public classes and methods.
- Format code using Visual Studio or `dotnet format`.

---

## Vue.js Frontend (JavaScript/TypeScript)
- Use [Airbnb JavaScript Style Guide](https://github.com/airbnb/javascript) as a reference.
- Use PascalCase for Vue component names.
- Use camelCase for variables, functions, and props.
- Organize components in the `src/components/` folder and views in `src/views/`.
- Use single-file components (`.vue`) with `<template>`, `<script>`, and `<style>` sections.
- Prefer composition API for new components.
- Use ES6+ features (let/const, arrow functions, destructuring).
- Format code using Prettier or ESLint.
- Add comments for complex logic.

---

## Folder Structure
- Keep API and UI code separated in their respective folders.
- Place migrations in `API/Migrations/`.
- Place models in `API/Models/`.
- Place controllers in `API/Controllers/`.
- Place reusable components in `UI/src/components/`.
- Place views in `UI/src/views/`.

---

## Git & Commit Messages
- Use clear, concise commit messages (e.g., `Add employee controller`, `Fix UI bug in AddEmployee.vue`).
- Group related changes in a single commit.
- Avoid committing generated files (bin/, obj/, node_modules/).

---

## Testing
- Write unit tests for critical logic and components.
- Place tests in appropriate folders (e.g., `API/Tests/`, `UI/tests/`).
- Run tests before pushing changes.

---

## Review & Merge
- Submit pull requests for all changes.
- Ensure code passes linting and tests before review.
- Address review comments promptly.

---

For questions or clarifications, contact the project maintainer or refer to the official documentation for .NET and Vue.js.
