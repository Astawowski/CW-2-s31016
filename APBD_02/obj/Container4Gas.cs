namespace APBD_02.obj;

public class Container4Gas : Container, IHazardNotifier
{
    protected double Pressure { get; set; }

    public Container4Gas(double high, double containerMass, double depth, double maxLoad, DistributionCenter dc, double pressure)
        : base(high, containerMass, depth, maxLoad, dc)
    {
        Pressure = pressure;
    }

    public void Notify()
    {
        Console.WriteLine("Hazardous action was about to be performed on container: "+GetSerialNumber());
    }
    
    public override void EmptyContainer()
    {
        LoadMass *= 0.05;
        Console.WriteLine("The container: " + GetSerialNumber() + " has been emptied. " +"CurrState: "+LoadMass+'\\'+MaxLoad+"kg");
    }
    
    public override string GetSerialNumber()
    {
        return "KON-G-" + SerialNumberInt;
    }
    
    
    public override void ListContainer()
    {
        Console.WriteLine("\n===Container: "+GetSerialNumber()+" has inside:===");
        if(Products.Count == 0) Console.WriteLine("Container is empty.");
        foreach(Product product in Products)
        {
            Console.WriteLine(product.Name);
        }
        Console.WriteLine("Pressure: "+Pressure+ " atm");
        Console.WriteLine("===Total load mass: "+LoadMass+'\\'+MaxLoad+"kg===\n");
    }
}