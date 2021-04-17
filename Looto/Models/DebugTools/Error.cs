using Looto.Views;
using System;
using System.IO;
using System.Text;

namespace Looto.Models.DebugTools
{
    /// <summary>Class for bug report writing.</summary>
    class Error
    {
        private readonly string _dirPath = ".data\\_logs\\bug-reports";
        private readonly Exception _error;
        private readonly Log _log;

        /// <summary>
        /// Create new error handler instance with default path. <br/>
        /// Default path = ".data\\_logs\\bug-reports".
        /// </summary>
        /// <param name="error">Error to handle.</param>
        public Error(Exception error)
        {
            _error = error;
            _log = new Log();

            CreateBugReportDirectories();
        }

        /// <summary>Create new error handler instance with custom path.</summary>
        /// <param name="error">Error to handle.</param>
        /// <param name="path">Custom path.</param>
        /// <exception cref="IOException">Throws when path is the path to file.</exception>
        public Error(Exception error, string path)
        {
            if(File.Exists(path))
                throw new IOException("Path must be a directory");

            _error = error;
            _dirPath = path;
            _log = new Log(Path.GetDirectoryName(_dirPath));

            CreateBugReportDirectories();
        }

        /// <summary>
        /// Handle error. <br/>
        /// Write log, bug report and open window to show error.
        /// </summary>
        public void HandleError()
        {
            ErrorOccured view = new ErrorOccured(_error);
            view.Show();

            using (var stream = new StreamWriter($"{_dirPath}\\{DateTime.Now:MMddyyHHmssfff}.log", false))
            {
                StringBuilder errorData = new StringBuilder();
                errorData.Append($"Date: {DateTime.Now:G} \n");
                errorData.Append($"Message: {_error.Message} \n");
                errorData.Append($"Class: {_error.GetType().Name} \n");
                errorData.Append($"Stack trace: {_error.StackTrace} \n");
                errorData.Append($"HResult: {_error.HResult}");

                _log.AppendLogMessage(_error.Message, "EXCP");
                stream.Write(errorData.ToString());
            }
        }

        /// <summary>Create all needed directories to avoid <see cref="DirectoryNotFoundException"/>.</summary>
        private void CreateBugReportDirectories()
        {
            if (!Directory.Exists(_dirPath))
                Directory.CreateDirectory(_dirPath);
        }
    }
}
