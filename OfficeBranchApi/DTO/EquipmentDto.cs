using OfficeBranchApi.models;
using OfficeBranchApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public string SerialNumber { get; set; }
        
    }

    public class EquipmentDetailDto
    {
        public int EquipmentId { get; set; }
        public int EquipmentTypeId { get; set; }
        public EquipmentTypeDto EquipmentType { get; set; }
        public string SerialNumber { get; set; }
        public PositionToEquipmentDtoPosi PositionToEquipmentDtoPosi { get; set; }
    }

    public class EquipmentDtoCreateUpdate
    {
        public int? EquipmentId { get; set; }
        public int EquipmentTypeId { get; set; }
        public string SerialNumber { get; set; }
        public int? PositionId { get; set; }
    }

    
}
