namespace TagCloudApp.App.Actions
{
	public interface IUiAction
	{
		string Category { get; }
		string Name { get; }
		string Description { get; }
        double Index { get; }
		void Perform(IApplication application);
	}
}