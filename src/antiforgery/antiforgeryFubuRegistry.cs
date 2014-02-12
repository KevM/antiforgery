using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore;
using FubuMVC.AntiForgery;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;

namespace antiforgery
{

	public class AntiForgerySettings
	{
		private ChainPredicate _filter;


		public AntiForgerySettings()
		{
			AppliesTo(filter => filter.RespondsToHttpMethod("POST")
						.And
						.ChainMatches(x => x.InputType() != null));
		}

		public void AppliesTo(Action<ChainPredicate> configuration)
		{
			_filter = new ChainPredicate();
			configuration(_filter);
		}


		public string Path { get; set; }
		public string Domain { get; set; }

		public bool AppliesTo(BehaviorChain chain)
		{
			return _filter.As<IChainFilter>().Matches(chain);
		}
	}

	public class CSRFAntiForgeryPolicy : IConfigurationAction
	{
		public void Configure(BehaviorGraph graph)
		{
			var antiForgerySettings = graph.Settings.Get<AntiForgerySettings>();
			graph.Behaviors.Where(antiForgerySettings.AppliesTo)
				.Each(x => x.Prepend(new AntiForgeryNode(x.InputType().FullName)));
		}
	}

	public class antiforgeryFubuRegistry : FubuRegistry
	{
		public antiforgeryFubuRegistry()
		{
			// Register any custom FubuMVC policies, inclusions, or 
			// other FubuMVC configuration here
			// Or leave as is to use the default conventions unchanged

			Policies.Add<CSRFAntiForgeryPolicy>();
		}
	}
}