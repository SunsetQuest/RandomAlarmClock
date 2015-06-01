namespace RandomAlarmClock
{
    partial class AlarmControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                rTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmControl));
            this.btnEnabled = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cbPeek = new System.Windows.Forms.CheckBox();
            this.listViewAlarms = new System.Windows.Forms.ListView();
            this.labelText = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.labelInterval = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEnabled
            // 
            this.btnEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnabled.ForeColor = System.Drawing.Color.Transparent;
            this.btnEnabled.ImageIndex = 0;
            this.btnEnabled.ImageList = this.imageList1;
            this.btnEnabled.Location = new System.Drawing.Point(0, 0);
            this.btnEnabled.Name = "btnEnabled";
            this.btnEnabled.Size = new System.Drawing.Size(37, 36);
            this.btnEnabled.TabIndex = 0;
            this.btnEnabled.Tag = "";
            this.btnEnabled.UseVisualStyleBackColor = true;
            this.btnEnabled.Click += new System.EventHandler(this.btnEnabled_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Stop.png");
            this.imageList1.Images.SetKeyName(1, "Go.png");
            // 
            // cbPeek
            // 
            this.cbPeek.AutoSize = true;
            this.cbPeek.Location = new System.Drawing.Point(10, 53);
            this.cbPeek.Name = "cbPeek";
            this.cbPeek.Size = new System.Drawing.Size(51, 17);
            this.cbPeek.TabIndex = 1;
            this.cbPeek.Tag = "";
            this.cbPeek.Text = "Peek";
            this.cbPeek.UseVisualStyleBackColor = true;
            this.cbPeek.CheckedChanged += new System.EventHandler(this.cbPeek0_CheckedChanged);
            // 
            // listViewAlarms
            // 
            this.listViewAlarms.Location = new System.Drawing.Point(76, 3);
            this.listViewAlarms.Name = "listViewAlarms";
            this.listViewAlarms.Size = new System.Drawing.Size(174, 75);
            this.listViewAlarms.TabIndex = 14;
            this.listViewAlarms.TabStop = false;
            this.listViewAlarms.Tag = "";
            this.listViewAlarms.UseCompatibleStateImageBehavior = false;
            this.listViewAlarms.View = System.Windows.Forms.View.SmallIcon;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(42, 116);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(0, 13);
            this.labelText.TabIndex = 12;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(76, 137);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(174, 20);
            this.txtText.TabIndex = 4;
            this.txtText.Tag = "";
            this.txtText.Validating += new System.ComponentModel.CancelEventHandler(this.txtText_Validating);
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(6, 115);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(64, 13);
            this.labelInterval.TabIndex = 13;
            this.labelInterval.Text = "Avg Interval";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(76, 112);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(174, 20);
            this.txtInterval.TabIndex = 3;
            this.txtInterval.Validating += new System.ComponentModel.CancelEventHandler(this.txtInterval0_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(76, 87);
            this.txtName.MaxLength = 10;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(174, 20);
            this.txtName.TabIndex = 2;
            this.txtName.Tag = "";
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // AlarmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnabled);
            this.Controls.Add(this.cbPeek);
            this.Controls.Add(this.listViewAlarms);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.txtInterval);
            this.Name = "AlarmControl";
            this.Size = new System.Drawing.Size(259, 163);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnabled;
        private System.Windows.Forms.CheckBox cbPeek;
        private System.Windows.Forms.ListView listViewAlarms;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
    }
}
