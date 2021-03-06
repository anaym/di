namespace FractalPainting.Infrastructure
{
	public interface IUiAction
	{
		string Category { get; }
		string Name { get; }
		string Description { get; }
        double Index { get; }
		void Perform();
	}
}