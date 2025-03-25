namespace APBD_02.obj;

public class PortSystem
{
    public static void Main()
    {
        DistributionCenter dc = new DistributionCenter();
        
        // ===============================TEST=======================================================
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("========================Rozpoczęcie pokazu testowego=================================");
        Console.ResetColor();
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
        Console.WriteLine("\n==========Kontenerowiec Tytanik========");
        ContainerShip cs01 = new ContainerShip(2, 2, 30, "Tytanik",dc);
        cs01.ListContainers();
        cs01.AddContainer("KON-L-0",dc);
        cs01.ListContainers();
        cs01.AddContainer("KON-D-0",dc); // Nieistniejący kontener
        cs01.AddContainer("KON-G-1",dc);
        cs01.AddContainer("KON-C-2",dc); // Przekroczenie max ilości kontenerów
        cs01.ListContainers();
        
        Console.WriteLine("\n==========Kontenerowiec Barabasz========");
        ContainerShip cs02 = new ContainerShip(2, 2, 30, "Barabasz",dc);
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
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("========================Koniec pokazu testowego=================================\n\n");
        Console.ResetColor();
        //=======================================================================================================
        
        ShowHelp();
        
        while (true)
        {
            Console.WriteLine("\nPodaj komendę.  ('Help' by wyświetlić dostępne komendy) ('-1' by zakończyć)");
            string? command = Console.ReadLine();
            if (command == "-1") break;
            if (command == "Help") ShowHelp();
            else if (command == "CreateC") CreateContainer(dc);
            else if (command == "LoadC")  LoadContainer(dc);
            else if (command == "EmptyC") EmptyContainer(dc);
            else if (command == "ListC") ListContainers(dc);
            else if (command == "ShowC") ShowContainer(dc);
            else if (command == "CreateShip") CreateContainerShip(dc);
            else if (command == "ListShips") ListContainersShips(dc);
            else if (command == "LoadShip1") AddContainer(dc,0);
            else if (command == "LoadShipMany") AddContainer(dc,1);
            else if (command == "UnloadShip") RemoveContainer(dc);
            else if (command == "ShowShip") ShowContainerShip(dc);
            else if (command == "ReplaceC") ReplaceContainer(dc);
            else if (command == "TransferC") TransferContainer(dc);
            else Console.WriteLine("Nieznana komenda.");
        }
    }
    
    
    
    private static void ShowHelp()
    {
        Console.WriteLine("\n\nDostępne akcje:");
        DisplayOption("Utwórz kontener", "CreateC");
        DisplayOption("Załaduj kontener", "LoadC");
        DisplayOption("Opróżnij kontener", "EmptyC");
        DisplayOption("Wyświetl listę wszystkich kontenerów", "ListC");
        DisplayOption("Wyświetl informacje o kontenerze", "ShowC");
        DisplayOption("Utwórz statek kontenerowy", "CreateShip");
        DisplayOption("Wyświetl listę wszystkich statków kontenerowych", "ListShips");
        DisplayOption("Załaduj pojedynczy kontener na statek", "LoadShip1");
        DisplayOption("Załaduj wiele kontenerów na statek", "LoadShipMany");
        DisplayOption("Usuń kontener ze statku", "UnloadShip");
        DisplayOption("Wymień kontener na statku", "ReplaceC");
        DisplayOption("Przenieś kontener z jednego statku na inny", "TransferC");
        DisplayOption("Wyświetl informacje o statku", "ShowShip");
    }
    
    private static void CreateContainer(DistributionCenter dc)
    {
        Console.WriteLine("Podaj parametr: Wysokość.");
        double high = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj parametr: Masa kontenera.");
        double containerMass = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj parametr: Głębokość.");
        double depth = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj parametr: Maksymalna ładowność.");
        double loadMass = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj typ kontenera: 'Liquid' / 'Gas' / 'Cooled'");
        string? containerType = Console.ReadLine();
        if (containerType == "Liquid")
        {
            new Container4Liquid(high, containerMass, depth, loadMass, dc);
        }
        else if (containerType == "Gas")
        {
            Console.WriteLine("Podaj parametr: Ciśnienie.");
            double pressure = double.Parse(Console.ReadLine()!);
            new Container4Gas(high, containerMass, depth, loadMass, dc, pressure);
        }
        else if (containerType == "Cooled")
        {
            Console.WriteLine("Podaj parametr: Temperatura.");
            double temperature = double.Parse(Console.ReadLine()!);
            new Container4Cooled(high, containerMass, depth, loadMass, dc, temperature);
        }
        else
        {
            Console.WriteLine("Nieznany typ kontenera.");
            return;
        }
        Console.WriteLine("Utworzono nowy kontener");
    }
    
    private static void LoadContainer(DistributionCenter dc)
    {
        Console.WriteLine("Podaj numer seryjny kontenera do załadowania:");
        string? containerSn = Console.ReadLine();
        Console.WriteLine("Podaj nazwe produktu do załadowania:");
        string? productName = Console.ReadLine();
        Console.WriteLine("Podaj minimalną wymaganą temperature.");
        double minTemperature = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Czy produkt niebezpieczny? 'true' or 'false'");
        bool hazardous = bool.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj masę produktu:");
        double mass = double.Parse(Console.ReadLine()!);
        dc.GetContainer(containerSn!)!.LoadContainer(mass,new Product(productName!, minTemperature, hazardous));
    }
    
