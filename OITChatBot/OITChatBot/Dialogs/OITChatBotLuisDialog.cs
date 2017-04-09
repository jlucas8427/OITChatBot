using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace OITChatBot
{
    [LuisModel("76a5efb1-e127-4637-9ff8-aad1c8378f4e", "7c8b455d7e754c2da71591c53d6d0f10")]
    [Serializable]
    public class OITChatBotLuisDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry. I didn't understand you.");
            context.Wait(MessageReceived);
        }
    }
}