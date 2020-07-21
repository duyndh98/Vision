using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DetoursWrapper
    {
        private const string DETOURS_BIN_X86_DIR = @"%DETOURS_BIN_X86%";
        private const string DETOURS_BIN_X64_DIR = @"%DETOURS_BIN_X64%";
        private const string SYELOGD_EXE_NAME = "syelogd.exe";
        private const string WITHDLL_EXE_NAME = "withdll.exe";

        private string _detoursBinDir = string.Empty;
        private string _traceLogDir = string.Empty;

        private readonly string[] TRACE_DLL_FILE_NAMES = new string[]
        {
            "trcapi{0}.dll",
            "trclnk{0}.dll",
            "trcmem{0}.dll",
            "trcreg{0}.dll",
            "trcser{0}.dll",
            "trctcp{0}.dll"
        };

        private bool _isX64 = false;
        private bool[] _traceRequired = null;
        private string _commandline = string.Empty;

        private string _logFilePath = string.Empty;

        private Process _loggerProcess = null;
        private Process _tracerProcess = null;

        private object _locker = new object();

        public string GetLogPath()
        {
            return _logFilePath;
        }
        public void SetLogPath(string logPath)
        {
            _logFilePath = logPath;
        }

        public DetoursWrapper()
        {
            _traceLogDir = Path.Combine(Environment.ExpandEnvironmentVariables(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName), "Log");
            if (!Directory.Exists(_traceLogDir))
                Directory.CreateDirectory(_traceLogDir);
        }

        public void Init(bool isX64, bool[] traceRequired, string commandline)
        {
            if (traceRequired.Length != this.TRACE_DLL_FILE_NAMES.Length)
                throw new Exception("Trace required is not match");

            if (!traceRequired.Any(x => x == true))
                throw new Exception("Must trace at least one option");

            _isX64 = isX64;
            _traceRequired = traceRequired;
            _commandline = Environment.ExpandEnvironmentVariables(commandline);

            _detoursBinDir = Environment.ExpandEnvironmentVariables(this._isX64 ? DETOURS_BIN_X64_DIR : DETOURS_BIN_X86_DIR);            
        }

        private Process StartLogger()
        {
            var filePath = Path.Combine(_detoursBinDir , SYELOGD_EXE_NAME);
            _logFilePath = Path.Combine(_traceLogDir, Guid.NewGuid().ToString("B"));
            var arguments = @"/q /o " + _logFilePath;

            return Process.Start(filePath, arguments);
        }

        private Process StartTrace()
        {
            var filePath = Path.Combine(_detoursBinDir, WITHDLL_EXE_NAME);

            var arguments = string.Empty;
            var bitStr = _isX64 ? "64" : "32";
            for (int iTrace= 0; iTrace < TRACE_DLL_FILE_NAMES.Length; iTrace++)
            {
                if (this._traceRequired[iTrace])
                {
                    arguments += string.Format(@" -d:{0}\{1}",
                       _detoursBinDir,
                       string.Format(this.TRACE_DLL_FILE_NAMES[iTrace], bitStr));
                }
            }

            arguments += " " + _commandline;

            return Process.Start(filePath, arguments);
        }

        public void Trace()
        {
            try
            {
                lock (_locker)
                {
                    // Log
                    _loggerProcess = StartLogger();

                    // Trace
                    _tracerProcess = StartTrace();
                }
            } catch (Exception ex)
            { 
            }
        }

        public void Terminate()
        {
            try
            {
                lock (_locker)
                {
                    _tracerProcess.Kill();
                    _loggerProcess.Kill();
                }
                
            } catch (Exception ex)
            {
            }
        }

        public void Cleanup()
        {
            foreach (var filePath in Directory.GetFiles(_traceLogDir))
            {
                File.Delete(filePath);
            }
        }
    }
}
