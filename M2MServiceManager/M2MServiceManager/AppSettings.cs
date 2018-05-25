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
using System.Collections.Generic;
using System.IO;

namespace M2MServiceManager
{
    [Serializable]
    public class AppSettings
    {
        /// <summary>
        /// False if a configuration was successfully loaded from disk.
        /// </summary>
        public bool IsBaseCfg { get { return _IsBaseCfg; } }
        /// <summary>
        /// Include the Processor Service when restarting?
        /// </summary>
        protected internal virtual bool IncludeProcSvc { get; set; }
        /// <summary>
        /// Send Email status report?
        /// </summary>
        protected internal virtual bool SendMail { get; set; }
        /// <summary>
        /// Kill off processes that don't respond to service requests?
        /// </summary>
        protected internal virtual bool KillProcesses { get; set; }
        /// <summary>
        /// Write the ServiceClientConfiguration file if it's out of date?
        /// </summary>
        protected internal virtual bool WriteSvcClientCfg { get; set; }
        /// <summary>
        /// This is the name of the M2M Net Services Host as displayed in the windows service manager.
        /// </summary>
        protected internal virtual string NetSvcHostName { get; set; }
        /// <summary>
        /// This is the name of the M2M Processor Service Host as displayed in the windows service manager.
        /// </summary>
        protected internal virtual string ProcSvcName { get; set; }
        /// <summary>
        /// This is the name of the M2M Notification Service Host as displayed in the windows service manager.
        /// </summary>
        protected internal virtual string NotifierSvcName { get; set; }
        /// <summary>
        /// This is the name of the M2M VFP Legacy Service as displayed in the windows service manager.
        /// </summary>
        protected internal virtual string LegacySvcName { get; set; }
        /// <summary>
        /// This is the name of the process as displayed in the windows task manager for the M2M Net Services Host.
        /// </summary>
        protected internal virtual string NetSvcHostProcessName { get; set; }
        /// <summary>
        /// This is the name of the process as displayed in the windows task manager for the M2M Processor Services Host.
        /// </summary>
        protected internal virtual string ProcSvcProcessName { get; set; }
        /// <summary>
        /// This is the name of the process as displayed in the windows task manager for the M2M Notifier Services Host.
        /// </summary>
        protected internal virtual string NotifierSvcProcessName { get; set; }
        /// <summary>
        /// This is the name of the mail server the M2M Service Manager will use to send notification emails. EXAMPLE: mail.yoursmtpserver.com
        /// </summary>
        protected internal virtual string MailServerName { get; set; }
        /// <summary>
        /// This is the from address used on the email notifications. EXAMPLE: M2MServiceManager@yourdomain.com
        /// </summary>
        protected internal virtual string FromMailAddress { get; set; }
        /// <summary>
        /// This is the path to the Service Client Configuration file the M2M server uses to assign clients to specific M2M servers.
        /// </summary>
        protected internal virtual string SvcClientCfgPathTarget { get; set; }
        /// <summary>
        /// This is the path to the Service Client Configuration backup file.
        /// </summary>
        protected internal virtual string SvcClientCfgPathBak { get; set; }
        /// <summary>
        /// This is the timeout in seconds the M2M Service Manager will use when cycling services and killing processes.
        /// </summary>
        protected internal virtual int ServiceTimoutSec { get; set; }
        /// <summary>
        /// This is the list of mail recipients email is sent to. Separate email addresses with a semicolon.
        /// </summary>
        protected internal virtual List<string> MailingList { get; set; }


        private string _SysPath;
        private string _ConfigPath;
        private bool _IsBaseCfg;
        /// <summary>
        /// Default ctor
        /// </summary>
        public AppSettings()
        {
            _ConfigPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            _ConfigPath = _ConfigPath.Remove(_ConfigPath.LastIndexOf("\\"));
            _ConfigPath += "\\Config.bin";
            if (!Load())
                Initialize();
        }

        public AppSettings(string ConfigPath)
        {
            _ConfigPath = ConfigPath;
            if (!Load())
                throw new Exception("Couldn't load configuration!");
        }
        /// <summary>
        /// Gathers the settings and paths from the M2M Environment.
        /// </summary>
        private bool LoadM2MEnvironment()
        {
            try
            {
                FileInfo FI = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\M2MWin.ini");
                if (!FI.Exists)
                    return false;
                using (FileStream FS = FI.OpenRead())
                using (StreamReader Reader = new StreamReader(FS))
                {
                    Reader.BaseStream.Position = 0;
                    while (!Reader.EndOfStream)
                    {
                        _SysPath = Reader.ReadLine();
                        if (_SysPath.ToUpper().Contains("SYSPATH"))
                        {
                            _SysPath = _SysPath.Split('=')[1];
                            return true;
                        }
                    }
                }
            }
            catch { return false; }
            return false;
        }

        protected internal virtual bool Save(string FilePath = null)
        {
            try
            {
                System.Runtime.Serialization.IFormatter OutFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                if (FilePath == null)
                    using (Stream FStream = new FileStream(_ConfigPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                        OutFormatter.Serialize(FStream, this);
                else
                    using (Stream FStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                        OutFormatter.Serialize(FStream, this);
            }
            catch { return false; }
            return true;
        }

        protected internal virtual bool Load()
        {
            try
            {
                AppSettings App;
                System.Runtime.Serialization.IFormatter InFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (Stream FStream = new FileStream(_ConfigPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    App = (AppSettings)InFormatter.Deserialize(FStream);
                var props = App.GetType().GetProperties(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                foreach (System.Reflection.PropertyInfo pinfo in props)
                    if (pinfo.CanWrite)
                        pinfo.SetValue(this, pinfo.GetValue(App));
            }
            catch (Exception ex)
            { return false; }
            _IsBaseCfg = false;
            return true;
        }

        protected internal virtual void Initialize()
        {
            if (LoadM2MEnvironment())
            {
                SvcClientCfgPathTarget = _SysPath + "\\Loading\\ServiceClientConfiguration.xml";
                SvcClientCfgPathBak = _SysPath + "\\Loading\\ServiceClientConfiguration_Backup.xml";
            }
            else
            {
                SvcClientCfgPathTarget = string.Empty;
                SvcClientCfgPathBak = string.Empty;
            }
            NetSvcHostName = "M2MNet750ServicesHost";
            NetSvcHostProcessName = "M2MNetServicesHost";
            ProcSvcName = "M2MNet750ProcessorServicesHost";
            ProcSvcProcessName = "M2MProcessorServicesHost";
            NotifierSvcName = "M2MNet750NotificationServicesHost";
            NotifierSvcProcessName = "NotificationServicesHost";
            LegacySvcName = "M2MVFPLegacyServiceHost";
            IncludeProcSvc = true;
            WriteSvcClientCfg = true;
            KillProcesses = false;
            SendMail = false;
            MailServerName = "";
            FromMailAddress = "";
            MailingList = new List<string>();
            ServiceTimoutSec = 180;
            _IsBaseCfg = true;
        }
    }
}
