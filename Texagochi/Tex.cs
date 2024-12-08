using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Texagochi
{
    internal class Tex
    {
        public Player_Handler player_Handler;

        public string playerID;
        public int food;
        public int happiness;
        public int rest;

        public string status;

        public DateTime lastUpdated;

        public Tex(string playerID, Player_Handler player_Handler)
        {
            lastUpdated = DateTime.Now;
            this.playerID = playerID;
            food = 50;
            happiness = 50;
            rest = 50;
            this.player_Handler = player_Handler;

            status = "chilling";
        }

        public KeyValuePair<FileAttachment, String> handleChange()
        {
            DateTime now = DateTime.Now;

            TimeSpan difference = now.Subtract(lastUpdated);

            lastUpdated = DateTime.Now;

            //int change = Convert.ToInt32(-1 * difference.TotalMinutes / 5);

            int change = Convert.ToInt32(-1 * difference.TotalSeconds);

            Console.WriteLine(change);

            KeyValuePair<FileAttachment, String> output;

            if (status == "playing")
            {
                change *= -1;
            }

            output = addHappiness(change, false);

            if (output.Value != "")
            {
                return output;
            }

            addFood(change, false);

            if (output.Value != "")
            {
                return output;
            }

            output = GetStatus();

            return output;
        }

        public KeyValuePair<FileAttachment, String> play()
        {
            status = "playing";

            string path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex gaming.png";

            KeyValuePair<FileAttachment, String> output = handleChange();

            if (status == "egg")
            {
                return output;
            }

            output = new KeyValuePair<FileAttachment, string>(new FileAttachment(path), "Tex is now playing!");

            return output;
        }

        public KeyValuePair<FileAttachment, String> chilling()
        {
            status = "chilling";

            string path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex happy.png";

            KeyValuePair<FileAttachment, String> output = handleChange();

            if (status == "egg")
            {
                return output;
            }

            output = new KeyValuePair<FileAttachment, string>(new FileAttachment(path), "Tex is now chilling!");

            return output;
        }

        public KeyValuePair<FileAttachment, String> feed()
        {
            string path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex eating.png";

            KeyValuePair<FileAttachment, String> output = handleChange();

            if (status == "egg")
            {
                return output;
            }

            output = new KeyValuePair<FileAttachment, string>(new FileAttachment(path), "Tex eats a slice of pizza!");

            addFood(30, false);

            return output;
        }

        public int changeHappiness(int newHappiness, int currentHappiness)
        {
            if (newHappiness < currentHappiness)
            {
                return newHappiness;
            }

            return currentHappiness;
        }

        public KeyValuePair<FileAttachment, string> GetStatus()
        {
            string outputText = "Your Tex is currently " + status + "!\n" + "Tex is ";
            int spriteHappiness = 3;

            if (food < 20)
            {
                outputText += "very hungry!\nYou should feed them!";
                spriteHappiness = changeHappiness(0, spriteHappiness);
            }
            else if (food < 40)
            {
                outputText += "hungry!\nYou should feed them";
                spriteHappiness = changeHappiness(1, spriteHappiness);
            }
            else if (food < 80)
            {
                outputText += "satiated!";
                spriteHappiness = changeHappiness(2, spriteHappiness);
            }
            else
            {
                outputText += "full!";
                spriteHappiness = changeHappiness(3, spriteHappiness);
            }

            outputText += "\n\n";

            outputText += "Tex is ";

            if (happiness < 20)
            {
                outputText += "very sad!\nYou should let them play!";
                spriteHappiness = changeHappiness(0, spriteHappiness);
            }
            else if (happiness < 40)
            {
                outputText += "sad!\nYou should let them play!";
                spriteHappiness = changeHappiness(1, spriteHappiness);
            }
            else if (happiness < 80)
            {
                outputText += "happy!";
                spriteHappiness = changeHappiness(2, spriteHappiness);
            }
            else
            {
                outputText += "Very happy!";
                spriteHappiness = changeHappiness(3, spriteHappiness);
            }

            string path = "";

            if (status == "chilling")
            {
                switch (spriteHappiness)
                {
                    case 0:
                        path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex very sad.png";
                        break;
                    case 1:
                        path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex sad.png";
                        break;
                    case 2:
                        path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex happy.png";
                        break;
                    case 3:
                        path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex very happy.png";
                        break;
                }
            }
            else if (status == "playing")
            {
                path = "C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex gaming.png";
            }

            FileAttachment fileAttachment = new FileAttachment(path);

            KeyValuePair<FileAttachment, string> output = new KeyValuePair<FileAttachment, string>(fileAttachment, outputText);

            return output;
        }

        public KeyValuePair<FileAttachment, String> addHappiness(int happiness, bool handleChanges)
        {
            if (handleChanges) handleChange();

            this.happiness += happiness;

            if (this.happiness > 100)
            {
                this.happiness = 100;
            }

            if (this.happiness <= 0)
            {
                status = "egg";
                return player_Handler.HandleDeath(playerID, "happiness");
            }

            return new KeyValuePair<FileAttachment, string>(new FileAttachment(), "");
        }

        public KeyValuePair<FileAttachment, String> addFood(int food, bool handleChanges)
        {
            if (handleChanges) handleChange();

            this.food += food;

            if (this.food > 100)
            {
                this.food = 100;
            }

            if (this.food <= 0)
            {
                status = "egg";
                return player_Handler.HandleDeath(playerID, "food");
            }

            return new KeyValuePair<FileAttachment, string>(new FileAttachment(), "");
        }

        /*public KeyValuePair<FileAttachment, String> addRest(int rest, bool handleChanges)
        {
            if (handleChanges) handleChange();

            this.rest += rest;

            if (this.rest > 100)
            {
                this.rest = 100;
            }

            if (this.rest <= 0)
            {
                return player_Handler.HandleDeath(playerID, "rest");
            }

            return new KeyValuePair<FileAttachment, string>(new FileAttachment(), "");
        }*/
    }
}
