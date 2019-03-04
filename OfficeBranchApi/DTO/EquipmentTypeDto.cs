using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class EquipmentTypeDto
    {
        public int EquipmentTypeId { get; set; }
        public string Name { get; set; }
    }

    public class EquipmentTypeDtoCreateUpdate
    {
        public int EquipmentTypeId { get; set; }
        public string Name { get; set; }
    }


}
