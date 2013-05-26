namespace ChickenCoopBaseStation
{
    partial class MainForm
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
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAvgLightLevel = new System.Windows.Forms.Label();
            this.lblDoorState = new System.Windows.Forms.Label();
            this.lblInstantLightLevel = new System.Windows.Forms.Label();
            this.lblFoodLevelWarning = new System.Windows.Forms.Label();
            this.lblFoodLevelLastChanged = new System.Windows.Forms.Label();
            this.lblFoodLevel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCoopDateTime = new System.Windows.Forms.Label();
            this.lblCoopTemp = new System.Windows.Forms.Label();
            this.lblDaylightBulbOn = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTimeToUpdate = new System.Windows.Forms.Label();
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.lblWaterHeaterOn = new System.Windows.Forms.Label();
            this.lblWaterSetTemp = new System.Windows.Forms.Label();
            this.lblWaterTemp = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrUpdateStatus = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrBlinkerLowFoodLevel = new System.Windows.Forms.Timer(this.components);
            this.zgTemp = new ZedGraph.ZedGraphControl();
            this.zgLight = new ZedGraph.ZedGraphControl();
            this.timerCheckEmail = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAvgLightLevel);
            this.groupBox1.Controls.Add(this.lblDoorState);
            this.groupBox1.Controls.Add(this.lblInstantLightLevel);
            this.groupBox1.Controls.Add(this.lblFoodLevelWarning);
            this.groupBox1.Controls.Add(this.lblFoodLevelLastChanged);
            this.groupBox1.Controls.Add(this.lblFoodLevel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblCoopDateTime);
            this.groupBox1.Controls.Add(this.lblCoopTemp);
            this.groupBox1.Controls.Add(this.lblDaylightBulbOn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblTimeToUpdate);
            this.groupBox1.Controls.Add(this.lblLastUpdated);
            this.groupBox1.Controls.Add(this.lblWaterHeaterOn);
            this.groupBox1.Controls.Add(this.lblWaterSetTemp);
            this.groupBox1.Controls.Add(this.lblWaterTemp);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 361);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Data";
            // 
            // lblAvgLightLevel
            // 
            this.lblAvgLightLevel.AutoSize = true;
            this.lblAvgLightLevel.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgLightLevel.ForeColor = System.Drawing.Color.Blue;
            this.lblAvgLightLevel.Location = new System.Drawing.Point(208, 194);
            this.lblAvgLightLevel.Name = "lblAvgLightLevel";
            this.lblAvgLightLevel.Size = new System.Drawing.Size(38, 25);
            this.lblAvgLightLevel.TabIndex = 12;
            this.lblAvgLightLevel.Text = "NA";
            // 
            // lblDoorState
            // 
            this.lblDoorState.AutoSize = true;
            this.lblDoorState.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoorState.ForeColor = System.Drawing.Color.Blue;
            this.lblDoorState.Location = new System.Drawing.Point(208, 144);
            this.lblDoorState.Name = "lblDoorState";
            this.lblDoorState.Size = new System.Drawing.Size(38, 25);
            this.lblDoorState.TabIndex = 13;
            this.lblDoorState.Text = "NA";
            // 
            // lblInstantLightLevel
            // 
            this.lblInstantLightLevel.AutoSize = true;
            this.lblInstantLightLevel.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstantLightLevel.ForeColor = System.Drawing.Color.Blue;
            this.lblInstantLightLevel.Location = new System.Drawing.Point(208, 169);
            this.lblInstantLightLevel.Name = "lblInstantLightLevel";
            this.lblInstantLightLevel.Size = new System.Drawing.Size(38, 25);
            this.lblInstantLightLevel.TabIndex = 10;
            this.lblInstantLightLevel.Text = "NA";
            // 
            // lblFoodLevelWarning
            // 
            this.lblFoodLevelWarning.Font = new System.Drawing.Font("Segoe UI Symbol", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoodLevelWarning.ForeColor = System.Drawing.Color.Red;
            this.lblFoodLevelWarning.Location = new System.Drawing.Point(3, 89);
            this.lblFoodLevelWarning.Name = "lblFoodLevelWarning";
            this.lblFoodLevelWarning.Size = new System.Drawing.Size(26, 29);
            this.lblFoodLevelWarning.TabIndex = 11;
            this.lblFoodLevelWarning.Text = "*";
            this.lblFoodLevelWarning.Visible = false;
            // 
            // lblFoodLevelLastChanged
            // 
            this.lblFoodLevelLastChanged.AutoSize = true;
            this.lblFoodLevelLastChanged.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoodLevelLastChanged.ForeColor = System.Drawing.Color.Blue;
            this.lblFoodLevelLastChanged.Location = new System.Drawing.Point(208, 119);
            this.lblFoodLevelLastChanged.Name = "lblFoodLevelLastChanged";
            this.lblFoodLevelLastChanged.Size = new System.Drawing.Size(38, 25);
            this.lblFoodLevelLastChanged.TabIndex = 11;
            this.lblFoodLevelLastChanged.Text = "NA";
            // 
            // lblFoodLevel
            // 
            this.lblFoodLevel.AutoSize = true;
            this.lblFoodLevel.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoodLevel.ForeColor = System.Drawing.Color.Blue;
            this.lblFoodLevel.Location = new System.Drawing.Point(208, 94);
            this.lblFoodLevel.Name = "lblFoodLevel";
            this.lblFoodLevel.Size = new System.Drawing.Size(38, 25);
            this.lblFoodLevel.TabIndex = 11;
            this.lblFoodLevel.Text = "NA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(26, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Avg Light Level:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(26, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 25);
            this.label4.TabIndex = 17;
            this.label4.Text = "Door State:";
            // 
            // lblCoopDateTime
            // 
            this.lblCoopDateTime.AutoSize = true;
            this.lblCoopDateTime.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoopDateTime.ForeColor = System.Drawing.Color.Blue;
            this.lblCoopDateTime.Location = new System.Drawing.Point(208, 19);
            this.lblCoopDateTime.Name = "lblCoopDateTime";
            this.lblCoopDateTime.Size = new System.Drawing.Size(38, 25);
            this.lblCoopDateTime.TabIndex = 18;
            this.lblCoopDateTime.Text = "NA";
            // 
            // lblCoopTemp
            // 
            this.lblCoopTemp.AutoSize = true;
            this.lblCoopTemp.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoopTemp.ForeColor = System.Drawing.Color.Blue;
            this.lblCoopTemp.Location = new System.Drawing.Point(208, 44);
            this.lblCoopTemp.Name = "lblCoopTemp";
            this.lblCoopTemp.Size = new System.Drawing.Size(38, 25);
            this.lblCoopTemp.TabIndex = 18;
            this.lblCoopTemp.Text = "NA";
            // 
            // lblDaylightBulbOn
            // 
            this.lblDaylightBulbOn.AutoSize = true;
            this.lblDaylightBulbOn.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaylightBulbOn.ForeColor = System.Drawing.Color.Blue;
            this.lblDaylightBulbOn.Location = new System.Drawing.Point(208, 69);
            this.lblDaylightBulbOn.Name = "lblDaylightBulbOn";
            this.lblDaylightBulbOn.Size = new System.Drawing.Size(38, 25);
            this.lblDaylightBulbOn.TabIndex = 15;
            this.lblDaylightBulbOn.Text = "NA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(26, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Instant Light Level:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(26, 119);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(187, 25);
            this.label14.TabIndex = 3;
            this.label14.Text = "Food Level Changed:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(26, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Food Level:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(26, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 25);
            this.label12.TabIndex = 4;
            this.label12.Text = "Coop DateTime:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(26, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 25);
            this.label8.TabIndex = 4;
            this.label8.Text = "Coop Temperature:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(26, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Daylight Bulb On:";
            // 
            // lblTimeToUpdate
            // 
            this.lblTimeToUpdate.AutoSize = true;
            this.lblTimeToUpdate.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeToUpdate.ForeColor = System.Drawing.Color.Blue;
            this.lblTimeToUpdate.Location = new System.Drawing.Point(208, 330);
            this.lblTimeToUpdate.Name = "lblTimeToUpdate";
            this.lblTimeToUpdate.Size = new System.Drawing.Size(38, 25);
            this.lblTimeToUpdate.TabIndex = 2;
            this.lblTimeToUpdate.Text = "NA";
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdated.ForeColor = System.Drawing.Color.Blue;
            this.lblLastUpdated.Location = new System.Drawing.Point(208, 305);
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(38, 25);
            this.lblLastUpdated.TabIndex = 2;
            this.lblLastUpdated.Text = "NA";
            // 
            // lblWaterHeaterOn
            // 
            this.lblWaterHeaterOn.AutoSize = true;
            this.lblWaterHeaterOn.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaterHeaterOn.ForeColor = System.Drawing.Color.Blue;
            this.lblWaterHeaterOn.Location = new System.Drawing.Point(208, 269);
            this.lblWaterHeaterOn.Name = "lblWaterHeaterOn";
            this.lblWaterHeaterOn.Size = new System.Drawing.Size(38, 25);
            this.lblWaterHeaterOn.TabIndex = 2;
            this.lblWaterHeaterOn.Text = "NA";
            // 
            // lblWaterSetTemp
            // 
            this.lblWaterSetTemp.AutoSize = true;
            this.lblWaterSetTemp.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaterSetTemp.ForeColor = System.Drawing.Color.Blue;
            this.lblWaterSetTemp.Location = new System.Drawing.Point(208, 244);
            this.lblWaterSetTemp.Name = "lblWaterSetTemp";
            this.lblWaterSetTemp.Size = new System.Drawing.Size(38, 25);
            this.lblWaterSetTemp.TabIndex = 5;
            this.lblWaterSetTemp.Text = "NA";
            // 
            // lblWaterTemp
            // 
            this.lblWaterTemp.AutoSize = true;
            this.lblWaterTemp.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaterTemp.ForeColor = System.Drawing.Color.Blue;
            this.lblWaterTemp.Location = new System.Drawing.Point(208, 219);
            this.lblWaterTemp.Name = "lblWaterTemp";
            this.lblWaterTemp.Size = new System.Drawing.Size(38, 25);
            this.lblWaterTemp.TabIndex = 8;
            this.lblWaterTemp.Text = "NA";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(26, 330);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 25);
            this.label11.TabIndex = 9;
            this.label11.Text = "Time To Update:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(26, 305);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 25);
            this.label10.TabIndex = 9;
            this.label10.Text = "Last Updated:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(26, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(158, 25);
            this.label9.TabIndex = 9;
            this.label9.Text = "Water Heater On:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(26, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "Water Set Temp:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(26, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Water Temperature:";
            // 
            // tmrUpdateStatus
            // 
            this.tmrUpdateStatus.Interval = 30000;
            this.tmrUpdateStatus.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 619);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1188, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(42, 17);
            this.statusLabel.Text = "Ready!";
            // 
            // tmrBlinkerLowFoodLevel
            // 
            this.tmrBlinkerLowFoodLevel.Interval = 300;
            this.tmrBlinkerLowFoodLevel.Tick += new System.EventHandler(this.tmrBlinkerLowFoodLevel_Tick);
            // 
            // zgTemp
            // 
            this.zgTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zgTemp.Location = new System.Drawing.Point(445, 0);
            this.zgTemp.Name = "zgTemp";
            this.zgTemp.ScrollGrace = 0D;
            this.zgTemp.ScrollMaxX = 0D;
            this.zgTemp.ScrollMaxY = 0D;
            this.zgTemp.ScrollMaxY2 = 0D;
            this.zgTemp.ScrollMinX = 0D;
            this.zgTemp.ScrollMinY = 0D;
            this.zgTemp.ScrollMinY2 = 0D;
            this.zgTemp.Size = new System.Drawing.Size(743, 619);
            this.zgTemp.TabIndex = 3;
            // 
            // zgLight
            // 
            this.zgLight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.zgLight.Location = new System.Drawing.Point(0, 378);
            this.zgLight.Name = "zgLight";
            this.zgLight.ScrollGrace = 0D;
            this.zgLight.ScrollMaxX = 0D;
            this.zgLight.ScrollMaxY = 0D;
            this.zgLight.ScrollMaxY2 = 0D;
            this.zgLight.ScrollMinX = 0D;
            this.zgLight.ScrollMinY = 0D;
            this.zgLight.ScrollMinY2 = 0D;
            this.zgLight.Size = new System.Drawing.Size(439, 238);
            this.zgLight.TabIndex = 4;
            // 
            // timerCheckEmail
            // 
            this.timerCheckEmail.Interval = 10000;
            this.timerCheckEmail.Tick += new System.EventHandler(this.timerCheckEmail_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 641);
            this.Controls.Add(this.zgLight);
            this.Controls.Add(this.zgTemp);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Chicken Coop Base Station";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAvgLightLevel;
        private System.Windows.Forms.Label lblDoorState;
        private System.Windows.Forms.Label lblInstantLightLevel;
        private System.Windows.Forms.Label lblFoodLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCoopTemp;
        private System.Windows.Forms.Label lblDaylightBulbOn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWaterHeaterOn;
        private System.Windows.Forms.Label lblWaterSetTemp;
        private System.Windows.Forms.Label lblWaterTemp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrUpdateStatus;
        private System.Windows.Forms.Label lblLastUpdated;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTimeToUpdate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Label lblCoopDateTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblFoodLevelWarning;
        private System.Windows.Forms.Timer tmrBlinkerLowFoodLevel;
        private ZedGraph.ZedGraphControl zgTemp;
        private ZedGraph.ZedGraphControl zgLight;
        private System.Windows.Forms.Label lblFoodLevelLastChanged;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer timerCheckEmail;

    }
}

