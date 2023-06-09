using AutoMapper;
using EmployeeService.Core.Dtos;
using EmployeeService.Core.Exceptions;
using EmployeeService.Core.Extensions;
using EmployeeService.Core.Interfaces;
using EmployeeService.Core.Models;


namespace EmployeeService.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
        {
            return  _mapper.Map<List<EmployeeDto>>(await _unitOfWork.Employees.GetAllAsync(cancellationToken));
        }

        public async Task<List<EmployeeDto>> GetFilteredAndPaginatedEmployeesAsync(string search, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Employees.GetFilteredAndPaginatedAsync(search, page, pageSize,cancellationToken);
            return _mapper.Map<List<EmployeeDto>>(result);
        }

        public async Task<EmployeeDetailDto> GetEmployeeDetailsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<EmployeeDetailDto>(await _unitOfWork.Employees.GetEmployeeDetailByIdAsync(id,cancellationToken));
        }

        public async Task<EmployeeDetailDto> AddEmployeeAsync(EmployeeDto employeeDto,CancellationToken cancellationToken = default)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Id = Guid.NewGuid();

            employee.SetCreated(DateTime.Now);
            await _unitOfWork.Employees.AddAsync(employee, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EmployeeDetailDto>(employee);
        }

        public async Task DeleteEmployeeAsync(Guid id,CancellationToken cancellationToken = default)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeDetailByIdAsync(id, cancellationToken);
            if (employee is null)
            {
                throw new NotFoundException($"Employee with id={id} not found");
            }
            employee.SetDeleted(DateTime.Now);
            _unitOfWork.Employees.Update(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEmployeeAsync(Guid id,EmployeeDto updatedEmployee,CancellationToken cancellationToken = default)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeDetailByIdAsync(id, cancellationToken);
            
            if (employee is null)
            {
                throw new NotFoundException($"Employee with id={id} not found");
            }
            _mapper.Map<EmployeeDto, Employee>(updatedEmployee, employee);
            employee.SetUpdated(DateTime.Now);
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }

}
