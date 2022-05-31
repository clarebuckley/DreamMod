using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace DreamMod
{
    public class ModEntry : Mod
    {
        private DreamEvent dreamEvent;
        private bool userTookEarlyNight;

        public ModEntry()
        {
            this.dreamEvent = new DreamEvent();
            this.userTookEarlyNight = false;
        }

        public override void Entry(IModHelper helper)
        {
            IModEvents events = helper.Events;
            events.GameLoop.DayStarted += this.OnDayStarted;
            events.GameLoop.DayEnding += this.OnDayEnding;
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            //TODO
            //ideally this would be after the user goes to sleep but needs to be a special event made with NightlyEvent class
            //spacecore also has nightly event stuff, need to add SpaceCore as a dependency to mod since it will be referencing its DLL
            dreamEvent.StartEvent(e, userTookEarlyNight);
        }

        private void OnDayEnding(object sender, DayEndingEventArgs e)
        {
            if(Game1.timeOfDay < 2230)
            {
                userTookEarlyNight = true;
            } else
            {
                userTookEarlyNight = false;
            }
        }
    }
}