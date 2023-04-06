[System.Serializable]

public class WeatherData 
{
    public HourlyData hourly;
}

[System.Serializable] 

public class HourlyData
{
    public string[] time;
    public float[] temperature_2m;
    public float[] relativehumidity_2m;
}