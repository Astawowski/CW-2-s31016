namespace APBD_02;

public class Container4Gas : Container, IHazardNotifier
{
    protected double Pressure { get; set; }

    public Container4Gas(double high, double containerMass, double depth, double maxLoad, DistributionCenter dc, double pressure)
        : base(high, containerMass, depth, maxLoad, dc)
    {
        this.Pressure = pressure;
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
}