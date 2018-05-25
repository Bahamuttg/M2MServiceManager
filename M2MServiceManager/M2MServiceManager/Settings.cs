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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;

namespace M2MServiceManager
{
    public partial class Settings : Form
    {
        private bool OverrideEvent = false;
        public ToolTip ttTipController;
        PropertyInfo[] AppcfgProps = Program.Appcfg.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the Checkstate of the CheckEdites based on the settings stored in the settings file
        /// </summary>
        /// <param name="Group">The Gropubox to look in</param>
        private void SetCheckBoxes(GroupBox Group)
        {
            foreach (Control control in Group.Controls)
            {
                if (control is CheckBox)
                {
                    try
                    {
                        if (AppcfgProps.Count(X => X.Name == control.Name.Replace("chk", string.Empty)) > 0)
                        {
                            if ((bool)AppcfgProps.First(X => X.Name == control.Name.Replace("chk", string.Empty)).GetValue(Program.Appcfg))
                                ((CheckBox)control).Checked = true;
                            else
                                ((CheckBox)control).Checked = false;
                        }
                    }
                    catch { }
                }
            }
        }
        /// <summary>
        /// Checks the email addresses to make sure they're at least somewhat valid.
        /// </summary>
        /// <returns></returns>
        private bool ValidateMailAddresses()
        {
            try
            {
                string[] addresses = txtMailingList.Text.Split(';');
                foreach (var item in addresses)
                {
                    MailMessage MsgTest = new MailMessage();
                    MsgTest.To.Add(new MailAddress(item.Trim()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid Mail Recipient Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Sets the Text of the TextBox based on the settings stored in the settings file
        /// </summary>
        /// <param name="Group">The Gropubox to look in</param>
        private void SetTxtBoxes(GroupBox Group)
        {

            foreach (Control control in Group.Controls)
            {
                if (control is TextBox)
                {
                    try
                    {
                        if (AppcfgProps.Count(X => X.Name == control.Name.Replace("txt", string.Empty)) > 0)
                        {
                            object Val = AppcfgProps.First(X => X.Name == control.Name.Replace("txt", string.Empty)).GetValue(Program.Appcfg);
                            if (typeof(IList).IsAssignableFrom(Val.GetType()))
                            {
                                List<string> Addr = (List<string>)Val;
                                for (int i = 0; i < Addr.Count; i++)
                                    if (!string.IsNullOrWhiteSpace(Addr[i]))
                                    {
                                        if (i >= Addr.Count - 1)
                                            ((TextBox)control).Text += Addr[i];
                                        else
                                            ((TextBox)control).Text += Addr[i] + ";";
                                    }
                            }
                            else
                                ((TextBox)control).Text = (string)Val;
                        }
                    }
                    catch (Exception ex) { }
                }
            }
        }

        #region Event Handlers
        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                ttTipController = new ToolTip()
                {
                    AutoPopDelay = 10000,
                    InitialDelay = 500,
                    ReshowDelay = 500,
                    Active = true,
                    UseAnimation = true,
                    IsBalloon = true,
                    ToolTipIcon = ToolTipIcon.Info,
                };
                //Setup tooltips for controls
                ttTipController.SetToolTip(txtSvcClientCfgPathTarget, "This is the path to the Service Client Configuration file the M2M server uses to assign clients to specific M2M servers.");
                ttTipController.SetToolTip(txtSvcClientCfgPathBak, "This is the path to the Service Client Configuration backup file.");
                ttTipController.SetToolTip(txtNetSvcHostName, "This is the name of the M2M Net Services Host as displayed in the windows service manager.");
                ttTipController.SetToolTip(txtNetSvcHostProcessName, "This is the name of the process as displayed in the windows task manager for the M2M Net Services Host.");
                ttTipController.SetToolTip(txtProcSvcName, "This is the name of the M2M Processor Service Host as displayed in the windows service manager.");
                ttTipController.SetToolTip(txtProcSvcProcessName, "This is the name of the process as displayed in the windows task manager for the M2M Processor Services Host.");
                ttTipController.SetToolTip(txtNotifierSvcName, "This is the name of the M2M Notification Service Host as displayed in the windows service manager.");
                ttTipController.SetToolTip(txtNotifierSvcProcessName, "This is the name of the process as displayed in the windows task manager for the M2M Notifier Services Host.");
                ttTipController.SetToolTip(txtLegacySvcName, "This is the name of the M2M VFP Legacy Service as displayed in the windows service manager.");
                ttTipController.SetToolTip(chkIncludeProcSvc, "Checking this option will bounce the processor service. If a server in the M2M cluster is not processing barcode data, this option should be toggled OFF. Multiple processor services running in the same M2M server cluster could cause problems with BARCODE posting.");
                ttTipController.SetToolTip(chkWriteSvcClientCfg, "Checking this option will allow the M2M Service Manager to maintain the integrity of the Service Client Configuration file.");
                ttTipController.SetToolTip(chkKillProcesses, "Checking this option will allow the M2M Service Manager to kill unresponsive service processes if they have not responded during the alloted timeout. CAUTION: Using this option could cause data loss!");
                ttTipController.SetToolTip(txtMailServerName, "This is the name of the mail server the M2M Service Manager will use to send notification emails. EXAMPLE: mail.yoursmtpserver.com");
                ttTipController.SetToolTip(txtFromMailAddress, "This is the from address used on the email notifications. EXAMPLE: M2MServiceManager@yourdomain.com");
                ttTipController.SetToolTip(txtMailingList, "This is the list of mail recipients email is sent to. Separate email addresses with a semicolon.");
                ttTipController.SetToolTip(chkSendMail, "Checking this option will allow the M2M Service Manager to send email alerts.");
                ttTipController.SetToolTip(seTimeout, "This is the timeout in seconds the M2M Service Manager will use when cycling services and killing processes.");

                OverrideEvent = true;
                SetCheckBoxes(gbMailSettings);
                SetCheckBoxes(gbOptions);
                SetTxtBoxes(gbMailSettings);
                SetTxtBoxes(gbPaths);
                SetTxtBoxes(gbServiceNames);
                seTimeout.Value = Convert.ToDecimal(Program.Appcfg.ServiceTimoutSec);
                OverrideEvent = false;
            }
            catch (Exception ex)
            {
            }

        }
        private void txtMailingList_TextChanged(object sender, EventArgs e)
        {
            if (OverrideEvent)
                return;
            string[] addresses = txtMailingList.Text.Split(';');
            Program.Appcfg.MailingList.Clear();
            foreach (var item in addresses)
                if (!string.IsNullOrWhiteSpace(item.Trim()))
                    Program.Appcfg.MailingList.Add(item.Trim());
        }
        /// <summary>
        /// Handles Changing the state of the CheckEdites in relation to the settings file
        /// </summary>
        /// <param name="ChkBox">the CheckEdit object</param>
        /// <param name="Name">the name of the settings property</param>
        private void HandleChkBoxChange_CheckedChanged(object sender, EventArgs e)
        {
            if (OverrideEvent)
                return;
            try
            {
                var Prop = AppcfgProps.First(X => X.Name == ((CheckBox)sender).Name.Replace("chk", string.Empty));
                if (((CheckBox)sender).CheckState == CheckState.Checked)
                    Prop.SetValue(Program.Appcfg, true);
                else
                    Prop.SetValue(Program.Appcfg, false);
            }
            catch { }
        }
        /// <summary>
        /// Handles Changing the state of the TextBoxes in relation to the settings file
        /// </summary>
        /// <param name="ChkBox">the CheckEdit object</param>
        /// <param name="Name">the name of the settings property</param>
        private void HandleTxtBoxChange_TextChanged(object sender, EventArgs e)
        {
            if (OverrideEvent)
                return;
            try
            {
                var Prop = AppcfgProps.First(X => X.Name == ((TextBox)sender).Name.Replace("txt", string.Empty));
                Prop.SetValue(Program.Appcfg, ((TextBox)sender).Text);
            }
            catch { }
        }
        private void seTimeout_ValueChanged(object sender, EventArgs e)
        {
            if (OverrideEvent)
                return;
            Program.Appcfg.ServiceTimoutSec = Convert.ToInt32(seTimeout.Value);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (chkSendMail.Checked)
                if (!ValidateMailAddresses())
                    return;
            Program.Appcfg.Save();
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnFBDTarget_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == fbdFilePaths.ShowDialog())
                txtSvcClientCfgPathTarget.Text = fbdFilePaths.SelectedPath + "\\ServiceClientConfiguration.xml";
        }
        private void btnFBDBak_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == fbdFilePaths.ShowDialog())
                txtSvcClientCfgPathBak.Text = fbdFilePaths.SelectedPath + "\\ServiceClientConfiguration_Backup.xml";
        }
        #endregion

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog()
            {
                DefaultExt = ".bin",
                Filter = "Binary Config File (*.bin)|*.bin",
                Title = "Select the location to save the configuration..."
            };
            if (DialogResult.OK == SFD.ShowDialog())
            {
                if (Program.Appcfg.Save(SFD.FileName))
                    MessageBox.Show("Successfully saved config!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("An error occurred whild attempting to save the configuration... Do you have write access to the path?", "Error Saving Configuration!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            About AB = new About();
            AB.ShowDialog();
        }
    }
}
