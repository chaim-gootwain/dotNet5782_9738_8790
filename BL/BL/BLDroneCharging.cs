﻿using DalApi;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : IBL
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneForCharge(int DroneId)
        {

            var dr = dronesList.SingleOrDefault(x => x.Id == DroneId);


            if (dr == null)
            {
                throw new BlApi.NoNumberFoundException("Drone ID not found");
            }

            if (dr.DroneStatus != DroneStatuses.Available)
            {
                throw new DroneNotAvailableException("");
            }

            Location stLocation;
            try { stLocation = FindClosestStationLocation(dr.LocationOfDrone, x => x.FreeChargeSlots > 0); }
            catch (BlApi.NoNumberFoundException ex)
            {
                throw new BlApi.NoNumberFoundException("There is no station with available charging stations", ex);
            }

            double KM = stLocation.Distance(dr.LocationOfDrone);


            if (KM <= dr.BatteryStatus * DroneAvailable)
            {
                dr.BatteryStatus -= KM / DroneAvailable;
                dr.LocationOfDrone = stLocation;
                dr.DroneStatus = DroneStatuses.Maintenance;

                DO.Station station = dal.GetStations(x => x.Lattitude == stLocation.Lattitude && x.Longitude == stLocation.Longitude).SingleOrDefault();

                dal.UsingChargingStation(station.Id);
                dal.AddDroneCharge(new() { DroneId = DroneId, StationId = station.Id, ChargeStart = DateTime.Now });
            }
            else
            {
                throw new NotEnoughBattery("Not enough to get to the nearest available station");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RealeseDroneFromCharge(int DroneId, TimeSpan time)
        {
            var dr = dronesList.Find(x => x.Id == DroneId);

            if (dr == null)
            {
                throw new BlApi.NoNumberFoundException("Drone ID not found");
            }

            if (dr.DroneStatus != DroneStatuses.Maintenance)
            {
                throw new DroneNotMaintenanceException("");
            }


            dr.BatteryStatus += time.TotalHours * ChargingRate;
            if (dr.BatteryStatus > 100)
            {
                dr.BatteryStatus = 100;
            }
            dr.DroneStatus = DroneStatuses.Available;

            IEnumerable<DO.Station> stationsT = dal.GetStations(x => x.Lattitude == dr.LocationOfDrone.Lattitude && x.Longitude == dr.LocationOfDrone.Longitude);
            DO.Station station = stationsT.SingleOrDefault(x => x.Lattitude == dr.LocationOfDrone.Lattitude && x.Longitude == dr.LocationOfDrone.Longitude);

            dal.RealeseChargingStation(station.Id);
            dal.DeleteDroneCharge(DroneId);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RealeseDroneFromCharge(int DroneId)
        {
            var timeOfCharge = DateTime.Now - dal.GetDroneCharge(DroneId).ChargeStart;

            RealeseDroneFromCharge(DroneId, (TimeSpan)timeOfCharge);
        }
    }
}
