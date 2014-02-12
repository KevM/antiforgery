using FubuMVC.Core;
using FubuMVC.StructureMap;
using StructureMap;

namespace antiforgery
{
	public class antiforgeryApplication : IApplicationSource
	{
	    public FubuApplication BuildApplication()
	    {
            return FubuApplication.For<antiforgeryFubuRegistry>()
				.StructureMap<antiforgeryRegistry>();
	    }
	}
}