using AutoMapper;
using Infrastructure;
using Infrastructure.DTO;
using Repository.CountryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using Infrastructure.Enums;

namespace EmployeeManagement_BLL
{
   public class CountryBLL
    {
        #region Contructor & Properties
        private readonly IMapper mapper;
        private readonly ICountryRepository repository;
        public CountryBLL(ICountryRepository _repository)
        {
            //Config Mapper
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<Country, CountryDTO>().ReverseMap();
                x.CreateMap<Country, CountryAdditionDTO>().ReverseMap();
            });
            this.mapper = config.CreateMapper();
            repository = _repository;

        }
        #endregion
        #region CRUD Operation
        public List<CountryDTO> Get()
        {
            List<CountryDTO> collection = mapper.Map<List<Country>, List<CountryDTO>>(repository.GetAll().ToList());
            return collection;
        }
        public CountryDTO Get(int id)
        {
            CountryDTO model = mapper.Map<Country, CountryDTO>(repository.GetByID(id));
            return model;
        }
        public bool Create(CountryAdditionDTO model)
        {
            Country entity = mapper.Map<CountryAdditionDTO, Country>(model);
            entity.CreatedBy = ActionBy.Anonymous_User.ToString();
            entity.CreatedDate = DateTime.Now;
            if (repository.Add(entity))
                return repository.SaveChanges();

            return false;
        }
        public ResponseCode Edit(CountryDTO model)
        {
            Country oldEntity = (Country)repository.FindAsNoTracking(b => b.Id == model.Id);
            if (oldEntity is null)
                return ResponseCode.ObjectNotFound;

            Country entity = mapper.Map<CountryDTO, Country>(model);
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
            Country entity = repository.Find(b => b.Id == id, b => b.Employees).FirstOrDefault();
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
