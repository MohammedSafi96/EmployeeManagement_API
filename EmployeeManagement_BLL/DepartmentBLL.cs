using AutoMapper;
using Infrastructure;
using Infrastructure.DTO;
using Repository.DepartmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using Infrastructure.Enums;

namespace EmployeeManagement_BLL
{
    public class DepartmentBLL
    {
        #region Contructor & Properties
        private readonly IMapper mapper;
        private readonly IDepartmentRepository repository;
        public DepartmentBLL(IDepartmentRepository _repository)
        {
            //Config Mapper
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<Department, DepartmentDTO>().ReverseMap();
                x.CreateMap<Department, DepartmentAdditionDTO>().ReverseMap();
            });
            this.mapper = config.CreateMapper();
            repository = _repository;

        }
        #endregion
        #region CRUD Operation
        public List<DepartmentDTO> Get()
        {
            List<DepartmentDTO> collection = mapper.Map<List<Department>, List<DepartmentDTO>>(repository.GetAll().ToList());
            return collection;
        }
        public DepartmentDTO Get(int id)
        {
            DepartmentDTO model = mapper.Map<Department, DepartmentDTO>(repository.GetByID(id));
            return model;
        }
        public bool Create(DepartmentAdditionDTO model)
        {
            Department entity = mapper.Map<DepartmentAdditionDTO, Department>(model);
            entity.CreatedBy = ActionBy.Anonymous_User.ToString();
            entity.CreatedDate = DateTime.Now;
            if (repository.Add(entity))
                return repository.SaveChanges();
            
            return false;
        }
        public ResponseCode Edit(DepartmentDTO model)
        {
            Department oldEntity = (Department)repository.FindAsNoTracking(b => b.Id == model.Id);
            if(oldEntity is null)
                return ResponseCode.ObjectNotFound;

            Department entity = mapper.Map<DepartmentDTO, Department>(model);
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
            Department entity = repository.Find(b => b.Id == id, b => b.Employees).FirstOrDefault();
            if (entity is null)
                return ResponseCode.ObjectNotFound;
            else if (entity.Employees.Count > 0)
                return ResponseCode.ObjectLinkedWithEmployee;

            if (entity is not null && repository.Delete(entity) && repository.SaveChanges())
                return ResponseCode.Success;

            return ResponseCode.Failed;
        }
        #endregion
    }
}
