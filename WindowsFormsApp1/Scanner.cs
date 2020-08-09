using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Scanner
    {
        public enum TraceType
        {
            Unk,
            Api,
            Lnk,
            Mem,
            Reg,
            Ser,
            Tcp,
            Count
        }

        public readonly string[] TypeStrings =
        {
            "unknown",
            "trcapi",
            "trclnk",
            "trcmem",
            "trcreg",
            "trcser",
            "trctcp"
        };

        public struct Log
        {
            public int Id;
            public string Time;
            public int Pid;
            public TraceType Type;
            public string Content;
        }

        public enum SignatureEnum
        {
            PsExec,
            Process_Injection,
            AutoRun,
            SignedBinaryProxyExecution,
            Mimikazt
        }

        private string _logPath = string.Empty;
        public void SetLogPath(string logPath)
        {
            _logPath = logPath;
        }

        private List<SignatureEnum> _scanResult = null;
        public List<SignatureEnum> GetScanResult()
        {
            return _scanResult;
        }

        public bool ContainsOrdinalIgnoreCase(string src, string sub)
        {
            return src.IndexOf(sub, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public bool ScanPsExec(Log[] logs)
        {
            var trcApiLogs = logs.Where(x => x.Type == TraceType.Reg).ToArray();
            foreach (var log in trcApiLogs)
            {
                if (ContainsOrdinalIgnoreCase(log.Content, @"Software\Sysinternals\PsExec"))
                    return true;
            }

            return false;
        }

        public bool ScanProcessInjection(Log[] logs)
        {
            var trcApiLogs = logs.Where(x => x.Type == TraceType.Api).ToArray();
            var virtualAllocExIds = trcApiLogs.Where(x =>
                ContainsOrdinalIgnoreCase(x.Content, @"+VirtualAllocEx"))
                .Select(x => x.Id).ToArray();
            var writeProcessMemoryIds = trcApiLogs.Where(x =>
                ContainsOrdinalIgnoreCase(x.Content, @"+WriteProcessMemory"))
                .Select(x => x.Id).ToArray();
            var createRemoteThreadIds = trcApiLogs.Where(x =>
                ContainsOrdinalIgnoreCase(x.Content, @"+CreateRemoteThread"))
                .Select(x => x.Id).ToArray();

            if (virtualAllocExIds.Length == 0 || writeProcessMemoryIds.Length == 0 || createRemoteThreadIds.Length == 0)
                return false;

            if (virtualAllocExIds.First() < writeProcessMemoryIds.Last()
                && writeProcessMemoryIds.First() < createRemoteThreadIds.First())
                return true;

            return false;
        }

        public bool ScanAutoRun(Log[] logs)
        {
            var trcApiLogs = logs.Where(x => x.Type == TraceType.Api).ToArray();
            if (trcApiLogs.Any(x => ContainsOrdinalIgnoreCase(x.Content, @"REG ADD") && ContainsOrdinalIgnoreCase(x.Content, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run")))
                return true;

            return false;
        }

        public bool ScanSignedBinaryProxyExecution(Log[] logs)
        {
            var trcApiLogs = logs.Where(x => x.Type == TraceType.Api).ToArray();
            if (trcApiLogs.Any(x => ContainsOrdinalIgnoreCase(x.Content, @"GetModuleFileName") && 
            (
                ContainsOrdinalIgnoreCase(x.Content, @"rundll32.exe") ||
                ContainsOrdinalIgnoreCase(x.Content, @"regsvr32.exe") ||
                ContainsOrdinalIgnoreCase(x.Content, @"mshta.exe")
            )))
                return true;

            return false;
        }

        public bool ScanMimikazt(Log[] logs)
        {
            var trcLnkLogs = logs.Where(x => x.Type == TraceType.Lnk).ToArray();
            var checkDlls = new string[]
            {
                "vaultcli.dll",
                "wlanapi.dll",
                "ntdsapi.dll",
                "netapi32.dll",
                "imm32.dll",
                "samlib.dll",
                "combase.dll",
                "srvcli.dll",
                "shcore.dll",
                "ntasn1.dll",
                "cryptdll.dll",
                "logoncli.dll"
            };
            int detectedCount = 0;
            foreach (var dllName in checkDlls)
            {
                if (trcLnkLogs.Any(x => ContainsOrdinalIgnoreCase(x.Content, dllName)))
                    detectedCount++;
            }

            if (detectedCount >= 8)
                return true;

            return false;
        }

        public Log[] PreProcess(string logData)
        {
            var dataSplited = logData.Split('\n');
            var logs = new Log[dataSplited.Length];
            for (int iLog = 0; iLog < logs.Length; iLog++)
            {
                var line = dataSplited[iLog];
                var lineSplited = line.Split(' ');

                // init
                logs[iLog].Id = iLog;
                logs[iLog].Time = string.Empty;
                logs[iLog].Pid = 0;
                logs[iLog].Type = TraceType.Unk;
                logs[iLog].Content = string.Empty;

                if (lineSplited.Length <= 0)
                    continue;
                logs[iLog].Time = lineSplited[0];

                if (lineSplited.Length <= 1)
                    continue;
                Int32.TryParse(lineSplited[1], out logs[iLog].Pid);

                if (lineSplited.Length <= 3)
                    continue;
                for (int iType = 0; iType < TypeStrings.Length; iType++)
                {
                    if (lineSplited[3].Contains(TypeStrings[iType]))
                    {
                        logs[iLog].Type = (TraceType)iType;
                        break;
                    }
                }

                if (lineSplited.Length <= 4)
                    continue;
                logs[iLog].Content = string.Empty;
                for (int iSplit = 4; iSplit < lineSplited.Length; iSplit++)
                {
                    logs[iLog].Content += lineSplited[iSplit] + " ";
                }
            }
            
            return logs;
        }

        public void Scan()
        {
            var logData = File.ReadAllText(_logPath);
            var logs = PreProcess(logData);

            var signatures = new List<SignatureEnum>();
            if (ScanPsExec(logs))
                signatures.Add(SignatureEnum.PsExec);
            
            if (ScanProcessInjection(logs))
                signatures.Add(SignatureEnum.Process_Injection);

            if (ScanAutoRun(logs))
                signatures.Add(SignatureEnum.AutoRun);

            if (ScanSignedBinaryProxyExecution(logs))
                signatures.Add(SignatureEnum.SignedBinaryProxyExecution);

            if (ScanMimikazt(logs))
                signatures.Add(SignatureEnum.Mimikazt);

            _scanResult = signatures;
        }
    }
}
