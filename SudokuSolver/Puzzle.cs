using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SudokuModel;

namespace SudokuSolver
{
	public partial class Puzzle : MetroFramework.Forms.MetroForm
	{
		private TextBox[,] boxes;

		public Puzzle()
		{
			InitializeComponent();

			boxes = new TextBox[9, 9];
			Font font = null;
			mCbLevel.SelectedItem = "Easy";
			TextBox txtBox;
			for (int x = 0; x < 9; x++)
				for (int y = 0; y < 9; y++)
				{
					{
						txtBox = new TextBox();
						if (font == null)
							font = new Font(txtBox.Font, FontStyle.Bold);
						txtBox.Location = new Point(x * 40 + 10, y * 30 + 70);
						txtBox.Size = new Size(36, 21);
						txtBox.Text = "-";
						txtBox.MaxLength = 1;
						txtBox.TextAlign = HorizontalAlignment.Center;
						txtBox.ShortcutsEnabled = false;
						if (x == 0 || x == 2 || x == 4 || x == 6 || x == 8)
							txtBox.BackColor = Color.WhiteSmoke;
						this.Controls.Add(txtBox);
						boxes[y, x] = txtBox;
					}
				}

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Pen pen = new Pen(Color.LightSteelBlue, 3);
			Graphics formGfx = this.CreateGraphics();

			formGfx.DrawRectangle(pen, 6, 66, 363, 267);
			pen.Width = 1;

			Rectangle[] rects = new Rectangle[]
			{
				new Rectangle(7,68,119,86), new Rectangle(127,68,119,86), new Rectangle(246,68,121,86),
				new Rectangle(8,140,119,105), new Rectangle(127,140,119,105), new Rectangle(246,140,121,105),
				new Rectangle(8,223,119,108), new Rectangle(127,223,119,108), new Rectangle(246,223,121,108)
			};
			formGfx.DrawRectangles(pen, rects);

			pen.Dispose();
			formGfx.Dispose();
		}

		private void BtnLoad_Click(object sender, EventArgs e)
		{
			ClearCells();
			Sudoku s = new Sudoku();
			byte[,] d = GetData();
			s.Data = d;
			var result = s.Generate(SpotRequired());
			if (result.Item2)
			{
				d = s.Data;
				SetData(d);
			}
		}

		private void BtnImport_Click(object sender, EventArgs e)
		{
			// Import Sudoku
			mLblInfo.Text = "";
			try
			{
				OpenFileDialog openDlg = new OpenFileDialog()
				{
					Filter = "txt files (*.txt)|*.txt",
					FilterIndex = 1,
					RestoreDirectory = true
				};
				if (openDlg.ShowDialog() == DialogResult.OK)
				{
					string fileName = openDlg.FileName;

					string[] fileLines = File.ReadAllLines(fileName);
					int[,] map = new int[fileLines.Length, fileLines[0].Split(' ').Length];
					for (int i = 0; i < fileLines.Length; ++i)
					{
						string line = fileLines[i];
						for (int j = 0; j < map.GetLength(1); ++j)
						{
							string[] split = line.Split(' ');
							map[i, j] = Convert.ToInt32(split[j]);
						}
					}
					LoadImportData(map);
				}
			}
			catch (Exception ex)
			{
				mLblInfo.Text = mLblInfo.Text +
					"--------------------" +
				   "\nInvalid sudoku File";
				// exception object can be used for logging or other purposes.

			}
		}

		private void BtnUnique_Click(object sender, EventArgs e)
		{
			Sudoku s = new Sudoku();
			byte[,] d = GetData();
			s.Data = d;
			mLblInfo.Text = "";

			mLblInfo.Text = mLblInfo.Text + "------------------\r\n";
			if (s.IsSudokuUnique())
			{
				mLblInfo.Text = mLblInfo.Text + "Sudoku unique\r\n";
			}
			else
			{
				mLblInfo.Text = mLblInfo.Text + "Sudoku not unique\r\n";
			}
		}

		private void BtnSolve_Click(object sender, EventArgs e)
		{
			Sudoku s = new Sudoku();
			mLblInfo.Text = "";
			byte[,] d = GetData();
			s.Data = d;

			mLblInfo.Text = mLblInfo.Text + "------------------\r\n";
			if (!s.IsSudokuFeasible())
			{
				mLblInfo.Text = mLblInfo.Text + "Sudoku not feasible\r\n";
				return;
			}

			DateTime now = DateTime.Now;
			mLblInfo.Text = mLblInfo.Text + "Solve started: " + now.ToLongTimeString() + "\r\n";
			// should not solve if sudoku is already solved
			if (IsSudokuGridFull() & s.IsSudokuUnique())
			{
				mLblInfo.Text = "--------------------\r\nSolve Not Required\r\n";
			}
			else
			{
				if (s.Solve())
				{
					// Solve successful
					mLblInfo.Text = mLblInfo.Text + "  Solve successful\r\n";

					d = s.Data;
					SetData(d);
					mLblInfo.Text = mLblInfo.Text + String.Format("{0} seconds\r\n", (DateTime.Now - now).TotalSeconds);
				}
				else
				{
					// Solve failed
					mLblInfo.Text = mLblInfo.Text + "  Solve failed\n Invalid Sudoku";
				}
			}


		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			ClearCells();
		}

		private byte[,] GetData()
		{
			byte[,] d = new byte[9, 9];
			for (int y = 0; y < 9; y++)
				for (int x = 0; x < 9; x++)
					if (Int32.TryParse(boxes[x, y].Text, out int ignoreMe))
					{
						d[x, y] = (byte)Int32.Parse(boxes[x, y].Text);
					}
			return d;
		}

		private void SetData(byte[,] d)
		{
			try
			{
				for (int y = 0; y < 9; y++)
					for (int x = 0; x < 9; x++)
						if (Convert.ToInt32(d[y, x]) == 0)
							boxes[y, x].Text = "-";
						else
							boxes[y, x].Text = d[y, x].ToString();
			}
			catch (Exception e)
			{
				// do nothing
			}

		}

		private bool IsSudokuGridFull()
		{
			bool ret = true;
			byte[,] d = GetData();
			for (int y = 0; y < 9; y++)
				for (int x = 0; x < 9; x++)
					if (d[y, x] == 0 || d[y, x].ToString() == "-")
						ret = false;
			return ret;
		}

		private void LoadImportData(int[,] d)
		{
			for (int y = 0; y < 9; y++)
				for (int x = 0; x < 9; x++)
					if (Convert.ToInt32(d[y, x]) == 0)
						boxes[y, x].Text = "-";
					else
						boxes[y, x].Text = d[y, x].ToString();
		}

		private void ClearCells()
		{
			mLblInfo.Text = "";
			for (int y = 0; y < 9; y++)
				for (int x = 0; x < 9; x++)
					boxes[y, x].Text = "-";
		}

		private int SpotRequired()
		{
			switch (mCbLevel.SelectedItem)
			{
				case "Easy":
					return 35;
				case "Medium":
					return 33;
				case "Hard":
					return 31;
				case "Samurai":
					return 29;
				default:
					return 30;
			}
		}

	}
}
