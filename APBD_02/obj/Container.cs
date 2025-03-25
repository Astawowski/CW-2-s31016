namespace APBD_02.obj;

public abstract class Container{
    public double LoadMass { get; set; }
    protected double High;
    public double ContainerTaraMass { get; set; }
    protected double Depth;
    protected int SerialNumberInt { get; }  
    protected double MaxLoad { get; set; }
    protected List<Product> Products { get; set; }  
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
        Console.WriteLine("The container: " + GetSerialNumber() + " has been loaded with: "+newLoadMass+"kg of: "+ product.Name+"\nCurrState: "+LoadMass+'\\'+MaxLoad+"kg");
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
            Console.WriteLine(product.Name);
        }
        Console.WriteLine("===Total load mass: "+LoadMass+'\\'+MaxLoad+"kg===\n");
    }
    
}