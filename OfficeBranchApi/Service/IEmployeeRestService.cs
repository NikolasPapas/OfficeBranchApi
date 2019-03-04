using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OfficeBranchApi.Controllers.EmployeeRestController;

namespace OfficeBranchApi.Service
{
    public interface IEmployeeRestService
    {
        
        Task<ResultGet<IEnumerable<EmployeeDto>>> GetAllEmployeesByResultSetAsync(ResultSet resultSet);
        Task<ResultGet<IEnumerable<EmployeeDto>>> GetAllEmployeesByResultSetAsyncNoPosition(ResultSet resultSet);
        EmployeeDetailDto GetEmployeeById(int id);
        void UpdateEmployee(EmployeeDtoCreateUpdate emp);
        //void InsertEmployee(EmployeeDtoCreateUpdate emp);
        void DeleteEmployee(int id);
    }
}
