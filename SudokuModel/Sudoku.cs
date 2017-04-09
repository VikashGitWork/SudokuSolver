using System;

namespace SudokuModel
{
	public class Sudoku
	{
		// 0 = solve for
		private byte[,] _mSudoku;
		private enum Ret { Unique, NotUnique, NoSolution };
		// Maps sub square index to m_sudoku
		private readonly EntryPoint[,] _mSubIndex =
			{
				{ new EntryPoint(0,0),new EntryPoint(0,1),new EntryPoint(0,2),new EntryPoint(1,0),new EntryPoint(1,1),new EntryPoint(1,2),new EntryPoint(2,0),new EntryPoint(2,1),new EntryPoint(2,2)},
				{ new EntryPoint(0,3),new EntryPoint(0,4),new EntryPoint(0,5),new EntryPoint(1,3),new EntryPoint(1,4),new EntryPoint(1,5),new EntryPoint(2,3),new EntryPoint(2,4),new EntryPoint(2,5)},
				{ new EntryPoint(0,6),new EntryPoint(0,7),new EntryPoint(0,8),new EntryPoint(1,6),new EntryPoint(1,7),new EntryPoint(1,8),new EntryPoint(2,6),new EntryPoint(2,7),new EntryPoint(2,8)},
				{ new EntryPoint(3,0),new EntryPoint(3,1),new EntryPoint(3,2),new EntryPoint(4,0),new EntryPoint(4,1),new EntryPoint(4,2),new EntryPoint(5,0),new EntryPoint(5,1),new EntryPoint(5,2)},
				{ new EntryPoint(3,3),new EntryPoint(3,4),new EntryPoint(3,5),new EntryPoint(4,3),new EntryPoint(4,4),new EntryPoint(4,5),new EntryPoint(5,3),new EntryPoint(5,4),new EntryPoint(5,5)},
				{ new EntryPoint(3,6),new EntryPoint(3,7),new EntryPoint(3,8),new EntryPoint(4,6),new EntryPoint(4,7),new EntryPoint(4,8),new EntryPoint(5,6),new EntryPoint(5,7),new EntryPoint(5,8)},
				{ new EntryPoint(6,0),new EntryPoint(6,1),new EntryPoint(6,2),new EntryPoint(7,0),new EntryPoint(7,1),new EntryPoint(7,2),new EntryPoint(8,0),new EntryPoint(8,1),new EntryPoint(8,2)},
				{ new EntryPoint(6,3),new EntryPoint(6,4),new EntryPoint(6,5),new EntryPoint(7,3),new EntryPoint(7,4),new EntryPoint(7,5),new EntryPoint(8,3),new EntryPoint(8,4),new EntryPoint(8,5)},
				{ new EntryPoint(6,6),new EntryPoint(6,7),new EntryPoint(6,8),new EntryPoint(7,6),new EntryPoint(7,7),new EntryPoint(7,8),new EntryPoint(8,6),new EntryPoint(8,7),new EntryPoint(8,8)}
		};

		// Maps sub square to index
		private readonly byte[,] _mSubSquare =
			{
				{0,0,0,1,1,1,2,2,2},
				{0,0,0,1,1,1,2,2,2},
				{0,0,0,1,1,1,2,2,2},
				{3,3,3,4,4,4,5,5,5},
				{3,3,3,4,4,4,5,5,5},
				{3,3,3,4,4,4,5,5,5},
				{6,6,6,7,7,7,8,8,8},
				{6,6,6,7,7,7,8,8,8},
				{6,6,6,7,7,7,8,8,8}
		};

		/// <summary>
		/// Sudoku byte[9,9] array
		/// </summary>
		public byte[,] Data
		{
			get
			{
				return _mSudoku.Clone() as byte[,];
			}

			set
			{
				if (value.Rank == 2 && value.GetUpperBound(0) == 8 && value.GetUpperBound(1) == 8)
					_mSudoku = value.Clone() as byte[,];
				else
					throw new Exception("Array has wrong size");
			}
		}

		public IRandomizer Randomizer { get; set; } = new DefaultRandomizer();

