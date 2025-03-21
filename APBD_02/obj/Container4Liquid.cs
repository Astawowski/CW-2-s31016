namespace APBD_02;

public class Container4Liquid : Container, IHazardNotifier
{
	protected bool HasHazardousLoad { set; get; }
    
    public Container4Liquid(double high, double containerMass, double depth, double maxLoad, DistributionCenter dc)
        : base(high, containerMass, depth, maxLoad, dc)
    {
        HasHazardousLoad = false;
    }

    public override void LoadContainer(double newLoadMass, Product productType)
    {
        if (HasHazardousLoad||productType.HazardousLoad)
        {
            if (LoadMass + newLoadMass > 0.5 * MaxLoad)
            {
                Notify();
                return;
            }
            
        }else if (LoadMass + newLoadMass > 0.9 * MaxLoad)
        {
            Notify();
            return;
        }
        if (productType.HazardousLoad) HasHazardousLoad = true;
        base.LoadContainer(newLoadMass, productType);
    }

    public override void EmptyContainer()
    {
        HasHazardousLoad = false;
        base.EmptyContainer();
    }

    public override string GetSerialNumber()
    {
        return "KON-L-" + SerialNumberInt;
    }


    public override void ListContainer()
    {
        Console.WriteLine("\n===Container: "+GetSerialNumber()+" has inside:===");
        if(Products.Count == 0) Console.WriteLine("Container is empty.");
        foreach(Product product in Products)
        {
            Console.WriteLine(product.name);
        }
        Console.WriteLine("Hazardous Load: " + HasHazardousLoad);
        Console.WriteLine("===Total load mass: "+LoadMass+'\\'+MaxLoad+"kg===\n");
    }

    public void Notify()
    {
        Console.WriteLine("Hazardous action was about to be performed on container: "+GetSerialNumber());
    }
    
}