using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeBranchApi.Condext;
using OfficeBranchApi.DTO;
using OfficeBranchApi.models;

namespace OfficeBranchApi.Service
{
    public class PositionService : IPositionService
    {

        public readonly DbContextRepo _context;

        public PositionService(DbContextRepo context)
        {
            _context = context;
        }


        public async Task<ResultGet<IEnumerable<PositionDetailsDto>>> GetAllPositionsByResultSetAsync(ResultSet resultSet)
        {

            ResultGet<IEnumerable<PositionDetailsDto>> ret = new ResultGet<IEnumerable<PositionDetailsDto>>();

            IQueryable<Position> queryable = _context.Position
                                                                .Include(c => c.Employee)
                                                                .Include(c => c.OfficeBranch)
                                                                .Include(c => c.PositionToEquipment)
                                                                    .ThenInclude(x => x.Equipment);
            if (resultSet.seartchBy != null) { queryable = queryable.Where<Position>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }
            if (resultSet.orderBy != null && resultSet.orderBy.Equals("name"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Position => Position.Name);
                }
                else
                {
                    queryable = queryable.OrderBy(Position => Position.Name);
                }
            }
            else
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Position => Position.PositionId);
                }
                else
                {
                    queryable = queryable.OrderBy(Position => Position.PositionId);
                }
            }

            queryable = queryable.Skip(resultSet.page * resultSet.pageSize);
            queryable = queryable.Take(resultSet.pageSize);

            ret.items = DtoSet.SetPositionDetailsDtoList(queryable.ToList());
            ret.totalCount = await (GetCount(resultSet));
            return ret;
        }

        private async Task<long> GetCount(ResultSet resultSet)
        {
            IQueryable<Position> queryable = _context.Position;
            if (resultSet.seartchBy != null) { queryable = queryable.Where<Position>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }
            return await (queryable.CountAsync());
        }










        public async Task<long> GetCount()
        {
            return await (_context.Position.CountAsync());
        }


        public IEnumerable<PositionDto> GetAllPositionAsync(int count,int page)
        {
            if (page > 0)
            {

                return DtoSet.SetPositionDtoList(_context.Position.Skip(page*count).Take(count).ToList());
                //return await _context.Position.ToListAsync();
            }
            else
            {

                return DtoSet.SetPositionDtoList(_context.Position.Take(count).ToList());
                //return await _context.Position.ToListAsync();
            }
        }

        public IEnumerable<PositionDetailsDto> GetAllPositionPlusAll(int count,int page)
        {
            //List<Position> pos = _context.Position.Include(c=>c.Employee).Include(c=>c.OfficeBranch).ToList();
            //List<PositionDetailsDto> posDtoList = new List<PositionDetailsDto>();
            //foreach (Position b in pos)
            //{
            //    PositionDetailsDto posDto = new PositionDetailsDto()
            //    {
            //        PositionId = b.PositionId,
            //        Name = b.Name,
            //        EmployeeId = b.EmployeeId,
            //        OfficeBranchId = b.OfficeBranchId,
            //        Employee = DtoSet.SetEmployeeDto(_context.Employee.Find(b.EmployeeId)),
            //        OfficeBranch = DtoSet.SetOfficeBranchDto(_context.OfficeBranch.Find(b.OfficeBranchId))

            //    };
            //    posDtoList.Add(posDto);
            //}
            //return DtoSet.SetPositionDetailsDtoList(_context.Position.Where(x => x.EmployeeId == 5).Include(x => x.Employee).ToList());

            if (page > 0)
            {

                return DtoSet.SetPositionDetailsDtoList(_context.Position
                                                                .Skip(page*count)
                                                                .Take(count)
                                                                .Include(c => c.Employee)
                                                                .Include(c => c.OfficeBranch)
                                                                .Include(c => c.PositionToEquipment)
                                                                    .ThenInclude(x => x.Equipment)
                                                                .ToList());
            }
            else
            {

                return DtoSet.SetPositionDetailsDtoList(_context.Position
                                                                .Take(count)
                                                                .Include(c => c.Employee)
                                                                .Include(c => c.OfficeBranch)
                                                                .Include(c => c.PositionToEquipment)
                                                                    .ThenInclude(x => x.Equipment)
                                                                .ToList());
            }

            //return await _context.Position.Include(c=>c.Employee).Include(x => x.OfficeBranch).ToListAsync();
        }

        public PositionDetailsDto GetPositionById(int id)
        {

            //var posDto = await _context.Position.Select(b =>
            //    new PositionDetailsDto()
            //    {
            //        PositionId = b.PositionId,
            //        Name = b.Name,
            //        EmployeeId = b.EmployeeId,
            //        OfficeBranchId = b.OfficeBranchId,
            //        Employee = DtoSet.SetEmployeeDto(b.Employee),
            //        OfficeBranch = DtoSet.SetOfficeBranchDto(b.OfficeBranch)

            //    }).SingleOrDefaultAsync(b => b.PositionId == id);
            if(id!=0)
            {

                return DtoSet.SetPositionDetailsDto(_context.Position
                                                            .Include(c => c.Employee)
                                                            .Include(c => c.OfficeBranch)
                                                            .Include(c => c.PositionToEquipment)
                                                                .ThenInclude(x => x.Equipment)
                                                            .SingleOrDefault(x => x.PositionId == id));

            }
            
            
                return new PositionDetailsDto();
            
            //return await _context.Position.FindAsync(id);
        }

        //public void InsertPosition(PositionDetailsDtoCreateUpdate insert)
        //{
        //    Position pos = new Position()
        //    {
        //        Name = insert.Name,
        //        EmployeeId = Convert.ToInt16(insert.EmployeeId),
        //        OfficeBranchId = Convert.ToInt16(insert.OfficeBranchId),
        //        Employee = _context.Employee.Find(insert.EmployeeId),
        //        OfficeBranch = _context.OfficeBranch.Find(insert.OfficeBranchId)
        //    };
        //    _context.Position.Add(pos);
        //    _context.SaveChangesAsync();
        //}

        public void UpdatePosition(PositionDetailsDtoCreateUpdate update)
        {
            if (_context.Position.Find(update.PositionId) != null)
            {
                Position position = _context.Position.Find(update.PositionId);
                if(update.Name!=null)
                    position.Name = update.Name;
                if(!update.EmployeeId.Equals("") && update.EmployeeId != null)
                {
                    position.EmployeeId = Convert.ToInt32(update.EmployeeId);
                }
                else
                {
                    position.EmployeeId = null;
                }

                if (update.OfficeBranchId != null & !update.OfficeBranchId.Equals(""))
                    position.OfficeBranchId = Convert.ToInt32(update.OfficeBranchId);
              
                _context.Position.Update(position);
            }
            else
            {
                Position pos = new Position()
                {
                    Name = update.Name,
                    EmployeeId = Convert.ToInt32(update.EmployeeId),
                    //Employee =_context.Employee.Find(Convert.ToInt32(update.EmployeeId)),
                    OfficeBranchId = Convert.ToInt32(update.OfficeBranchId),
                    //OfficeBranch = _context.OfficeBranch.Find(Convert.ToInt32(update.OfficeBranchId)),

                };
                //pos.PositionToEquipment = new List<Models.PositionToEquipment>();
                _context.Position.Add(pos);
            }
             _context.SaveChanges();
        }

        public void DeletePosition(int id)
        {
            _context.Position.Remove(_context.Position.Find(id));
            _context.SaveChanges();
        }

       
    }
}
