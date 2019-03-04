using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public interface IEquipmentsRestService
    {

        Task<long> GetCount();
        Task<ResultGet<IEnumerable<EquipmentDetailDto>>> GetAllEquipmentsByResultSetAsync(ResultSet resultSet);
        IEnumerable<EquipmentDto> GetAllEquipments(int count,int page);
        IEnumerable<EquipmentDetailDto> GetAllEquipmentsPlusType(int count, int page);
        Task<EquipmentDetailDto> GetEquipmentById(int id);
        //void InsertEquipment(EquipmentDtoCreateUpdate equ);
        void UpdateEquipment(EquipmentDtoCreateUpdate equ);
        void DeleteEquipment(int id);

    }
}
