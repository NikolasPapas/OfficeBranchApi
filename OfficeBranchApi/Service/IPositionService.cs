using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public interface IPositionService
    {
        Task<ResultGet<IEnumerable<PositionDetailsDto>>> GetAllPositionsByResultSetAsync(ResultSet resultSet);
        Task<long> GetCount();
        IEnumerable<PositionDto> GetAllPositionAsync(int count,int page);
        IEnumerable<PositionDetailsDto> GetAllPositionPlusAll(int count,int page);
        PositionDetailsDto GetPositionById(int id);
        void UpdatePosition(PositionDetailsDtoCreateUpdate pos);
        //void InsertPosition(PositionDetailsDtoCreateUpdate pos);
        void DeletePosition(int id);
    }
}
