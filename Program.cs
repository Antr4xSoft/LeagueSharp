using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;


namespace SUPmaster
{
    internal class Program
    {

        internal static Orbwalking.Orbwalker Orbwalker;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
        private static void Game_OnGameLoad(EventArgs args)
        {

            initializer.initialize();

        }


    }
}