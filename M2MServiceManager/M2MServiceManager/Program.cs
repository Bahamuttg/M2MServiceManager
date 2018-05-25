/*
    M2M Service Manager
    Copyright © 2018 Thomas George

    This file is part of M2M Service Manager.

    M2M Service Manager is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    M2M Service Manager is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with M2M Service Manager.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace M2MServiceManager
{
    class Program
    {
        public static AppSettings Appcfg;

        static BackgroundWorker Worker = new BackgroundWorker();
        static StringBuilder ReportText = new StringBuilder();
        static FileSystemWatcher FSWatch = null;
        static System.Timers.Timer Timer = new System.Timers.Timer(1000);
        static Mutex MutexLocker = new Mutex();
        static byte[] SVCCONF;
        static bool WorkerResult;
        static bool DirtyBit = true;
        static bool FileEvent = false;
        static int EventCounter = 0;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("M2M Service Manager - Copyright 2018 Tom George");
                Console.WriteLine();
                Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY.");
                Console.WriteLine("This is free software, and you are welcome to redistribute it");
                Console.WriteLine("under the GNU General Public License");
                Console.WriteLine("You should have received a copy of the GNU General Public License");
                Console.WriteLine("along with this program. If not, see <https://www.gnu.org/licenses/>.");
                Console.WriteLine();
                Appcfg = new AppSettings();
                if (args.Length == 0)
                {
                    if (Appcfg.IsBaseCfg)
                    {
                        Console.WriteLine("A configuration has not been loaded.");
                        Console.WriteLine("Press any key to launch the settings interface.");
                        Console.ReadLine();
                        Settings S = new Settings();
                        S.ShowDialog();
                    }
                    ExecuteBounceSequenceWithConfirmation();
                }
                else
                {
                    //Validate Command line arguments.
                    bool flag = false;
                    for (int x = 0; x < args.Length; x++)
                    {
                        if (args[x].StartsWith("-"))
                        {
                            switch (args[x].ToLower().Trim())
                            {
                                case "-b":
                                case "-restart":
                                case "-bounce":
                                case "-s":
                                case "-stop":
                                case "-r":
                                case "-run":
                                case "-start":
                                    x++;
                                    if (args.Length < x + 1 || args[x].StartsWith("-"))
                                    {
                                        PrintCmdArgs();
                                        Console.WriteLine($"Invalid Command Line Parameter! Expected Service name!");
                                        return;
                                    }
                                    break;
                                case "-loadcfg":
                                    if (flag)
                                    {
                                        Console.WriteLine($"Invalid Command Line Parameter!");
                                        Console.WriteLine($"Only one configuration can be specified at runtime!");
                                        return;
                                    }
                                    flag = true;
                                    x++;
                                    if (args.Length < x + 1 || args[x].StartsWith("-"))
                                    {
                                        PrintCmdArgs();
                                        Console.WriteLine($"Invalid Command Line Parameter! Expected Cfg file path!");
                                        return;
                                    }
                                    Appcfg = new AppSettings(args[x]);
                                    Console.WriteLine($"Using configuration from: {Environment.NewLine}{args[x].ToUpper()}");
                                    break;
                                case "-?":
                                case "-h":
                                case "-help":
                                case "-man":
                                    PrintCmdArgs();
                                    break;
                                case "-svcclient":
                                case "-backup":
                                case "-settings":
                                case "-nc":
                                case "-noconfirm":
                                    break;
                                default:
                                    Console.WriteLine("Invalid Command Line Switch!");
                                    PrintCmdArgs();
                                    return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Command Line Switch!");
                            PrintCmdArgs();
                            return;
                        }
                    }
                    for (int i = 0; i < args.Length; i++)
                    {
                        switch (args[i].ToLower().Trim())
                        {
                            case "-svcclient":
                                WriteSvcClientCfg();
                                break;
                            case "-backup":
                                CopyGoodSvcConfToMem();
                                BackupSvcClientCfg();
                                break;
                            case "-b":
                            case "-restart":
                            case "-bounce":
                                i++;
                                if (ServiceUtility.BounceService(args[i], Appcfg.ServiceTimoutSec))
                                    Console.WriteLine($"Successfully bounced the {args[i]} service!");
                                else
                                    Console.WriteLine($"Failed to bounce the {args[i]} service!");
                                break;
                            case "-s":
                            case "-stop":
                                i++;
                                if (ServiceUtility.StopService(args[i], Appcfg.ServiceTimoutSec))
                                    Console.WriteLine($"Successfully stopped the {args[i]} service!");
                                else
                                    Console.WriteLine($"Failed to stop the {args[i]} service!");
                                break;
                            case "-r":
                            case "-run":
                            case "-start":
                                i++;
                                if (ServiceUtility.StartService(args[i], Appcfg.ServiceTimoutSec))
                                    Console.WriteLine($"Successfully started the {args[i]} service!");
                                else
                                    Console.WriteLine($"Failed to start the {args[i]} service!");
                                break;
                            case "-settings":
                                Settings S = new Settings();
                                S.ShowDialog();
                                return;
                            case "-nc":
                            case "-noconfirm":
                                ExecuteBounceSequence();
                                break;
                        }
                    }
                    if (flag && args.Length == 2)
                        ExecuteBounceSequenceWithConfirmation();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Abnormal Program Termination!" + Environment.NewLine + ex.Message);
            }
        }
        /// <summary>
        /// Executes the M2M Service restart sequence asynchronously using a Worker thread.
        /// </summary>
        private static void ExecuteBounceSequence()
        {
            Worker.WorkerReportsProgress = true;
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            FSWatch = new FileSystemWatcher();
            FSWatch.Path = Appcfg.SvcClientCfgPathTarget.Remove(Appcfg.SvcClientCfgPathTarget.LastIndexOf("\\"));
            FSWatch.Filter = Appcfg.SvcClientCfgPathTarget.Remove(0, Appcfg.SvcClientCfgPathTarget.LastIndexOf("\\") + 1);
            FSWatch.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
            FSWatch.Changed += FSWatch_Changed;
            FSWatch.EnableRaisingEvents = true;

            Timer.Elapsed += Timer_Elapsed;
            Timer.Enabled = true;
            Timer.AutoReset = true;

            Console.WriteLine("Initiating M2M Service Restart...");
            //TODO: Validate existence of services then run worker.
            CopyGoodSvcConfToMem();
            Worker.RunWorkerAsync();
            while (Worker.IsBusy)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.Write(Environment.NewLine);
            if (Appcfg.WriteSvcClientCfg && WorkerResult)
            {
                Console.WriteLine("Waiting for M2MService to finish writing the SVCCONF file.");
                ReportText.AppendLine($"{DateTime.Now.ToString()} --- Waiting for M2MService to finish writing the SVCCONF file.");
                Timer.Start();
                while (Timer.Enabled)
                {
                    Thread.Sleep(1000);
                    Console.Write(".");
                }
                Console.WriteLine();
                if (DirtyBit)
                    WriteSvcClientCfg();
                else
                    ReportText.AppendLine($"{DateTime.Now.ToString()} --- Service Client Configuration is the correct size ({SVCCONF.Length} bytes), skipping rebuild.");
            }

            Thread.Sleep(2000);
            if (WorkerResult)
            {
                if (!ServiceUtility.SendMail(ReportText.ToString(), Environment.MachineName + " --- Successfully Restarted M2M Services!"))
                    Console.WriteLine("Failed to send mail alert!");
            }
            else
            {
                if (!ServiceUtility.SendMail(ReportText.ToString(), Environment.MachineName + " --- Error(s) Encountered While Attempting To Restart M2M Services!", true))
                    Console.WriteLine("Failed to send mail alert!");
            }
        }
        /// <summary>
        /// asks for user confirmation before executing the M2M Service restart sequence asynchronously using a Worker thread.
        /// </summary>
        private static void ExecuteBounceSequenceWithConfirmation()
        {
            Console.WriteLine("Press the [S] key to enter the settings interface.");
            Console.WriteLine("Are you sure you want to restart the M2M services?");
            Console.WriteLine("y/n?");

            string response = Console.ReadLine().Trim().ToLower();
            if (response == "s")
            {
                Settings S = new Settings();
                S.ShowDialog();
                return;
            }
            if (response != "y")
            {
                Console.WriteLine("Aborting Sequence...");
                Thread.Sleep(2000);
                return;
            }
            try
            {
                ExecuteBounceSequence();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
                ServiceUtility.WriteToApplicationLog(ex.Message + Environment.NewLine + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Displays the command line switches available using the console window.
        /// </summary>
        private static void PrintCmdArgs()
        {
            Console.WriteLine("");
            Console.WriteLine("===========================Service Tools===================================");
            Console.WriteLine("-b, -Bounce -Restart: Restarts the target service. USAGE -b {serviceName}");
            Console.WriteLine("-s, -Stop: Stops the target service. USAGE -s {ServiceName}");
            Console.WriteLine("-r, -Run, -Start: Starts the target service. USAGE -r {ServiceName}");
            Console.WriteLine("-nc, -noconfirm: Starts the service manager without confirmation.");
            Console.WriteLine();
            Console.WriteLine("===========================SVCCONF Tools===================================");
            Console.WriteLine("-svcclient: Writes the SVCCONF file from known good backup.");
            Console.WriteLine("-backup: Creates a backup of the current SVCCONF file. (OVERWRITES BACKUP)");
            Console.WriteLine();
            Console.WriteLine("===========================Settings Tools==================================");
            Console.WriteLine("-settings: Launches the setup UI.");
            Console.WriteLine("-loadcfg: Loads a custom cfg file. USAGE -loadcfg {path to config.bin}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

        }
        /// <summary>
        /// Rebuilds the ServiceClientConfiguration File from the existing backup.
        /// </summary>
        /// <param name="WriteToBakFile"></param>
        public static void WriteSvcClientCfg()
        {
            try
            {
                if (FSWatch != null)
                    FSWatch.EnableRaisingEvents = false;
                CopyGoodSvcConfToMem();
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Attempting to rebuild Service Client Configuration...");
                Console.WriteLine("Attempting to rebuild Service Client Configuration...");
                using (MemoryStream MemStream = new MemoryStream(SVCCONF, false))
                using (FileStream FS_Writer = new FileStream(Appcfg.SvcClientCfgPathTarget, FileMode.Open, FileAccess.Write))
                {
                    MemStream.Position = 0;
                    MemStream.CopyTo(FS_Writer);
                    FS_Writer.Flush();
                    ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Successfully rebuilt Service Client Configuration.");
                    Console.WriteLine("Successfully rebuilt Service Client Configuration.");
                }
                if (FSWatch != null)
                    FSWatch.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Failed to rebuild Service Client Configuration!" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                Console.WriteLine("Failed to rebuild Service Client Configuration!" + Environment.NewLine + ex.Message);
            }
        }
        /// <summary>
        /// Backs up the SVCCONF stored in memory to the backup file.
        /// </summary>
        private static void BackupSvcClientCfg()
        {
            try
            {
                if (SVCCONF == null)
                    CopyGoodSvcConfToMem();
                if (string.IsNullOrWhiteSpace(Appcfg.SvcClientCfgPathTarget))
                    throw new Exception("Source or Target File Path Cannot Be Null Or Empty!");
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Attempting to backup Service Client Configuration...");
                Console.WriteLine("Attempting to backup Service Client Configuration...");
                FileInfo FI = new FileInfo(Appcfg.SvcClientCfgPathBak);
                using (MemoryStream MemStream = new MemoryStream(SVCCONF, false))
                using (FileStream FS_Writer = FI.Open(FileMode.OpenOrCreate, FileAccess.Write))
                {
                    MemStream.Position = 0;
                    MemStream.CopyTo(FS_Writer);
                    FS_Writer.Flush();
                }
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Successfully backed up Service Client Configuration.");
                Console.WriteLine("Successfully backed up Service Client Configuration.");
            }
            catch (Exception ex)
            {
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Failed to backup Service Client Configuration!" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                Console.WriteLine("Failed to backup Service Client Configuration!" + Environment.NewLine + ex.Message);
            }
        }
        /// <summary>
        /// Backs up the current Service Configuration File. 
        /// </summary>
        public static void CopyGoodSvcConfToMem()
        {
            try
            {
                //First get the known good backup. If the file is smaller than the one we have loaded it means that M2M has been 
                //adding clients to the SVCCONF file and it's good to use. Otherwise something wiped it and we need to use the good backup.
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Attempting to store Service Client Configuration...");
                Console.WriteLine("Attempting to store Service Client Configuration...");
                if (string.IsNullOrWhiteSpace(Appcfg.SvcClientCfgPathBak) || string.IsNullOrWhiteSpace(Appcfg.SvcClientCfgPathTarget))
                    throw new Exception("Source or Target File Path Cannot Be Null Or Empty!");
                FileInfo FI = new FileInfo(Appcfg.SvcClientCfgPathBak);
                using (MemoryStream MemStreamBak = new MemoryStream())
                using (FileStream FSStreamBak = FI.Open(FileMode.OpenOrCreate, FileAccess.Read))
                using (MemoryStream MemStreamPrime = new MemoryStream())
                using (FileStream FSStreamPrime = new FileStream(Appcfg.SvcClientCfgPathTarget, FileMode.Open, FileAccess.Read))
                {
                    FSStreamBak.CopyTo(MemStreamBak);
                    FSStreamPrime.CopyTo(MemStreamPrime);
                    if (MemStreamPrime.Length < MemStreamBak.Length)
                    {
                        MemStreamPrime.Position = 0;
                        MemStreamBak.Position = 0;
                        MemStreamBak.CopyTo(MemStreamPrime);
                    }
                    SVCCONF = MemStreamPrime.ToArray();
                }
                ReportText.AppendLine($"{DateTime.Now.ToString()} --- File size is {SVCCONF.Length} bytes.");
                Console.WriteLine($"File size is {SVCCONF.Length} bytes.");
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Successfully stored Service Client Configuration.");
                Console.WriteLine("Successfully stored Service Client Configuration.");
            }
            catch (Exception ex)
            {
                ReportText.AppendLine(DateTime.Now.ToString() + " --- " + "Failed to store Service Client Configuration!" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                Console.WriteLine("Failed to store Service Client Configuration!" + Environment.NewLine + ex.Message);
            }
        }

        #region Event Handlers
        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!FileEvent && EventCounter > 60)
            {
                Timer.Enabled = false;
                Timer.Stop();
            }
            else if (!FileEvent)
                EventCounter++;
            else
                EventCounter = 0;
            FileEvent = false;
        }

        private static void FSWatch_Changed(object sender, FileSystemEventArgs e)
        {
            FileInfo FI = new FileInfo(e.FullPath);
            if (!FI.Exists)
                return;
            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine($"{e.FullPath} Change detected! {Environment.NewLine}{FSWatch.Filter} file size is {FI.Length} bytes. {Environment.NewLine}SVCCONF size is {SVCCONF.Length} bytes.");
            //ReportText.AppendLine($"{DateTime.Now.ToString()} --- SVCCONF File Change detected!");
            if (FI.Length == SVCCONF.Length)
                DirtyBit = false;
            else
                DirtyBit = true;
            FileEvent = true;
        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerResult = (bool)e.Result;
        }

        private static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.Write(Environment.NewLine);
            Console.Write((string)e.UserState);
            MutexLocker.WaitOne();
            ReportText.AppendLine((string)e.UserState);
            MutexLocker.ReleaseMutex();
        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //============================================================================================================================================================
                //Try to stop the Notifier service
                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Attempting to stop " + Appcfg.NotifierSvcName);
                if (!ServiceUtility.StopService(Appcfg.NotifierSvcName, Appcfg.ServiceTimoutSec))
                {
                    if (Appcfg.KillProcesses)
                    {
                        Worker.ReportProgress(0, DateTime.Now.ToString() + " --- " + "Failed to stop " + Appcfg.NotifierSvcName + ", attempting to kill process...");
                        if (ServiceUtility.KillProcess(Appcfg.NotifierSvcProcessName, Appcfg.ServiceTimoutSec))
                        {
                            Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Killed " + Appcfg.NotifierSvcProcessName + ", verifying service is halted...");
                            if (ServiceUtility.StopService(Appcfg.NotifierSvcName, Appcfg.ServiceTimoutSec))
                                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Stopped " + Appcfg.NotifierSvcName);
                            else
                                throw new Exception("Couldn't Stop Service: " + Appcfg.NotifierSvcName);
                        }
                        else
                            throw new Exception("Couldn't Stop Process: " + Appcfg.NotifierSvcProcessName);
                    }
                    else
                        throw new Exception("Couldn't Stop Process: " + Appcfg.NotifierSvcProcessName);
                }
                else
                    Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Successfully stopped " + Appcfg.NotifierSvcName);
                //============================================================================================================================================================
                //Check if we are restarting the Processor Service, if we are, we need to stop it.
                if (Appcfg.IncludeProcSvc)
                {
                    Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Attempting to stop " + Appcfg.ProcSvcName);
                    //Try to stop processor service...
                    if (!ServiceUtility.StopService(Appcfg.ProcSvcName, Appcfg.ServiceTimoutSec))
                    {
                        if (Appcfg.KillProcesses)
                        {
                            Worker.ReportProgress(0, DateTime.Now.ToString() + " --- " + "Failed to stop " + Appcfg.ProcSvcName + ", attempting to kill process...");
                            if (ServiceUtility.KillProcess(Appcfg.ProcSvcProcessName, Appcfg.ServiceTimoutSec))
                            {
                                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Killed " + Appcfg.ProcSvcProcessName + ", verifying service is halted...");
                                if (ServiceUtility.StopService(Appcfg.ProcSvcName, Appcfg.ServiceTimoutSec))
                                    Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Stopped " + Appcfg.ProcSvcName);
                                else
                                    throw new Exception("Couldn't Stop Service: " + Appcfg.ProcSvcName);
                            }
                            else
                                throw new Exception("Couldn't Stop Process: " + Appcfg.ProcSvcProcessName);
                        }
                        else
                            throw new Exception("Couldn't Stop Process: " + Appcfg.NotifierSvcProcessName);
                    }
                    else
                        Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Successfully stopped " + Appcfg.ProcSvcName);
                }
                //============================================================================================================================================================
                //Try to stop the net services host...
                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Attempting to stop " + Appcfg.NetSvcHostName);
                if (!ServiceUtility.StopService(Appcfg.NetSvcHostName, Appcfg.ServiceTimoutSec))
                {
                    if (Appcfg.KillProcesses)
                    {
                        Worker.ReportProgress(0, DateTime.Now.ToString() + " --- " + "Failed to stop " + Appcfg.NetSvcHostName + ", attempting to kill process...");
                        if (ServiceUtility.KillProcess(Appcfg.NetSvcHostProcessName, Appcfg.ServiceTimoutSec))
                        {
                            Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Killed " + Appcfg.NetSvcHostProcessName + ", verifying service is halted...");
                            if (ServiceUtility.StopService(Appcfg.NetSvcHostName, Appcfg.ServiceTimoutSec))
                                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Stopped " + Appcfg.NetSvcHostName);
                            else
                                throw new Exception("Couldn't Stop Service: " + Appcfg.NetSvcHostName);
                        }
                        else
                            throw new Exception("Couldn't Stop Process: " + Appcfg.NetSvcHostProcessName);
                    }
                    else
                        throw new Exception("Couldn't Stop Process: " + Appcfg.NotifierSvcProcessName);
                }
                else
                    Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Successfully stopped " + Appcfg.NetSvcHostName);
                //============================================================================================================================================================
                //Try to start the Notifier service
                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Attempting to start " + Appcfg.NotifierSvcName);
                if (ServiceUtility.StopService(Appcfg.NotifierSvcName, Appcfg.ServiceTimoutSec))//Make sure it's not running
                {
                    if (ServiceUtility.StartService(Appcfg.NotifierSvcName, Appcfg.ServiceTimoutSec))
                        Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Successfully started " + Appcfg.NotifierSvcName);
                    else
                        throw new Exception("Couldn't Start Service: " + Appcfg.NotifierSvcName);
                }
                else
                    throw new Exception("The service could not be reliably started: " + Appcfg.NotifierSvcName);
                //============================================================================================================================================================
                //Check if we are restarting the Processor Service, if we are, try to start it.
                if (Appcfg.IncludeProcSvc)
                {
                    //Try to start the processor service
                    Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Attempting to start " + Appcfg.ProcSvcName);
                    if (ServiceUtility.StopService(Appcfg.ProcSvcName, Appcfg.ServiceTimoutSec))//Make sure it's not running
                    {
                        if (ServiceUtility.StartService(Appcfg.ProcSvcName, Appcfg.ServiceTimoutSec))
                            Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Successfully started " + Appcfg.ProcSvcName);
                        else
                            throw new Exception("Couldn't Start Service: " + Appcfg.ProcSvcName);
                    }
                    else
                        throw new Exception("The service could not be reliably started: " + Appcfg.ProcSvcName);
                }
                //============================================================================================================================================================
                //Try to start the Net Services host
                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Attempting to start " + Appcfg.NetSvcHostName);
                if (ServiceUtility.StopService(Appcfg.NetSvcHostName, Appcfg.ServiceTimoutSec))//Make sure it's not running
                {
                    if (ServiceUtility.StartService(Appcfg.NetSvcHostName, Appcfg.ServiceTimoutSec))
                        Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Successfully started " + Appcfg.NetSvcHostName);
                    else
                        throw new Exception("Couldn't Start Service: " + Appcfg.NetSvcHostName);
                }
                else
                    throw new Exception("The service could not be reliably started: " + Appcfg.NetSvcHostName);
                //============================================================================================================================================================
                e.Result = true;//if we got this far we accomplished everything we NEED to do.

                //This service is kind of usless so lets just bounce it anyway and hope for the best...
                Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Attempting to bounce " + Appcfg.LegacySvcName);
                if (!ServiceUtility.BounceService(Appcfg.LegacySvcName, Appcfg.ServiceTimoutSec))
                    Worker.ReportProgress(0, DateTime.Now.ToString() + " --- " + "Failed to bounce " + Appcfg.LegacySvcName + "! Manual restart necessary.");
                else
                    Worker.ReportProgress(1, DateTime.Now.ToString() + " --- " + "Successfully bounced " + Appcfg.LegacySvcName);
                //============================================================================================================================================================
            }
            catch (Exception ex)
            {
                e.Result = false;
                Worker.ReportProgress(-100, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        #endregion

    }
}
