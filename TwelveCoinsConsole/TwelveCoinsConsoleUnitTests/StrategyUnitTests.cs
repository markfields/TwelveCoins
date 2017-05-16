using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TwelveCoinsConsole;

namespace TwelveCoinsConsoleUnitTests
{
	[TestClass]
	public class StrategyUnitTests
	{
		[TestMethod]
		public void CloneStrategy_JustRoot()
		{
			StrategyNode foo = new StrategyNode(new WeighPoint(null, null));

			StrategyNode clone = new StrategyNode(null, foo);

			
		}
	}
}
