using OfficeBranchApi.models;
using OfficeBranchApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class PositionDto
    {
        public int PositionId { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
    }

    public class PositionDetailsDto
    {
        public int PositionId { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
        public int OfficeBranchId { get; set; }
        public OfficeBranchDto OfficeBranch { get; set; }

        public List<PositionToEquipmentDtoEqui> PositionToEquipmentDtoEqui { get; set; }
    }

    public class PositionDetailsDtoCreateUpdate
    {
        public int PositionId { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string OfficeBranchId { get; set; }
    }

    public class PositionDetailsDtoCreateUpdateFull
    {
        public int? PositionId { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
        public EmployeeDtoCreateUpdate EmployeeDtoCreateUpdate { get; set; }
        public int? OfficeBranchId { get; set; }
    }
}
