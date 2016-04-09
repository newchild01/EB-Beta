using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTWTeemo
{
	class Program
	{
		private static bool _switch = false;
		private static bool _Toggle
		{
			get
			{
				_switch = !_switch;
				return _switch;
			}
		}
		private static Menu TeemoMenu;
		private static CheckBox _EvadeLegit = new CheckBox("Use custom Evade", false);
		private static CheckBox _Disco = new CheckBox("Activate Disco Drawings", false);
		private static Spell.Targeted _Q = new Spell.Targeted(SpellSlot.Q, 650);
		static void Main(string[] args)
		{
			Loading.OnLoadingComplete += Loading_OnLoadingComplete;
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			TeemoMenu = MainMenu.AddMenu("SBTW Teemo", "Extreeeme Teemo");
			TeemoMenu.Add("CustomOrbwalker", new CheckBox("Use custom Orbwalker"));
			TeemoMenu.Add("UseCustomEvade", _EvadeLegit);
			_EvadeLegit.OnValueChange += _EvadeLegit_OnValueChange;
			TeemoMenu.Add("UseDisco", _Disco);
			_Disco.OnValueChange += _Disco_OnValueChange;
			TeemoMenu.Add("UseSpells", new CheckBox("Use Spells"));
			Game.OnTick += Game_OnTick;
			Drawing.OnDraw += Drawing_OnDraw;
		}

		private static void Drawing_OnDraw(EventArgs args)
		{
			if (_Toggle)
			{
				Line.DrawLine(System.Drawing.Color.Red, Player.Instance.Path);
				foreach(var Champion in EntityManager.Heroes.AllHeroes)
				{
					Line.DrawLine(System.Drawing.Color.Green, 40f, new SharpDX.Vector3[] { Player.Instance.Position, Champion.Position });
				}
			}
				
		}

		private static void Game_OnTick(EventArgs args)
		{
			_Q.Cast(TargetSelector.GetTarget(_Q.Range, DamageType.Magical));
		}

		private static void _Disco_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
		{
			if (args.NewValue == false)
				_EvadeLegit.CurrentValue = true;
		}

		private static void _EvadeLegit_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
		{
			if (args.NewValue == true)
				_EvadeLegit.CurrentValue = false;
		}
	}
}
