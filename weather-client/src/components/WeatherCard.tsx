import type {City, WeatherData} from '../types';

interface WeatherCardProps {
  weather: WeatherData | null;
  city: City | null;
}

export function WeatherCard({ weather, city }: WeatherCardProps) {
  if (!weather) return <p className="italic">Select a city to view weather.</p>;

  return (
    <div className="bg-blue-100 border-l-4 border-blue-500 text-blue-700 p-4 rounded shadow">
      <p className="text-lg font-semibold">{city?.Name}, {city?.Country}, {city?.AdministrativeArea}</p>
      <p className="text-sm">Temperature: {weather.Temperature}°C</p>
      <p className="text-sm">Condition: {weather.Description}</p>
    </div>
  );
}