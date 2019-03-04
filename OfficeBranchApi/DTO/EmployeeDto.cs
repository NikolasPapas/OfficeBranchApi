using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
    }

    public class EmployeeDetailDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public int PositionId { get; set; }
        public PositionDto Position { get; set; }
    }

    public class EmployeeDtoCreateUpdate
    {
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public int? PositionId { get; set; }
    }

}
