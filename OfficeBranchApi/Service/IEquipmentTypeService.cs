using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public interface IEquipmentTypeService
    {

        Task<ResultGet<IEnumerable<EquipmentTypeDto>>> GetAllEquipmentTypeByResultSetAsync(ResultSet resultSet);
        Task<long> GetCount();
        IEnumerable<EquipmentTypeDto> GetAllEquipmentTypeAsync(int count,int page);
        Task<EquipmentTypeDto> GetEquipmentTypeById(int id);
        void UpdateEquipmentType(EquipmentTypeDtoCreateUpdate equ);
        //void InsertEquipmentType(EquipmentTypeDtoCreateUpdate Name);
        void DeleteEquipmentType(int id);
    }
}
