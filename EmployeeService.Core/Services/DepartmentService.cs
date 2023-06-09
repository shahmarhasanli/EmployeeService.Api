using AutoMapper;
using EmployeeService.Core.Dtos;
using EmployeeService.Core.Exceptions;
using EmployeeService.Core.Extensions;
using EmployeeService.Core.Interfaces;
using EmployeeService.Core.Models;


namespace EmployeeService.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService(IUnitOfWork departmentRepository,IMapper mapper)
        {
            _unitOfWork = departmentRepository;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.Departments.GetAllDepartmentsAsync();
            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid id)
        {
            var department = await _unitOfWork.Departments.GetDepartmentByIdAsync(id);

            if (department is null)
                throw new NotFoundException($"Department is not found with id:{id}");

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<Guid> AddDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            department.Id = Guid.NewGuid();
            department.SetCreated(DateTime.Now);

            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();

            return department.Id;
        }

        public async Task DeleteDepartmentAsync(Guid id)
        {
            var department = await _unitOfWork.Departments.GetDepartmentByIdAsync(id);
            if (department is null)
                throw new NotFoundException($"Department is not found with ID:{id}");

            department.SetDeleted(DateTime.Now);
            _unitOfWork.Departments.Update(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(Guid id,DepartmentDto departmentDto)
        {
            var department = await _unitOfWork.Departments.GetDepartmentByIdAsync(id);

            if (department is null)
            {
                throw new NotFoundException($"Department with id={id} not found");
            }
            _mapper.Map<DepartmentDto, Department>(departmentDto, department);
            department.SetUpdated(DateTime.Now);
            _unitOfWork.Departments.Update(department);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
