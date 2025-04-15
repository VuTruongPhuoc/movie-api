namespace Movie.API
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; } = default!;

        public int TemperatureC { get; set; } = default!;

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; } = default!;
    }
}
