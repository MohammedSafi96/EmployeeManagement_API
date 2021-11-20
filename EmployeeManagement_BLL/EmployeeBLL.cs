using AutoMapper;
using Infrastructure;
using Infrastructure.DTO;
using Repository.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using Infrastructure.Enums;
using Repository.DepartmentRepository;
using Repository.CountryRepository;

namespace EmployeeManagement_BLL
{
    public class EmployeeBLL
    {
        #region Contructor & Properties
        private readonly IMapper mapper;
        private readonly IEmployeeRepository repository;
        private IDepartmentRepository departmentRepository;
        private ICountryRepository countryRepository;
        public EmployeeBLL(IEmployeeRepository _repository, IDepartmentRepository _departmentRepository, ICountryRepository _countryRepository)
        {
            //Config Mapper
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                x.CreateMap<Employee, EmployeeAdditionDTO>().ReverseMap();
            });
            this.mapper = config.CreateMapper();
            repository = _repository;
            departmentRepository = _departmentRepository;
            countryRepository = _countryRepository;

        }
        #endregion
        #region CRUD Operation
        public List<EmployeeDTO> Get()
        {
            List<EmployeeDTO> collection = mapper.Map<List<Employee>, List<EmployeeDTO>>(repository.GetAll().ToList());
            return collection;
        }
        public EmployeeDTO Get(int id)
        {
            EmployeeDTO model = mapper.Map<Employee, EmployeeDTO>(repository.GetByID(id));
            return model;
        }
        public ResponseCode Create(EmployeeAdditionDTO model)
        {
            //Check if selected department or country is found?
            if(!departmentRepository.Find(b => b.Id == model.DepartmentId).Any())
            {
                return ResponseCode.SelectedDepartmentIDNotExist;
            }
            if (!countryRepository.Find(b => b.Id == model.CountryId).Any())
            {
                return ResponseCode.SelectedCountryIDNotExist;
            }

            Employee entity = mapper.Map<EmployeeAdditionDTO, Employee>(model);
            entity.CreatedBy = ActionBy.Anonymous_User.ToString();
            entity.CreatedDate = DateTime.Now;
            if (repository.Add(entity) && repository.SaveChanges())
                return ResponseCode.Success;

            return ResponseCode.Failed;
        }
        public ResponseCode Edit(EmployeeDTO model)
        {
            Employee oldEntity = (Employee)repository.FindAsNoTracking(b => b.Id == model.Id);
            if (oldEntity is null)
                return ResponseCode.ObjectNotFound;

            Employee entity = mapper.Map<EmployeeDTO, Employee>(model);
            entity.CreatedBy = oldEntity.CreatedBy;
            entity.CreatedDate = oldEntity.CreatedDate;
            entity.ModifiedBy = ActionBy.Anonymous_User.ToString();
            entity.ModifiesDate = DateTime.Now;
            if (repository.Update(entity) && repository.SaveChanges())
                return ResponseCode.Success;

            return ResponseCode.Failed;
        }
        public ResponseCode Delete(int id)
        {
            Employee entity = repository.GetByID(id);
            if (entity is null)
                return ResponseCode.ObjectNotFound;
            
            if (repository.Delete(entity) && repository.SaveChanges())
                return ResponseCode.Success;

            return ResponseCode.Failed;
        }
        #endregion
    }
}
