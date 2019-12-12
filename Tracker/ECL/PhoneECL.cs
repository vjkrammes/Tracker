using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AutoMapper;

using Tracker.ECL.DTO;
using Tracker.ECL.Interfaces;

using TrackerCommon;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace Tracker.ECL
{
    public class PhoneECL : IPhoneECL
    {
        private readonly IMapper _mapper;
        private readonly IPhoneDAL _dal;

        public PhoneECL(IMapper mapper, IPhoneDAL dal)
        {
            _mapper = mapper;
            _dal = dal;
        }

        public int Count { get => _dal.Count; }

        public void Insert(Phone dto)
        {
            var entity = _mapper.Map<PhoneEntity>(dto);
            _dal.Insert(entity);
            dto.Id = entity.Id;
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Update(Phone dto)
        {
            var entity = _mapper.Map<PhoneEntity>(dto);
            _dal.Update(entity);
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Delete(Phone dto)
        {
            var entity = _mapper.Map<PhoneEntity>(dto);
            _dal.Delete(entity);
        }

        public void Delete(int id) => _dal.Delete(id);

        public IEnumerable<Phone> Get(Expression<Func<PhoneEntity, bool>> pred = null, string sort = null, char direction = 'a')
        {
            var entities = _dal.Get(pred, sort, direction);
            return _mapper.Map<List<Phone>>(entities);
        }

        public IEnumerable<Phone> GetForClient(int cid) => Get(x => x.ClientId == cid, "Number", 'a');

        public IEnumerable<Phone> GetForPhoneType(int ptid) => Get(x => x.PhoneTypeId == ptid, "Number", 'a');

        public Phone Read(int id) => _mapper.Map<Phone>(_dal.Read(id));

        public bool ClientHasPhones(int cid) => GetForClient(cid).Any();

        public bool PhoneTypeHasPhones(int ptid) => GetForPhoneType(ptid).Any();
    }
}
