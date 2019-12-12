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
    public class ClientECL : IClientECL
    {
        private readonly IMapper _mapper;
        private readonly IClientDAL _dal;

        public ClientECL(IMapper mapper, IClientDAL dal)
        {
            _mapper = mapper;
            _dal = dal;
        }

        public int Count { get => _dal.Count; }

        public void Insert(Client dto)
        {
            var entity = _mapper.Map<ClientEntity>(dto);
            _dal.Insert(entity);
            dto.Id = entity.Id;
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Update(Client dto)
        {
            var entity = _mapper.Map<ClientEntity>(dto);
            _dal.Update(entity);
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Delete(Client dto)
        {
            var entity = _mapper.Map<ClientEntity>(dto);
            _dal.Delete(entity);
        }

        public void Delete(int id) => _dal.Delete(id);

        public IEnumerable<Client> Get(Expression<Func<ClientEntity, bool>> pred = null, string sort = null, char direction = 'a')
        {
            var entities = _dal.Get(pred, sort, direction);
            return _mapper.Map<List<Client>>(entities);
        }

        public Client Read(int id) => _mapper.Map<Client>(_dal.Read(id));
    }
}
