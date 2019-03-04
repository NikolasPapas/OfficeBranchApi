using Microsoft.EntityFrameworkCore;
using OfficeBranchApi.Condext;
using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using OfficeBranchApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public class EquipmentsRestService : IEquipmentsRestService
    {
        public readonly DbContextRepo _context;

        public EquipmentsRestService(DbContextRepo context)
        {
            _context = context;
        }

        public async Task<long> GetCount()
        {
            return await (_context.Equipment.CountAsync());
        }



        public async Task<ResultGet<IEnumerable<EquipmentDetailDto>>> GetAllEquipmentsByResultSetAsync(ResultSet resultSet)
        {

            ResultGet<IEnumerable<EquipmentDetailDto>> ret = new ResultGet<IEnumerable<EquipmentDetailDto>>();

            IQueryable<Equipment> queryable = _context.Equipment
                                                            .Include(c => c.EquipmentType)
                                                            .OrderBy(Equipment=> Equipment.EquipmentId)
                                                            .Include(c => c.PositionToEquipment)
                                                                .ThenInclude(x => x.Position);
            if (resultSet.seartchBy != null) { queryable = queryable.Where<Equipment>(c => EF.Functions.Like(c.SerialNumber, resultSet.seartchBy)); }
            if (resultSet.orderBy != null && resultSet.orderBy.Equals("serialNumber"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Equipment => Equipment.SerialNumber);
                }
                else
                {
                    queryable = queryable.OrderBy(Equipment => Equipment.SerialNumber);
                }
            }
            else
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Equipment => Equipment.EquipmentId);
                }
                else
                {
                    queryable = queryable.OrderBy(Equipment => Equipment.EquipmentId);
                }
            }
            queryable = queryable.Skip(resultSet.page * resultSet.pageSize);
            queryable = queryable.Take(resultSet.pageSize);

            ret.items = DtoSet.SetEquipmentDetailDtoList(queryable.ToList());
            ret.totalCount = await (GetCount(resultSet));
            return ret;
        }

        private async Task<long> GetCount(ResultSet resultSet)
        {
            IQueryable<Equipment> queryable = _context.Equipment;
            if (resultSet.seartchBy != null) { queryable = queryable.Where<Equipment>(c => EF.Functions.Like(c.SerialNumber, resultSet.seartchBy)); }
            return await (queryable.CountAsync());
        }









        public IEnumerable<EquipmentDto> GetAllEquipments(int count,int page)
        {
           

                return DtoSet.SetEquipmentDtoList(_context.Equipment.Skip(page* count).Take(count).ToList());
           
        }

        public IEnumerable<EquipmentDetailDto> GetAllEquipmentsPlusType(int count, int page)
        {
           
                return DtoSet.SetEquipmentDetailDtoList(_context.Equipment
                                                            .Skip(page * count)
                                                            .Take(count)
                                                            .Include(c => c.EquipmentType)
                                                            .Include(c => c.PositionToEquipment)
                                                                .ThenInclude(x => x.Position)
                                                            .ToList());
            
            
            //return await _context.Equipment.Include(c => c.EquipmentType).ToListAsync();
        }

        public async Task<EquipmentDetailDto> GetEquipmentById(int id)
        {
            //var equipmentDetailDto = await _context.Equipment.Select(b =>
            //     new EquipmentDetailDto()
            //     {
            //         EquipmentId = b.EquipmentId,
            //         EquipmentTypeId = b.EquipmentTypeId,
            //         EquipmentType = b.EquipmentType,
            //         SerialNumber = b.SerialNumber
            //     }).SingleOrDefaultAsync(b => b.EquipmentId == id);

            return DtoSet.SetEquipmentDetailDto(_context.Equipment
                                                .Include(c => c.EquipmentType)
                                                .Include(c=>c.PositionToEquipment)
                                                    .ThenInclude(x=>x.Position)
                                                .SingleOrDefault(x => x.EquipmentId == id));
            //return await  _context.Equipment.FindAsync(id);
        }

        public async void UpdateEquipment(EquipmentDtoCreateUpdate equ)
        {
            if (equ.EquipmentId!=null && _context.Equipment.Find(equ.EquipmentId) != null)
            {
                Equipment equip = _context.Equipment.Find(equ.EquipmentId);
                if(!equ.SerialNumber.Equals(""))
                    equip.SerialNumber = equ.SerialNumber;
                equip.EquipmentTypeId = equ.EquipmentTypeId;
                equip.EquipmentType =_context.EquipmentType.Find(equip.EquipmentTypeId);
                _context.Equipment.Update(equip);

                if (equ.PositionId.HasValue && _context.PositionToEquipment.SingleOrDefault(x => x.EquipmentId == equ.EquipmentId) != null && _context.Position.SingleOrDefault(x => x.PositionId == equ.PositionId) != null)
                {
                    PositionToEquipment posToEqu = _context.PositionToEquipment.SingleOrDefault(x => x.EquipmentId == equ.EquipmentId);
                    posToEqu.PositionId = equ.PositionId.Value;
                    _context.PositionToEquipment.Update(posToEqu);

                }else if (equ.PositionId.HasValue && _context.PositionToEquipment.SingleOrDefault(x => x.EquipmentId == equ.EquipmentId) == null && _context.Position.SingleOrDefault(x => x.PositionId == equ.PositionId) != null)
                {
                    PositionToEquipment posToEqu = new PositionToEquipment();
                    posToEqu.PositionId = equ.PositionId.Value;
                    posToEqu.EquipmentId = equip.EquipmentId;
                    _context.PositionToEquipment.Add(posToEqu);
                }
                else if (!equ.PositionId.HasValue && _context.PositionToEquipment.SingleOrDefault(x => x.EquipmentId == equ.EquipmentId) != null)
                {
                    PositionToEquipment posToEqu = _context.PositionToEquipment.SingleOrDefault(x => x.EquipmentId == equ.EquipmentId);
                    _context.PositionToEquipment.Remove(posToEqu);
                }
            }
            else
            {
                Equipment equip = new Equipment
                {
                    SerialNumber = equ.SerialNumber,
                    EquipmentTypeId = equ.EquipmentTypeId
                };
                _context.Equipment.Add(equip);
            }
             _context.SaveChanges();
        }


        //public void InsertEquipment(EquipmentDtoCreateUpdate equ)
        //{
        //    Equipment equip = new Equipment
        //    {
        //        SerialNumber = equ.SerialNumber,
        //        EquipmentId = Convert.ToInt16(equ.EquipmentId),
        //        EquipmentTypeId = Convert.ToInt16(equ.EquipmentTypeId),
        //        EquipmentType = _context.EquipmentType.Find(Convert.ToInt16(equ.EquipmentTypeId))
        //};
        //    _context.Equipment.Add(equip);
        //    _context.SaveChanges();
        //}

        public void DeleteEquipment(int id)
        {
            foreach(PositionToEquipment posToEqu in _context.PositionToEquipment)
            {
                if(posToEqu.EquipmentId == id)
                {
                    _context.PositionToEquipment.Remove(posToEqu);
                }
            }
             _context.Equipment.Remove(_context.Equipment.Find(id));
            _context.SaveChangesAsync();
        }
    }
}
