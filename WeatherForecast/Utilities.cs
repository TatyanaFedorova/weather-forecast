public static class TemperatureConverter
{
    public static double ToCelsius(double fahrenheit) =>
        Math.Round((fahrenheit - 32) * 5 / 9, 1);
}