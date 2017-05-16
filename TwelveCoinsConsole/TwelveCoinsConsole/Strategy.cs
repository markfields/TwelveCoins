using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwelveCoinsConsole
{
	internal class StrategyNode
	{
		internal WeighPoint WeighPoint { get; private set; }

		internal StrategyNode Parent { get; private set; }

		internal int Level => (Parent?.Level ?? 0 ) + 1;

		internal List<StrategyNode> IfLeftWins;
		internal List<StrategyNode> IfRightWins;
		internal List<StrategyNode> IfEven;

		internal StrategyNode(WeighPoint weighPoint)
		{
			WeighPoint = weighPoint;
			Parent = null;
		}

		internal StrategyNode(WeighPoint weighPoint, StrategyNode parent)
		{
			WeighPoint = weighPoint;
			Parent = parent;
		}
	}

	internal class StrategyManager
	{
		internal List<StrategyNode> GetAllStrategies(Knowledge knowledge, StrategyNode parentNode = null)
		{
			List<StrategyNode> roots = ComputeWeighPoints(knowledge).Select(
					balance => new StrategyNode(new WeighPoint(knowledge, balance), parentNode)
				).ToList();

			foreach (var node in roots)
			{
				Console.WriteLine($"Execute | {node.Level}");
				ExecuteWeighPointOnNode(node);
			}

			return roots;
		}

		internal void ExecuteWeighPointOnNode(StrategyNode node)
		{
			if (node.Level > 3)
				return;

			var weighPoint = node.WeighPoint;

			node.IfLeftWins = GetAllStrategies(weighPoint.SupposeLeftWins(), node /*parentNode*/);
			node.IfRightWins = GetAllStrategies(weighPoint.SupposeRightWins(), node /*parentNode*/);
			node.IfEven = GetAllStrategies(weighPoint.SupposeEven(), node /*parentNode*/);
		}

		private static List<Balance> ComputeWeighPoints(Knowledge knowledge)
		{
			//throw new NotImplementedException();
			return new List<Balance> { new Balance(new CoinCounts(unknownCoins: 1), new CoinCounts(unknownCoins: 1))};
		}

		internal bool AnyWinningStrategiesUnderNode(StrategyNode node)
		{
			if (node.Level == 3)
			{
				return node.WeighPoint.Knowledge.FoundHeavyCoin || node.WeighPoint.Knowledge.FoundLightCoin || node.WeighPoint.Knowledge.AllAreEven;
			}

			return
				node.IfLeftWins.Any(AnyWinningStrategiesUnderNode) ||
				node.IfRightWins.Any(AnyWinningStrategiesUnderNode) ||
				node.IfEven.Any(AnyWinningStrategiesUnderNode);
		}
	}
}
