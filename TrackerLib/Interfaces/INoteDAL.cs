using System.Collections.Generic;

using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface INoteDAL : IDAL<NoteEntity>, IIdDAL<NoteEntity>
    {
        IEnumerable<NoteEntity> GetForClient(int cid);
        bool ClientHasNotes(int cid);
    }
}
