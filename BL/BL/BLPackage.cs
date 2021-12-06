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
        public void AddPackage(Package package)
        {
            try
            {
                dal.AddPackage(new IDAL.DO.Package
                {
                    SenderId = package.SenderCustomerInPackage.CustomerId,
                    TargetId = package.TargetCustomerInPackage.CustomerId,
                    Weight = (IDAL.DO.Weight)package.Weight,
                    Priority = (IDAL.DO.Priorities)package.Priority,
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null
                });
            }
            catch (IDAL.ExistsNumberException ex)
            {
                throw new IBL.ExistsNumberException("Package already exists ", ex);
            }
        }

        public Package GetPackage(int packageId)
        {
            try
            {
                var DoPackage = dal.GetPackage(packageId);
                var dr = droneLists.Find(x => x.PackageNumber == packageId);                

                Package BoPackage = new()
                {
                    Id = DoPackage.Id,
                    Weight = (Weight)DoPackage.Weight,
                    Priority = (Priorities)DoPackage.Priority,
                    Scheduled = DoPackage.Scheduled,
                    Requested = DoPackage.Requested,
                    PickedUp = DoPackage.PickedUp,
                    Delivered = DoPackage.Delivered,
                    droneInPackage = new()
                    {
                        Id = dr.Id,
                        BatteryStatus = dr.BatteryStatus,
                        LocationOfDrone = dr.LocationOfDrone
                    },
                    SenderCustomerInPackage = dal.GetCustomer(DoPackage.SenderId).GetCusomerInPackage(),
                    TargetCustomerInPackage = dal.GetCustomer(DoPackage.TargetId).GetCusomerInPackage()
                };              

                return BoPackage;
            }
            catch (IDAL.NoNumberFoundException ex)
            {
                throw new IBL.NoNumberFoundException("Package ID not found", ex);
            }
        }

        public IEnumerable<PackageToList> GetPackages(Predicate<PackageToList> predicate = null)
        {
            List<IDAL.DO.Package> DoPackage = (List<IDAL.DO.Package>)dal.GetPackages();
            List<PackageToList> BoPackageToLists = new();

            foreach (var item in DoPackage)
            {
                PackageStatus p = PackageStatus.Defined;

                if (item.Delivered != null)
                {
                    p = PackageStatus.Provided;
                }
                else if (item.PickedUp != null)
                {
                    p = PackageStatus.Collected;
                }
                else if (item.Scheduled != null)
                {
                    p = PackageStatus.Associated;
                }             

                PackageToList PckToLists = new()
                {
                    Id = item.Id,
                    SenderName = dal.GetCustomer(item.SenderId).Name,
                    TargetName = dal.GetCustomer(item.TargetId).Name,
                    Priority = (Priorities)item.Priority,
                    Weight = (Weight)item.Weight,
                    PackageStatus = p
                };

                if (predicate != null ? predicate(PckToLists) : true)
                    BoPackageToLists.Add(PckToLists);

            }

            return BoPackageToLists;
        }

        public void DeletePackage(int id)
        {
            try
            {
                dal.DeletePackage(id);
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
            
        }
    }
}
