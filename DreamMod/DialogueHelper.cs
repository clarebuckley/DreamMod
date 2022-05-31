using System;

namespace DreamMod
{
    internal class DialogueHelper
    {
        private readonly string[] wakeUpMessages;
        private readonly string[] earlyWakeUpMessages;
        private readonly string[] crobusDreamSpeech;
        private readonly string[] crobusWakeSpeech;
        private readonly string[] crobusEarlyDreamSpeech;
        private readonly string[] crobusEarlyWakeSpeech;

        public DialogueHelper()
        {
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
                "That early night was worth it.",
                "You feel so much better after an early night!",
                "Is it morning already?!"
             };
        }

        public string GetRandomWakeUpMessage()
        {
            return this.wakeUpMessages[GetRandomIndex(this.wakeUpMessages.Length)];
        }

        public string GetRandomEarlyWakeUpMessage()
        {
            return this.earlyWakeUpMessages[GetRandomIndex(this.earlyWakeUpMessages.Length)];
        }

        public string GetRandomCrobusDreamSpeech()
        {
            return this.crobusDreamSpeech[GetRandomIndex(this.crobusDreamSpeech.Length)];
        }

        public string GetRandomCrobusEarlyDreamSpeech()
        {
            return this.crobusEarlyDreamSpeech[GetRandomIndex(this.crobusEarlyDreamSpeech.Length)];
        }

        public string GetRandomCrobusWakeSpeech()
        {
            return this.crobusWakeSpeech[GetRandomIndex(this.crobusWakeSpeech.Length)];
        }

        public string GetRandomCrobusEarlyWakeSpeech()
        {
            return this.crobusEarlyWakeSpeech[GetRandomIndex(this.crobusEarlyWakeSpeech.Length)];
        }

        private static int GetRandomIndex(int length)
        {
            Random random = new Random();
            return random.Next(0, length);
        }
    }
}