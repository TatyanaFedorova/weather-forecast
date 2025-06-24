import type {City} from '../types';

interface SavedCityProps {
  cities: City[];
  selectedKey: string;
  onChange: (selectedKey: string) => void;
}

export function SavedCities({cities, selectedKey, onChange}: SavedCityProps){
    if(cities?.length === 0) return (<span className="block mb-2 font-semibold">City with this name is not found.</span>)
    return (
    <div>      
      <label htmlFor="citySelect" className="block mb-2 font-semibold">Choose from your favorite cities:</label>
      <select
        id="citySelect"
        className="w-full border rounded px-3 py-2 text-gray-700 bg-white shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
        value={selectedKey}
        onChange={e => onChange(e.target.value)}
      >
        <option value="">-- Choose a city --</option>
        {cities.map(city => (
          <option key={city.LocationKey} value={city.LocationKey}>
            {city.Name}, {city.Country}, {city.AdministrativeArea}
          </option>
        ))}
      </select>
    </div>
  ); 

} 