﻿using System;
using System.Diagnostics;

namespace DWSIM.ProFeatures
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class FormPortal : System.Windows.Forms.Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPortal));
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.StatusMessage = new System.Windows.Forms.Label();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label1 = new System.Windows.Forms.Label();
            this.NoLicensePanel = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.LoadingPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.notLoggedInPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.SuccessPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.NoLicensePanel.SuspendLayout();
            this.LoadingPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.notLoggedInPanel.SuspendLayout();
            this.SuccessPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(67, 15);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(451, 23);
            this.ProgressBar1.TabIndex = 0;
            // 
            // StatusMessage
            // 
            this.StatusMessage.AutoSize = true;
            this.StatusMessage.Location = new System.Drawing.Point(236, 41);
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(123, 13);
            this.StatusMessage.TabIndex = 1;
            this.StatusMessage.Text = "This is a progress label...";
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Location = new System.Drawing.Point(364, 12);
            this.LinkLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(32, 13);
            this.LinkLabel1.TabIndex = 2;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "Shop";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(77, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(290, 13);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "You don\'t have an active DWSIM Pro license. Please go to ";
            // 
            // NoLicensePanel
            // 
            this.NoLicensePanel.Controls.Add(this.Label2);
            this.NoLicensePanel.Controls.Add(this.Label1);
            this.NoLicensePanel.Controls.Add(this.LinkLabel1);
            this.NoLicensePanel.Location = new System.Drawing.Point(3, 86);
            this.NoLicensePanel.Name = "NoLicensePanel";
            this.NoLicensePanel.Size = new System.Drawing.Size(580, 47);
            this.NoLicensePanel.TabIndex = 4;
            this.NoLicensePanel.Visible = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(395, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(82, 13);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "to get a license.";
            // 
            // LoadingPanel
            // 
            this.LoadingPanel.AutoScroll = true;
            this.LoadingPanel.Controls.Add(this.ProgressBar1);
            this.LoadingPanel.Controls.Add(this.StatusMessage);
            this.LoadingPanel.Location = new System.Drawing.Point(3, 3);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(580, 77);
            this.LoadingPanel.TabIndex = 5;
            this.LoadingPanel.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.LoadingPanel);
            this.flowLayoutPanel1.Controls.Add(this.NoLicensePanel);
            this.flowLayoutPanel1.Controls.Add(this.notLoggedInPanel);
            this.flowLayoutPanel1.Controls.Add(this.SuccessPanel);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(88, 138);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(583, 249);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // notLoggedInPanel
            // 
            this.notLoggedInPanel.Controls.Add(this.label3);
            this.notLoggedInPanel.Controls.Add(this.label4);
            this.notLoggedInPanel.Controls.Add(this.linkLabel2);
            this.notLoggedInPanel.Location = new System.Drawing.Point(3, 139);
            this.notLoggedInPanel.Name = "notLoggedInPanel";
            this.notLoggedInPanel.Size = new System.Drawing.Size(580, 54);
            this.notLoggedInPanel.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "to continue.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Please";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(272, 17);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(33, 13);
            this.linkLabel2.TabIndex = 5;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Login";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // SuccessPanel
            // 
            this.SuccessPanel.Controls.Add(this.label5);
            this.SuccessPanel.Location = new System.Drawing.Point(3, 199);
            this.SuccessPanel.Name = "SuccessPanel";
            this.SuccessPanel.Size = new System.Drawing.Size(580, 42);
            this.SuccessPanel.TabIndex = 7;
            this.SuccessPanel.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(353, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Double-click on the DWSIM Pro icon to launch a session in your browser.";
            // 
            // FormPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 399);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPortal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DWSIM Pro portal";
            this.Load += new System.EventHandler(this.FormPortal_Load);
            this.NoLicensePanel.ResumeLayout(false);
            this.NoLicensePanel.PerformLayout();
            this.LoadingPanel.ResumeLayout(false);
            this.LoadingPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.notLoggedInPanel.ResumeLayout(false);
            this.notLoggedInPanel.PerformLayout();
            this.SuccessPanel.ResumeLayout(false);
            this.SuccessPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.Label StatusMessage;
        internal System.Windows.Forms.LinkLabel LinkLabel1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel NoLicensePanel;
        internal System.Windows.Forms.Panel LoadingPanel;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel notLoggedInPanel;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Panel SuccessPanel;
        private System.Windows.Forms.Label label5;
    }
}