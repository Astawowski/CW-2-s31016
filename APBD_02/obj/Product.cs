namespace APBD_02.obj;

public class Product
{
    public string Name { get; set; }
    public double? MinTemperatureRequired { get; set; }
    public bool HazardousLoad { get; set; }

    public Product(string type, double temperatureRequired)
    {
        Name = type;
        MinTemperatureRequired = temperatureRequired;
        HazardousLoad = false;
    }
    
    public Product(string type)
    {
        Name = type;
        HazardousLoad = false;
    }
    
    public Product(string type, bool hazardousLoad)
    {
        Name = type;
        HazardousLoad = hazardousLoad;
    }
    
    public Product(string type, double temperatureRequired, bool hazardousLoad)
    {
        Name = type;
        HazardousLoad = hazardousLoad;
        MinTemperatureRequired = temperatureRequired;
    }
    
}