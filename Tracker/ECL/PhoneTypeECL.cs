using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using AutoMapper;

using Tracker.ECL.DTO;
using Tracker.ECL.Interfaces;

using TrackerCommon;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace Tracker.ECL
{
    public class PhoneTypeECL : IPhoneTypeECL
    {
        private readonly IMapper _mapper;
        private readonly IPhoneTypeDAL _dal;

        public PhoneTypeECL(IMapper mapper, IPhoneTypeDAL dal)
        {
            _mapper = mapper;
            _dal = dal;
        }

        public int Count { get => _dal.Count; }

        public void Insert(PhoneType dto)
        {
            var entity = _mapper.Map<PhoneTypeEntity>(dto);
            _dal.Insert(entity);
            dto.Id = entity.Id;
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Update(PhoneType dto)
        {
            var entity = _mapper.Map<PhoneTypeEntity>(dto);
            _dal.Update(entity);
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Delete(PhoneType dto)
        {
            var entity = _mapper.Map<PhoneTypeEntity>(dto);
            _dal.Delete(entity);
        }

        public void Delete(int id) => _dal.Delete(id);

        public IEnumerable<PhoneType> Get(Expression<Func<PhoneTypeEntity, bool>> pred = null, string sort = null, char direction = 'a')
        {
            var entities = _dal.Get(pred, sort, direction);
            return _mapper.Map<List<PhoneType>>(entities);
        }

        public PhoneType Read(int id) => _mapper.Map<PhoneType>(_dal.Read(id));

        public PhoneType Read(string name) => _mapper.Map<PhoneType>(_dal.Read(name));
    }
}
