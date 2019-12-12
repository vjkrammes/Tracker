using System.Collections.Generic;
using System.IO;
using System.Linq;

using Tracker.Interfaces;

namespace Tracker.Infrastructure
{
    public class ExplorerService : IExplorerService
    {
        public IEnumerable<FileInfo> GetFiles(string path)
        {
            List<FileInfo> ret = new List<FileInfo>();
            var files = Directory.GetFiles(path, "*.*");
            foreach (var file in files)
            {
                ret.Add(new FileInfo(file));
            }
            return ret;
        }

        private static readonly List<string> _exclusions = new List<string>
        {
            "$Recycle.Bin", "$RECYCLE.BIN", "Config.Msi", "Recovery", "System Volume Information"
        };

        public IEnumerable<DirectoryInfo> GetDirectories(string path)
        {
            List<DirectoryInfo> ret = new List<DirectoryInfo>();
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                if (_exclusions.Contains(Path.GetFileName(directory)))
                {
                    continue;
                }
                ret.Add(new DirectoryInfo(directory));
            }
            return ret;
        }

        public IEnumerable<DriveInfo> GetDrives() => DriveInfo.GetDrives().ToList();
    }
}
