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
        private string[] buffSource;
        private string[] wakeUpMessages;
        private string[] crobusDreamSpeech;
        private string[] crobusWakeSpeech;


        public ModEntry()
        {
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
                "Wow! That was the best night's sleep of your life!",
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
        }


        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            IModEvents events = helper.Events;
            events.GameLoop.DayStarted += this.OnDayStarted;
        }



        /// <summary> The method called after a new day starts.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            //ideally this would be after the user goes to sleep but needs to be a special event made with NightlyEvent class
            //spacecore has nightly event stuff
            //need to add SpaceCore as a dependency to your mod since it will be referencing its DLL
            this.DreamEvent(e);
        }


        /// <summary Handles running the dream event.</summary>
        /// <param name="e"> OnWarp events.</param>
        private void DreamEvent(EventArgs e)
        {


            Event dreamEvent = new(string.Join(string.Empty, GetTuckInEvent()));
            dreamEvent.onEventFinished = () =>
            {
                UpdateBuff();
                DisplayWakeUpMessage();
            };

            Game1.currentLocation.startEvent(dreamEvent);
        }




        /// <summary>After a dream, applies a buff to the player.</summary>
        private void UpdateBuff()
        {
            Buff buff = Game1.buffsDisplay.otherBuffs.FirstOrDefault(p => p.which == this.BuffUniqueID);
            if (buff == null)
            {
                buff = new Buff(0, 0, 0, 0, 10, 0, 0, 0, 10, 10, 0, 0, 1000, this.buffSource[this.GetRandomIndex(this.buffSource.Length)], this.buffSource[this.GetRandomIndex(this.buffSource.Length)]) { which = this.BuffUniqueID };
                Game1.buffsDisplay.addOtherBuff(buff);
            }
        }

        private void DisplayWakeUpMessage()
        {
            Game1.activeClickableMenu = new DialogueBox(this.wakeUpMessages[this.GetRandomIndex(this.wakeUpMessages.Length)]);
        }

        private int GetRandomIndex(int length)
        {
            Random random = new Random();
            return random.Next(0, length);
        }

        private string[] GetTuckInEvent()
        {
            string[] dreamEventString = new[]
            {
                "grandpas_theme/6 6/farmer 9 9 2 Crobus 7 7 1/skippable/pause 500/emote Crobus 32/pause 500/move Crobus 0 2 2/move Crobus 1 0 1/pause 500/speak Crobus \"" + this.crobusDreamSpeech[this.GetRandomIndex(this.crobusDreamSpeech.Length)] + "\"/pause 500/emote Crobus 20/pause 500/globalFade/viewport -1000 -1000/end dialogue Crobus \"" + this.crobusWakeSpeech[this.GetRandomIndex(this.crobusWakeSpeech.Length)] + "\"",
            };

            return dreamEventString;
        }

    }
}