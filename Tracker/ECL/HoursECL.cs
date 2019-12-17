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
    public class HoursECL : IHoursECL
    {
        private readonly IMapper _mapper;
        private readonly IHoursDAL _dal;

        public HoursECL(IMapper mapper, IHoursDAL dal)
        {
            _mapper = mapper;
            _dal = dal;
        }

        public int Count { get => _dal.Count; }

        public void Insert(Hours dto)
        {
            var entity = _mapper.Map<HoursEntity>(dto);
            _dal.Insert(entity);
            dto.Id = entity.Id;
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Update(Hours dto)
        {
            var entity = _mapper.Map<HoursEntity>(dto);
            _dal.Update(entity);
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Delete(Hours dto)
        {
            var entity = _mapper.Map<HoursEntity>(dto);
            _dal.Delete(entity);
        }

        public void Delete(int id) => _dal.Delete(id);

        public IEnumerable<Hours> Get(Expression<Func<HoursEntity, bool>> pred = null, string sort = null, char direction = 'a')
        {
            var entities = _dal.Get(pred, sort, direction);
            return _mapper.Map<List<Hours>>(entities);
        }

        public IEnumerable<Hours> GetForClient(int cid) => Get(x => x.ClientId == cid, "Date", 'd');

        public IEnumerable<Hours> Extract() => Get(null, "Date", 'd');

        public IEnumerable<Hours> ExtractYear(int year) => Get(x => x.Date.Year == year, "Date", 'd');

        public IEnumerable<Hours> ExtractYear(int cid, int year) => Get(x => x.ClientId == cid && x.Date.Year == year, "Date", 'd');

        public IEnumerable<Hours> ExtractRange(int start, int end) =>
            Get(x => x.Date.Year >= start && x.Date.Year <= end, "Date", 'd');

        public IEnumerable<Hours> ExtractRange(int cid, int start, int end) =>
            Get(x => x.ClientId == cid && x.Date.Year >= start && x.Date.Year <= end, "Date", 'd');

        public Hours Read(int id) => _mapper.Map<Hours>(_dal.Read(id));

        public bool ClientHasHours(int cid) => GetForClient(cid).Any();

        public decimal TotalHours() => _dal.TotalHours();

        public decimal TotalHours(int cid) => _dal.TotalHours(cid);
    }
}
