using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace SUPmaster
{
     internal class initializer
    {
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        private static Orbwalking.Orbwalker orbwalker;


        public static void initialize()
        {

            Game.PrintChat("SUPmaster AIO by Antr4xSoft Loaded");

            if (Player.ChampionName == "Azir") //thresh ! not azir
            {
                champions.thresh.load();
            }
            else
            {
                champions.notsupported.load(); 
            }

        }
    }
}