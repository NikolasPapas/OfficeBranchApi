using Microsoft.EntityFrameworkCore;
using OfficeBranchApi.Condext;
using OfficeBranchApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public class PositionToEquipmentService: IPositionToEquipmentService
    {

        public readonly DbContextRepo _context;

        public PositionToEquipmentService(DbContextRepo context)
        {
            _context = context;
        }

        public async Task<long> GetCount()
        {
            return await (_context.PositionToEquipment.CountAsync());
        }

        public async Task<PositionToEquipment> GetPositionToEquipment(int id)
        {
            return _context.PositionToEquipment.Include(c => c.Position).Include(c => c.Equipment).SingleOrDefault(x => x.EquipmentId == id);
        }

        public IEnumerable<PositionToEquipment> GetAllPositionToEquipment()
        {
            return _context.PositionToEquipment.Include(c => c.Position).Include(c => c.Equipment).ToList();
        }

        
        public void InsertPositionToEquipment(PositionToEquipment insert)
        {
            PositionToEquipment positionTo = new PositionToEquipment
            {
                EquipmentId = insert.EquipmentId,
                PositionId = insert.PositionId
            };
            _context.PositionToEquipment.Add(positionTo);
            _context.SaveChanges();

        }

        public void UpdatePositionToEquipment(PositionToEquipment update)
        {
            if (_context.PositionToEquipment.Find(update.EquipmentId ) != null)
            {
                PositionToEquipment posToEqu = _context.PositionToEquipment.Find(update.EquipmentId);
                posToEqu.EquipmentId = update.EquipmentId;
                posToEqu.PositionId = update.PositionId;
                _context.PositionToEquipment.Update(posToEqu);
            }
        }

        public void DeletePositionToEquipment(int id)
        {
            _context.PositionToEquipment.Remove(_context.PositionToEquipment.Find(id));
            _context.SaveChangesAsync();
        }

       
    }
}
