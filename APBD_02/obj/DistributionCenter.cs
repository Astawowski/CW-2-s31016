namespace APBD_02;

public class DistributionCenter
{
    private int MaxSerialNumberInt { get; set; }
    public List<Container> ExistingContainers { get; set; }

    public DistributionCenter()
    {
        MaxSerialNumberInt = 0;
        ExistingContainers = new List<Container>();
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

}