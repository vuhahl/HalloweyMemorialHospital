namespace HalloweyMemorialHospital
{
    partial class SelectPatient
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMedications = new System.Windows.Forms.Button();
            this.btnAllergy = new System.Windows.Forms.Button();
            this.btnPatientDemo = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSelectPatient = new System.Windows.Forms.Button();
            this.btnPopulatePatient = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(3, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 81);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(13, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(385, 50);
            this.panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hollowell Memorial Hospital";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel2.Controls.Add(this.btnMedications);
            this.panel2.Controls.Add(this.btnAllergy);
            this.panel2.Controls.Add(this.btnPatientDemo);
            this.panel2.Location = new System.Drawing.Point(3, 399);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(409, 48);
            this.panel2.TabIndex = 2;
            // 
            // btnMedications
            // 
            this.btnMedications.Location = new System.Drawing.Point(276, 7);
            this.btnMedications.Name = "btnMedications";
            this.btnMedications.Size = new System.Drawing.Size(116, 32);
            this.btnMedications.TabIndex = 2;
            this.btnMedications.Text = "Go To Medications";
            this.btnMedications.UseVisualStyleBackColor = true;
            this.btnMedications.Click += new System.EventHandler(this.btnMedications_Click);
            // 
            // btnAllergy
            // 
            this.btnAllergy.Location = new System.Drawing.Point(143, 7);
            this.btnAllergy.Name = "btnAllergy";
            this.btnAllergy.Size = new System.Drawing.Size(116, 32);
            this.btnAllergy.TabIndex = 1;
            this.btnAllergy.Text = "Go To Allergies";
            this.btnAllergy.UseVisualStyleBackColor = true;
            this.btnAllergy.Click += new System.EventHandler(this.btnAllergy_Click);
            // 
            // btnPatientDemo
            // 
            this.btnPatientDemo.Location = new System.Drawing.Point(9, 7);
            this.btnPatientDemo.Name = "btnPatientDemo";
            this.btnPatientDemo.Size = new System.Drawing.Size(116, 32);
            this.btnPatientDemo.TabIndex = 0;
            this.btnPatientDemo.Text = "Go To Demographics";
            this.btnPatientDemo.UseVisualStyleBackColor = true;
            this.btnPatientDemo.Click += new System.EventHandler(this.btnPatientDemo_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(379, 144);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel4.Controls.Add(this.btnSelectPatient);
            this.panel4.Controls.Add(this.btnPopulatePatient);
            this.panel4.Location = new System.Drawing.Point(121, 265);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(158, 106);
            this.panel4.TabIndex = 4;
            // 
            // btnSelectPatient
            // 
            this.btnSelectPatient.Location = new System.Drawing.Point(28, 56);
            this.btnSelectPatient.Name = "btnSelectPatient";
            this.btnSelectPatient.Size = new System.Drawing.Size(99, 28);
            this.btnSelectPatient.TabIndex = 1;
            this.btnSelectPatient.Text = "Select Patient";
            this.btnSelectPatient.UseVisualStyleBackColor = true;
            this.btnSelectPatient.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnPopulatePatient
            // 
            this.btnPopulatePatient.Location = new System.Drawing.Point(28, 22);
            this.btnPopulatePatient.Name = "btnPopulatePatient";
            this.btnPopulatePatient.Size = new System.Drawing.Size(99, 28);
            this.btnPopulatePatient.TabIndex = 0;
            this.btnPopulatePatient.Text = "Get Patients";
            this.btnPopulatePatient.UseVisualStyleBackColor = true;
            this.btnPopulatePatient.Click += new System.EventHandler(this.btnPopulatePatient_Click);
            // 
            // SelectPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(409, 450);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SelectPatient";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnMedications;
        private System.Windows.Forms.Button btnAllergy;
        private System.Windows.Forms.Button btnPatientDemo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSelectPatient;
        private System.Windows.Forms.Button btnPopulatePatient;
    }
}

