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
    [LuisModel("b0fdd4b9-4b4a-4731-9943-99d37ab65587", "450b44bb31be48259054b12f0f32cc1c")]
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