#region StartCODING

//SUPmaster AIO - created by Antr4xSoft
//Changing this won't make you the coder.
//If you are a programmer and wants to refer from this code, PM me through http://www.joduska.me Username : antraxSoft
//Enjoy!

#endregion


using System;
using System.Collections;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Reflection;
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
        static Spell Q, W, E, R;

        static Obj_AI_Hero catchedUnit = null;

        static int qTimer;
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
            comboMenu.AddItem(new MenuItem("ComboActive", "Combo", true).SetValue(new KeyBind(32, KeyBindType.Press)));

            //harass menu
            var harassMenu = Config.AddSubMenu(new Menu("Harass", "Harass"));
            harassMenu.AddItem(new MenuItem("harassE", "Always use E toward ally").SetValue(true));

            //misc menu
            var miscMenu = Config.AddSubMenu(new Menu("Misc", "Misc"));
            miscMenu.AddItem(new MenuItem("autoE", "Auto E on gapclosing targets").SetValue(true));
            miscMenu.AddItem(new MenuItem("autoEI", "Auto E to interrupt enemy flee").SetValue(true));
            Q = new Spell(SpellSlot.Q, 1100);
            W = new Spell(SpellSlot.W, 950);
            E = new Spell(SpellSlot.E, 400);
            R = new Spell(SpellSlot.R, 450);

        }




        #region Q


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


        static Tuple<bool, Obj_AI_Hero> IsPulling()
        {
            bool Catched;
            Obj_AI_Hero CatchedQtarget;

            if (catchedUnit != null)
            {   
                Catched = true;
                CatchedQtarget = catchedUnit;
            }
            else
            {
                Catched = false;
                CatchedQtarget = null;
            }

            return new Tuple<bool, Obj_AI_Hero>(Catched, CatchedQtarget);
        }

        #endregion




        #region E



        static void CastE(Obj_AI_Hero enemy)
        {
            if(!E.IsReady() || enemy == null || !enemy.IsValidTarget())
            {
                return;
            }
            bool caught = IsPulling().Item1;
            Obj_AI_Hero CatchedQtarget = IsPulling().Item2;

            if(!caught && qTimer == 0 )
            {
                if(Player.Distance(enemy.Position) <= E.Range)
                {
                    if(Player.HealthPercentage() < 20 && enemy.HealthPercentage() > 20)
                    {
                        Push(enemy);
                    } 
                    else
                    {
                        Pull(enemy);
                    }
                }
            }

        }

        static void Pull(Obj_AI_Base target)
        {
            var pos = target.Position.Extend(Player.Position, Player.Distance(target.Position) + 200);
            E.Cast(pos);
        }

        static void Push(Obj_AI_Base target)
        {
            var pos = target.Position.Extend(Player.Position, Player.Distance(target.Position) - 200);
            E.Cast(pos);
        }




        #endregion



        #region Combo

        private static void combo()
        {
            var target = TargetSelector.GetTarget(1100, TargetSelector.DamageType.Magical);
            var etarget = TargetSelector.GetTarget(400, TargetSelector.DamageType.Magical);

            if (Config.Item("ComboActive").GetValue<bool>())
            {
                if (Config.Item("comboQ").GetValue<bool>())
                {

                    CastQ(target);

                }
                if (Config.Item("comboE").GetValue<bool>() || etarget != null || E.IsReady())
                {
                    CastE(etarget);
                }
            }
        }


        
        #endregion



        #region Harass

        private static void harass()
        {
            var target = TargetSelector.GetTarget(1100, TargetSelector.DamageType.Magical);
            var QpredictionForHarass = Q.GetPrediction(target);
        }
    }
}


#endregion