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
    public class ClientTypeECL : IClientTypeECL
    {
        private readonly IMapper _mapper;
        private readonly IClientTypeDAL _dal;

        public ClientTypeECL(IMapper mapper, IClientTypeDAL dal)
        {
            _mapper = mapper;
            _dal = dal;
        }

        public int Count { get => _dal.Count; }

        public void Insert(ClientType dto)
        {
            var entity = _mapper.Map<ClientTypeEntity>(dto);
            _dal.Insert(entity);
            dto.Id = entity.Id;
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Update(ClientType dto)
        {
            var entity = _mapper.Map<ClientTypeEntity>(dto);
            _dal.Update(entity);
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Delete(ClientType dto)
        {
            var entity = _mapper.Map<ClientTypeEntity>(dto);
            _dal.Delete(entity);
        }

        public void Delete(int id) => _dal.Delete(id);

        public IEnumerable<ClientType> Get(Expression<Func<ClientTypeEntity, bool>> pred = null, string sort = null, char direction = 'a')
        {
            var entities = _dal.Get(pred, sort, direction);
            return _mapper.Map<List<ClientType>>(entities);
        }

        public ClientType Read(int id) => _mapper.Map<ClientType>(_dal.Read(id));

        public ClientType Read(string name) => _mapper.Map<ClientType>(_dal.Read(name));
    }
}
