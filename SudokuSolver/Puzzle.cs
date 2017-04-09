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
		private TextBox[,] _boxes;

		public Puzzle()
		{
			InitializeComponent();

			_boxes = new TextBox[9, 9];
			Font font = null;
			mCbLevel.SelectedItem = "Easy";
			for (var x = 0; x < 9; x++)
				for (var y = 0; y < 9; y++)
				{
					{
						var txtBox = new TextBox();
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
						_boxes[y, x] = txtBox;
					}
				}

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			var pen = new Pen(Color.LightSteelBlue, 3);
			var formGfx = this.CreateGraphics();

			formGfx.DrawRectangle(pen, 6, 66, 363, 267);
			pen.Width = 1;

			var rects = new[]
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
			var s = new Sudoku();
			var d = GetData();
			s.Data = d;
			var result = s.Generate(SpotRequired());
			if (!result.Item2)
			{
				return;
			}

			d = s.Data;
			SetData(d);
		}

		private void BtnImport_Click(object sender, EventArgs e)
		{
			// Import Sudoku
			mLblInfo.Text = "";
			try
			{
				var openDlg = new OpenFileDialog()
				{
					Filter = "txt files (*.txt)|*.txt",
					FilterIndex = 1,
					RestoreDirectory = true
				};
				if (openDlg.ShowDialog() == DialogResult.OK)
				{
					var fileName = openDlg.FileName;

					var fileLines = File.ReadAllLines(fileName);
					var map = new int[fileLines.Length, fileLines[0].Split(' ').Length];
					for (var i = 0; i < fileLines.Length; ++i)
					{
						var line = fileLines[i];
						for (var j = 0; j < map.GetLength(1); ++j)
						{
							var split = line.Split(' ');
							map[i, j] = Convert.ToInt32(split[j]);
						}
					}
					LoadImportData(map);
				}
			}
			catch (Exception)
			{
				mLblInfo.Text = mLblInfo.Text +
					"--------------------" +
				   "\nInvalid sudoku File";
				// exception object can be used for logging or other purposes.

			}
		}

		private void BtnUnique_Click(object sender, EventArgs e)
		{
			var s = new Sudoku();
			var d = GetData();
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
			var s = new Sudoku();
			mLblInfo.Text = "";
			var d = GetData();
			s.Data = d;

			mLblInfo.Text = mLblInfo.Text + "------------------\r\n";
			if (!s.IsSudokuFeasible())
			{
				mLblInfo.Text = mLblInfo.Text + "Sudoku not feasible\r\n";
				return;
			}

			var now = DateTime.Now;
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
					mLblInfo.Text = mLblInfo.Text + string.Format("{0} seconds\r\n", (DateTime.Now - now).TotalSeconds);
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
			var d = new byte[9, 9];
			for (var y = 0; y < 9; y++)
				for (var x = 0; x < 9; x++)
					if (int.TryParse(_boxes[x, y].Text, out int _))
					{
						d[x, y] = (byte)int.Parse(_boxes[x, y].Text);
					}
			return d;
		}

		private void SetData(byte[,] d)
		{
			try
			{
				for (var y = 0; y < 9; y++)
					for (var x = 0; x < 9; x++)
						_boxes[y, x].Text = Convert.ToInt32(d[y, x]) == 0 ? "-" : d[y, x].ToString();
			}
			catch (Exception)
			{
				// do nothing
			}

		}

		private bool IsSudokuGridFull()
		{
			var ret = true;
			var d = GetData();
			for (var y = 0; y < 9; y++)
				for (var x = 0; x < 9; x++)
					if (d[y, x] == 0 || d[y, x].ToString() == "-")
					{
						ret = false;
					}

			return ret;
		}

		private void LoadImportData(int[,] d)
		{
			for (var y = 0; y < 9; y++)
				for (var x = 0; x < 9; x++)
					_boxes[y, x].Text = Convert.ToInt32(d[y, x]) == 0 ? "-" : d[y, x].ToString();
		}

		private void ClearCells()
		{
			mLblInfo.Text = "";
			for (var y = 0; y < 9; y++)
				for (var x = 0; x < 9; x++)
					_boxes[y, x].Text = "-";
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
