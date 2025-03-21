namespace APBD_02;

public class ContainerShip
{
    protected List<Container> Containers { get; set; }
    protected double MaxSpeed { get; set; }
    protected int MaxContainersCount { get; set; }
    protected double MaxContainersWeight { get; set; }
    public string ShipName { get; set; }

    public ContainerShip(double maxSpeed, int maxContainerCount, double maxContainerWeight, string shipName)
    {
        MaxSpeed = maxSpeed;
        MaxContainersCount = maxContainerCount;
        MaxContainersWeight = maxContainerWeight;
        Containers = new List<Container>();
        ShipName = shipName;
    }

    
    
    public void AddContainer(String containerSn, DistributionCenter dc)
    {
        Container? container = dc.GetContainer(containerSn);
        if (container == null) return;

        if (container.TransportingShip != null)
        {
            Console.WriteLine("The container is already being transported by different ship: "+container.TransportingShip.ShipName);
            return;
        }
        
        double totalContainersMass = 0f;
        foreach (Container c in Containers)
        {
            totalContainersMass += (c.LoadMass + c.ContainerTaraMass);
        }
        
        if ((totalContainersMass + container.LoadMass + container.ContainerTaraMass > MaxContainersWeight*1000)
            || Containers.Count() + 1 > MaxContainersCount)
        {
            Console.WriteLine("Could not load countainer onto the container ship because either the TotalContainersMass or " +
                              "ContainersCount is greater than allowed.");
            return;
        }
        Containers.Add(container);
        container.TransportingShip = this;

        Console.WriteLine("Container: " +container.GetSerialNumber()+" has been added to the ship: "+ShipName);
    }


    
    public void AddContainers(List<string> containersSn, DistributionCenter dc)
    {
        foreach (string containerSn in containersSn) AddContainer(containerSn, dc);
    }

    
    
    
    public void RemoveContainer(String containerSn, DistributionCenter dc)
    {
        Container? container = dc.GetContainer(containerSn);
        if (container == null) return;

        if (container.TransportingShip != this)
        {
            Console.WriteLine("This ship is not transporting this container.");
            return;
        }

        for (int i = 0; i < Containers.Count; i++)
        {
            if (Containers[i].GetSerialNumber() == containerSn)
            {
                Containers.RemoveAt(i);
                break;
            }
                
        }
        container.TransportingShip = null;
        Console.WriteLine("Container: " +container.GetSerialNumber()+" has been removed from the ship: "+ShipName);
    }

    
    
    public void ListContainers()
    {
        Console.WriteLine("\nShip: "+ShipName+" has containers: ");
        if(Containers.Count==0) Console.WriteLine("Ship has no containers yet.");
        foreach (Container container in Containers)
        {
            Console.WriteLine(container.GetSerialNumber());
        }
    }
    
    
}