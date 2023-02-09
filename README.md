# Angular To-do Application

This is a sample Angular application for managing To-do tasks using .NET Core and Angualat technologies.

## Dependencies
- .NET Core 6
- Angular 15
- SQLite In-Memory Database (No need to install)

## Running The App
- git clone `https://github.com/dmumladze/AngularTodo.git`
- cd AngularTodo
- dotnet restore
- cd ClientApp
- npm install
- cd ../

## Features
- Create independent To-do tasks.
- Create and manage Project based To-dos.
- View To-do task dependencies.
- Set reminders for To-dos, Projects and Project based To-dos.

## Furure Enhancements
- Add Authentication.
- Support Guntt charts view for Projects.
- Create Direct Acyclic Graph of To-dos and display them in topological order (d3-graphviz).