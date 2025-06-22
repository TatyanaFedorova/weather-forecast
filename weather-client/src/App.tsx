import { useState } from "react";
import axios from "axios";
import type {WeatherResponse} from './types'

function App() {
  const [city, setCity] = useState("");
  const [weather, setWeather] = useState<WeatherResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  const apiBase = import.meta.env.VITE_API_BASE;

  const getWeather = async () => {
    try {
      setError(null);
      console.log(`${apiBase}/currentweather/${city}`);
      const res = await axios.get(`${apiBase}/currentweather/${city}`);
      
      setWeather(res.data);
    } catch (err: unknown) {
      setWeather(null);
      if (axios.isAxiosError(err)) {
        console.log(err);
        setError(err.response?.data?.title || err.message);
      } else {
        setError('An unknown error occurred');
      }
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-2xl font-bold mb-4">Weather Forecast</h1>
      <input
        className="border p-2 w-full mb-2"
        type="text"
        placeholder="Enter city name"
        value={city}
        onChange={(e) => setCity(e.target.value)}
      />
      <button
        className="bg-blue-500 text-white px-4 py-2 rounded"
        onClick={getWeather}
      >
        Get Weather
      </button>

      {error && <p className="text-red-500 mt-4">{error}</p>}

      {weather && (
        <div className="mt-4 bg-gray-100 p-4 rounded">
          <p><strong>Weather:</strong> {weather.Description}</p>
          <p><strong>Temperature:</strong> {weather.Temperature} °C</p>
        </div>
      )}
    </div>
  );
}

export default App;