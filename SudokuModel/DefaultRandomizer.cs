using System;
namespace SudokuModel
{
	class DefaultRandomizer : IRandomizer
	{
		Random _rnd = new Random();

		public int GetInt(int max)
		{
			return _rnd.Next(max);
		}

		public int GetInt(int min, int max)
		{
			return _rnd.Next(min, max);
		}
	}
}
