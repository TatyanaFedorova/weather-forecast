import { useState, useEffect, useCallback } from 'react';
import { CitySearchBar } from './components/CitySearchBar';
import { SearchResultsList } from './components/SearchResultsList';
import { SavedCities } from './components/SavedCities';
import { WeatherCard } from './components/WeatherCard';
import type {City, WeatherData} from './types';
import axios from 'axios';

const apiBase = import.meta.env.VITE_API_BASE;

function App() {
  const [foundCities, setFoundCities] = useState<City[]>([]);
  const [savedCities, setSavedCities] = useState<City[]>([]);
  const [selectedCity, setSelectedCity] = useState<City|null>(null);
  const [weather, setWeather] = useState<WeatherData | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchCities = useCallback( async () => {
    setError(null);
    setLoading(true);
    try {
        const response = await axios.get<City[]>(`${apiBase}/api/cities`);
        const result = response.data;
        setSavedCities(result);
        if(result && result[0]){
          setSelectedCity(result[0])
          await getWeater(result[0].LocationKey);
        }

    } catch (error) {
        console.error('Failed to fetch cities:', error);
        setError('Failed to load saved cities. Please try again later.');
      } finally {
        setLoading(false);
      }
    }, []);

  useEffect(() => {
    fetchCities();
  }, [fetchCities]);


  const handleSearch = async (query: string) => {
    setLoading(true);
    setFoundCities([]);
    try {
      const response = await axios.get<City[]>(`${apiBase}/searchCityByName/${query}`);
      setFoundCities(response.data);
      setSelectedCity(null);
      setWeather(null);    
    } catch (err) {
      setError('Failed to fetch city list');
      console.error('Failed to fetch city list:', err);
    }
    finally{
      setLoading(false);
    }
  };

  const handleSelectCity = async (selectedCity: City | null) => {
      await addCity(selectedCity);
      await fetchCities();
      setFoundCities([]);
  };

  const handleChange = async (selectedCityKey: string) => {
      if(!selectedCityKey) return;
      const selectedCity = savedCities.find(c=>c.LocationKey == selectedCityKey) ?? null;
      if(selectedCity == null) return;
      setSelectedCity(selectedCity);
      await getWeater(selectedCityKey);     
  };

  const getWeater = async (selectedCityKey: string) => {
    setLoading(true);
    try {          
        const response  = await axios.get<WeatherData>(`${apiBase}/currentweather/${selectedCityKey}`);
        setWeather(response.data);
      } catch (err) {
        setError('Failed to fetch weather data.')
      console.error('Failed to fetch weather data:', err);
    }
    setLoading(false);
  }

  const addCity = async (city: City | null)=>{
    setError(null);
    try {
      if(city == null) return;
      setLoading(true);

      const response = await axios.post(`${apiBase}/api/cities`, city);
      console.log('City added successfully:', response.data);
       
    } catch (error) {
      setError('Failed to add city')
      console.error('Failed to add city:', error);
    }
    finally{ setLoading(true);}
  }

  if (loading) {
    return <div className="p-4">Loading...</div>;
  }

  return (
    <div className="max-w-xl mx-auto p-4 space-y-6">
    { error && (
      <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
        <strong className="font-bold">Error:</strong>
      <span className="block sm:inline ml-2">{error}</span>
    </div> )   
    }      
      <WeatherCard weather={weather} city={selectedCity}/>
      <SavedCities cities={savedCities} 
        selectedKey={selectedCity?.LocationKey ?? ""} 
        onChange ={handleChange} />
      <CitySearchBar onSearch={handleSearch} />
      <SearchResultsList cities={foundCities} onSelect={handleSelectCity} />     
    </div>
  );
}

export default App;