    private static void EmptyContainer(DistributionCenter dc)
    {
        Console.WriteLine("Podaj numer seryjny kontenera do wyświetlenia:");
        string? containerSn = Console.ReadLine();
        dc.GetContainer(containerSn!)!.EmptyContainer();
    }
    
    private static void ListContainers(DistributionCenter dc)
    {
        Console.WriteLine("Utworzone kontenery: ");
        foreach (var container in dc.ExistingContainers) Console.WriteLine("-"+container.GetSerialNumber()+ " w statku: "+(container.TransportingShip != null ? container.TransportingShip.ShipName : "null"));
        if(dc.ExistingContainers.Count == 0) Console.WriteLine("Brak istniejących kontenerów.\n");
    }
    
    private static void ShowContainer(DistributionCenter dc)
    {
        Console.WriteLine("Podaj numer seryjny kontenera do wyświetlenia:");
        string? containerSn = Console.ReadLine();
        dc.GetContainer(containerSn!)!.ListContainer();
    }
    
    private static void CreateContainerShip(DistributionCenter dc)
    {
        Console.WriteLine("Podaj parametr: Max Prędkość.");
        double maxSpeed = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj parametr: Max ilość kontenerów.");
        int maxContainerCount = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj parametr: Max ładowność statku.");
        double maxContainersMass = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Podaj parametr: Nazwa statku.");
        string shipName = Console.ReadLine()!;
        new ContainerShip(maxSpeed, maxContainerCount, maxContainersMass, shipName, dc);
        Console.WriteLine("Utworzono nowy kontenerowiec.");
    }
    
    private static void ListContainersShips(DistributionCenter dc)
    {
        Console.WriteLine("Utworzone kontenerowce: ");
        foreach (var containerShip in dc.ExistingContainerShips) Console.WriteLine("-"+containerShip.ShipName);
        if(dc.ExistingContainerShips.Count == 0) Console.WriteLine("Brak istniejących kontenerowców.\n");
    }
    
    private static void AddContainer(DistributionCenter dc, int loop)
    {
        int i = 1;
        Console.WriteLine("Podaj nazwę kontenerowca:");
        string shipName = Console.ReadLine()!;
        while (i + loop > 0)
        {
            Console.WriteLine("Podaj numer seryjny kontenera do dodania");
            string containerSn = Console.ReadLine()!;
            if (containerSn == "-1") return;
            dc.GetContainerShip(shipName)!.AddContainer(containerSn, dc);
            i--;
            if(loop > 0) Console.WriteLine("'-1' By przerwać.");
        }
    }
    
    private static void RemoveContainer(DistributionCenter dc)
    {
        Console.WriteLine("Podaj nazwę kontenerowca:");
        string shipName = Console.ReadLine()!;
        Console.WriteLine("Podaj numer seryjny kontenera do usunięcia");
        string containerSn = Console.ReadLine()!;
        dc.GetContainerShip(shipName!)!.RemoveContainer(containerSn,dc);
    }
    
    private static void ReplaceContainer(DistributionCenter dc)
    {
        Console.WriteLine("Podaj nazwę kontenerowca, na którym chcesz podmienić kontener.");
        string shipName = Console.ReadLine()!;
        Console.WriteLine("Podaj numer seryjny kontenera, który chcesz odstawić.");
        string containerSn1 = Console.ReadLine()!;
        Console.WriteLine("Podaj numer seryjny kontenera, który chcesz wstawić.");
        string containerSn2 = Console.ReadLine()!;
        dc.GetContainerShip(shipName)!.RemoveContainer(containerSn1,dc);
        dc.GetContainerShip(shipName)!.AddContainer(containerSn2,dc);
    }

    private static void TransferContainer(DistributionCenter dc)
    {
        Console.WriteLine("Podaj nazwę kontenerowca, z którego chcesz zabrać kontener.");
        string shipNameFrom = Console.ReadLine()!;
        Console.WriteLine("Podaj nazwę kontenerowca, na który chcesz dodać ten kontener.");
        string shipNameTo = Console.ReadLine()!;
        Console.WriteLine("Podaj numer seryjny kontenera, który chcesz przetransferować.");
        string containerSn = Console.ReadLine()!;
        dc.GetContainerShip(shipNameFrom)!.RemoveContainer(containerSn,dc);
        dc.GetContainerShip(shipNameTo)!.AddContainer(containerSn,dc);
    }
    
    private static void ShowContainerShip(DistributionCenter dc)
    {
        Console.WriteLine("Podaj nazwę kontenerowca:");
        string shipName = Console.ReadLine()!;
        dc.GetContainerShip(shipName)!.ListContainers();
    }

    
    private static void TransferContainer(ContainerShip from, ContainerShip to, string containerSn, DistributionCenter dc)
    {
        from.RemoveContainer(containerSn,dc);
        to.AddContainer(containerSn,dc);
    }
    
    private static void ReplaceContainer(string container1, string container2, ContainerShip ship, DistributionCenter dc)
    {
        ship.RemoveContainer(container1,dc);
        ship.AddContainer(container2,dc);
    }
    
    static void DisplayOption(string description, string command)
    {
        Console.Write($"- {description}: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"'{command}'");
        Console.ResetColor();
    }
}