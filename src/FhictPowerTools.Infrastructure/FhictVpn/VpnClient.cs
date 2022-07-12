using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.Core.VpnClient;

namespace FhictPowerTools.Infrastructure.FhictVpn
{
    public class VpnClient : IVpnClient
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private ProcessStartInfo CreateCliProcessStartInfo()
        {
            return new()
            {
                CreateNoWindow = false,
                RedirectStandardOutput = false,
                RedirectStandardInput = true,
                RedirectStandardError = false,
                UseShellExecute = false,
                Arguments = "",
                FileName = CliPath
            };
        }
        public VpnClient(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
            CliPath = @"C:\Program Files (x86)\Cisco\Cisco AnyConnect Secure Mobility Client\vpncli.exe";
            GuiPath = @"C:\Program Files (x86)\Cisco\Cisco AnyConnect Secure Mobility Client\vpnui.exe";
        }

        public string CliPath { get; init; }
        public bool CliExsists()
        {
            return File.Exists(CliPath);
        }

        public string GuiPath { get; init; }
        public bool GuiExists()
        {
            return File.Exists(GuiPath);
        }

        public void Connect(string host)
        {
            KillAllCliProcesses();
            ProcessStartInfo cliProcessStartInfo = CreateCliProcessStartInfo();
            cliProcessStartInfo.Arguments = $"-s connect {host}";
            cliProcessStartInfo.RedirectStandardOutput = true;
            Process process = new() {StartInfo = cliProcessStartInfo};
            process.Start();
            process.StandardInput.WriteLine(_credentialsRepository.GetUsername());
            process.StandardInput.WriteLine(_credentialsRepository.GetPassword());
            process.WaitForExit();
        }

        public void Disconnect()
        {
            Process.Start(CliPath, "disconnect")?.WaitForExit();
        }

        public bool IsConnected()
        {
            ProcessStartInfo cliProcessStartInfo = CreateCliProcessStartInfo();
            Process process = new() {StartInfo = cliProcessStartInfo};
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.Arguments = "status";
            process.Start();
            string readToEnd = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            Regex regex = new(@"state: (\w*)");
            string valueOfGroup1InLastMatch = regex.Matches(readToEnd)[^1].Groups[1].Value;
            return string.Equals(valueOfGroup1InLastMatch, "Connected", StringComparison.OrdinalIgnoreCase);
        }

        private static void KillAllCliProcesses()
        {
            Process[] processesByName = Process.GetProcessesByName("vpncli");
            foreach (Process process in processesByName)
            {
                process.Kill();
            }
        }
    }
}