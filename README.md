# Angular To-do Application

This is a sample Angular application for managing To-do tasks using .NET Core and Angualar technologies.

## Dependencies
- .NET Core 6
- Angular 15
- SQLite (In-Memory)
- SignalR
- Hangfire (Dashboard: `http://localhost:5252/hangfire/jobs/scheduled`)

## How to Run Locally
Note: Launching the app could take a few minutes at the first run.
- git clone `https://github.com/dmumladze/AngularTodo.git`
- cd AngularTodo
- dotnet restore
- cd ClientApp
- npm install
- cd ../
- dotnet run
- explorer `"http://localhost:5252"`

## Features
- Create independent To-do tasks.
- Create and manage Project based To-dos.
- View To-do task dependencies.
- Set reminders for To-dos, Projects and Project based To-dos.
- Searching and sort based on priority.

## Future Enhancements
- Add Unit Tests.
- Add Authentication.
- Enable SQLite Full-text search.
- Support Guntt charts view for Projects.
- Create Direct Acyclic Graph of To-dos and display them in topological order (d3-graphviz).
