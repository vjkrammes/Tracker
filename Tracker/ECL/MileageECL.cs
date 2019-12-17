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
    public class MileageECL : IMileageECL
    {
        private readonly IMapper _mapper;
        private readonly IMileageDAL _dal;

        public MileageECL(IMapper mapper, IMileageDAL dal)
        {
            _mapper = mapper;
            _dal = dal;
        }

        public int Count { get => _dal.Count; }

        public void Insert(Mileage dto)
        {
            var entity = _mapper.Map<MileageEntity>(dto);
            _dal.Insert(entity);
            dto.Id = entity.Id;
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Update(Mileage dto)
        {
            var entity = _mapper.Map<MileageEntity>(dto);
            _dal.Update(entity);
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Delete(Mileage dto)
        {
            var entity = _mapper.Map<MileageEntity>(dto);
            _dal.Delete(entity);
        }

        public void Delete(int id) => _dal.Delete(id);

        public IEnumerable<Mileage> Get(Expression<Func<MileageEntity, bool>> pred = null, string sort = null, char direction = 'a')
        {
            var entities = _dal.Get(pred, sort, direction);
            return _mapper.Map<List<Mileage>>(entities);
        }

        public IEnumerable<Mileage> GetForClient(int cid) => Get(x => x.ClientId == cid, "Date", 'd');

        public IEnumerable<Mileage> Extract() => Get(null, "Date", 'd');

        public IEnumerable<Mileage> ExtractYear(int year) => Get(x => x.Date.Year == year, "Date", 'd');

        public IEnumerable<Mileage> ExtractYear(int cid, int year) => Get(x => x.ClientId == cid && x.Date.Year == year);

        public IEnumerable<Mileage> ExtractRange(int start, int end) =>
            Get(x => x.Date.Year >= start && x.Date.Year <= end);

        public IEnumerable<Mileage> ExtractRange(int cid, int start, int end) =>
            Get(x => x.ClientId == cid && x.Date.Year >= start && x.Date.Year <= end);

        public Mileage Read(int id) => _mapper.Map<Mileage>(_dal.Read(id));

        public bool ClientHasMileage(int cid) => GetForClient(cid).Any();

        public decimal TotalMiles() => _dal.TotalMiles();

        public decimal TotalMiles(int cid) => _dal.TotalMiles(cid);
    }
}
