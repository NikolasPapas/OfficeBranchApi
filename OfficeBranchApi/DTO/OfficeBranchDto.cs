using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class OfficeBranchDto
    {
        public int OfficeBranchId { get; set; }
        public string Name { get; set; }
    }

    public class OfficeBranchDetailsDto
    {
        public int OfficeBranchId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public ICollection<PositionDto> Position { get; set; }

    }

    public class OfficeBranchDtoCreateUpdate
    {
        public int OfficeBranchId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
    }

    public class OfficeBranchDtoCreateAllUpdate
    {
        public int OfficeBranchId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public ICollection<PositionDetailsDtoCreateUpdate> PositionDetailsDtoCreateUpdate { get; set; }
    }

    public class OfficeBranchDtoCreateAllUpdateFull
    {
        public int? OfficeBranchId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public ICollection<PositionDetailsDtoCreateUpdateFull> PositionDetailsDtoCreateUpdateFull { get; set; }
    }
}
