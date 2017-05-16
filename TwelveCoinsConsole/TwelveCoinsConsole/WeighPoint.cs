using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwelveCoinsConsole
{
	internal class WeighPoint
	{
		internal Knowledge Knowledge;
		internal Balance Balance;

		internal WeighPoint(Knowledge startingKnowledge, Balance balance)
		{
			Knowledge = startingKnowledge;
			Balance = balance;
		}

		internal Knowledge SupposeLeftWins()
		{
			//throw new NotImplementedException();
			return new Knowledge(new CoinCounts(12));
		}

		internal Knowledge SupposeRightWins()
		{
			//throw new NotImplementedException();
			return new Knowledge(new CoinCounts(12));
		}

		internal Knowledge SupposeEven()
		{
			//throw new NotImplementedException();
			return new Knowledge(new CoinCounts(12));
		}
	}
}
