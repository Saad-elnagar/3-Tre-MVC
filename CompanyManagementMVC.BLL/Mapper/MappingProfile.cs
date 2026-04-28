using AutoMapper;
using CompanyManagementMVC.BLL.ModelVM.Department;
using CompanyManagementMVC.BLL.ModelVM.Employee;
using CompanyManagementMVC.DAL.Entity;

namespace CompanyManagementMVC.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ==================== Employee Mappings ====================

            // Entity → VM (Get)
            CreateMap<Employee, GetEmployeeVM>()
                .ForMember(dest => dest.DepartmentName,
                           opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));

            // VM → Entity (Create)
            CreateMap<CreateEmployeeVM, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore());

            // VM → Entity (Update - existing object)
            CreateMap<UpdateEmployeeVM, Employee>()
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore());


            // ==================== Department Mappings ====================

            // Entity → VM (Get)
            CreateMap<Department, DepartmentVM>()
       .ForMember(dest => dest.Employees,
                  opt => opt.MapFrom(src => src.Employees != null
                      ? src.Employees.Where(e => !e.IsDeleted).ToList()
                      : null));

            // VM → Entity (Create)
            CreateMap<CreateDepartmentVM, Department>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore());

            // VM → Entity (Update - existing object)
            CreateMap<UpdateDepartmentVM, Department>()
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore());
            CreateMap<Department, DepartmentVM>()
    .ForMember(dest => dest.Employees,
               opt => opt.MapFrom(src => src.Employees));

            CreateMap<Employee, CreateEmployeeVM>();
        }
    }
}