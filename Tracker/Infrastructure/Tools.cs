using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

using TrackerCommon;

namespace Tracker.Infrastructure
{
    public static class Tools
    {
        public static Locator Locator { get => Application.Current.Resources[Constants.Locator] as Locator ?? new Locator(); }

        public static string Hexify(byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException("array");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append($"[length={array.Length}]: 0x");
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using var derivebytes = new Rfc2898DeriveBytes(password, salt, iterations);
            return derivebytes.GetBytes(length);
        }

        public static IEnumerable<T> GetValues<T>() where T : Enum => Enum.GetValues(typeof(T)).Cast<T>();

        public static BitmapImage CachedImage(string uri) => CachedImage(new Uri(uri));

        public static BitmapImage CachedImage(Uri uri)
        {
            BitmapImage ret = new BitmapImage();
            ret.BeginInit();
            ret.UriSource = uri;
            ret.CacheOption = BitmapCacheOption.OnLoad;
            ret.EndInit();
            return ret;
        }

        public static Uri PackedUri(string res) => new Uri($"pack://application:,,,/Tracker;component/resources/{res}");

        public static string Normalize(double d)
        {
            if (d < 1_000) return d.ToString("n0") + " bytes";
            if (d < 1_000_000) return Math.Round(d / 1_000, 2).ToString("n2") + " KB";
            if (d < 1_000_000_000) return Math.Round(d / 1_000_000, 2).ToString("n2") + " MB";
            return Math.Round(d / 1_000_000_000, 2).ToString("n2") + " GB";
        }

        public static IEnumerable<string> GetImages(Assembly a)
        {
            List<string> ret = new List<string>();
            string[] resources = a.GetManifestResourceNames();
            foreach (var r in resources)
            {
                if (!r.Contains("g.resources"))
                {
                    continue;
                }
                ResourceSet rs = null;
                try
                {
                    rs = new ResourceSet(a.GetManifestResourceStream(r));
                }
                catch
                {
                    rs?.Dispose();
                    continue;
                }
                var hashes = rs.Cast<DictionaryEntry>().ToList();
                rs.Dispose();
                List<string> keys = new List<string>();
                foreach (var hash in hashes)
                {
                    if (!hash.Key.ToString().EndsWith("-32.png"))
                    {
                        continue;
                    }
                    keys.Add(hash.Key.ToString());
                }
                var uris = from k in keys orderby k select k;   // sort em
                foreach (var uri in uris)
                {
                    string u = uri;
                    if (!u.StartsWith("/"))
                    {
                        u = "/" + u;
                    }
                    string l = u.ToLower();
                    if (l.Contains("database", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    if (!l.Contains("resources/", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    ret.Add(u);
                }
            }
            ret.Insert(0, "");
            return ret;
        }
    }
}
