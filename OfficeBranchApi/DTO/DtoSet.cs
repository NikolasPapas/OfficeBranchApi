using OfficeBranchApi.models;
using OfficeBranchApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeBranchApi.DTO
{
    public static class DtoSet
    {
        //////////////////////////////////////////////////// EMPLOYEE
        public static EmployeeDto SetEmployeeDto(Employee emp)
        {
            EmployeeDto empDto = new EmployeeDto
            {
                EmployeeId = emp.EmployeeId,
                Name = emp.Name
            };

            return empDto;

        }

        public static List<EmployeeDto> SetEmployeeDtoList(List<Employee> empList)
        {
            List<EmployeeDto> empDtoList = new List<EmployeeDto>();
            if (empList != null)
            {
                foreach (Employee emp in empList)
                {
                    empDtoList.Add(SetEmployeeDto(emp));
                }
            }

            return empDtoList;

        }


        public static EmployeeDetailDto SetEmployeeDetailDto(Employee emp)
        {
            EmployeeDetailDto empDto = new EmployeeDetailDto
            {
                EmployeeId = emp.EmployeeId,
                Name = emp.Name,
                Gender = emp.Gender,


            };
            if (emp.Position != null) { 
                empDto.PositionId = emp.Position.PositionId;
                empDto.Position = SetPositionDto(emp.Position);
            }
            return empDto;

        }

        public static List<EmployeeDetailDto> SetEmployeeDetailDtoList(List<Employee> empList)
        {
            List<EmployeeDetailDto> empDtoList = new List<EmployeeDetailDto>();
            if (empList != null)
            {
                foreach (Employee emp in empList)
                {
                    empDtoList.Add(SetEmployeeDetailDto(emp));
                }
            }
            return empDtoList;

        }



        //////////////////////////////////////////////////// EQUIPMENT
        public static EquipmentDto SetEquipmentDto(Equipment item)
        {
            EquipmentDto Dto = new EquipmentDto
            {
                EquipmentId = item.EquipmentId,
                SerialNumber = item.SerialNumber
            };
            return Dto;
        }

        public static List<EquipmentDto> SetEquipmentDtoList(List<Equipment> list)
        {
            List<EquipmentDto> dtoList = new List<EquipmentDto>();
            if (list != null)
            {
                foreach (Equipment item in list)
                {
                    dtoList.Add(SetEquipmentDto(item));
                }
            }
            return dtoList;
        }

        public static EquipmentDetailDto SetEquipmentDetailDto(Equipment item)
        {
            EquipmentDetailDto Dto = new EquipmentDetailDto
            {
                EquipmentId = item.EquipmentId,
                EquipmentTypeId = item.EquipmentTypeId,
                EquipmentType = SetEquipmentTypeDto(item.EquipmentType),
                SerialNumber = item.SerialNumber
            };
            if (item.PositionToEquipment != null)
            {
                Dto.PositionToEquipmentDtoPosi = DtoSet.SetPositionToEquipmentDtoPosi(item.PositionToEquipment);
            }
            return Dto;
        }

        public static List<EquipmentDetailDto> SetEquipmentDetailDtoList(List<Equipment> list)
        {
            List<EquipmentDetailDto> dtoList = new List<EquipmentDetailDto>();
            if (list != null)
            {
                foreach (Equipment item in list)
                {
                    dtoList.Add(SetEquipmentDetailDto(item));
                }
            }

            return dtoList;

        }






        //////////////////////////////////////////////////// EQUIPMENT TYPE
        public static EquipmentTypeDto SetEquipmentTypeDto(EquipmentType item)
        {
            EquipmentTypeDto Dto = new EquipmentTypeDto
            {
                Name = item.Name,
                EquipmentTypeId = item.EquipmentTypeId
            };

            return Dto;

        }

        public static List<EquipmentTypeDto> SetEquipmentTypeDtoList(List<EquipmentType> list)
        {
            List<EquipmentTypeDto> dtoList = new List<EquipmentTypeDto>();
            if (list != null)
            {
                foreach (EquipmentType item in list)
                {
                    dtoList.Add(SetEquipmentTypeDto(item));
                }
            }

            return dtoList;

        }



        //////////////////////////////////////////////////// OFFICE BRANCH
        public static OfficeBranchDto SetOfficeBranchDto(OfficeBranch item)
        {
            OfficeBranchDto officeDto = new OfficeBranchDto
            {
                OfficeBranchId = item.OfficeBranchId,
                Name = item.Name
            };

            return officeDto;

        }

        public static List<OfficeBranchDto> SetOfficeBranchDtoList(List<OfficeBranch> list)
        {
            List<OfficeBranchDto> dtoList = new List<OfficeBranchDto>();
            if (list != null)
            {
                foreach (OfficeBranch item in list)
                {
                    dtoList.Add(SetOfficeBranchDto(item));
                }
            }
            return dtoList;

        }

        public static OfficeBranchDetailsDto SetOfficeBranchDetailsDto(OfficeBranch item)
        {
            OfficeBranchDetailsDto officeDto = new OfficeBranchDetailsDto
            {
                OfficeBranchId = item.OfficeBranchId,
                Name = item.Name,
                Address = item.Address,
                Position = SetPositionDtoList(item.Position.ToList())
            };

            //if (item.Position!= null)
            //{
            //    officeDto.Position = SetPositionDtoList(item.Position.ToList());
            //}

            return officeDto;

        }

        public static List<OfficeBranchDetailsDto> SetOfficeBranchDetailsDtoList(List<OfficeBranch> list)
        {
            List<OfficeBranchDetailsDto> dtoList = new List<OfficeBranchDetailsDto>();
            if (list != null)
            {
                foreach (OfficeBranch item in list)
                {
                    dtoList.Add(SetOfficeBranchDetailsDto(item));
                }
            }
            return dtoList;

        }


        //////////////////////////////////////////////////// POSITION
        public static PositionDto SetPositionDto(Position item)
        {
            PositionDto Dto = null;
            if (item != null)
            {
                Dto = new PositionDto
                {
                    Name = item.Name,
                    PositionId = item.PositionId,
                    EmployeeId = item.EmployeeId
            };
                

            }
            return Dto;

        }

        public static List<PositionDto> SetPositionDtoList(List<Position> list)
        {
            List<PositionDto> dtoList = new List<PositionDto>();
            if (list != null)
            {
                foreach (Position item in list)
                {
                    dtoList.Add(SetPositionDto(item));
                }
            }
            return dtoList;

        }

        public static PositionDetailsDto SetPositionDetailsDto(Position item)
        {
            PositionDetailsDto Dto = new PositionDetailsDto
            {
                PositionId = item.PositionId,
                Name = item.Name,
                OfficeBranchId = item.OfficeBranchId,
                OfficeBranch = DtoSet.SetOfficeBranchDto(item.OfficeBranch),
                PositionToEquipmentDtoEqui = DtoSet.SetPositionToEquipmentDtoEquiList(item.PositionToEquipment),
                EmployeeId = item.EmployeeId
        };
            if (item.Employee != null)
            {
                Dto.Employee = DtoSet.SetEmployeeDto(item.Employee);
            }
                
            return Dto;

        }

        public static List<PositionDetailsDto> SetPositionDetailsDtoList(List<Position> list)
        {
            List<PositionDetailsDto> dtoList = new List<PositionDetailsDto>();
            if (list != null)
            {
                foreach (Position item in list)
                {
                    dtoList.Add(SetPositionDetailsDto(item));
                }
            }
            return dtoList;

        }






        //////////////////////////////////////////////////// POSITION TO EQUIPMENT
        public static PositionToEquipmentDtoEqui SetPositionToEquipmentDtoEqui(PositionToEquipment item)
        {
            PositionToEquipmentDtoEqui Dto = new PositionToEquipmentDtoEqui
            {
                EquipmentId = item.EquipmentId,
                //PositionId = item.PositionId,
                EquipmentDto = DtoSet.SetEquipmentDto(item.Equipment)
            };
            return Dto;
        }


        public static List<PositionToEquipmentDtoEqui> SetPositionToEquipmentDtoEquiList(List<PositionToEquipment> list)
        {
            List<PositionToEquipmentDtoEqui> dtoList = new List<PositionToEquipmentDtoEqui>();
            if (list != null)
            {
                foreach (PositionToEquipment item in list)
                {
                    dtoList.Add(SetPositionToEquipmentDtoEqui(item));
                }
            }
            return dtoList;
        }

        public static PositionToEquipmentDtoPosi SetPositionToEquipmentDtoPosi(PositionToEquipment item)
        {
            PositionToEquipmentDtoPosi Dto = new PositionToEquipmentDtoPosi
            {
                // EquipmentId = item.EquipmentId,
                PositionId = item.PositionId,
                PositionDto = DtoSet.SetPositionDto(item.Position)
            };
            return Dto;
        }

        public static List<PositionToEquipmentDtoPosi> SetPositionToEquipmentDtoPosiList(List<PositionToEquipment> list)
        {
            List<PositionToEquipmentDtoPosi> dtoList = new List<PositionToEquipmentDtoPosi>();
            if (list != null)
            {
                foreach (PositionToEquipment item in list)
                {
                    dtoList.Add(SetPositionToEquipmentDtoPosi(item));
                }
            }

            return dtoList;

        }

        //public static PositionToEquipmentDetailsDto SetPositionToEquipmentDetailsDto(PositionToEquipment item)
        //{
        //    PositionToEquipmentDetailsDto Dto = new PositionToEquipmentDetailsDto
        //    {
        //        EquipmentId = item.EquipmentId,
        //        PositionId = item.PositionId,
        //        Position = item.Position,
        //        Equipment = item.Equipment                
        //    };
        //    return Dto;
        //}

        //public static List<PositionToEquipmentDetailsDto> SetPositionToEquipmentDetailsDtoiList(List<PositionToEquipment> list)
        //{
        //    List<PositionToEquipmentDetailsDto> dtoList = new List<PositionToEquipmentDetailsDto>();
        //    if (list != null)
        //    {
        //        foreach (PositionToEquipment item in list)
        //        {
        //            dtoList.Add(SetPositionToEquipmentDetailsDtoPosi(item));
        //        }
        //    }

        //    return dtoList;

        //}


    }
}
