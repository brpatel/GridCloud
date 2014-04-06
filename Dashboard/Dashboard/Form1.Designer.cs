namespace Dashboard
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.busNameValue = new System.Windows.Forms.Label();
            this.busIdValue = new System.Windows.Forms.Label();
            this.busTypeValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.busIdLabel = new System.Windows.Forms.Label();
            this.busAreaNumberValue = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.busVoltageValue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.busBaseKiloVoltageValue = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.busPhaseAngleValue = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.busPhaseAngleValue);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.busBaseKiloVoltageValue);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.busVoltageValue);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.busAreaNumberValue);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.busNameValue);
            this.panel1.Controls.Add(this.busIdValue);
            this.panel1.Controls.Add(this.busTypeValue);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.busIdLabel);
            this.panel1.Location = new System.Drawing.Point(200, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 235);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            this.panel1.Click += new System.EventHandler(this.Panel_Click);
            // 
            // busNameValue
            // 
            this.busNameValue.AutoSize = true;
            this.busNameValue.Location = new System.Drawing.Point(165, 70);
            this.busNameValue.Name = "busNameValue";
            this.busNameValue.Size = new System.Drawing.Size(13, 13);
            this.busNameValue.TabIndex = 5;
            this.busNameValue.Text = "0";
            // 
            // busIdValue
            // 
            this.busIdValue.AutoSize = true;
            this.busIdValue.Location = new System.Drawing.Point(165, 38);
            this.busIdValue.Name = "busIdValue";
            this.busIdValue.Size = new System.Drawing.Size(13, 13);
            this.busIdValue.TabIndex = 4;
            this.busIdValue.Text = "0";
            // 
            // busTypeValue
            // 
            this.busTypeValue.AutoSize = true;
            this.busTypeValue.Location = new System.Drawing.Point(165, 100);
            this.busTypeValue.Name = "busTypeValue";
            this.busTypeValue.Size = new System.Drawing.Size(13, 13);
            this.busTypeValue.TabIndex = 3;
            this.busTypeValue.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Bus Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bus Name:";
            // 
            // busIdLabel
            // 
            this.busIdLabel.AutoSize = true;
            this.busIdLabel.Location = new System.Drawing.Point(49, 38);
            this.busIdLabel.Name = "busIdLabel";
            this.busIdLabel.Size = new System.Drawing.Size(40, 13);
            this.busIdLabel.TabIndex = 0;
            this.busIdLabel.Text = "Bus Id:";
            // 
            // busAreaNumberValue
            // 
            this.busAreaNumberValue.AutoSize = true;
            this.busAreaNumberValue.Location = new System.Drawing.Point(165, 129);
            this.busAreaNumberValue.Name = "busAreaNumberValue";
            this.busAreaNumberValue.Size = new System.Drawing.Size(13, 13);
            this.busAreaNumberValue.TabIndex = 7;
            this.busAreaNumberValue.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Bus Area Number:";
            // 
            // busVoltageValue
            // 
            this.busVoltageValue.AutoSize = true;
            this.busVoltageValue.Location = new System.Drawing.Point(165, 151);
            this.busVoltageValue.Name = "busVoltageValue";
            this.busVoltageValue.Size = new System.Drawing.Size(13, 13);
            this.busVoltageValue.TabIndex = 9;
            this.busVoltageValue.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Bus Voltage :";
            // 
            // busBaseKiloVoltageValue
            // 
            this.busBaseKiloVoltageValue.AutoSize = true;
            this.busBaseKiloVoltageValue.Location = new System.Drawing.Point(165, 177);
            this.busBaseKiloVoltageValue.Name = "busBaseKiloVoltageValue";
            this.busBaseKiloVoltageValue.Size = new System.Drawing.Size(13, 13);
            this.busBaseKiloVoltageValue.TabIndex = 11;
            this.busBaseKiloVoltageValue.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(49, 177);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Base kV:";
            // 
            // busPhaseAngleValue
            // 
            this.busPhaseAngleValue.AutoSize = true;
            this.busPhaseAngleValue.Location = new System.Drawing.Point(165, 200);
            this.busPhaseAngleValue.Name = "busPhaseAngleValue";
            this.busPhaseAngleValue.Size = new System.Drawing.Size(13, 13);
            this.busPhaseAngleValue.TabIndex = 13;
            this.busPhaseAngleValue.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 200);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Bus Phase Angle:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 377);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label busIdLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label busNameValue;
        private System.Windows.Forms.Label busIdValue;
        private System.Windows.Forms.Label busTypeValue;
        private System.Windows.Forms.Label busPhaseAngleValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label busBaseKiloVoltageValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label busVoltageValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label busAreaNumberValue;
        private System.Windows.Forms.Label label5;
    }
}

