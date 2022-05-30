using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.Menus;
using StardewValley;
using System;
using System.Linq;


namespace DreamMod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        private readonly int BuffUniqueID = 58012397;
        private readonly string[] buffSource;
        private readonly string[] wakeUpMessages;
        private readonly string[] earlyWakeUpMessages;
        private readonly string[] crobusDreamSpeech;
        private readonly string[] crobusWakeSpeech;
        private readonly string[] crobusEarlyDreamSpeech;
        private readonly string[] crobusEarlyWakeSpeech;
        private bool userTookEarlyNight;


        public ModEntry()
        {
            userTookEarlyNight = false;
            BuffUniqueID = 58012397;
            buffSource = new string[5]
            {
                "A strange dream...",
                "Did I dream that?",
                "How odd!",
                "???",
                "!@#~??&*£)"
            };
            wakeUpMessages = new string[3]
            {
                "You wake up from the coziest night's sleep...",
                "Wow! That was the best night's sleep you've ever had!",
                "You wake up with a newfound sense of inner peace"
            };
            crobusDreamSpeech = new string[5]
            {
                "Good night @...!$s#$b#Sweet dreams.$h",
                "You had a big day today... time to get some rest#$b##Good night @.$h",
                "Sleep well, you have a big day ahead of you tomorrow!$h",
                "Shhhh...#$b##Time to sleep now.$h",
                "All tucked in?$s#$b#Good... sweet dreams.$h",
            };
            crobusWakeSpeech = new string[5]
            {
                "You look like you slept well last night.#$b#(You're welcome)$h",
                "Big night?",
                "I like to watch you sleep sometimes...#$b#I hope that's okay.",
                "Zzzzz...#$b#Sorry, I was working late last night.",
                "Hi again @!#$b#Again? Uh.. no-#$b#I mean...#$b#Hi @."
            };
            crobusEarlyDreamSpeech = new string[3]
{
                "An early night was a good idea @...!$s#$b#Sweet dreams.$h",
                "You had a big day today... time to get some rest#$b##I hope this early night makes you feel better.$h",
                "I'm proud of you for turning in early today, @!$h",
};
            crobusEarlyWakeSpeech = new string[2]
            {
                "Early night, early rise!$h",
                "Nothing better than an early night when you need it.",
            };
            earlyWakeUpMessages = new string[3]
{
                "You wake up from the coziest night's sleep...",
                "Wow! That was the best night's sleep you've ever had!",
                "You wake up with a newfound sense of inner peace"
};
        }


        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            IModEvents events = helper.Events;
            events.GameLoop.DayStarted += this.OnDayStarted;
            events.GameLoop.DayEnding += this.OnDayEnding;
        }



        /// <summary> The method called after a new day starts.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            //ideally this would be after the user goes to sleep but needs to be a special event made with NightlyEvent class
            //spacecore has nightly event stuff
            //need to add SpaceCore as a dependency to mod since it will be referencing its DLL
            this.DreamEvent(e);
        }

        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            this.Monitor.Log(Game1.timeOfDay.ToString(), LogLevel.Info);
            if(Game1.timeOfDay < 2230)
            {
                userTookEarlyNight = true;
            } else
            {
                userTookEarlyNight = false;
            }
        }



        private void DreamEvent(EventArgs e)
        {
            string[] eventString = new[] { "" } ;

            if (userTookEarlyNight)
            {
                eventString = GetEarlyNightEventString();
            }
            else if (Game1.isRaining || Game1.isSnowing)
            {
                eventString = GetTuckInEventString();
            }


            if (eventString[0].Length > 0)
            {
                Event dreamEvent = new(string.Join(string.Empty, eventString));
                dreamEvent.onEventFinished = () =>
                {
                    UpdateBuff();
                    DisplayWakeUpMessage();
                };

                Game1.currentLocation.startEvent(dreamEvent);
            }


        }




        /// <summary>After a dream, applies a buff to the player.</summary>
        private void UpdateBuff()
        {
            Buff buff = Game1.buffsDisplay.otherBuffs.FirstOrDefault(p => p.which == this.BuffUniqueID);
            if (buff == null)
            {
                buff = new Buff(0, 0, 0, 0, 10, 0, 0, 0, 10, 10, 0, 0, 500, this.buffSource[this.GetRandomIndex(this.buffSource.Length)], this.buffSource[this.GetRandomIndex(this.buffSource.Length)]) { which = this.BuffUniqueID };
                Game1.buffsDisplay.addOtherBuff(buff);
            }
        }

        private void DisplayWakeUpMessage()
        {
            if (userTookEarlyNight)
            {
                Game1.activeClickableMenu = new DialogueBox(this.earlyWakeUpMessages[this.GetRandomIndex(this.earlyWakeUpMessages.Length)]);
            } else
            {
                Game1.activeClickableMenu = new DialogueBox(this.wakeUpMessages[this.GetRandomIndex(this.wakeUpMessages.Length)]);
            }

        }

        private int GetRandomIndex(int length)
        {
            Random random = new Random();
            return random.Next(0, length);
        }

        private string[] GetTuckInEventString()
        {
            string[] dreamEventString = new[]
            {
                "grandpas_theme/6 6/farmer 9 9 2 Crobus 7 7 1/skippable/ambientLight 180 140 80/pause 500/emote Crobus 32/pause 500/move Crobus 0 2 2/move Crobus 1 0 1/pause 500/speak Crobus \"" + this.crobusDreamSpeech[this.GetRandomIndex(this.crobusDreamSpeech.Length)] + "\"/pause 500/emote Crobus 20/pause 500/globalFade/viewport -1000 -1000/end dialogue Crobus \"" + this.crobusWakeSpeech[this.GetRandomIndex(this.crobusWakeSpeech.Length)] + "\"",
            };

            return dreamEventString;
        }

        private string[] GetEarlyNightEventString()
        {
            string[] dreamEventString = new[]
            {
                "grandpas_theme/6 6/farmer 9 9 2 Crobus 7 7 1/skippable/ambientLight 180 140 80/pause 500/emote Crobus 32/pause 500/move Crobus 0 2 2/move Crobus 1 0 1/pause 500/speak Crobus \"" + this.crobusEarlyDreamSpeech[this.GetRandomIndex(this.crobusEarlyDreamSpeech.Length)] + "\"/pause 500/emote Crobus 20/pause 500/globalFade/viewport -1000 -1000/end dialogue Crobus \"" + this.crobusEarlyWakeSpeech[this.GetRandomIndex(this.crobusEarlyWakeSpeech.Length)] + "\"",
            };

            return dreamEventString;
        }

    }
}