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
using System.Diagnostics;
using System.Net.Mail;
using System.ServiceProcess;

namespace M2MServiceManager
{
    static class ServiceUtility
    {
        /// <summary>
        /// Stops and restarts a specified service
        /// </summary>
        /// <param name="ServiceName">The service name </param>
        /// <param name="TimeOutSec">Optionl Timout in seconds</param>
        /// <returns></returns>
        public static bool BounceService(string ServiceName, int TimeOutSec = 180)
        {
            bool Exists = false;
            foreach (ServiceController SvcCtrl in ServiceController.GetServices())
                if (SvcCtrl.ServiceName == ServiceName)
                {
                    Exists = true;
                    WriteToApplicationLog("Bouncing Service: " + ServiceName, EventLogEntryType.Information);
                    try
                    {
                        switch (SvcCtrl.Status)
                        {
                            case ServiceControllerStatus.Stopped:
                                SvcCtrl.Start();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                            case ServiceControllerStatus.StopPending:
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Start();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                            case ServiceControllerStatus.PausePending:
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Paused, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Stop();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Start();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                            case ServiceControllerStatus.ContinuePending:
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Stop();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Start();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                            default:
                                SvcCtrl.Stop();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Start();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        WriteToApplicationLog(ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
                        return false;
                    }
                    WriteToApplicationLog("Service restarted successfully: " + ServiceName, EventLogEntryType.SuccessAudit);
                    break;
                }
            if (!Exists)
            {
                WriteToApplicationLog("Service Not Found: " + ServiceName, EventLogEntryType.SuccessAudit);
                throw new Exception("Service Not Found: " + ServiceName);
            }
            return true;
        }
        /// <summary>
        /// Stops the specified service
        /// </summary>
        /// <param name="ServiceName">The service name </param>
        /// <param name="TimeOutSec">Optionl Timout in seconds</param>
        /// <returns></returns>
        public static bool StopService(string ServiceName, int TimeOutSec = 180)
        {
            bool Exists = false;
            foreach (ServiceController SvcCtrl in ServiceController.GetServices())
                if (SvcCtrl.ServiceName == ServiceName)
                {
                    Exists = true;
                    WriteToApplicationLog("Stop Service: " + ServiceName, EventLogEntryType.Information);
                    try
                    {
                        if (SvcCtrl.Status == ServiceControllerStatus.Stopped)
                            return true;
                        switch (SvcCtrl.Status)
                        {
                            case ServiceControllerStatus.PausePending:
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Paused, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Stop();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                            case ServiceControllerStatus.ContinuePending:
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                SvcCtrl.Stop();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                            default:
                                SvcCtrl.Stop();
                                SvcCtrl.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteToApplicationLog(ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
                        return false;
                    }
                    WriteToApplicationLog("Service Stopped successfully: " + ServiceName, EventLogEntryType.SuccessAudit);
                    break;
                }
            if (!Exists)
            {
                WriteToApplicationLog("Service Not Found: " + ServiceName, EventLogEntryType.SuccessAudit);
                throw new Exception("Service Not Found: " + ServiceName);
            }
            return true;
        }
        /// <summary>
        /// Starts the specified service
        /// </summary>
        /// <param name="ServiceName">The service name </param>
        /// <param name="TimeOutSec">Optionl Timout in seconds</param>
        /// <returns></returns>
        public static bool StartService(string ServiceName, int TimeOutSec = 180)
        {
            bool Exists = false;
            foreach (ServiceController SvcCtrl in ServiceController.GetServices())
                if (SvcCtrl.ServiceName == ServiceName)
                {
                    Exists = true;
                    WriteToApplicationLog("Start Service: " + ServiceName, EventLogEntryType.Information);
                    try
                    {
                        if (SvcCtrl.Status == ServiceControllerStatus.Stopped)
                        {
                            SvcCtrl.Start();
                            SvcCtrl.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(TimeSpan.TicksPerSecond * TimeOutSec));
                        }
                        else throw new Exception(ServiceName + "Service is already running!");
                    }
                    catch (Exception ex)
                    {
                        WriteToApplicationLog(ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
                        return false;
                    }
                    WriteToApplicationLog("Service Started successfully: " + ServiceName, EventLogEntryType.SuccessAudit);
                    break;
                }
            if (!Exists)
            {
                WriteToApplicationLog("Service Not Found: " + ServiceName, EventLogEntryType.SuccessAudit);
                throw new Exception("Service Not Found: " + ServiceName);
            }
            return true;
        }
        /// <summary>
        /// Attempts to kill a specified process
        /// </summary>
        /// <param name="ServiceName">The process by name </param>
        /// <param name="TimeOutSec">Optionl Timout in seconds</param>
        /// <returns></returns>
        public static bool KillProcess(string ProcessName, int TimeOutSec = 180)
        {
            Process[] LocalProcess = Process.GetProcessesByName(ProcessName);
            foreach (Process Proc in LocalProcess)
            {
                try
                {
                    WriteToApplicationLog("Attempting to kill process: " + ProcessName, EventLogEntryType.Information);
                    Proc.Kill();
                    return Proc.WaitForExit(TimeOutSec * 1000);
                }
                catch (Exception ex)
                {
                    WriteToApplicationLog(ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Sends Email to the IT group
        /// </summary>
        /// <param name="body"></param>
        /// <param name="MsgSubject"></param>
        public static bool SendMail(string body, string MsgSubject, bool HighPriority = false)
        {
            if (!Program.Appcfg.SendMail)
                return true;
            try
            {
                SmtpClient smtp = new SmtpClient(Program.Appcfg.MailServerName);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Program.Appcfg.FromMailAddress);
                foreach (string Addr in Program.Appcfg.MailingList)
                    mail.To.Add(new MailAddress(Addr));
                mail.Subject = MsgSubject;
                if (HighPriority)
                    mail.Priority = MailPriority.High;
                mail.Body = body;
                smtp.Send(mail);
                WriteToApplicationLog("Server Sent Mail", EventLogEntryType.SuccessAudit);
                return true;
            }
            catch (Exception ex)
            {
                WriteToApplicationLog("Critical Error During Mail Send" + Environment.NewLine + ex.Message, EventLogEntryType.SuccessAudit);
                return false;
            }

        }
        /// <summary>
        /// Writes events to the Winodws Event Log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        public static void WriteToApplicationLog(string message, EventLogEntryType type, int id = 0)
        {
            try
            {
                if (!EventLog.SourceExists("M2MServiceManager"))
                    EventLog.CreateEventSource("M2MServiceManager", "Application");
                EventLog.WriteEntry("M2MServiceManager", message, type, id);
            }
            catch { }
        }
    }
}
