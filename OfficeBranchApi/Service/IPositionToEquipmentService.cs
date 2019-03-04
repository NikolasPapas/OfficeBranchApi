using OfficeBranchApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public interface IPositionToEquipmentService
    {

        Task<long> GetCount();
        IEnumerable<PositionToEquipment> GetAllPositionToEquipment();
        Task<PositionToEquipment> GetPositionToEquipment(int id);
        //void InsertPositionToEquipment(PositionToEquipment insert);
        void UpdatePositionToEquipment(PositionToEquipment update);
        void DeletePositionToEquipment(int id);
    }
}
