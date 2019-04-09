using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Reflection;

namespace RegexTool.Core
{
    public class OpResult<T> where T : class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public OpResult(T data)
            : this(data, true, string.Empty)
        {

        }

        public OpResult(string errorMsg)
            : this(null, false, errorMsg)
        {
        }

        public OpResult(T data, bool success, string errorMsg)
        {
            Data = data;
            Success = success;
            ErrorMessage = errorMsg;
        }
    }

    public interface IFileManager
    {
        OpResult<PageFile> Open(string path);
        OpResult<string> Save(PageFile model, string path);
    }

    /// <summary>
    /// TODO we need to think about versions.
    /// </summary>
    public class FileManager : IFileManager
    {
        public OpResult<PageFile> Open(string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PageFile));

                using (StreamReader sr = new StreamReader(path))
                {
                    var data = (PageFile)serializer.Deserialize(sr);

                    if (data != null)
                    {
                        return new OpResult<PageFile>(data);
                    }
                    else
                    {
                        return new OpResult<PageFile>("Failed to open the file");
                    }
                }

            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("FileManager.Open: " + ex.Message);
#endif
                return new OpResult<PageFile>(ex.Message);
            }
        }


        public OpResult<string> Save(PageFile model, string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PageFile));
                using (var stream = File.Open(path, FileMode.OpenOrCreate))
                {
                    serializer.Serialize(stream, model);
                    stream.Close();
                }

                return new OpResult<string>(path, true, string.Empty);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("FileManager.Open: " + ex.Message);
#endif
                return new OpResult<string>(ex.Message);
            }
        }
    }


    public interface IRecentFilesMonitor
    {
        int MaxItemsCount { get; }

        List<string> RecentItems { get; }

        void Add(string file, bool autoSave = true);

        void Remove(string file, bool autoSave = true);

        void Clear();
    }

    //public interface IRecentProjectsMonitor : IRecentFilesMonitor
    //{

    //}

    public class RecentFilesMonitor : IRecentFilesMonitor
    {
        private string _sysFileName;

        public string SysFileName
        {
            get { return _sysFileName; }
            protected set { _sysFileName = value; }
        }

        private int _maxItemsCount;

        public int MaxItemsCount
        {
            get { return _maxItemsCount; }
            protected set { _maxItemsCount = value; }
        }

        public RecentFilesMonitor(string fileName, int maxItemsCount)
        {
            //TODO validation goes here
            _sysFileName = fileName ?? "recent.txt";
            _maxItemsCount = maxItemsCount < 5 ? 5 : maxItemsCount;
        }

        private List<string> _items = null;

        public List<string> RecentItems
        {
            get
            {
                if (_items == null)
                    _items = LoadItems();

                return _items;
            }
        }

        protected virtual string FileLocation
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;

                if (codeBase.StartsWith("file:///"))
                {
                    codeBase = codeBase.Substring(8);
                }

                Debug.WriteLine("codeBase: " + codeBase);
                string path = Path.Combine(Path.GetDirectoryName(codeBase), SysFileName);
#if DEBUG
                Debug.WriteLine("Recent files saved in: " + path);
#endif
                return path;//TODO
            }
        }

        protected List<string> LoadItems()
        {
            string location = FileLocation;
            var files = new List<string>();

            if (!File.Exists(location))
                return files;

            using (StreamReader sw = new StreamReader(location))
            {
                string line = null;

                while (false == string.IsNullOrEmpty(line = sw.ReadLine()))
                {
                    line = line.ToLower();
                    if (files.Contains(line))
                    {
                        continue;
                    }
                    files.Add(line.ToLower());
                }
                sw.Close();
            }

            return files;
        }

        protected void Save(List<string> files)
        {
            try
            {
                var location = FileLocation;

                if (File.Exists(location))
                {
                    //Clear existing ones
                    File.WriteAllText(location, "");
                }

                using (StreamWriter sw = new StreamWriter(location))
                {
                    foreach (var file in files)
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            sw.WriteLine(file);
                        }
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RecentFilesMonitor.Save error:");
                Debug.WriteLine(ex.Message);
            }
        }

        public void Add(string file, bool autoSave = true)
        {
            try
            {
                var files = LoadItems();

                if (files.Contains(file))
                {
                    return;
                }

                files.Add(file);

                while (files.Count > MaxItemsCount)
                {
                    files.RemoveAt(0);
                }

                _items = null;
                if (autoSave) Save(files);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("RecentFilesMonitor.Save error:");
                Debug.WriteLine(ex.Message);
            }
        }

        public void Clear()
        {
            _items = null;
            Save(new List<string>());
        }


        public void Remove(string file, bool autoSave = true)
        {
            if (file == null) return;

            var items = LoadItems();

            if(items.Contains(file))
            {
                _items = null;
                items.Remove(file);
                if (autoSave) Save(items);
            }
        }
    }

    //public class RecentProjectsMonitor : RecentFilesMonitor, IRecentProjectsMonitor
    //{
    //    public RecentProjectsMonitor()
    //    {
    //        SysFileName = "recent2.data";
    //        MaxItemsCount = 15;
    //    }
    //}

}
