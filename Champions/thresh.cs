using System;
using System.Collections;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Drawing;
using Color = System.Drawing.Color;
using System.Collections.Generic;
using System.Threading;

namespace SUPmaster.champions
{
    internal class thresh
    {
        public static Menu Config;
        static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        static Orbwalking.Orbwalker Orbwalker { get { return Program.Orbwalker; } }
        static Spell Q, Q2, W, E, R;

        public static void load()
        {
            //menu code framework copied from ahrisharp by beaving.   
            (Config = new Menu("SUPmaster-AIO", "SUPmaster-AIO ", true)).AddToMainMenu();

            var targetSelectorMenu = new Menu("Target Selector", "TargetSelector");
            TargetSelector.AddToMenu(targetSelectorMenu);
            Config.AddSubMenu(targetSelectorMenu);


            var comboMenu = Config.AddSubMenu(new Menu("Combo", "Combo"));
            comboMenu.AddItem(new MenuItem("comboQ", "Use Q").SetValue(true));
            comboMenu.AddItem(new MenuItem("comboW", "Use W").SetValue(true));
            comboMenu.AddItem(new MenuItem("comboE", "Use E").SetValue(true));
            comboMenu.AddItem(new MenuItem("comboR", "Use R").SetValue(true));


            var harassMenu = Config.AddSubMenu(new Menu("Harass", "Harass"));
            harassMenu.AddItem(new MenuItem("harassE", "Always use E toward ally").SetValue(true));

            var miscMenu = Config.AddSubMenu(new Menu("Misc", "Misc"));
            miscMenu.AddItem(new MenuItem("autoE", "Auto E on gapclosing targets").SetValue(true));
            miscMenu.AddItem(new MenuItem("autoEI", "Auto E to interrupt enemy flee").SetValue(true));
            Q = new Spell(SpellSlot.Q, 1100);
            Q2 = new Spell(SpellSlot.Q, 1400);
            W = new Spell(SpellSlot.W, 950);
            E = new Spell(SpellSlot.E, 400);
            R = new Spell(SpellSlot.R, 450);

        }

        private static void combo()
        {
            var target = TargetSelector.GetTarget(1300, TargetSelector.DamageType.Magical);
            var Qprediction = Q.GetPrediction(target);

        }
    }
}