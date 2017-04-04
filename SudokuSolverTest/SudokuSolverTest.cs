using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuModel;

namespace SudokuSolverTest
{
	[TestClass]
	public class SudokuTest
	{
		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		Sudoku sudoku = new Sudoku();

		[TestMethod()]
		public void IsSudokuUniqueTest()
		{
			sudoku.Data = new Byte[,]{
				{1,7,0,0,0,0,0,0,0},
				{0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,0,6,8,0},
				{8,0,0,0,0,5,4,0,0},
				{0,0,0,0,0,0,0,6,0},
				{3,5,0,6,0,7,1,0,0},
				{0,0,2,1,0,0,3,0,4},
				{5,0,3,0,0,0,0,0,8},
				{0,0,0,0,9,4,0,0,0}
			};

			var actual = sudoku.IsSudokuUnique();
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		public void IsSudokuNotUniqueTest()
		{
			sudoku.Data = new Byte[,]{
				{0,0,0,0,0,0,0,9,0},
				{0,0,0,0,9,0,4,6,7},
				{9,0,4,1,0,0,0,2,5},
				{0,0,9,0,0,0,0,0,8},
				{8,7,0,0,6,0,0,1,4},
				{6,0,0,6,0,0,3,0,0},
				{7,4,0,0,0,2,9,0,1},
				{2,9,8,0,3,0,0,0,0},
				{0,1,0,0,0,0,0,0,0}
			};

			var actual = sudoku.IsSudokuUnique();
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		public void SolveSuccessTest()
		{
			sudoku.Data = new Byte[,]{
				{1,7,0,0,0,0,0,0,0},
				{0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,0,6,8,0},
				{8,0,0,0,0,5,4,0,0},
				{0,0,0,0,0,0,0,6,0},
				{3,5,0,6,0,7,1,0,0},
				{0,0,2,1,0,0,3,0,4},
				{5,0,3,0,0,0,0,0,8},
				{0,0,0,0,9,4,0,0,0}
			};

			var actual = sudoku.Solve();
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		public void SolveNoSuccessTest()
		{
			sudoku.Data = new Byte[,]{
				{1,7,0,0,0,0,0,0,0},
				{0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,0,6,8,0},
				{8,0,0,0,0,5,4,0,0},
				{0,0,0,0,0,0,0,6,0},
				{3,5,0,6,0,7,1,0,0},
				{0,0,2,1,0,0,3,0,4},
				{5,0,3,0,0,0,0,0,8},
				{0,0,0,0,9,4,0,0,7}
			};

			var actual = sudoku.Solve();
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		public void IsSudokuFeasibleTest()
		{
			sudoku.Data = new Byte[,]{
				{1,7,0,0,0,0,0,0,0},
				{0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,0,6,8,0},
				{8,0,0,0,0,5,4,0,0},
				{0,0,0,0,0,0,0,6,0},
				{3,5,0,6,0,7,1,0,0},
				{0,0,2,1,0,0,3,0,4},
				{5,0,3,0,0,0,0,0,8},
				{0,0,0,0,9,4,0,0,7}
			};

			var actual = sudoku.IsSudokuFeasible();
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		public void IsSudokuNotFeasibleTest()
		{
			sudoku.Data = new Byte[,]{
				{1,7,0,0,0,0,0,0,0},
				{0,0,0,2,0,0,0,0,0},
				{0,0,0,0,0,0,6,8,0},
				{8,0,0,0,0,5,4,0,0},
				{0,0,0,0,0,0,0,6,0},
				{3,5,0,6,0,7,1,0,0},
				{0,0,2,1,0,0,3,0,4},
				{5,0,3,0,0,0,0,0,8},
				{0,0,0,0,9,4,4,0,7}
			};

			var actual = sudoku.IsSudokuFeasible();
			Assert.AreEqual(false, actual);
		}

		[TestMethod()]
		public void GenerateTest()
		{
			sudoku.Data = new Byte[,]{
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0}
			};

			bool actual = sudoku.Generate(30).Item2;
			bool feasible = sudoku.IsSudokuFeasible();
			bool unique = sudoku.IsSudokuUnique();
			bool solve = sudoku.Solve();

			Assert.AreEqual(true, actual && feasible && unique && solve);
		}
	}
}
