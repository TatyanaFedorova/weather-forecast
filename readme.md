# WeatherForecast App

This is a full-stack web application that allows users to search for cities, view the current weather, and save locations for quick access later.

## 🧱 Technologies

- **Backend**: .NET 9 Minimal API, Entity Framework Core, AccuWeather API
- **Frontend**: React (TypeScript), Axios, Vite
- **Database**: SQLite (local file-based)
- **Cache**: IMemoryCache

## 📂 Project Structure

- `WeatherForecast/` — .NET Web API project
- `weather-client/` — React frontend project

---

## 🚀 Deployment Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/TatyanaFedorova/weather-forecast.git
cd weather-forecast
```

### 2. Setup Environment Variables if you want them to be different

#### In `WeatherForecast` open appsettings.json and change AccuWeater ApiKey. The existing one is mine with 50 request per day limit:

```ini
"ApiKey": "Ac4oatYHCSBguQ85aoSE79sXAH6Gv6dR"
```

#### In `WeatherForecast` open launchSettings.json and change applicationUrl for http and https:

```ini
"applicationUrl": "http://localhost:5181"
```

#### Open an `.env` file In `weather-client` and change port number to one your api working on (applicationUrl from appsettings.json):

```ini
VITE_API_BASE=http://localhost:5181
```

### 3. Start the API

```bash
cd WeatherForecast
dotnet restore
dotnet ef database update
dotnet run
```

Runs on: `http://localhost:5181`

### 4. Start the React App

```bash
cd weather-client
npm install
npm run dev
```

Runs on: `http://localhost:5173`

---

## 💡 Features

- City search by name with disambiguation support
- Displays real-time weather using AccuWeather
- Stores user-selected cities
- Supports CORS for cross-domain integration
- Implements retry and caching for resilience

## 🧪 API Endpoints

- `GET /location/{city}`: search for cities
- `GET /weather/{key}`: get current weather
- `GET /getlatest`: get weather for all stored cities

## ✅ Notes

- AccuWeather API key is required (free tier available)
- SQLite used for local development (can swap with PostgreSQL or SQL Server)
- Swagger UI available at `/swagger`

---


