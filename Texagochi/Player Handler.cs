using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texagochi
{
    internal class Player_Handler
    {
        Dictionary<string, Tex> texes = new Dictionary<string, Tex>();

        public Player_Handler()
        {

        }

        public KeyValuePair<FileAttachment, String> HandleDeath(string id, string reason)
        {
            texes.Remove(id);

            FileAttachment fileAttachment = new FileAttachment("C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex egg.png");

            string outputText = "Your Tex has ran out of " + reason + "!\n" + "Tex has turned back into an egg, taking away your money and items in the process!";

            KeyValuePair<FileAttachment, String> output = new KeyValuePair<FileAttachment, string>(fileAttachment, outputText);

            return output;
        }

        public KeyValuePair<FileAttachment, String> RegisterTex(string id)
        {
            if (texes.ContainsKey(id))
            {
                return new KeyValuePair<FileAttachment, string>(new FileAttachment("C:\\Users\\samue\\Pictures\\Camera Roll\\WIN_20241208_13_17_46_Pro.jpg"), "You already have a Tex!");
            }

            texes.Add(id, new Tex(id, this));

            FileAttachment fileAttachment = new FileAttachment("C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex happy.png");

            string outputText = "You have registered your Tex!\nMake sure to feed them with /feed\nMake sure to let them play with /play\n";

            KeyValuePair<FileAttachment, String> output = new KeyValuePair<FileAttachment, string>(fileAttachment, outputText);

            return output;
        }

        public KeyValuePair<FileAttachment, String> Status(string id)
        {
            if (!texes.ContainsKey(id))
            {
                return new KeyValuePair<FileAttachment, string>(new FileAttachment("C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex egg.png"), "You need to register Tex!");
            }

            Tex tex = texes[id];

            KeyValuePair<FileAttachment, String> output = tex.handleChange();

            return output;
        }

        public KeyValuePair<FileAttachment, String> Play(string id)
        {
            if (!texes.ContainsKey(id))
            {
                return new KeyValuePair<FileAttachment, string>(new FileAttachment("C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex egg.png"), "You need to register Tex!");
            }

            Tex tex = texes[id];

            KeyValuePair<FileAttachment, String> output = tex.handleChange();

            if (tex.status == "egg")
            {
                return output;
            }

            output = tex.play();

            return output;
        }

        public KeyValuePair<FileAttachment, String> Feed(string id)
        {
            if (!texes.ContainsKey(id))
            {
                return new KeyValuePair<FileAttachment, string>(new FileAttachment("C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex egg.png"), "You need to register Tex!");
            }

            Tex tex = texes[id];

            KeyValuePair<FileAttachment, String> output = tex.handleChange();

            if (tex.status == "egg")
            {
                return output;
            }

            output = tex.feed();

            return output;
        }

        public KeyValuePair<FileAttachment, String> Chilling(string id)
        {
            if (!texes.ContainsKey(id))
            {
                return new KeyValuePair<FileAttachment, string>(new FileAttachment("C:\\Programming\\c#\\Texagochi\\Texagochi\\bin\\Debug\\net6.0\\Tex egg.png"), "You need to register Tex!");
            }

            Tex tex = texes[id];

            KeyValuePair<FileAttachment, String> output = tex.handleChange();

            if (tex.status == "egg")
            {
                return output;
            }

            output = tex.chilling();

            return output;
        }
    }
}
