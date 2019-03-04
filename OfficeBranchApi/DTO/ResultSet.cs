using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class ResultSet
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public String seartchBy { get; set; }
        public String orderBy { get; set; }
        public Boolean orderByAsc { get; set; }
    }
}
