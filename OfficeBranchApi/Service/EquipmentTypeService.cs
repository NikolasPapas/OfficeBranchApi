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
    public class EquipmentTypeService : IEquipmentTypeService
    {
        public readonly DbContextRepo _context;

        public EquipmentTypeService(DbContextRepo context)
        {
            _context = context;
        }




        public async Task<ResultGet<IEnumerable<EquipmentTypeDto>>> GetAllEquipmentTypeByResultSetAsync(ResultSet resultSet)
        {

            ResultGet<IEnumerable<EquipmentTypeDto>> ret = new ResultGet<IEnumerable<EquipmentTypeDto>>();

            IQueryable<EquipmentType> queryable = _context.EquipmentType;
            //if (resultSet.seartchName != null) { queryable = queryable.Where<Employee>(c => c.Name.Contains(resultSet.seartchName)); }
            if (resultSet.seartchBy != null) { queryable = queryable.Where<EquipmentType>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }


            if (resultSet.orderBy != null && resultSet.orderBy.Equals("name"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(equipmentType => equipmentType.Name);
                }
                else
                {
                    queryable = queryable.OrderBy(equipmentType => equipmentType.Name);
                }
            }
            else
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(equipmentType => equipmentType.EquipmentTypeId);
                }
                else
                {
                    queryable = queryable.OrderBy(equipmentType => equipmentType.EquipmentTypeId);
                }
            }
            queryable = queryable.Skip(resultSet.page * resultSet.pageSize);
            queryable = queryable.Take(resultSet.pageSize);

            ret.items = DtoSet.SetEquipmentTypeDtoList(queryable.ToList());
            ret.totalCount = await (GetCount(resultSet));
            return ret;
        }

        private async Task<long> GetCount(ResultSet resultSet)
        {
            IQueryable<EquipmentType> queryable = _context.EquipmentType;
            if (resultSet.seartchBy != null) { queryable = queryable.Where<EquipmentType>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }
            return await (queryable.CountAsync());
        }










        public async Task<long> GetCount()
        {
            return await (_context.EquipmentType.CountAsync());
        }

        public IEnumerable<EquipmentTypeDto> GetAllEquipmentTypeAsync(int count,int page)
        {
            if (page > 0)
            {
                return DtoSet.SetEquipmentTypeDtoList(_context.EquipmentType.Skip(page*count).Take(count).ToList());
            }
            else
            {
                return DtoSet.SetEquipmentTypeDtoList(_context.EquipmentType.Take(count).ToList());
            }
            //return _context.EquipmentType.ToListAsync();
        }

        public async Task<EquipmentTypeDto> GetEquipmentTypeById(int id)
        {
            return DtoSet.SetEquipmentTypeDto(_context.EquipmentType.Find(id));
            //return await _context.EquipmentType.FindAsync(id);
        }


        //public  void InsertEquipmentType(EquipmentTypeDtoCreateUpdate insert)
        //{
        //    _context.EquipmentType.Add(new EquipmentType { Name = insert.Name});
        //    _context.SaveChangesAsync();
        //}

        public void UpdateEquipmentType(EquipmentTypeDtoCreateUpdate update)
        {
            if (_context.EquipmentType.Find(update.EquipmentTypeId) != null)
            {
                EquipmentType equip = _context.EquipmentType.Find(update.EquipmentTypeId);
                if(update.Name!=null)
                equip.Name = update.Name;
                _context.EquipmentType.Update(equip);
                _context.SaveChangesAsync();
            }
            else
            {
                _context.EquipmentType.Add(new EquipmentType { Name = update.Name });
                _context.SaveChangesAsync();
            }

        }


        public void DeleteEquipmentType(int id)
        {
            foreach(Equipment equipment in _context.Equipment.ToList())
            {
                if(equipment.EquipmentTypeId ==id)
                {
                    foreach (PositionToEquipment posToEqu in _context.PositionToEquipment)
                    {
                        if (posToEqu.EquipmentId == equipment.EquipmentId)
                        {
                            _context.PositionToEquipment.Remove(posToEqu);
                        }
                    }
                    _context.Equipment.Remove(_context.Equipment.Find(equipment.EquipmentId));
                }
            }
            _context.EquipmentType.Remove(_context.EquipmentType.Find(id));
            _context.SaveChangesAsync();
        }
    }
}
