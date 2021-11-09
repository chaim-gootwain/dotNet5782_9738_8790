﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enumerations;

namespace IBL.BO
{
    class DroneToList
    {
        /// <summary>
        /// gets the id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// gets the model.
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// gets the max weight.
        /// </summary>
        public Weight MaxWeight { get; set; }
        /// <summary>
        /// Gets the drone's battery stutus;
        /// </summary>
        public double BatteryStatus { get; set; }

        //מיקום
        /// <summary>
        /// 
        /// </summary>
        public int PackageNumber { get; set; }


        public override string ToString()
        {
            return "Details of Id :" + Id + "\nModel: " + Model + "\nMax weight: " +
                 MaxWeight + "\nStatus of battery: " + BatteryStatus + "\n";
        }
    }
}
