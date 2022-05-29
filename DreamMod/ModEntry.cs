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
        private string[] source;


        public ModEntry()
        {
            BuffUniqueID = 58012397;
            source = new string[5] 
            { 
                "A strange dream...", 
                "Did I dream that?", 
                "How odd!", 
                "???", 
                "!@#~??&*£)" 
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
            this.Monitor.Log("Starting dream event", LogLevel.Info);
            //this is glitchy but works kind of
            string[] dreamEventString = new[]
            {
                "grandpas_theme/6 6/farmer 9 9 2 Crobus 7 7 1/skippable/pause 500/emote Crobus 36/move Crobus 0 2 2/move Crobus 1 0 1/pause 500/speak Crobus \"Good night @...!$s#$b#Sweet dreams.$h\"/pause 500/emote Crobus 64/globalFade/viewport -1000 -1000/end dialogue Crobus \"I like to watch you sleep.$h\"",
            };

            Event dreamEvent = new(string.Join(string.Empty, dreamEventString));
            dreamEvent.onEventFinished = () =>
            {
                UpdateBuff();
                string[] wakeUpMessages = new string[3]
                {
                "You wake up from the coziest night's sleep...",
                "Wow! That was the best night's sleep of your life!",
                "You wake up with a newfound sense of inner peace"
                };
                Random random = new Random();
                int randomIndex = random.Next(0, wakeUpMessages.Length);
                Game1.activeClickableMenu = new DialogueBox(wakeUpMessages[randomIndex]);

            };

            Game1.currentLocation.startEvent(dreamEvent);
        }




        /// <summary>After a dream, applies a buff to the player.</summary>
        private void UpdateBuff()
        {
            Buff buff = Game1.buffsDisplay.otherBuffs.FirstOrDefault(p => p.which == this.BuffUniqueID);
            if (buff == null)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, this.source.Length);
                buff = new Buff(0, 0, 0, 0, 10, 0, 0, 0, 10, 10, 0, 0, 1000, this.source[randomIndex], this.source[randomIndex]) { which = this.BuffUniqueID };
                Game1.buffsDisplay.addOtherBuff(buff);
            }
        }

    }
}