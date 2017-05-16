using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwelveCoinsConsole
{
	internal class Knowledge
	{
		internal CoinCounts Coins { get; }

		internal bool FoundLightCoin { get; }

		internal bool FoundHeavyCoin { get; }

		internal bool AllAreEven => Coins.ReferenceCoins == 12;

		internal Knowledge(CoinCounts coins)
		{
			Coins = coins;
			FoundLightCoin = false;
			FoundHeavyCoin = false;

			if (!IsValid())
			{
				throw new InvalidOperationException();
			}
		}

		internal bool IsValid()
		{
			if (FoundHeavyCoin && FoundLightCoin)
				return false;

			int specialCoins = (FoundLightCoin || FoundHeavyCoin) ? 1 : 0;

			return Coins.TotalCount + specialCoins == 12;
		}
	}

	internal struct CoinCounts
	{
		internal int UnknownCoins { get; }

		internal int LightishCoins { get; }

		internal int HeavyishCoins { get; }

		internal int ReferenceCoins { get; }

		internal int TotalCount => (UnknownCoins + LightishCoins + HeavyishCoins + ReferenceCoins);

		internal CoinCounts(int unknownCoins = 0, int lightishCoins = 0, int heavyishCoins = 0, int referenceCoins = 0)
		{
			UnknownCoins = unknownCoins;
			LightishCoins = lightishCoins;
			HeavyishCoins = heavyishCoins;
			ReferenceCoins = referenceCoins;
		}
	}

	internal class Balance
	{
		internal CoinCounts Left { get; }
		internal CoinCounts Right { get; }

		internal Balance(CoinCounts left, CoinCounts right)
		{
			Left = left;
			Right = right;
		}
	}
}
