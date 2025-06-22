public record CityDTO
{
    public required string LocationKey { get; init; }
    public required string Name { get; init; }
    public string Country { get; init; } = string.Empty;
    public string AdministrativeArea { get; init; } = string.Empty;
    public int Rank { get; init; } = int.MaxValue;
}
