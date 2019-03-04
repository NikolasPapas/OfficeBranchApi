using OfficeBranchApi.DTO;
using OfficeBranchApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.models
{
    public class Position
    {
       
        [Key]
        public int PositionId { get; set; }
        public string Name { get; set; }

        
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        
        public int OfficeBranchId { get; set; }

        [ForeignKey("OfficeBranchId")]
        public OfficeBranch OfficeBranch { get; set; }
       

        public virtual List<PositionToEquipment> PositionToEquipment { get; set; }

       
    }
}
