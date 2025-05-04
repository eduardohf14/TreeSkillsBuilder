namespace TreeSkillsBuilder
{
    partial class Main
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
            this.btnCaptureMousePos = new System.Windows.Forms.Button();
            this.muWindowNameTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.skillNameTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pointsTxt = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.positionTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.skillsDataGrid = new System.Windows.Forms.DataGridView();
            this.skill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSaveSkill = new System.Windows.Forms.Button();
            this.btnDistribute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pointsTxt)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCaptureMousePos
            // 
            this.btnCaptureMousePos.Location = new System.Drawing.Point(246, 21);
            this.btnCaptureMousePos.Name = "btnCaptureMousePos";
            this.btnCaptureMousePos.Size = new System.Drawing.Size(217, 47);
            this.btnCaptureMousePos.TabIndex = 0;
            this.btnCaptureMousePos.Text = "Capturar posição do mouse";
            this.btnCaptureMousePos.UseVisualStyleBackColor = true;
            this.btnCaptureMousePos.Click += new System.EventHandler(this.btnCaptureMousePos_Click);
            // 
            // muWindowNameTxt
            // 
            this.muWindowNameTxt.Location = new System.Drawing.Point(28, 43);
            this.muWindowNameTxt.Name = "muWindowNameTxt";
            this.muWindowNameTxt.Size = new System.Drawing.Size(281, 20);
            this.muWindowNameTxt.TabIndex = 2;
            this.muWindowNameTxt.Text = "Mu Arena Season 6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nome da janela do MU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nome da skill";
            // 
            // skillNameTxt
            // 
            this.skillNameTxt.Location = new System.Drawing.Point(22, 61);
            this.skillNameTxt.Name = "skillNameTxt";
            this.skillNameTxt.Size = new System.Drawing.Size(133, 20);
            this.skillNameTxt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Quantidade de pontos";
            // 
            // pointsTxt
            // 
            this.pointsTxt.Location = new System.Drawing.Point(22, 113);
            this.pointsTxt.Name = "pointsTxt";
            this.pointsTxt.Size = new System.Drawing.Size(133, 20);
            this.pointsTxt.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Posição X Y";
            // 
            // positionTxt
            // 
            this.positionTxt.Location = new System.Drawing.Point(19, 166);
            this.positionTxt.Name = "positionTxt";
            this.positionTxt.Size = new System.Drawing.Size(136, 20);
            this.positionTxt.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveSkill);
            this.groupBox1.Controls.Add(this.btnCaptureMousePos);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.skillNameTxt);
            this.groupBox1.Controls.Add(this.positionTxt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pointsTxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(28, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 223);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add skill";
            // 
            // skillsDataGrid
            // 
            this.skillsDataGrid.AllowUserToAddRows = false;
            this.skillsDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.skillsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skillsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.skill,
            this.points,
            this.position});
            this.skillsDataGrid.Location = new System.Drawing.Point(28, 330);
            this.skillsDataGrid.Name = "skillsDataGrid";
            this.skillsDataGrid.Size = new System.Drawing.Size(469, 226);
            this.skillsDataGrid.TabIndex = 12;
            this.skillsDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.skillsDataGrid_CellEndEdit);
            this.skillsDataGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.skillsDataGrid_UserDeletingRow);
            // 
            // skill
            // 
            this.skill.HeaderText = "Skill";
            this.skill.Name = "skill";
            // 
            // points
            // 
            this.points.HeaderText = "QTD Pontos";
            this.points.Name = "points";
            // 
            // position
            // 
            this.position.HeaderText = "Posição X Y";
            this.position.Name = "position";
            // 
            // btnSaveSkill
            // 
            this.btnSaveSkill.Location = new System.Drawing.Point(246, 139);
            this.btnSaveSkill.Name = "btnSaveSkill";
            this.btnSaveSkill.Size = new System.Drawing.Size(217, 47);
            this.btnSaveSkill.TabIndex = 11;
            this.btnSaveSkill.Text = "Salvar skill";
            this.btnSaveSkill.UseVisualStyleBackColor = true;
            this.btnSaveSkill.Click += new System.EventHandler(this.btnSaveSkill_Click);
            // 
            // btnDistribute
            // 
            this.btnDistribute.Location = new System.Drawing.Point(350, 43);
            this.btnDistribute.Name = "btnDistribute";
            this.btnDistribute.Size = new System.Drawing.Size(147, 23);
            this.btnDistribute.TabIndex = 13;
            this.btnDistribute.Text = "Distribuir pontos";
            this.btnDistribute.UseVisualStyleBackColor = true;
            this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 579);
            this.Controls.Add(this.btnDistribute);
            this.Controls.Add(this.skillsDataGrid);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.muWindowNameTxt);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TreeSkillsBuilder";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pointsTxt)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCaptureMousePos;
        private System.Windows.Forms.TextBox muWindowNameTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox skillNameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown pointsTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox positionTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView skillsDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn skill;
        private System.Windows.Forms.DataGridViewTextBoxColumn points;
        private System.Windows.Forms.DataGridViewTextBoxColumn position;
        private System.Windows.Forms.Button btnSaveSkill;
        private System.Windows.Forms.Button btnDistribute;
    }
}