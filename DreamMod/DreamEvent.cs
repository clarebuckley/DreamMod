using System;
using System.Linq;
using StardewValley.Menus;
using StardewValley;

namespace DreamMod
{
    internal class DreamEvent
    {
        private readonly int BuffUniqueID = 58012397;
        private readonly string[] buffSource;
        private DialogueHelper dialogueHelper;

        public DreamEvent()
        {
            this.dialogueHelper = new DialogueHelper();
            this.BuffUniqueID = 58012397;
            buffSource = new string[5]
            {
                "A strange dream...",
                "Did I dream that?",
                "How odd!",
                "???",
                "!@#~??&*£)"
            };

        }

        public void StartEvent(EventArgs e, bool userTookEarlyNight)
        {
            string[] eventString = new[] { "" };

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
                    DisplayWakeUpMessage(userTookEarlyNight);
                };
                Game1.currentLocation.startEvent(dreamEvent);
            }
        }

        private void UpdateBuff()
        {
            Buff buff = Game1.buffsDisplay.otherBuffs.FirstOrDefault(p => p.which == this.BuffUniqueID);
            if (buff == null)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, this.buffSource.Length);
                buff = new Buff(0, 0, 0, 0, 10, 0, 0, 0, 10, 10, 0, 0, 500, this.buffSource[randomIndex], this.buffSource[randomIndex]) { which = this.BuffUniqueID };
                Game1.buffsDisplay.addOtherBuff(buff);
            }
        }

        private void DisplayWakeUpMessage(bool userTookEarlyNight)
        {
            if (userTookEarlyNight)
            {
                Game1.activeClickableMenu = new DialogueBox(dialogueHelper.GetRandomEarlyWakeUpMessage());
            }
            else
            {
                Game1.activeClickableMenu = new DialogueBox(dialogueHelper.GetRandomWakeUpMessage());
            }
        }

        private string[] GetTuckInEventString()
        {
            string[] dreamEventString = new[]
            {
                "grandpas_theme/6 6/farmer 9 9 2 Crobus 7 7 1/skippable/ambientLight 180 140 80/pause 500/emote Crobus 32/pause 500/move Crobus 0 2 2/move Crobus 1 0 1/pause 500/speak Crobus \"" + dialogueHelper.GetRandomCrobusDreamSpeech() + "\"/pause 500/emote Crobus 20/pause 500/globalFade/viewport -1000 -1000/end dialogue Crobus \"" + dialogueHelper.GetRandomCrobusWakeSpeech() + "\"",
            };

            return dreamEventString;
        }

        private string[] GetEarlyNightEventString()
        {
            string[] dreamEventString = new[]
            {
                "grandpas_theme/6 6/farmer 9 9 2 Crobus 7 7 1/skippable/ambientLight 180 140 80/pause 500/emote Crobus 32/pause 500/move Crobus 0 2 2/move Crobus 1 0 1/pause 500/speak Crobus \"" + dialogueHelper.GetRandomCrobusEarlyDreamSpeech() + "\"/pause 500/emote Crobus 20/pause 500/globalFade/viewport -1000 -1000/end dialogue Crobus \"" + dialogueHelper.GetRandomCrobusEarlyWakeSpeech() + "\"",
            };

            return dreamEventString;
        }
    }
}