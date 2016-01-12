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

        //player as Player
        static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

        //orbwalker
        static Orbwalking.Orbwalker Orbwalker { get { return Program.Orbwalker; } }

        //spells
        static Spell Q, Q2, W, E, R;

        public static void load()
        {

            (Config = new Menu("SUPmaster-AIO", "SUPmaster-AIO ", true)).AddToMainMenu();

            var targetSelectorMenu = new Menu("Target Selector", "TargetSelector");
            TargetSelector.AddToMenu(targetSelectorMenu);
            Config.AddSubMenu(targetSelectorMenu);

            //combo menu
            var comboMenu = Config.AddSubMenu(new Menu("Combo", "Combo"));
            comboMenu.AddItem(new MenuItem("comboQ", "Use Q").SetValue(true));
            comboMenu.AddItem(new MenuItem("comboW", "Use W").SetValue(true));
            comboMenu.AddItem(new MenuItem("comboE", "Use E").SetValue(true));
            comboMenu.AddItem(new MenuItem("comboR", "Use R").SetValue(true));

            //harass menu
            var harassMenu = Config.AddSubMenu(new Menu("Harass", "Harass"));
            harassMenu.AddItem(new MenuItem("harassE", "Always use E toward ally").SetValue(true));

            //misc menu
            var miscMenu = Config.AddSubMenu(new Menu("Misc", "Misc"));
            miscMenu.AddItem(new MenuItem("autoE", "Auto E on gapclosing targets").SetValue(true));
            miscMenu.AddItem(new MenuItem("autoEI", "Auto E to interrupt enemy flee").SetValue(true));
            Q = new Spell(SpellSlot.Q, 1100);
            Q2 = new Spell(SpellSlot.Q, 1400);
            W = new Spell(SpellSlot.W, 950);
            E = new Spell(SpellSlot.E, 400);
            R = new Spell(SpellSlot.R, 450);

        }


        static void CastQ(Obj_AI_Hero enemy)
        {

            //won't cast if not ready or no enemy in range
            if (!Q.IsReady() || enemy == null || !enemy.IsValidTarget())
                return;

            //cast if hitchance is >= High.
            if (Q.IsReady())
            {
                var b = Q.GetPrediction(enemy);

                if (b.Hitchance >= HitChance.High &&
                    Player.Distance(enemy.ServerPosition) < Q.Range)
                {
                    Q.Cast(enemy);
                }
            }


        }

        private static void combo()
        {
            if (Config.Item("comboQ").GetValue<bool>())
            {



            }
        }
        private static void harass()
        {
            var target = TargetSelector.GetTarget(1100, TargetSelector.DamageType.Magical);
            var QpredictionForHarass = Q.GetPrediction(target);
        }
    }
}