		/// <summary>
		/// Generate a new Sudoku
		/// </summary>
		/// <param name="spots">Number count for sudkou generation</param>
		/// <param name="numberOfTries">Number of tries before ending generation.</param>
		/// <returns>(Number of tries, success)</returns>
		public Tuple<long, bool> Generate(int spots, int numberOfTries = 1000000)
		{

			if (!IsSudokuFeasible())
			{
				// The supplied data is not feasible.
				// - or -
				// The supplied data has too many spots set.
				return Tuple.Create(0L, false);
			}

			/////////////////////////////////////
			// Randomize spots
			/////////////////////////////////////

			var originalData = Data;

			long tries = 0;
			for (; tries < numberOfTries; tries++)
			{
				// Try to generate spots
				{
					if (Gen(spots))

						// Test if unique solution.
						if (IsSudokuUnique())
						{
							return Tuple.Create(tries, true);
						}
				}

				// Start over.
				Data = originalData;
			}

			return Tuple.Create(tries, false);
		}

		private bool Gen(int spots)
		{
			int xRand, yRand;
			for (var i = 0; i < spots; i++)
			{
				// Selecting random non used spot
				do
				{
					xRand = Randomizer.GetInt(9);
					yRand = Randomizer.GetInt(9);
				} while (_mSudoku[yRand, xRand] != 0);

				/////////////////////////////////////
				// Get feasible values for spot.
				/////////////////////////////////////

				// Set P is possible solutions 
				byte[] P = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

				// Remove used numbers in the column from possiblities & set it to 0
				for (var a = 0; a < 9; a++)
					P[_mSudoku[a, xRand]] = 0;

				// Remove used numbers in the row from possiblities & set it to 0
				for (var b = 0; b < 9; b++)
					P[_mSudoku[yRand, b]] = 0;

				// Remove used numbers in the 3X3 block from possiblities & set it to 0
				EntryPoint entryPoint;
				int squareIndex = _mSubSquare[yRand, xRand];
				for (var c = 0; c < 9; c++)
				{
					entryPoint = _mSubIndex[squareIndex, c];
					P[_mSudoku[entryPoint.X, entryPoint.Y]] = 0;
				}

				var cP = 0;
				// Calculate cardinality of P (Possbilities)
				for (var d = 1; d < 10; d++)
					cP += P[d] == 0 ? 0 : 1;

				// Is there a number to use?
				if (cP > 0)
				{
					var e = 0;

					do
					{
						// Randomize number from the feasible set P except 0
						e = Randomizer.GetInt(1, 10);
					} while (P[e] == 0);

					// Set one possible number in Sudoku from rule satisfied possbilities (Row, Column & Block Constraint) 
					_mSudoku[yRand, xRand] = (byte)e;
				}
				else
				{
					// Error
					return false;
				}
			}

			// Successfully generated a feasible set.
			return true;
		}

