namespace SudokuSolver
{
	partial class Puzzle
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Puzzle));
			this.btnSolve = new MetroFramework.Controls.MetroButton();
			this.btnLoad = new MetroFramework.Controls.MetroButton();
			this.btnUnique = new MetroFramework.Controls.MetroButton();
			this.btnClear = new MetroFramework.Controls.MetroButton();
			this.mLblInfo = new MetroFramework.Controls.MetroLabel();
			this.mCbLevel = new MetroFramework.Controls.MetroComboBox();
			this.mBtnImport = new MetroFramework.Controls.MetroButton();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.SuspendLayout();
			// 
			// btnSolve
			// 
			this.btnSolve.FontWeight = MetroFramework.MetroButtonWeight.Regular;
			this.btnSolve.Location = new System.Drawing.Point(337, 392);
			this.btnSolve.Name = "btnSolve";
			this.btnSolve.Size = new System.Drawing.Size(153, 41);
			this.btnSolve.TabIndex = 3;
			this.btnSolve.Text = "Solve";
			this.btnSolve.UseSelectable = true;
			this.btnSolve.Click += new System.EventHandler(this.BtnSolve_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.FontWeight = MetroFramework.MetroButtonWeight.Regular;
			this.btnLoad.Location = new System.Drawing.Point(433, 132);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(153, 46);
			this.btnLoad.TabIndex = 1;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseSelectable = true;
			this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
			// 
			// btnUnique
			// 
			this.btnUnique.FontWeight = MetroFramework.MetroButtonWeight.Regular;
			this.btnUnique.Location = new System.Drawing.Point(178, 392);
			this.btnUnique.Name = "btnUnique";
			this.btnUnique.Size = new System.Drawing.Size(153, 41);
			this.btnUnique.TabIndex = 2;
			this.btnUnique.Text = "Validate";
			this.btnUnique.UseSelectable = true;
			this.btnUnique.Click += new System.EventHandler(this.BtnUnique_Click);
			// 
			// btnClear
			// 
			this.btnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
			this.btnClear.Location = new System.Drawing.Point(496, 392);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(153, 41);
			this.btnClear.TabIndex = 4;
			this.btnClear.Text = "Clear";
			this.btnClear.UseSelectable = true;
			this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
			// 
			// mLblInfo
			// 
			this.mLblInfo.AutoSize = true;
			this.mLblInfo.FontSize = MetroFramework.MetroLabelSize.Small;
			this.mLblInfo.Location = new System.Drawing.Point(33, 392);
			this.mLblInfo.Name = "mLblInfo";
			this.mLblInfo.Size = new System.Drawing.Size(0, 0);
			this.mLblInfo.TabIndex = 5;
			this.mLblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mCbLevel
			// 
			this.mCbLevel.FormattingEnabled = true;
			this.mCbLevel.ItemHeight = 23;
			this.mCbLevel.Items.AddRange(new object[] {
            "Easy",
            "Medium",
            "Hard",
            "Samurai"});
			this.mCbLevel.Location = new System.Drawing.Point(433, 63);
			this.mCbLevel.Name = "mCbLevel";
			this.mCbLevel.Size = new System.Drawing.Size(153, 29);
			this.mCbLevel.TabIndex = 6;
			this.mCbLevel.UseSelectable = true;
			// 
			// mBtnImport
			// 
			this.mBtnImport.BackColor = System.Drawing.Color.White;
			this.mBtnImport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
			this.mBtnImport.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.mBtnImport.Location = new System.Drawing.Point(433, 240);
			this.mBtnImport.Name = "mBtnImport";
			this.mBtnImport.Size = new System.Drawing.Size(153, 47);
			this.mBtnImport.TabIndex = 5;
			this.mBtnImport.Text = "Import";
			this.mBtnImport.UseSelectable = true;
			this.mBtnImport.Click += new System.EventHandler(this.BtnImport_Click);
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.Location = new System.Drawing.Point(46, 373);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(80, 19);
			this.metroLabel1.TabIndex = 8;
			this.metroLabel1.Text = "Your Game!";
			// 
			// metroLabel2
			// 
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.Location = new System.Drawing.Point(496, 199);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(28, 19);
			this.metroLabel2.TabIndex = 9;
			this.metroLabel2.Text = "OR";
			// 
			// Puzzle
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
			this.ClientSize = new System.Drawing.Size(659, 472);
			this.Controls.Add(this.metroLabel2);
			this.Controls.Add(this.metroLabel1);
			this.Controls.Add(this.mBtnImport);
			this.Controls.Add(this.mCbLevel);
			this.Controls.Add(this.mLblInfo);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnUnique);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnSolve);
			this.DoubleBuffered = false;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Puzzle";
			this.Resizable = false;
			this.Text = "Sudoku Solver";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private MetroFramework.Controls.MetroComboBox mCbLevel;
		private MetroFramework.Controls.MetroButton mBtnImport;
		private MetroFramework.Controls.MetroLabel metroLabel1;
		private MetroFramework.Controls.MetroLabel metroLabel2;
		private MetroFramework.Controls.MetroButton btnSolve;
		private MetroFramework.Controls.MetroButton btnLoad;
		private MetroFramework.Controls.MetroButton btnUnique;
		private MetroFramework.Controls.MetroButton btnClear;
		private MetroFramework.Controls.MetroLabel mLblInfo;
	}
}

