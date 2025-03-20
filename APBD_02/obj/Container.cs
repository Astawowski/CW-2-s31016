namespace APBD_02;

public abstract class Container{
    public double LoadMass { get; set; }    // by default the container is empty
    protected double High { get; set; }
    public double ContainerTaraMass { get; set; }
    protected double Depth { get; set; }
    protected int SerialNumberInt { get; }  // automatically assigned
    protected double MaxLoad { get; set; }
    protected List<Product> Products { get; set; }  // empty container has no products inside
    public ContainerShip? TransportingShip { get; set; }

    protected Container(double high, double containerMass, double depth, double maxLoad, DistributionCenter dc)
    {
        LoadMass = 0f;
        High = high;
        ContainerTaraMass = containerMass;
        Depth = depth;
        SerialNumberInt = dc.GetMaxSerialNumberInt();
        MaxLoad = maxLoad;
        Products = new List<Product>();
        dc.ExistingContainers.Add(this);
    }

    public virtual void EmptyContainer()
    {
        LoadMass = 0f;
        Products = new List<Product>(); 
        Console.WriteLine("The container: " + GetSerialNumber() + " has been emptied. " +"CurrState: "+LoadMass+'\\'+MaxLoad+"kg");
    }

    public virtual void LoadContainer(double newLoadMass, Product product)
    {
        if(LoadMass + newLoadMass > MaxLoad)
            throw new OverfillException("Load is too high for the container: "+ GetSerialNumber());
        LoadMass += newLoadMass;
        Products.Add(product);
        Console.WriteLine("The container: " + GetSerialNumber() + " has been loaded with: "+newLoadMass+"kg of: "+ product.name+"\nCurrState: "+LoadMass+'\\'+MaxLoad+"kg");
    }
    
    public virtual string GetSerialNumber()
    {
        return "KON-A-" + SerialNumberInt;
    }

    public virtual void ListContainer()
    {
        Console.WriteLine("\n===Container: "+GetSerialNumber()+" has inside:===");
        if(Products.Count == 0) Console.WriteLine("Container is empty.");
        foreach(Product product in Products)
        {
            Console.WriteLine(product.name);
        }
        Console.WriteLine("===Total load mass: "+LoadMass+'\\'+MaxLoad+"kg===\n");
    }
    
}