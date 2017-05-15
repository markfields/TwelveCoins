using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwelveCoinsConsole
{
	internal struct Knowledge
	{
		internal int UnknownCoins { get; }

		internal int LightishCoins { get; }

		internal int HeavyishCoins { get; }

		internal int ReferenceCoins { get; }

		internal bool FoundLightCoin { get; }

		internal bool FoundHeavyCoin { get; }

		internal Knowledge(int unknownCoins, int lightishCoins, int heavyishCoins, int referenceCoins)
		{
			UnknownCoins = unknownCoins;
			LightishCoins = lightishCoins;
			HeavyishCoins = heavyishCoins;
			ReferenceCoins = referenceCoins;
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

			return UnknownCoins + LightishCoins + HeavyishCoins + ReferenceCoins + specialCoins == 12;
		}
	}
}
