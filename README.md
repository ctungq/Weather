# PROJECT: WEATHER MAP

### This project provides a backend API, a website (UI) to display weather information of a city in a country.

================================================================

### Prerequisites
This project has been developed on Visual Studio Code v1.89, running with .NET Framework 8.0.

Eventhough this can be run with Visual Studio 2022, but I haven't personally tried out yet.

### Set up the project
1. Download this project to your local drive.
2. Open the solution folder in Visual Studio Code.

### How to build/run/test using Solution Explorer
1. Install [C# Dev Kit for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
2. Open Solution Explorer extension
3. To build: ***right click*** at the solution, and click ***Build***
4. To run tests: find the project **Weather.Api.UnitTests**, ***right click*** and choose ***Run Tests***
5. To run:
   1. Run **Weather.API**, ***right click*** at the project **Weather.API**, choose ***Debug***, then ***Start New Instance***. The API URL is **http://localhost:5000**
   2. Run **Weather.UI**, ***right click*** at the project **Weather.UI**, choose ***Debug***, then ***Start New Instance***. The UI URL is **http://localhost:5180**. You need to run **Weather.API** in order for **Weather.UI** to work.

### How to build/run/test using Terminal command
1. Open a new Terminal, go to the project folder
2. To build, type the command:
   
        dotnet build .\Weather.sln
   
5. To run the api project, type the command (the API URL is **http://localhost:5000**):

        dotnet run --project .\Weather.API\

6. To run the website project, open a new terminal, type the command (The UI URL is **http://localhost:5180**):
   
        dotnet run --project .\Weather.UI\
   
8. To test, type the command:
   
        dotnet test

### Notes
Several improvements can be made to this project.
1. Caching weather data: because weather forecast don't change very frequently, so caching can be consider for a short period of time (e.g. 1hr-4hrs).
2. Most config values are saved in **appSettings.json** files. A few hardcoded strings can be moved to appSettings.json.
3. API security (using API keys) can be automatically tests by writing integration tests.
4. A new contract library can be created for Weather.UI.
