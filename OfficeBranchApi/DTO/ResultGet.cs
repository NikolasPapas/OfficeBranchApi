using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public class ResultGet<T>
    {
        public long totalCount { get; set; }
        public T items { get; set; }
    }
}
