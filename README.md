# Vitura assessment 

The code is split in a frontend project and a backend project:

- [frontend](https://github.com/emewen/VituraHealthFrontend)
- [backend](https://github.com/emewen/VituraHealthBackend)

## Vitura assessment backend startup

To run, open in Visual Studio and run with the "http" profile.  If you run as https, a self signed cert will be needed/created
on first run.  Also axiosInstance.tsx on the front end will need:

```js
const API_BASE_URL = 'http://localhost:5191/api';
```
changed to:
```js
const API_BASE_URL = 'http://localhost:7134/api';
```

If you prefer to run from the command line, navigate to the \VituraHealthBackend\VituraHealthBackend project folder and run
the below in Developer Powershell or cmd:

```js
dotnet run --launch-profile http
```

# Vitura assessment frontend startup

Open frontend codebase in VSCode preferably.  Navigate to \VituraHealthFrontend directory and in terminal run:
```js
npm install
npm run dev
```

Axios and react bootstrap were installed as well but should be pulled in with the above.  If not run:
```js
npm install axios
npm install react-bootstrap bootstrap
```


## Assumptions / Incompletes

- I scaffolded the frontend with vite (react/typescript) and used a .net core 8 web api project stucture for the backend
- Cache is used (IMemoryCache) instead of database.  One minute sliding expiration, 5 minute absolute expiration.
- I created a list of Patients with an expanding Prescriptions per Patient with a mobile friendly style using react Bootstrap
- I used axios to create an apiclient to access the backend

## Incompletes

- I only created two tests to show testing competency with nUnit and Moq
- Exception handling is NOT fully fleshed out.  Mainly 500 server errors and "pretty" api responses
- Ideally I would create multiple appsettings.json (one for each environment).  Since there were minimal config entries, I didn't lean on .netcore configuration
- Didn't have time for a Patient/Prescription search.  Relatively easy:
	- textbox in frontend
	- pass textbox value via querystring appended to /api/patients?somesearch=blah
	- update backend patients controller getter ([FromQuery])
	- linq filter on result set if query text is not String.Empty

## Time taken

- Roughly 4 hours
- 15 minutes solutioning/whiteboarding backend
- 1 hour backend dev
- 15 minutes solutioning/whiteboarding frontend
- 1.5 hours frontend dev
- 30 minutes style
- 30 minutes polish and readme documentation