﻿using System;
using System.Linq;
using IBL;
using IBL.BO;

namespace ConsoleUiBL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBl bl = new BL.BL();


            MenuOptions.OpeningOptions ch;


            do
            {
                try { ch = MenuOptions.PrintOpeningMenu(); }
                catch (FormatException) { ch = MenuOptions.OpeningOptions.DEFAULT; }

                switch (ch)
                {
                    case MenuOptions.OpeningOptions.EXIT:
                        Console.WriteLine("\ngood bye!");
                        break;

                    case MenuOptions.OpeningOptions.ADD:
                        MenuOptions.InsertOptions IChoice;

                        do
                        {
                            try { IChoice = MenuOptions.PrintInsertMenu(); }
                            catch (FormatException) { IChoice = MenuOptions.InsertOptions.DEFAULT; }

                            switch (IChoice)
                            {
                                case MenuOptions.InsertOptions.BACK:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.InsertOptions.STATION:
                                    Console.WriteLine("Enter station ID: ");
                                    int stationId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter station name: ");
                                    string name = (Console.ReadLine());
                                    Console.WriteLine("Enter num of Charge Slots: ");
                                    int ChargeSlots = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude of stations adress: ");
                                    double longitude = (int)double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter lattitude of stations adress: ");
                                    double lattitude = (int)double.Parse(Console.ReadLine());

                                    try
                                    {
                                        bl.AddStation(new()
                                        {
                                            Id = stationId,
                                            Name = name,
                                            FreeChargeSlots = ChargeSlots,
                                            LocationOfStation = new()
                                            {
                                                Longitude = longitude,
                                                Lattitude = lattitude
                                            }
                                        });
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    //Output that displays the success of a request:
                                    bl.GetStations().ToList().ForEach(st => Console.WriteLine(st));
                                    break;

                                case MenuOptions.InsertOptions.DRONE:
                                    Console.WriteLine("Enter drone ID: ");
                                    int droneId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone model: ");
                                    string model = (Console.ReadLine());
                                    Console.WriteLine("Enter drone Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
                                    Weight maxWeight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter a station ID for initial charging: ");
                                    int firstStationId = int.Parse(Console.ReadLine());

                                    try
                                    {
                                        bl.AddDrone(
                                            new()
                                            {
                                                Id = droneId,
                                                Model = model,
                                                MaxWeight = maxWeight,
                                            },
                                        firstStationId);
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    //Output that displays the success of a request:
                                    bl.GetDrones().ToList().ForEach(dr => Console.WriteLine(dr));
                                    break;

                                case MenuOptions.InsertOptions.CUSTOMER:
                                    Console.WriteLine("Enter customer ID: ");
                                    int cusId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter customer name: ");
                                    string cusName = (Console.ReadLine());
                                    Console.WriteLine("Enter customer phone: ");
                                    string phone = (Console.ReadLine());
                                    Console.WriteLine("Enter longitude of customers adress: ");
                                    double cusLongitude = (int)double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter lattitude of customers adress: ");
                                    double cusLattitude = (int)double.Parse(Console.ReadLine());
                                    try
                                    {
                                        bl.AddCustomer(new()
                                        {
                                            Id = cusId,
                                            Name = cusName,
                                            Phone = phone,
                                            CustomerLocation = new()
                                            {
                                                Longitude = cusLongitude,
                                                Lattitude = cusLattitude
                                            }
                                        });
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    //Output that displays the success of a request:
                                    bl.GetCustomers().ToList().ForEach(cus => Console.WriteLine(cus));
                                    break;

                                case MenuOptions.InsertOptions.PACKAGE:
                                    Console.WriteLine("Enter target ID: ");
                                    int sendersId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter sender ID: ");
                                    int targetsId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter package Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
                                    Weight weight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter pariority - To NORMAL enter 0, to FAST enter 1 and to EMERENCY enter 2: ");
                                    Priorities priority = (Priorities)int.Parse(Console.ReadLine());

                                    bl.AddPackage(new()
                                    {
                                        SenderCustomerInPackage = sendersId,
                                        TargetCustomerInPackage = targetsId,
                                        Weight = weight,
                                        Priority = priority,
                                    });

                                    //Output that displays the success of a request:
                                    bl.GetPackages().ToList().ForEach(pck => Console.WriteLine(pck));
                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)IChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.UPDATE:
                        MenuOptions.UpdateOptions UChoice;

                        do
                        {
                            try { UChoice = MenuOptions.PrintUpdateMenu(); }
                            catch (FormatException) { UChoice = MenuOptions.UpdateOptions.DEFAULT; }

                            switch (UChoice)
                            {
                                case MenuOptions.UpdateOptions.BACK:
                                    Console.WriteLine("\n");

                                    break;

                                case MenuOptions.UpdateOptions.STATION:
                                    break;
                                case MenuOptions.UpdateOptions.DRONE:
                                    break;
                                case MenuOptions.UpdateOptions.CUSTOMER:
                                    break;
                                case MenuOptions.UpdateOptions.ASSOCIATION:
                                    Console.WriteLine("Enter drone ID: ");
                                    int assoDroneId = int.Parse(Console.ReadLine());

                                    bl.packageAssigning(assoDroneId);

                                    //Output that displays the success of a request:
                                    Console.WriteLine(bl.GetDrone(assoDroneId));

                                    break;
                                case MenuOptions.UpdateOptions.PICKING_UP:
                                    Console.WriteLine("Enter drone ID: ");
                                    int pickUpDroneId = int.Parse(Console.ReadLine());
                                    bl.PickUp(pickUpDroneId);

                                    //Output that displays the success of a request:
                                    Console.WriteLine(bl.GetDrone(pickUpDroneId));

                                    break;
                                case MenuOptions.UpdateOptions.SUPPLY:
                                    Console.WriteLine("Enter drone ID: ");
                                    int supDroneId = int.Parse(Console.ReadLine());

                                    bl.Deliver(supDroneId);

                                    //Output that displays the success of a request:
                                    Console.WriteLine(bl.GetDrone(supDroneId));

                                    break;
                                case MenuOptions.UpdateOptions.CHARGING:
                                    Console.WriteLine("Enter drone ID : ");
                                    int chDroneId = (int.Parse(Console.ReadLine()));

                                    bl.SendDroneForCharge(chDroneId);

                                    //Output that displays the success of a request:
                                    Console.WriteLine(bl.GetDrone(chDroneId));

                                    break;
                                case MenuOptions.UpdateOptions.RELEASE:

                                    Console.WriteLine("Enter drone ID : ");
                                    int reDroneId = (int.Parse(Console.ReadLine()));
                                    Console.WriteLine("Enter the number of charging hours:");
                                    int h = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter the number of charging minutes:");
                                    int m = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter the number of charging seconds:");
                                    int s = int.Parse(Console.ReadLine());

                                    bl.RealeseDroneFromCharge(reDroneId, new(h, m, s));

                                    //Output that displays the success of a request:
                                    Console.WriteLine(bl.GetDrone(reDroneId));

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)UChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.PRINT:
                        MenuOptions.DisplayOptions DChoice;

                        do
                        {
                            try { DChoice = MenuOptions.PrintDisplayMenu(); }
                            catch (FormatException) { DChoice = MenuOptions.DisplayOptions.DEFAULT; }

                            switch (DChoice)
                            {
                                case MenuOptions.DisplayOptions.BACK:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.DisplayOptions.STATION:
                                    Console.WriteLine("Enter station ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetStation(int.Parse(Console.ReadLine())));

                                    break;
                                case MenuOptions.DisplayOptions.DRONE:
                                    Console.WriteLine("Enter drone ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetDrone(int.Parse(Console.ReadLine())));

                                    break;
                                case MenuOptions.DisplayOptions.CUSTOMER:
                                    Console.WriteLine("Enter customer ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetCustomer(int.Parse(Console.ReadLine())));

                                    break;
                                case MenuOptions.DisplayOptions.PACKAGE:
                                    Console.WriteLine("Enter package ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetPackage(int.Parse(Console.ReadLine())));

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)DChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.PRINT_LISTS:
                        MenuOptions.ListViewOptions LVChoice;

                        do
                        {
                            try { LVChoice = MenuOptions.PrintListViewMenu(); }
                            catch (FormatException) { LVChoice = MenuOptions.ListViewOptions.DEFAULT; }

                            switch (LVChoice)
                            {
                                case MenuOptions.ListViewOptions.BACK:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.ListViewOptions.STATIONS:
                                    var st1 = dalObject.GetStations();
                                    foreach (var st in st1)
                                    {
                                        Console.WriteLine("\n" + st);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.DRONES:
                                    foreach (var dr in dalObject.GetDrones())
                                    {
                                        Console.WriteLine("\n" + dr);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.CUSTOMERS:
                                    foreach (var cus in dalObject.GetCustomers())
                                    {
                                        Console.WriteLine("\n" + cus);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.PACKAGES:
                                    foreach (var pck in dalObject.GetPackages())
                                    {
                                        Console.WriteLine("\n" + pck);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.UNASSIGNED_PACKAGES:
                                    foreach (var pck in dalObject.GetPackages(x => x.Scheduled == DateTime.MinValue))
                                    {
                                        Console.WriteLine("\n" + pck);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.AVAILABLE_FOR_CHARGING:
                                    foreach (var st in dalObject.GetStations(x => x.FreeChargeSlots != 0))
                                    {
                                        Console.WriteLine("\n" + st);
                                    }

                                    break;
                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)LVChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.DISTANCE:
                        MenuOptions.DistanceOptions DiChoice;

                        do
                        {

                            try { DiChoice = MenuOptions.PrintDistanceMenu(); }
                            catch (FormatException) { DiChoice = MenuOptions.DistanceOptions.DEFAULT; }

                            double lattitude, longitude;

                            switch (DiChoice)
                            {
                                case MenuOptions.DistanceOptions.BACK:
                                    Console.WriteLine("\n");
                                    break;
                                case MenuOptions.DistanceOptions.STATION:

                                    Console.WriteLine("Enter lattitude: ");
                                    lattitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude: ");
                                    longitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter station ID to calculate from: ");
                                    Station station = dalObject.GetStation(int.Parse(Console.ReadLine()));
                                    Console.WriteLine("\nthe distance between " + lattitude + "\u00B0N ," + longitude + "\u00B0E to station " + station.Id + " is " + Distance(lattitude, longitude, station.Lattitude, station.Longitude) + " KM");

                                    break;
                                case MenuOptions.DistanceOptions.CUSTOMER:
                                    Console.WriteLine("Enter lattitude: ");
                                    lattitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude: ");
                                    longitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter customer ID to calculate from: ");
                                    Customer customer = dalObject.GetCustomer(int.Parse(Console.ReadLine()));
                                    Console.WriteLine("\nthe distance between " + lattitude + "\u00B0N ," + longitude + "\u00B0E to station " + customer.Id + " is " + Distance(lattitude, longitude, customer.Lattitude, customer.Longitude) + " KM");

                                    break;
                                case MenuOptions.DistanceOptions.DEFAULT:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                                default:
                                    break;
                            }

                        } while ((int)DiChoice != 0);

                        break;
                    default:
                        Console.WriteLine("\nERROR: invalid choice\n");
                        break;
                }

            }
            while ((int)ch != 0);

            Console.Read();

        }



    }
}
