namespace APBD_02.obj;

public class DistributionCenter
{
    private int MaxSerialNumberInt { get; set; }
    public List<Container> ExistingContainers { get; set; }
    public List<ContainerShip> ExistingContainerShips { get; set; }
    
    public DistributionCenter()
    {
        MaxSerialNumberInt = 0;
        ExistingContainers = new List<Container>();
        ExistingContainerShips = new List<ContainerShip>();
    }


    public int GetMaxSerialNumberInt()
    {
        int maxSn = MaxSerialNumberInt++;
        return maxSn;
    }

    public Container? GetContainer(String containerSn)
    {
        Container? container = null;
        foreach (Container c in ExistingContainers)
        {
            if (c.GetSerialNumber() == containerSn)
            {
                container = c;
                break;
            }
        }
        if (container != null) return container;
        Console.WriteLine("The container you are searching for does not exist.");
        return null;
    }
    
    public ContainerShip? GetContainerShip(String containerShipName)
    {
        ContainerShip? containerShip = null;
        foreach (ContainerShip c in ExistingContainerShips)
        {
            if (c.ShipName == containerShipName)
            {
                containerShip = c;
                break;
            }
        }
        if (containerShip != null) return containerShip;
        Console.WriteLine("The ContainerShip you are searching for does not exist.");
        return null;
    }

}