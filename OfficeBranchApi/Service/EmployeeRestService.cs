using Microsoft.EntityFrameworkCore;
using OfficeBranchApi.Condext;
using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.Service
{
    public class EmployeeRestService : IEmployeeRestService
    {
        public readonly DbContextRepo _context;

        public EmployeeRestService(DbContextRepo context)
        {
            _context = context;
        }


        public async Task<ResultGet<IEnumerable<EmployeeDto>>> GetAllEmployeesByResultSetAsyncNoPosition(ResultSet resultSet)
        {

            ResultGet<IEnumerable<EmployeeDto>> ret = new ResultGet<IEnumerable<EmployeeDto>>();

            IQueryable<Employee> queryable = _context.Employee;
            //if (resultSet.seartchName != null) { queryable = queryable.Where<Employee>(c => c.Name.Contains(resultSet.seartchName)); }
            if (resultSet.seartchBy != null) { queryable = queryable.Where<Employee>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }
            if (resultSet.orderBy != null && resultSet.orderBy.Equals("name"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Employee => Employee.Name);
                }
                else
                {
                    queryable = queryable.OrderBy(Employee => Employee.Name);
                }
            }
            else if (resultSet.orderBy != null && resultSet.orderBy.Equals("employeeId"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Employee => Employee.EmployeeId);
                }
                else
                {
                    queryable = queryable.OrderBy(Employee => Employee.EmployeeId);
                }
            }
            else
            {
                queryable = queryable.OrderBy(Employee => Employee.EmployeeId);
            }
            List<int>EmployeeIdWithPositions = new List<int>();
            EmployeeIdWithPositions = _context.Position.Where(pos => pos.EmployeeId != null).Select(pos => pos.EmployeeId.Value).ToList();

            List<int> EmployeeIdAll = new List<int>();
            EmployeeIdAll = _context.Employee.Select(emp => emp.EmployeeId).ToList();

            List<int> ReturnEmployeeId = new List<int>();
            ReturnEmployeeId = EmployeeIdAll;
            foreach (int id in EmployeeIdWithPositions)
            {
                if (EmployeeIdAll.Contains(id))
                {
                    ReturnEmployeeId.Remove(id);
                }

            }

            queryable = queryable.Where(Employee => ReturnEmployeeId.Contains(Employee.EmployeeId));


            queryable = queryable.Skip(resultSet.page * resultSet.pageSize);
            queryable = queryable.Take(resultSet.pageSize);
            ret.items = DtoSet.SetEmployeeDtoList(queryable.ToList());
            ret.totalCount = await (GetCount(resultSet));
            return ret;


        }








            public async Task<ResultGet<IEnumerable<EmployeeDto>>> GetAllEmployeesByResultSetAsync(ResultSet resultSet)
        {

            ResultGet<IEnumerable<EmployeeDto>> ret = new ResultGet<IEnumerable<EmployeeDto>>();

            IQueryable<Employee> queryable = _context.Employee;
            //if (resultSet.seartchName != null) { queryable = queryable.Where<Employee>(c => c.Name.Contains(resultSet.seartchName)); }
            if (resultSet.seartchBy != null) { queryable = queryable.Where<Employee>(c => EF.Functions.Like(c.Name,resultSet.seartchBy)); }
            if (resultSet.orderBy!=null && resultSet.orderBy.Equals("name"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Employee => Employee.Name);
                }
                else
                {
                    queryable = queryable.OrderBy(Employee => Employee.Name);
                }
            }
            else if(resultSet.orderBy != null && resultSet.orderBy.Equals("employeeId"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Employee => Employee.EmployeeId);
                }
                else
                {
                    queryable = queryable.OrderBy(Employee => Employee.EmployeeId);
                }
            }
            else if(resultSet.orderBy != null && resultSet.orderBy.Equals("position"))
            {
                if (!resultSet.orderByAsc)
                {
                    queryable = queryable.OrderByDescending(Employee => Employee.Position.PositionId);
                }
                else
                {
                    queryable = queryable.OrderBy(Employee => Employee.Position.PositionId);
                }
            }
            else
            {
                queryable = queryable.OrderBy(Employee => Employee.EmployeeId);
            }
            queryable = queryable.Skip(resultSet.page * resultSet.pageSize);
            queryable = queryable.Take(resultSet.pageSize);

            ret.items = DtoSet.SetEmployeeDtoList(queryable.ToList());
            ret.totalCount = await (GetCount(resultSet));
            return ret;
        }

        private async Task<long> GetCount(ResultSet resultSet)
        {
            IQueryable<Employee> queryable = _context.Employee;
            if (resultSet.seartchBy != null) { queryable = queryable.Where<Employee>(c => EF.Functions.Like(c.Name, resultSet.seartchBy)); }
            return await (queryable.CountAsync());
        }





        public EmployeeDetailDto GetEmployeeById(int id)
        {
            return DtoSet.SetEmployeeDetailDto(_context.Employee.Include(c=>c.Position).SingleOrDefault(x => x.EmployeeId == id));
            //return await _context.Employee.FindAsync(id);
        }

        public void InsertEmployee(EmployeeDtoCreateUpdate emp)
        {
            _context.Employee.Add(new Employee { Name = emp.Name, Gender = emp.Gender /*, Position = _context.Position.Find(Convert.ToInt16(emp.PositionId))*/ });
            _context.SaveChanges();
        }

        public void UpdateEmployee(EmployeeDtoCreateUpdate emp)
        {
            if (_context.Employee.Find(emp.EmployeeId) != null)
            {
                Employee UpEmp = _context.Employee.Include(c => c.Position).SingleOrDefault(x => x.EmployeeId == emp.EmployeeId);
                    UpEmp.Name = emp.Name;
                    UpEmp.Gender = emp.Gender;
                if (emp.PositionId != null & !emp.PositionId.Equals(""))
                {
                    UpEmp.Position = _context.Position.Find(emp.PositionId);
                    Position pos = _context.Position.Find(emp.PositionId);
                    pos.EmployeeId = emp.EmployeeId;
                    _context.Position.Update(pos);
                }
                else if(UpEmp.Position!=null)
                {
                    Position pos = _context.Position.Find(UpEmp.Position.PositionId);
                    pos.EmployeeId = null;
                    _context.Position.Update(pos);
                }
                _context.Employee.Update(UpEmp);
            }
            else
            {
                _context.Employee.Add(new Employee { Name = emp.Name, Gender = emp.Gender });
                _context.SaveChanges();

                var EmployeeCreated = _context.Employee
                     .Where(b => b.Name == emp.Name)
                     .Where(b => b.Gender == emp.Gender)
                     .FirstOrDefault();

                if (emp.PositionId != null & emp.PositionId!=0 ){ 
                    Position pos = _context.Position.Find(emp.PositionId);
                    pos.EmployeeId = EmployeeCreated.EmployeeId;
                    _context.Position.Update(pos);
                }
            }
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            Employee emp = new Employee();
            emp = _context.Employee.Include(c => c.Position).SingleOrDefault(x => x.EmployeeId == id);
            if (emp.Position != null)
            {
                Position position = _context.Position.Find(emp.Position.PositionId);

                 position.EmployeeId = null;
                _context.Position.Update(position);
            }

            _context.Employee.Remove(emp);
            _context.SaveChanges();
        }
    }
}
