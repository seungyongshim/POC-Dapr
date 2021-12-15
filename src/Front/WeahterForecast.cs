namespace Front;

public class WeahterForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int Temperature { get; set; }
    public string Summary { get; set; } = string.Empty;
}
