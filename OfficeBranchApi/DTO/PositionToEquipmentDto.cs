using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class PositionToEquipmentDtoEqui
    {
            public int EquipmentId { get; set; }
            public EquipmentDto EquipmentDto { get; set; }
            //public Position Position { get; set; }

    }

    public class PositionToEquipmentDtoPosi
    {
        //public int EquipmentId { get; set; }
        public int PositionId { get; set; }
        public PositionDto PositionDto { get; set; }
    }

    //public class PositionToEquipmentDetailsDto
    //{
    //    public int EquipmentId { get; set; }
    //    public Equipment Equipment { get; set; }
    //    public int PositionId { get; set; }
    //    public Position Position { get; set; }
    //}
}