		/// <summary>
		/// Fast test if the data is feasible. 
		/// Does not check if there is more than one solution.
		/// </summary>
		/// <returns>True if feasible</returns>
		public bool IsSudokuFeasible()
		{
			for (var y = 0; y < 9; y++)
			{
				for (var x = 0; x < 9; x++)
				{
					// Set of possible solutions
					var m = new byte[10];

					// Count used numbers in the vertical direction
					for (var a = 0; a < 9; a++)
						m[_mSudoku[a, x]]++;
					// Sudoku feasible?
					if (!Feasible(m))
						return false;

					m = new byte[10];
					// Count used numbers in the horizontal direction
					for (var b = 0; b < 9; b++)
						m[_mSudoku[y, b]]++;
					if (!Feasible(m))
						return false;

					m = new byte[10];
					// Count used numbers in the sub square.
					int squareIndex = _mSubSquare[y, x];
					for (var c = 0; c < 9; c++)
					{
						var p = _mSubIndex[squareIndex, c];
						if (p.X != y && p.Y != x)
							m[_mSudoku[p.X, p.Y]]++;
					}
					if (!Feasible(m))
						return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Tests to check supplied number appears exactly once.
		/// </summary>
		/// <returns>True if exactly once rule is satisfied</returns>
		private bool Feasible(byte[] m)
		{
			for (var d = 1; d < 10; d++)
				if (m[d] > 1)
					return false;

			return true;
		}

		/// <summary>
		/// Test generated Sudoku for solvability.
		/// A true Sudoku has one and only one solution.
		/// </summary>
		/// <returns>True if unique</returns>
		public bool IsSudokuUnique()
		{
			var m = Data;
			var b = TestUniqueness() == Ret.Unique;
			Data = m;
			return b;
		}


		/// <summary>
		// Is there one and only one solution
		//
		/// </summary>
		private Ret TestUniqueness()
		{
			// Find untouched location with most information
			var xp = 0;
			var yp = 0;
			byte[] mp = null;
			var cMp = 10;

			for (var y = 0; y < 9; y++)
			{
				for (var x = 0; x < 9; x++)
				{
					// Is this spot unused?
					if (_mSudoku[y, x] == 0)
					{
						// Set M of possible solutions
						byte[] m = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

						// Remove used numbers in the vertical direction
						for (var a = 0; a < 9; a++)
							m[_mSudoku[a, x]] = 0;

						// Remove used numbers in the horizontal direction
						for (var b = 0; b < 9; b++)
							m[_mSudoku[y, b]] = 0;

						// Remove used numbers in the sub square.
						int squareIndex = _mSubSquare[y, x];
						for (var c = 0; c < 9; c++)
						{
							var p = _mSubIndex[squareIndex, c];
							m[_mSudoku[p.X, p.Y]] = 0;
						}

						var cM = 0;
						// Calculate cardinality of M
						for (var d = 1; d < 10; d++)
							cM += m[d] == 0 ? 0 : 1;

						// Is there more information in this spot than in the best yet?
						if (cM < cMp)
						{
							cMp = cM;
							mp = m;
							xp = x;
							yp = y;
						}
					}
				}
			}

			// Finished?
			if (cMp == 10)
				return Ret.Unique;

			// Couldn't find a solution?
			if (cMp == 0)
				return Ret.NoSolution;

			// Try elements
			var success = 0;
			for (var i = 1; i < 10; i++)
			{
				if (mp[i] != 0)
				{
					_mSudoku[yp, xp] = mp[i];

					switch (TestUniqueness())
					{
						case Ret.Unique:
							success++;
							break;

						case Ret.NotUnique:
							return Ret.NotUnique;

						case Ret.NoSolution:
							break;
					}

					// More than one solution found?
					if (success > 1)
						return Ret.NotUnique;
				}
			}

			// Restore to original state.
			_mSudoku[yp, xp] = 0;

			switch (success)
			{
				case 0:
					return Ret.NoSolution;

				case 1:
					return Ret.Unique;

				default:
					// Won't happen.
					return Ret.NotUnique;
			}
		}

		/// <summary>
		/// Solves the given Sudoku.
		/// </summary>
		/// <returns>Success</returns>
		public bool Solve()
		{

			// Find untouched location with most information
			var xp = 0;
			var yp = 0;
			byte[] mp = null;
			var cMp = 10;

			if (TestUniqueness() == Ret.NotUnique)
				return false;

			for (var y = 0; y < 9; y++)
			{
				for (var x = 0; x < 9; x++)
				{
					// Is this spot unused?
					if (_mSudoku[y, x] == 0)
					{
						// Set P of possible solutions
						byte[] P = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

						// Remove used numbers in the vertical direction
						for (var a = 0; a < 9; a++)
							P[_mSudoku[a, x]] = 0;

						// Remove used numbers in the horizontal direction
						for (var b = 0; b < 9; b++)
							P[_mSudoku[y, b]] = 0;

						// Remove used numbers in the sub square.
						int squareIndex = _mSubSquare[y, x];
						for (var c = 0; c < 9; c++)
						{
							var p = _mSubIndex[squareIndex, c];
							P[_mSudoku[p.X, p.Y]] = 0;
						}

						var cP = 0;
						// Calculate cardinality of M
						for (var d = 1; d < 10; d++)
							cP += P[d] == 0 ? 0 : 1;

						// Is there more information in this spot than in the best yet?
						if (cP < cMp)
						{
							cMp = cP;
							mp = P;
							xp = x;
							yp = y;
						}
					}
				}
			}

			// Finished?
			if (cMp == 10)
				return true;

			// Couldn't find a solution?
			if (cMp == 0)
				return false;

			// Try elements
			for (var i = 1; i < 10; i++)
			{
				if (mp[i] != 0)
				{
					_mSudoku[yp, xp] = mp[i];
					if (Solve())
						return true;
				}
			}

			// Restore to original state.
			_mSudoku[yp, xp] = 0;
			return false;
		}


	}
}
