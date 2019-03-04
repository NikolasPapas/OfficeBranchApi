using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeBranchApi.Models;

namespace OfficeBranchApi.models
{
    public class Equipment
    {

       
        [Key]
        public int EquipmentId { get; set; }
        public int EquipmentTypeId { get; set; }
        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentType{ get; set; }
        public string SerialNumber { get; set; }

        public PositionToEquipment PositionToEquipment { get; set; }


    }
}
