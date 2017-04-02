
namespace SudokuModel
{
	public interface IRandomizer
	{
		int GetInt(int max);
		int GetInt(int min, int max);
	}
}
