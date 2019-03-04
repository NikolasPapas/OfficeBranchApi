using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Models
{
    public class PositionToEquipment
    {

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
