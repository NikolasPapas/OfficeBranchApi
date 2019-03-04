using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.models
{
    public class EquipmentType
    {
        [Key]
        public int EquipmentTypeId { get; set; }
        public string Name { get; set; }

    }
}
