namespace APBD_02;

public class Container4Cooled : Container
{
    private double Temperature { get; set; }
    private String? ProductType { get; set; }
    
    public Container4Cooled(double high, double containerMass, double depth, double maxLoad, DistributionCenter dc, double temperature)
        : base(high, containerMass, depth, maxLoad, dc)
    {
        Temperature = temperature;
    }
    
    public override void LoadContainer(double newLoadMass, Product product)
    {
        if (IsTemperatureTooLow(product)) return;
        if ((ProductType != null )&&(product.name != ProductType))
        {
            Console.WriteLine("Container: "+ GetSerialNumber() +" can not be loaded with this product type.");
            return;
        }
        this.ProductType = product.name;
        base.LoadContainer(newLoadMass, product);
    }

    public bool IsTemperatureTooLow(Product product)
    {
        if (Temperature < product.MinTemperatureRequired)
        {
            Console.WriteLine("Can not load. Temperature is too low for the product. Raise the container temperature.");
            return true;
        }
        return false;
    }
    
    public override string GetSerialNumber()
    {
        return "KON-C-" + SerialNumberInt;
    }
    
    
    public override void ListContainer()
    {
        Console.WriteLine("\n===Container: "+GetSerialNumber()+" has inside:===");
        if(Products.Count == 0) Console.WriteLine("Container is empty.");
        foreach(Product product in Products)
        {
            Console.WriteLine(product.name);
        }
        Console.WriteLine("Temperature: " + Temperature);
        Console.WriteLine("ProductType: " + ProductType);
        Console.WriteLine("===Total load mass: "+LoadMass+'\\'+MaxLoad+"kg===\n");
    }

    public override void EmptyContainer()
    {
        ProductType = null;
        base.EmptyContainer();
    }
    
}