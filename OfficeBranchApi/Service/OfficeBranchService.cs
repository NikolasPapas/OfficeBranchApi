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
    public class OfficeBranchService : IOfficeBranchService
    {

        public readonly DbContextRepo _context;

        public OfficeBranchService(DbContextRepo context)
        {
            _context = context;
        }



        public async Task<ResultGet<IEnumerable<OfficeBranchDto>>> GetAllOfficeBranchByResultSetAsync(ResultSet resultSet)
        {

            ResultGet<IEnumerable<OfficeBranchDto>> ret = new ResultGet<IEnumerable<OfficeBranchDto>>();

            IQueryable<OfficeBranch> queryable = _context.OfficeBranch;
            if (resultSet.seartchBy != null) { queryable = queryable.Where<OfficeBranch>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }
            if (resultSet.orderBy != null && resultSet.orderBy.Equals("name"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(OfficeBranch => OfficeBranch.Name);
                }
                else
                {
                    queryable = queryable.OrderBy(OfficeBranch => OfficeBranch.Name);
                }
            }
            else
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(OfficeBranch => OfficeBranch.OfficeBranchId);
                }
                else
                {
                    queryable = queryable.OrderBy(OfficeBranch => OfficeBranch.OfficeBranchId);
                }
            }

           
            queryable = queryable.Skip(resultSet.page * resultSet.pageSize);
            queryable = queryable.Take(resultSet.pageSize);

            ret.items = DtoSet.SetOfficeBranchDtoList(queryable.ToList());
            ret.totalCount = await (GetCount(resultSet));
            return ret;
        }

        private async Task<long> GetCount(ResultSet resultSet)
        {
            IQueryable<OfficeBranch> queryable = _context.OfficeBranch;
            if (resultSet.seartchBy != null) { queryable = queryable.Where<OfficeBranch>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }
            return await (queryable.CountAsync());
        }





        public async Task<long> GetCount()
        {
            return await (_context.OfficeBranch.CountAsync());
        }


        public IEnumerable<OfficeBranchDto> GetAllOfficeBranchAsync(int count,int page)
        {
            if (page > 0)
            {

                return DtoSet.SetOfficeBranchDtoList(_context.OfficeBranch.Skip(page*count).Take(count).ToList());
            }
            else
            {

                return DtoSet.SetOfficeBranchDtoList(_context.OfficeBranch.Take(count).ToList());
            }
            //return await _context.OfficeBranch.ToListAsync();
        }

        public IEnumerable<OfficeBranchDetailsDto> GetAllOfficeBranchPlusPositionsAsync(int count, int page)
        {
            if (page > 0)
            {

                return DtoSet.SetOfficeBranchDetailsDtoList(_context.OfficeBranch.Skip(page*count).Take(count).Include(c => c.Position).ToList());
            }
            else
            {

                return DtoSet.SetOfficeBranchDetailsDtoList(_context.OfficeBranch.Take(count).Include(c => c.Position).ToList());
            }
            // return await _context.OfficeBranch.Include(c => c.Position).ToListAsync();
        }

        public async Task<OfficeBranchDetailsDto> GetOfficeBranchById(int id)
        {
            return DtoSet.SetOfficeBranchDetailsDto(_context.OfficeBranch.Include(c => c.Position).SingleOrDefault(x => x.OfficeBranchId == id));
            //return await _context.OfficeBranch.FindAsync(id);
        }

        public void InsertOfficeBranch(OfficeBranchDtoCreateAllUpdate insert)
        {
            OfficeBranch office = new OfficeBranch();
            office.Name = insert.Name;
            office.Address = insert.Address;
            _context.OfficeBranch.Add(office);
            _context.SaveChangesAsync();
        }



        //public void UpdateOfficeBranch(OfficeBranchDtoCreateUpdate update)
        //{
        //    if (_context.OfficeBranch.Find(update.OfficeBranchId) != null)
        //    {
        //        OfficeBranch ofBra = _context.OfficeBranch.Find(update.OfficeBranchId);
        //        if(update.Name!=null)
        //        ofBra.Name = update.Name;
        //        if (update.Address != null)
        //            ofBra.Address = update.Address;
        //        _context.OfficeBranch.Update(ofBra);
        //        _context.SaveChangesAsync();
        //    }
        //}

        public void DeleteOfficeBranch(int id)
        {
            _context.OfficeBranch.Remove(_context.OfficeBranch.Find(id));
            foreach (Position pos in _context.Position.ToList())
            {
                if (pos.OfficeBranchId == id)
                {
                    _context.Position.Remove(_context.Position.Find(pos.PositionId));
                }
            }

            _context.SaveChangesAsync();
        }



        //public void InsertOfficeBranchAll(OfficeBranchDtoCreateAllUpdate insert)
        //{

        //    OfficeBranch office = new OfficeBranch
        //    {
        //        Name = insert.Name,
        //        Address = insert.Address
        //    };
        //    _context.OfficeBranch.Add(office);
        //    _context.SaveChanges();

        //    var ofiiceCreated = _context.OfficeBranch
        //           .Where(b => b.Name == insert.Name)
        //           .Where(b => b.Address == insert.Address)
        //           .FirstOrDefault();

        //    foreach (PositionDetailsDtoCreateUpdate pos in insert.PositionDetailsDtoCreateUpdate)
        //    {
        //        Position posCre = new Position
        //        {
        //            Name = pos.Name,
        //            EmployeeId = Convert.ToInt32(pos.EmployeeId),
        //            OfficeBranchId = ofiiceCreated.OfficeBranchId,
        //            Employee = _context.Employee.Find(Convert.ToInt32(pos.EmployeeId))
        //        };
        //        _context.Position.Add(posCre);
        //    }
        //    _context.SaveChanges();
        //}


        



        public void UpdateOfficeBranchDynamicFull(OfficeBranchDtoCreateAllUpdateFull UpdateInsert)
        {
            List<PositionDetailsDtoCreateUpdateFull> UpdateList = new List<PositionDetailsDtoCreateUpdateFull>();
            List<PositionDetailsDtoCreateUpdateFull> CreateList = new List<PositionDetailsDtoCreateUpdateFull>();
           

            if (UpdateInsert.OfficeBranchId!=null && _context.OfficeBranch.Find(UpdateInsert.OfficeBranchId.Value) != null)
            {
                //if OfficeBranch Exist Update
                OfficeBranch ofBra = _context.OfficeBranch.Find(UpdateInsert.OfficeBranchId.Value);
                ofBra.Name = UpdateInsert.Name;
                ofBra.Address = UpdateInsert.Address;
                _context.OfficeBranch.Update(ofBra);
            }
            else
            {
                OfficeBranch office = new OfficeBranch
                {
                    Name = UpdateInsert.Name,
                    Address = UpdateInsert.Address
                };
                _context.OfficeBranch.Add(office);
                _context.SaveChanges();
                var ofiiceCreated = _context.OfficeBranch
                       .Where(b => b.Name == UpdateInsert.Name)
                       .Where(b => b.Address == UpdateInsert.Address)
                       .FirstOrDefault();
                UpdateInsert.OfficeBranchId=ofiiceCreated.OfficeBranchId;
            }

            OfficeBranch officeBranch = _context.OfficeBranch.Include(c => c.Position).SingleOrDefault(x => x.OfficeBranchId == UpdateInsert.OfficeBranchId.Value);

          

            List<int> existingPositionIds = officeBranch.Position.Select(x => x.PositionId).ToList();
            List<int> updatePositionIds = new List<int>();
            List<Position> DeletePositionList = new List<Position>();

            if (UpdateInsert.PositionDetailsDtoCreateUpdateFull != null)
                updatePositionIds = UpdateInsert.PositionDetailsDtoCreateUpdateFull.Where(x => x.PositionId > 0).Select(x => x.PositionId.Value).ToList();

            foreach (int id in existingPositionIds)
            {
                if (!updatePositionIds.Contains(id))
                {
                    DeletePositionList.Add(officeBranch.Position.Single(x => x.PositionId == id));
                }

            }
            if (UpdateInsert.PositionDetailsDtoCreateUpdateFull != null)
                UpdateList.AddRange(UpdateInsert.PositionDetailsDtoCreateUpdateFull.Where(x => x.PositionId > 0));
            if (UpdateInsert.PositionDetailsDtoCreateUpdateFull != null)
                CreateList.AddRange(UpdateInsert.PositionDetailsDtoCreateUpdateFull.Where(x => x.PositionId ==null));
            CreationPositionListFull(UpdateList, CreateList, DeletePositionList, UpdateInsert.OfficeBranchId.Value);

            _context.SaveChanges();
        }



        private void CreationPositionListFull(List<PositionDetailsDtoCreateUpdateFull> UpdateList,
                                    List<PositionDetailsDtoCreateUpdateFull> CreateList,
                                    List<Position> DeleteList, int id)
        {

           
          List<int> existingEmployeeIds = _context.Employee.Select(x => x.EmployeeId).ToList();
          
           
           

            // For Every Position in Conteiner
            foreach (PositionDetailsDtoCreateUpdateFull UpdatePos in UpdateList)
            {
                //If Position Exist Update
                Position posCre = _context.Position.Find(UpdatePos.PositionId);
                posCre.Name = UpdatePos.Name;
                posCre.OfficeBranchId = id;
                posCre.EmployeeId = UpdatePos.EmployeeId;

                Employee emp = new Employee();
                emp.Name = UpdatePos.EmployeeDtoCreateUpdate.Name;
                emp.Gender = UpdatePos.EmployeeDtoCreateUpdate.Gender;
                if (UpdatePos.EmployeeId!=null && existingEmployeeIds.Contains(UpdatePos.EmployeeId.Value))
                {
                    UpdateEmployee(emp);
                }
                else
                {
                    UpdatePos.EmployeeId=CreateEmployee(emp);
                }
                _context.Position.Update(posCre);
            }
            foreach (PositionDetailsDtoCreateUpdateFull InsertPos in CreateList)
            {
                //If Position Not Exist Insert
                Employee emp = new Employee();
                emp.Name = InsertPos.EmployeeDtoCreateUpdate.Name;
                emp.Gender = InsertPos.EmployeeDtoCreateUpdate.Gender;
                if (InsertPos.EmployeeId != null && existingEmployeeIds.Contains(InsertPos.EmployeeId.Value))
                {
                    UpdateEmployee(emp);
                }
                else
                {
                   
                    InsertPos.EmployeeId = CreateEmployee(emp); 
                }

               
                InsertPos.OfficeBranchId = id;
                Position posCre = new Position
                {
                    Name = InsertPos.Name,
                    EmployeeId = InsertPos.EmployeeId,
                    OfficeBranchId = InsertPos.OfficeBranchId.Value
                };

                
                _context.Position.Add(posCre);
            }
            foreach (Position DeletePos in DeleteList)
            {
                Position pos = _context.Position.Find(DeletePos.PositionId);
                _context.Position.Remove(pos);
            }


            


        }

        private int CreateEmployee(Employee emp)
        {
            _context.Employee.Add(emp);
            _context.SaveChanges();

            emp = _context.Employee.Where(x => x.Name == emp.Name).Where(x => x.Gender == emp.Gender).FirstOrDefault();
            return emp.EmployeeId;
        }

        private void UpdateEmployee(Employee emp)
        {
            Employee UpEmp = _context.Employee.Include(c => c.Position).SingleOrDefault(x => x.EmployeeId == emp.EmployeeId);
            UpEmp.Name = emp.Name;
            UpEmp.Gender = emp.Gender;
            _context.Employee.Update(UpEmp);
        }

        private void CreationEmployeeListFull(List<Employee> UpdateList,
                                    List<Employee> CreateList)
        {
            foreach (Employee UpdateEmp in UpdateList)
            {
                Employee UpEmp = _context.Employee.Include(c => c.Position).SingleOrDefault(x => x.EmployeeId == UpdateEmp.EmployeeId);
                UpEmp.Name = UpdateEmp.Name;
                UpEmp.Gender = UpdateEmp.Gender;
                _context.Employee.Update(UpEmp);
            }
            foreach (Employee InsertEmp in CreateList)
            {
                _context.Employee.Add(new Employee { Name = InsertEmp.Name, Gender = InsertEmp.Gender });
            }
           
        }










            public void UpdateOfficeBranchDynamic(OfficeBranchDtoCreateAllUpdate UpdateInsert)
        {
            List<PositionDetailsDtoCreateUpdate> UpdateList = new List<PositionDetailsDtoCreateUpdate>();
            List<PositionDetailsDtoCreateUpdate> CreateList = new List<PositionDetailsDtoCreateUpdate>();
            List<Position> DeleteList = new List<Position>();

            CreationOffice(UpdateInsert);

            OfficeBranch officeBranch = _context.OfficeBranch.Include(c => c.Position).SingleOrDefault(x => x.OfficeBranchId == UpdateInsert.OfficeBranchId);
            List<int> existingIds = officeBranch.Position.Select(x => x.PositionId).ToList();
            List<int> updateIds = new List<int>();
            
            if(UpdateInsert.PositionDetailsDtoCreateUpdate!=null)
            updateIds = UpdateInsert.PositionDetailsDtoCreateUpdate.Where(x => x.PositionId > 0).Select(x => x.PositionId).ToList();
            
            foreach (int id in existingIds)
            {
                if (!updateIds.Contains(id))
                {
                    DeleteList.Add(officeBranch.Position.Single(x => x.PositionId == id));
                }

            }
            if (UpdateInsert.PositionDetailsDtoCreateUpdate != null)
                UpdateList.AddRange(UpdateInsert.PositionDetailsDtoCreateUpdate.Where(x => x.PositionId > 0));
            if (UpdateInsert.PositionDetailsDtoCreateUpdate != null)
                CreateList.AddRange(UpdateInsert.PositionDetailsDtoCreateUpdate.Where(x => x.PositionId == 0));
            CreationList(UpdateList, CreateList, DeleteList , UpdateInsert.OfficeBranchId);
            
            _context.SaveChanges();
        }


        private void CreationOffice(OfficeBranchDtoCreateAllUpdate UpdateInsert)
        {
            // OfficeBranch Insert Or Update
            if (_context.OfficeBranch.Find(UpdateInsert.OfficeBranchId) != null)
            {
                //if OfficeBranch Exist Update
                UpdateOfficeBranch(UpdateInsert);
            }
            else
            {
                //if OfficeBranch Not Exist Insert
                UpdateInsert.OfficeBranchId = InsertOfficeBranchFromAll(UpdateInsert);
            }
        }



        private void CreationList ( List<PositionDetailsDtoCreateUpdate> UpdateList ,
                                    List<PositionDetailsDtoCreateUpdate> CreateList,
                                    List<Position> DeleteList ,int id)
        {

            // For Every Position in Conteiner
            foreach (PositionDetailsDtoCreateUpdate UpdatePos in UpdateList)
            {
                //If Position Exist Update
                UpdatePosition(UpdatePos,id);
            }
            foreach (PositionDetailsDtoCreateUpdate InsertPos in CreateList)
            {
                //If Position Not Exist Insert
                InsertPos.OfficeBranchId = id.ToString();
                CreatePosition(InsertPos);
            }
            foreach (Position DeletePos in DeleteList)
            {
                Position pos = _context.Position.Find(DeletePos.PositionId);
                _context.Position.Remove(pos);
            }

        }





        private void UpdateOfficeBranch(OfficeBranchDtoCreateAllUpdate UpdateInsert)
        {
            OfficeBranch ofBra = _context.OfficeBranch.Find(UpdateInsert.OfficeBranchId);
            ofBra.Name = UpdateInsert.Name;
            ofBra.Address = UpdateInsert.Address;
            _context.OfficeBranch.Update(ofBra);
        }

        private int InsertOfficeBranchFromAll(OfficeBranchDtoCreateAllUpdate UpdateInsert)
        {
            OfficeBranch office = new OfficeBranch
            {
                Name = UpdateInsert.Name,
                Address = UpdateInsert.Address
            };
            _context.OfficeBranch.Add(office);
            _context.SaveChanges();
            var ofiiceCreated = _context.OfficeBranch
                   .Where(b => b.Name == UpdateInsert.Name)
                   .Where(b => b.Address == UpdateInsert.Address)
                   .FirstOrDefault();
            return ofiiceCreated.OfficeBranchId;
        }

        private void UpdatePosition(PositionDetailsDtoCreateUpdate UpdatePos ,int id)
        {
            Position posCre = _context.Position.Find(UpdatePos.PositionId);
            posCre.Name = UpdatePos.Name;
            posCre.OfficeBranchId = id;
            posCre.EmployeeId = Convert.ToInt32(UpdatePos.EmployeeId);
            _context.Position.Update(posCre);
        }

        private void CreatePosition(PositionDetailsDtoCreateUpdate UpdatePos)
        {
            Position posCre = new Position
            {
                Name = UpdatePos.Name,
                EmployeeId = Convert.ToInt32(UpdatePos.EmployeeId),
                OfficeBranchId = Convert.ToInt32(UpdatePos.OfficeBranchId)
            };
            _context.Position.Add(posCre);
        }


    }
}
