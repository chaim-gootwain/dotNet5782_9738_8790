﻿using System;
using System.Collections.Generic;
using IDAL;
using IDAL.DO;



namespace ConsoleUI
{

    class Program
    {

        static void Main(string[] args)
        {
            IDal dalObject = new DalObject.DalObject();

            MenuOptions.OpeningOptions ch;


            do
            {
                try { ch = MenuOptions.PrintOpeningMenu(); }
                catch (FormatException) { ch = MenuOptions.OpeningOptions.Default; }

                switch (ch)
                {
                    case MenuOptions.OpeningOptions.Exit:
                        Console.WriteLine("\ngood bye!");
                        break;

                    case MenuOptions.OpeningOptions.Add:
                        MenuOptions.InsertOptions IChoice;

                        do
                        {
                            try { IChoice = MenuOptions.PrintInsertMenu(); }
                            catch (FormatException) { IChoice = MenuOptions.InsertOptions.Default; }

                            switch (IChoice)
                            {
                                case MenuOptions.InsertOptions.Back:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.InsertOptions.Station:
                                    Console.WriteLine("Enter station ID: ");
                                    int stationId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter station name: ");
                                    string name = (Console.ReadLine());
                                    Console.WriteLine("Enter num of free station: ");
                                    int numOfFreeStation = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude of stations adress: ");
                                    double longitude = (int)double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter lattitude of stations adress: ");
                                    double lattitude = (int)double.Parse(Console.ReadLine());

                                    try
                                    {
                                        dalObject.AddStation(new()
                                        {
                                            Id = stationId,
                                            Name = name,
                                            FreeChargeSlots = numOfFreeStation,
                                            Longitude = longitude,
                                            Lattitude = lattitude
                                        });
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    break;

                                case MenuOptions.InsertOptions.Drone:
                                    Console.WriteLine("Enter drone ID: ");
                                    int droneId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone model: ");
                                    string model = (Console.ReadLine());
                                    Console.WriteLine("Enter drone Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
                                    Weight maxWeight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone status - To  AVAILABLE  enter 0, to MAINTENANCE enter 1 and to DELIVERY enter 2: ");

                                    try
                                    {
                                        dalObject.AddDrone(new()
                                        {
                                            Id = droneId,
                                            Model = model,
                                            MaxWeight = maxWeight,
                                        });
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    break;

                                case MenuOptions.InsertOptions.Customer:
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
                                        dalObject.AddCustomer(new()
                                        {
                                            Id = cusId,
                                            Name = cusName,
                                            Phone = phone,
                                            Longitude = cusLongitude,
                                            Lattitude = cusLattitude
                                        });
                                    }
                                    catch (ExistsNumberException ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    break;

                                case MenuOptions.InsertOptions.Package:
                                    Console.WriteLine("Enter target ID: ");
                                    int sendersId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter sender ID: ");
                                    int targetsId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter package Weight - To LIGHT enter 0, to MEDIUM enter 1 and to HEAVY enter 2: ");
                                    Weight weight = (Weight)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter pariority - To NORMAL enter 0, to FAST enter 1 and to EMERENCY enter 2: ");
                                    Priorities priority = (Priorities)int.Parse(Console.ReadLine());

                                    dalObject.AddPackage(new()
                                    {
                                        SenderId = sendersId,
                                        TargetId = targetsId,
                                        Weight = weight,
                                        Priority = priority,
                                        Requested = DateTime.Now,
                                        Scheduled = new DateTime(0,0,0),
                                        PickedUp = new DateTime(0, 0, 0),
                                        Delivered = new DateTime(0, 0, 0),
                                        DroneId = -1
                                    });

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)IChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.Update:
                        MenuOptions.UpdateOptions UChoice;

                        do
                        {
                            try { UChoice = MenuOptions.PrintUpdateMenu(); }
                            catch (FormatException) { UChoice = MenuOptions.UpdateOptions.Default; }

                            switch (UChoice)
                            {
                                case MenuOptions.UpdateOptions.Back:
                                    Console.WriteLine("\n");

                                    break;

                                case MenuOptions.UpdateOptions.Association:
                                    Console.WriteLine("Enter package ID  for associating: ");
                                    int id = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter drone ID: ");
                                    int droneId = int.Parse(Console.ReadLine());

                                    dalObject.ConnectPackageToDrone(id, droneId);

                                    break;
                                case MenuOptions.UpdateOptions.PickingUp:
                                    Console.WriteLine("Enter package ID for picking up: ");
                                    dalObject.PickUp(int.Parse(Console.ReadLine()));

                                    break;
                                case MenuOptions.UpdateOptions.Supply:
                                    Console.WriteLine("Enter package ID for supply : ");
                                    Package pck = dalObject.GetPackage(int.Parse(Console.ReadLine()));

                                    dalObject.PackageDeliver(pck.Id);

                                    break;
                                case MenuOptions.UpdateOptions.Charging:
                                    Console.WriteLine("Enter drone ID for charge : ");
                                    int drId = (int.Parse(Console.ReadLine()));
                                    Console.WriteLine("Enter sttion ID for charge : ");
                                    int stId = (int.Parse(Console.ReadLine()));
                                    dalObject.UsingChargingStation(stId);
                                    dalObject.AddDroneCharge(new()
                                    {
                                        DroneId = drId,
                                        StationId = stId
                                    });

                                    break;
                                case MenuOptions.UpdateOptions.Release:

                                    Console.WriteLine("Enter drone ID for release : ");
                                    DroneCharge drCh = dalObject.GetDroneCharge(int.Parse(Console.ReadLine()));


                                    dalObject.RealeseChargingStation(drCh.StationId);
                                    dalObject.DeleteDroneCharge(drCh.DroneId);

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)UChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.Print:
                        MenuOptions.DisplayOptions DChoice;

                        do
                        {
                            try { DChoice = MenuOptions.PrintDisplayMenu(); }
                            catch (FormatException) { DChoice = MenuOptions.DisplayOptions.Default; }

                            switch (DChoice)
                            {
                                case MenuOptions.DisplayOptions.Back:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.DisplayOptions.Station:
                                    Console.WriteLine("Enter station ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetStation(int.Parse(Console.ReadLine())));

                                    break;
                                case MenuOptions.DisplayOptions.Drone:
                                    Console.WriteLine("Enter drone ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetDrone(int.Parse(Console.ReadLine())));

                                    break;
                                case MenuOptions.DisplayOptions.Customer:
                                    Console.WriteLine("Enter customer ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetCustomer(int.Parse(Console.ReadLine())));

                                    break;
                                case MenuOptions.DisplayOptions.Package:
                                    Console.WriteLine("Enter package ID to see: ");
                                    Console.WriteLine("\n" + dalObject.GetPackage(int.Parse(Console.ReadLine())));

                                    break;

                                default:
                                    Console.WriteLine("\nERROR: invalid choice\n");
                                    break;
                            }

                        } while ((int)DChoice != 0);

                        break;

                    case MenuOptions.OpeningOptions.PrintLists:
                        MenuOptions.ListViewOptions LVChoice;

                        do
                        {
                            try { LVChoice = MenuOptions.PrintListViewMenu(); }
                            catch (FormatException) { LVChoice = MenuOptions.ListViewOptions.Default; }

                            switch (LVChoice)
                            {
                                case MenuOptions.ListViewOptions.Back:
                                    Console.WriteLine("\n");

                                    break;
                                case MenuOptions.ListViewOptions.Stations:
                                    var st1 = dalObject.GetStations();
                                    foreach (var st in st1)
                                    {
                                        Console.WriteLine("\n" + st);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.Drones:
                                    foreach (var dr in dalObject.GetDrones())
                                    {
                                        Console.WriteLine("\n" + dr);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.Customers:
                                    foreach (var cus in dalObject.GetCustomers())
                                    {
                                        Console.WriteLine("\n" + cus);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.Packages:
                                    foreach (var pck in dalObject.GetPackages())
                                    {
                                        Console.WriteLine("\n" + pck);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.UnassignedPackages:
                                    foreach (var pck in dalObject.GetPackages(x => x.Scheduled == DateTime.MinValue))
                                    {
                                        Console.WriteLine("\n" + pck);
                                    }

                                    break;
                                case MenuOptions.ListViewOptions.AvailableForCharging:
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

                    case MenuOptions.OpeningOptions.Distance:
                        MenuOptions.DistanceOptions DiChoice;

                        do
                        {

                            try { DiChoice = MenuOptions.PrintDistanceMenu(); }
                            catch (FormatException) { DiChoice = MenuOptions.DistanceOptions.Default; }

                            double lattitude, longitude;

                            switch (DiChoice)
                            {
                                case MenuOptions.DistanceOptions.Back:
                                    Console.WriteLine("\n");
                                    break;
                                case MenuOptions.DistanceOptions.Station:

                                    Console.WriteLine("Enter lattitude: ");
                                    lattitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude: ");
                                    longitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter station ID to calculate from: ");
                                    Station station = dalObject.GetStation(int.Parse(Console.ReadLine()));
                                    Console.WriteLine("\nthe distance between " + lattitude + "\u00B0N ," + longitude + "\u00B0E to station " + station.Id + " is " + Distance(lattitude, longitude, station.Lattitude, station.Longitude) + " KM");

                                    break;
                                case MenuOptions.DistanceOptions.Customer:
                                    Console.WriteLine("Enter lattitude: ");
                                    lattitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter longitude: ");
                                    longitude = double.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter customer ID to calculate from: ");
                                    Customer customer = dalObject.GetCustomer(int.Parse(Console.ReadLine()));
                                    Console.WriteLine("\nthe distance between " + lattitude + "\u00B0N ," + longitude + "\u00B0E to station " + customer.Id + " is " + Distance(lattitude, longitude, customer.Lattitude, customer.Longitude) + " KM");

                                    break;
                                case MenuOptions.DistanceOptions.Default:
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

        /// <summary>
        /// Calculate the distance (KM) between two received locations 
        /// according to their coordinates,
        /// Using a distance calculation formula
        /// </summary>
        /// <param name="latStart">Start latitude on the map</param>
        /// <param name="lonStart">Start longitude on the map</param>
        /// <param name="latEnd">End latitude on the map</param>
        /// <param name="lonEnd">End longitude on the map</param>
        /// <returns>distance (KM) between two received locations</returns>
        static double Distance(double latStart, double lonStart, double latEnd, double lonEnd)
        {
            //Converts decimal degrees to radians:
            var rlat1 = Math.PI * latStart / 180;
            var rlat2 = Math.PI * latEnd / 180;
            var rLon1 = Math.PI * lonStart / 180;
            var rLon2 = Math.PI * lonEnd / 180;
            var theta = lonStart - lonEnd;
            var rtheta = Math.PI * theta / 180;

            //Formula for calculating the distance 
            //between two coordinates represented by radians:
            var dist = (Math.Sin(rlat1) * Math.Sin(rlat2)) + Math.Cos(rlat1) *
                      Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;  //Converts radians to decimal degrees
            dist = dist * 60 * 1.1515;

            return dist * 1.61081082288953;      //Converts to KM
        }
    }
}
