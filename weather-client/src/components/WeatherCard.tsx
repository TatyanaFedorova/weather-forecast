import type {City, WeatherData} from '../types';

interface WeatherCardProps {
  weather: WeatherData | null;
  city: City | null;
}

export function WeatherCard({ weather, city }: WeatherCardProps) {
  if (!weather) return(
    <div className="bg-blue-100 border-l-4 border-blue-500 text-blue-700 p-4 rounded shadow">
      <p className="italic">Select a city to view weather.</p>
    </div> );

  return (
    <div className="bg-blue-100 border-l-4 border-blue-500 text-blue-700 p-4 rounded shadow">
      <p className="text-lg font-bold">{city?.Name}, {city?.Country}, {city?.AdministrativeArea}</p>
      <p className="text-sm">Temperature: <b>{weather.Temperature}°C</b></p>
      <p className="text-sm">Condition: <b>{weather.Description}</b></p>
    </div>
  );
}