
interface City {
  LocationKey: string;
  Name: string;
  Country: string;
  AdministrativeArea: string;
}

interface SearchResultsListProps {
  cities: City[];
  onSelect: (selectedCity: City) => void;
}

export function SearchResultsList({ cities, onSelect }: SearchResultsListProps) {
  if(cities?.length === 0) return (<span className="block mb-2 font-semibold">City with this name is not found.</span>)
  return (
    <div>
    <label htmlFor="citySelect" className="block mb-2 font-semibold">Please choose from the cities found:</label>
    <ul className="border rounded divide-y">
      {cities.map(city => (
        <li
          key={city.LocationKey}
          onClick={() => onSelect(city)}
          className="p-2 hover:bg-blue-50 cursor-pointer"
        >
          {city.Name}, {city.Country}, {city.AdministrativeArea}
        </li>
      ))}
    </ul>
    </div>
  );
}