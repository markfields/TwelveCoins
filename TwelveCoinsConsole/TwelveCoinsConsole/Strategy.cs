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

	internal class WeighNode
	{
		internal WeighPoint WeighPoint { get; private set; }

		internal KnowledgeNode Parent { get; private set; }

		internal int Level => Parent.Level + 1;

		internal KnowledgeNode IfLeftWins => new KnowledgeNode(WeighPoint.SupposeLeftWins(), this /*parent*/);
		internal KnowledgeNode IfRightWins => new KnowledgeNode(WeighPoint.SupposeRightWins(), this /*parent*/);
		internal KnowledgeNode IfEven => new KnowledgeNode(WeighPoint.SupposeEven(), this /*parent*/);

		internal WeighNode(WeighPoint weighPoint, KnowledgeNode parent)
		{
			if (parent == null)
				throw new ArgumentNullException(nameof(parent));

			WeighPoint = weighPoint;
			Parent = parent;
		}
	}
	internal class KnowledgeNode
	{
		internal Knowledge Knowledge { get; private set; }

		internal WeighNode Parent { get; private set; }

		internal int Level => Parent?.Level ?? 0;

		// TODO: Add indication of whether this came from Left, Right, or Even

		internal List<WeighNode> WeighNodes => Knowledge.ComputeBalancesToTry().Select(balance => new WeighNode(new WeighPoint(Knowledge, balance), this /*parent*/)).ToList();

		internal KnowledgeNode(Knowledge knowledge, WeighNode parent = null)
		{
			Knowledge = knowledge;
			Parent = parent;
		}
	}

	internal class StrategyNode2
	{
		internal WeighPoint WeighPoint { get; private set; }

		internal StrategyNode Parent { get; private set; }

		internal int Level => (Parent?.Level ?? 0) + 1;

		internal List<StrategyNode> IfLeftWins;
		internal List<StrategyNode> IfRightWins;
		internal List<StrategyNode> IfEven;

		internal StrategyNode2(WeighPoint weighPoint)
		{
			WeighPoint = weighPoint;
			Parent = null;
		}

		internal StrategyNode2(WeighPoint weighPoint, StrategyNode parent)
		{
			WeighPoint = weighPoint;
			Parent = parent;
		}
	}

	internal class StrategyTree
	{
		private KnowledgeNode _root;

		internal StrategyTree(KnowledgeNode root)
		{
			_root = root;
		}

		internal void Go()
		{
			var allLeafNodes = EvaluateAllPossibleStrategies();
			var winningLeafNodes = allLeafNodes.Where(node => node.Knowledge.FoundHeavyCoin || node.Knowledge.FoundLightCoin || node.Knowledge.AllAreEven).ToList();

			WalkUpToFindWinningStrategies(winningLeafNodes);
		}

		internal List<KnowledgeNode> EvaluateStrategiesUnderNode(KnowledgeNode node)
		{
			if (node.Level > 3)
				return new List<KnowledgeNode> { node };

			List<KnowledgeNode> childKnowledgeNodes = new List<KnowledgeNode>();
			foreach (var weighNode in node.WeighNodes)
			{
				childKnowledgeNodes.AddRange(new KnowledgeNode[] { weighNode.IfLeftWins, weighNode.IfRightWins, weighNode.IfEven });
			}

			return childKnowledgeNodes.SelectMany(EvaluateStrategiesUnderNode).ToList();
		}

		/// <summary>
		/// Returns a list of all leaf Knowledges possible after 3 weighings
		/// </summary>
		/// <returns></returns>
		internal List<KnowledgeNode> EvaluateAllPossibleStrategies()
		{
			return EvaluateStrategiesUnderNode(_root);
		}

		internal void WalkUpToFindWinningStrategies(List<KnowledgeNode> winningLeafNodes)
		{

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
