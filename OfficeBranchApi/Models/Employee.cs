using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public Position Position { get; set; }
    }
}
