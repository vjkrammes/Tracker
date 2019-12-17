using System.Collections.Generic;
using System.IO;

using Tracker.Models;

namespace Tracker.Interfaces
{
    public interface IExcelManager
    {
        public void Create(FileInfo info, IEnumerable<HoursModel> hours, IEnumerable<MileageModel> mileage);
    }
}
