using System.Collections.Generic;

using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface INoteECL : IECL<NoteEntity, Note>
    {
        void Delete(int id);
        IEnumerable<Note> GetForClient(int cid);
        Note Read(int id);
        bool ClientHasNotes(int cid);
    }
}
