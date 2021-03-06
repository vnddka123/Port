#region License

/*
 Copyright 2014 - 2015 Nikita Bernthaler
 Game.cs is part of SFXUtility.

 SFXUtility is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 SFXUtility is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with SFXUtility. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion License

#region

using System;
using System.Linq;
using System.Threading.Tasks;
using EloBuddy;
using LeagueSharp.Common;
using SFXUtility.Classes;
using SFXUtility.Library;
//using SFXUtility.Library.Logger;

#endregion

namespace SFXUtility.Features.Events
{
    internal class Game : Child<Events>
    {
        private bool _onEndTriggerd;
        private bool _onStartDone;
        private bool _onStartTriggerd;

        public Game(Events parent) : base(parent)
        {
            OnLoad();
        }

        public override string Name
        {
            get { return "Game"; }
        }

        protected override void OnEnable()
        {
            EloBuddy.Game.OnUpdate += OnGameUpdate;
            EloBuddy.Game.OnNotify += OnGameNotify;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            EloBuddy.Game.OnNotify -= OnGameNotify;
            base.OnDisable();
        }

        protected sealed override void OnLoad()
        {
            try
            {
                Menu = new Menu(Name, Name);
                var startMenu = new Menu("OnStart", Name + "OnStart");
                startMenu.AddItem(new MenuItem(startMenu.Name + "Delay", "Delay").SetValue(new Slider(20, 0, 75)));
                startMenu.AddItem(
                    new MenuItem(startMenu.Name + "Greeting", "Greeting").SetValue(
                        new StringList(new[] { "gl & hf", "good luck & have fun" })));
                startMenu.AddItem(new MenuItem(startMenu.Name + "SayGreeting", "Say Greeting").SetValue(false));
                startMenu.AddItem(new MenuItem(startMenu.Name + "MuteAll", "Mute All").SetValue(false));
                
                var endMenu = new Menu("OnEnd", Name + "OnEnd");
                endMenu.AddItem(
                    new MenuItem(endMenu.Name + "Ending", "Goodbye").SetValue(
                        new StringList(new[] { "gg", "gg wp", "good game", "well played" })));
                endMenu.AddItem(new MenuItem(endMenu.Name + "SayEnding", "Say Goodbye").SetValue(false));
                endMenu.AddItem(new MenuItem(endMenu.Name + "Quit", "Quit Game").SetValue(false));
                endMenu.AddItem(new MenuItem(endMenu.Name + "QuitDelay", "Quit Delay").SetValue(new Slider(10, 3, 20)));

                Menu.AddSubMenu(startMenu);
                Menu.AddSubMenu(endMenu);

                Menu.AddItem(new MenuItem(Name + "Enabled", "Enabled").SetValue(false));

                Parent.Menu.AddSubMenu(Menu);

                EloBuddy.Game.OnLoad += delegate { _onStartTriggerd = true; };
            }
            catch (Exception ex)
            {
                //Global.Logger.AddItem(new LogItem(ex));
            }
        }

        private void OnGameUpdate(EventArgs args)
        {
             if (!_onStartDone)
            {
                if (EloBuddy.Game.Time > Menu.Item(Name + "OnStartDelay").GetValue<Slider>().Value)
                {
                    if (Menu.Item(Name + "OnStartSayGreeting").GetValue<bool>() && ObjectManager.Player.Level == 1)
                    {
                        EloBuddy.Chat.Say("/all " + Menu.Item(Name + "OnStartGreeting").GetValue<StringList>().SelectedValue);
                    }

                    if (Menu.Item(Name + "OnStartMuteAll").GetValue<bool>() && ObjectManager.Player.Level == 1)
                    {
                        EloBuddy.Chat.Say("/mute all");
                        EloBuddy.Chat.Print("Mute All - OK!");
                    }
                    _onStartDone = true;
                }
            }
            return;
            try
            {
                if (_onStartTriggerd && !_onStartDone)
                {
                    var map = LeagueSharp.Common.Utility.Map.GetMap().Type;
                    var maxLevel = map == LeagueSharp.Common.Utility.Map.MapType.CrystalScar || map == LeagueSharp.Common.Utility.Map.MapType.HowlingAbyss
                        ? 3
                        : 1;
                    if (Menu.Item(Name + "OnStartSayGreeting").GetValue<bool>() &&
                        !GameObjects.Heroes.Any(h => h.Level >= maxLevel))
                    {
                        _onStartDone = true;
                        LeagueSharp.Common.Utility.DelayAction.Add(
                            Menu.Item(Name + "OnStartDelay").GetValue<Slider>().Value * 1000,
                            delegate
                            {
                                EloBuddy.Chat.Say(
                                    "/all " + Menu.Item(Name + "OnStartGreeting").GetValue<StringList>().SelectedValue);
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                //Global.Logger.AddItem(new LogItem(ex));
            }
        }

        private void OnGameNotify(GameNotifyEventArgs args)
        {
            try
            {
                if (_onEndTriggerd ||
                    (args.EventId != GameEventId.OnEndGame && args.EventId != GameEventId.OnHQDie &&
                     args.EventId != GameEventId.OnHQKill))
                {
                    return;
                }

                _onEndTriggerd = true;

                if (Menu.Item(Name + "OnEndSayEnding").GetValue<bool>())
                {
                    EloBuddy.Chat.Say("/all " + Menu.Item(Name + "OnEndEnding").GetValue<StringList>().SelectedValue);
                }
                if (Menu.Item(Name + "OnEndQuit").GetValue<bool>())
                {
                    Task.Run(
                        async () =>
                        {
                            await Task.Delay(Menu.Item(Name + "OnEndQuitDelay").GetValue<Slider>().Value * 1000);
                            EloBuddy.Game.QuitGame();
                        });
                }
            }
            catch (Exception ex)
            {
                //Global.Logger.AddItem(new LogItem(ex));
            }
        }
    }
}
