using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;
using EloBuddy.SDK.Events;

namespace Thresh {
	class Program {
		static void Main(string[] args) {
		//	CustomEvents.Game.OnGameLoad += Thresh.OnLoad;
            Loading.OnLoadingComplete += Thresh.OnLoad;
        }

		
	}
}
