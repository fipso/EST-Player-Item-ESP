using NLog.Targets;

namespace ClassLibrary2
{
	[Target(nameof(XXT))]
	public sealed class XXT : TargetWithLayout
	{
		public XXT()
		{
			Class1.Load();
		}
	}
}
