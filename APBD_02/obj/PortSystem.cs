namespace APBD_02.obj;

public class PortSystem
{
    public static void Main()
    {
        DistributionCenter dc = new DistributionCenter();
        
        // =========Tworzenie kontenerów i ich załadywania oraz opróżnianie=============
        Console.WriteLine("\n=======Kontener na Płyny===========");
        Container4Liquid c01 = new Container4Liquid(2, 1500, 15, 15000, dc); 
        c01.LoadContainer(14000,new Product("Olej",false));  // Nie zadziała bo >90% load
        c01.LoadContainer(1000,new Product("Wódka Parkowa",true)); // Zadziała i zmieni HasHazardousLoad -> true
        c01.LoadContainer(12000,new Product("Piwo",false)); // Nie zadziała bo >50% load
        c01.LoadContainer(1000,new Product("Ocet",false)); 
        c01.ListContainer();
        c01.EmptyContainer();
        c01.ListContainer();
        
        Console.WriteLine("\n=======Kontener na Gaz===========");
        Container4Gas c02 = new Container4Gas(2, 1500, 15, 15000, dc,25);
        c02.LoadContainer(10000,new Product("Butan"));
        c02.ListContainer();
        c02.EmptyContainer();
        c02.ListContainer(); // 5% ładunku pozostanie
        
        Console.WriteLine("\n=======Kontener chłodniczy===========");
        Container4Cooled c03 = new Container4Cooled(2, 1500, 15, 15000, dc,12);
        c03.LoadContainer(10000,new Product("Banany",13.3)); // Nie zadziała bo za niska temperature
        c03.LoadContainer(10000, new Product("Frozen Pizza", -30));
        c03.LoadContainer(1000, new Product("Frozen Pizza", -30));
        c03.LoadContainer(1000, new Product("Lody", -20)); // Nie zadziała bo to już kontener na Pizze
        c03.ListContainer();
        c03.EmptyContainer();
        c03.LoadContainer(1000, new Product("Lody", -20)); // Teraz już zadziała
        
        
        // ======== Załadowywanie kontenerów na kontenerowce, transfer i ich rozładywanie ==========
        ContainerShip cs01 = new ContainerShip(2, 2, 30, "Tytanik");
        cs01.ListContainers();
        cs01.AddContainer("KON-L-0",dc);
        cs01.ListContainers();
        cs01.AddContainer("KON-D-0",dc); // Nieistniejący kontener
        cs01.AddContainer("KON-G-1",dc);
        cs01.AddContainer("KON-C-2",dc); // Przekroczenie max ilości kontenerów
        cs01.ListContainers();
        
        ContainerShip cs02 = new ContainerShip(2, 2, 30, "Barabasz");
        cs02.AddContainer("KON-G-1",dc); // Kontener już w innym statku
        TransferContainer(cs01,cs02,"KON-G-1",dc);   // Transfer kontenera z jednego statku na drugi
        cs02.ListContainers();
        
        cs02.RemoveContainer("KON-G-1",dc);  // Usuniecie kontenera ze staku
        List<string> containers2Add = new List<string>();
        containers2Add.Add("KON-G-1");
        containers2Add.Add("KON-C-2");
        cs02.AddContainers(containers2Add,dc); // Dodawanie listy kontenerów
        cs02.RemoveContainer("KON-G-1",dc); 
        ReplaceContainer("KON-C-2","KON-G-1",cs02,dc); // Podmiana kontenerów
        
    }

    public static void TransferContainer(ContainerShip from, ContainerShip to, string containerSn, DistributionCenter dc)
    {
        from.RemoveContainer(containerSn,dc);
        to.AddContainer(containerSn,dc);
    }

    public static void ReplaceContainer(string container1, string container2, ContainerShip ship, DistributionCenter dc)
    {
        ship.RemoveContainer(container1,dc);
        ship.AddContainer(container2,dc);
    }
}