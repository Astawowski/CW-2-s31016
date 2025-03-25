namespace APBD_02;

public class Product
{
    public string name { get; set; }
    public double? MinTemperatureRequired { get; set; }
    public bool HazardousLoad { get; set; }

    public Product(string type, double temperatureRequired)
    {
        name = type;
        MinTemperatureRequired = temperatureRequired;
        HazardousLoad = false;
    }
    
    public Product(string type)
    {
        name = type;
        HazardousLoad = false;
    }
    
    public Product(string type, bool hazardousLoad)
    {
        name = type;
        HazardousLoad = hazardousLoad;
    }
    
    public Product(string type, double temperatureRequired, bool hazardousLoad)
    {
        name = type;
        HazardousLoad = hazardousLoad;
        MinTemperatureRequired = temperatureRequired;
    }
    
}