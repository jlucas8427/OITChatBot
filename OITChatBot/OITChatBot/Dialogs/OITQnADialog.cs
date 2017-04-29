using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using QnAMakerDialog;


namespace OITChatBot
{
    //Inherit from the QnAMakerDialog
    //[QnAMakerService("subkey", "kbid")]
    [QnAMakerService("74dacf0b891444fcba55590241654813", "0b579358-4d27-4ce4-b5a2-037320099c9a")]
    [Serializable]
    public class BasicQnAMakerDialog : QnAMakerDialog<object>
    {
        //Parameters to QnAMakerService are:
        //Compulsory: subscriptionKey, knowledgebaseId, 
        //Optional: defaultMessage, scoreThreshold[Range 0.0 – 1.0]
        public override async Task NoMatchHandler(IDialogContext context, string originalQueryText)
        {
            await context.PostAsync($"Sorry, I couldn't find an answer for '{originalQueryText}'.");
            await context.PostAsync("You can contact our front desk by typing \"transfer to front desk\"");
            context.Wait(MessageReceived);
        }

        [QnAMakerResponseHandler(50)]
        public async Task LowScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
            await context.PostAsync($"I found an answer that might help...{result.Answer}.");
            context.Wait(MessageReceived);
        }
    }
}
