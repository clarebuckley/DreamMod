using StardewModdingAPI;
using StardewModdingAPI.Events;
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
            source = new string[3] { "A strange dream...", "Did I dream that?", "How odd!" };
        }

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
        IModEvents events = helper.Events;
            events.GameLoop.DayStarted += this.OnDayStarted;
        }


        /// <summary>The method called after a new day starts.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
            UpdateBuff();

        }

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