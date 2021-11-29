﻿using IDAL;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBl
    {
        public void SendDroneForCharge(int DroneId)
        {

            int iDr = DroneLists.FindIndex(x => x.Id == DroneId);
            var dr = DroneLists.Find(x => x.Id == DroneId);


            if (dr == null)
            {
                throw new IBL.NoNumberFoundException("Drone ID not found");
            }

            if (dr.DroneStatus != DroneStatuses.AVAILABLE)
            {
                throw new DroneNotAvailableException();
            }

            Location stLocation;
            try { stLocation = FindClosestStationLocation(dr.LocationOfDrone, x => x.FreeChargeSlots > 0); }
            catch ( IBL.NoNumberFoundException ex) 
            { 
                throw new IBL.NoNumberFoundException("There is no station with available charging stations",ex); 
            }

            double KM = Distance(stLocation, dr.LocationOfDrone);


            if (KM <= dr.BatteryStatus * DroneAvailable)
            {
                dr.BatteryStatus = dr.BatteryStatus - KM /DroneAvailable;
                dr.LocationOfDrone = stLocation;
                dr.DroneStatus = DroneStatuses.MAINTENANCE;

                DroneLists.Insert(iDr, dr);

                List<IDAL.DO.Station> stationsT = dal.GetStations(x => x.FreeChargeSlots > 0).ToList();
                IDAL.DO.Station station = stationsT.Find(x => x.Lattitude == stLocation.Lattitude && x.Longitude == stLocation.Longitude);

                dal.UsingChargingStation(station.Id);
                dal.AddDroneCharge(new() { DroneId = DroneId, StationId = station.Id });
            }
            else
            {
                throw new NotEnoughBattery("Not enough to get to the nearest available station");
            }


        }


        public void RealeseDroneFromCharge(int DroneId, TimeSpan time)
        {
                int iDr = DroneLists.FindIndex(x => x.Id == DroneId);
                var dr = DroneLists.Find(x => x.Id == DroneId);

                if (dr == null)
                {
                    throw new IBL.NoNumberFoundException("Drone ID not found");
                }

                if (dr.DroneStatus != DroneStatuses.MAINTENANCE)
                {
                    throw new DroneNotMaintenanceException();
                }


                dr.BatteryStatus = time.TotalHours * ChargingRate;
                dr.DroneStatus = DroneStatuses.AVAILABLE;

                DroneLists.Insert(iDr, dr);

                List<IDAL.DO.Station> stationsT = dal.GetStations(x => x.Lattitude == dr.LocationOfDrone.Lattitude && x.Longitude == dr.LocationOfDrone.Longitude).ToList();
                IDAL.DO.Station station = stationsT.Find(x => x.Lattitude == dr.LocationOfDrone.Lattitude && x.Longitude == dr.LocationOfDrone.Longitude);

                dal.RealeseChargingStation(station.Id);
                dal.DeleteDroneCharge(DroneId);
            
            
        }
    }
}
