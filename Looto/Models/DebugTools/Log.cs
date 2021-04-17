using System;
using System.IO;

namespace Looto.Models.DebugTools
{
    /// <summary>Class for writing informational data to log file.</summary>
    class Log
    {
        private readonly string _path = ".data\\_logs\\.log";

        /// <summary>
        /// Create log writer with default path to file. <br/>
        /// Default path = ".data\\_logs\\.log".
        /// </summary>
        public Log() 
        {
            CreateLogsDirectory();
        }
        /// <summary>Create log writer with custom path.</summary>
        /// <param name="path">Custom path.</param>
        /// <exception cref="IOException">Throws when path is the path to directory.</exception>
        public Log(string path)
        {
            if (Directory.Exists(path))
                throw new IOException("Path must be a file");

            _path = path;
            CreateLogsDirectory();
        }

        /// <summary>Add new log entry to the log file.</summary>
        /// <param name="message">Message of the log entry.</param>
        /// <param name="type">Type of the entry. Helps to see, where info entry and where error entry.</param>
        public void AppendLogMessage(string message, string type = "INFO")
        {
            try
            {
                string log = $"{DateTime.Now:MM.dd.yy - HH:mm:ss.fff}  ---  {type}  ---  {message}";
                using (var stream = File.AppendText(_path))
                    stream.WriteLine(log);
            }
            catch { }
        }

        /// <summary>Create all needed directories to avoid <see cref="DirectoryNotFoundException"/>.</summary>
        private void CreateLogsDirectory()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(_path));
        }
    }
}
