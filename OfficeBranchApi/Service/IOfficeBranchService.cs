using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public interface IOfficeBranchService
    {
        
        Task<ResultGet<IEnumerable<OfficeBranchDto>>> GetAllOfficeBranchByResultSetAsync(ResultSet resultSet);
        Task<long> GetCount();
        IEnumerable<OfficeBranchDto> GetAllOfficeBranchAsync(int count,int page);
        IEnumerable<OfficeBranchDetailsDto> GetAllOfficeBranchPlusPositionsAsync(int count,int page);
        Task<OfficeBranchDetailsDto> GetOfficeBranchById(int id);
        //void UpdateOfficeBranch(OfficeBranchDtoCreateUpdate update);
        void InsertOfficeBranch(OfficeBranchDtoCreateAllUpdate insert);
        //void InsertOfficeBranchAll(OfficeBranchDtoCreateAllUpdate insert);
        void UpdateOfficeBranchDynamic(OfficeBranchDtoCreateAllUpdate UpdateInsert);
        void UpdateOfficeBranchDynamicFull(OfficeBranchDtoCreateAllUpdateFull UpdateInsert);
        
        void DeleteOfficeBranch(int id);
    }
}
