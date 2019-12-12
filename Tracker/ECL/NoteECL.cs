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
    public class NoteECL : INoteECL
    {
        private readonly IMapper _mapper;
        private readonly INoteDAL _dal;

        public NoteECL(IMapper mapper, INoteDAL dal)
        {
            _mapper = mapper;
            _dal = dal;
        }

        public int Count { get => _dal.Count; }

        public void Insert(Note dto)
        {
            var entity = _mapper.Map<NoteEntity>(dto);
            _dal.Insert(entity);
            dto.Id = entity.Id;
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Update(Note dto)
        {
            var entity = _mapper.Map<NoteEntity>(dto);
            _dal.Update(entity);
            dto.RowVersion = entity.RowVersion.ArrayCopy();
        }

        public void Delete(Note dto)
        {
            var entity = _mapper.Map<NoteEntity>(dto);
            _dal.Delete(entity);
        }

        public void Delete(int id) => _dal.Delete(id);

        public IEnumerable<Note> Get(Expression<Func<NoteEntity, bool>> pred = null, string sort = null, char direction = 'a')
        {
            var entities = _dal.Get(pred, sort, direction);
            return _mapper.Map<List<Note>>(entities);
        }

        public IEnumerable<Note> GetForClient(int cid) => Get(x => x.ClientId == cid, "Date", 'd');

        public Note Read(int id) => _mapper.Map<Note>(_dal.Read(id));

        public bool ClientHasNotes(int cid) => GetForClient(cid).Any();
    }
}
