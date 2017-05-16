using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwelveCoinsConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var startingKnowledge = new Knowledge(new CoinCounts(unknownCoins: 12, lightishCoins: 0, heavyishCoins: 0, referenceCoins: 0));

			var strategyTree = new StrategyTree(new KnowledgeNode(startingKnowledge));
			strategyTree.Go();
			



			var strategyManager = new StrategyManager();

			var strategyRoots = strategyManager.GetAllStrategies(startingKnowledge);

			Console.WriteLine(strategyRoots.Any(strategyManager.AnyWinningStrategiesUnderNode));
		}
	}
}
