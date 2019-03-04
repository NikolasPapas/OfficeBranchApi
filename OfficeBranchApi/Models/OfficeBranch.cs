using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.models
{
    public class OfficeBranch
    {
        [Key]
        public int OfficeBranchId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public ICollection<Position> Position { get; set; }

    }
}
