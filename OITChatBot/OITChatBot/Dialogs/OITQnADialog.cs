using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Azure;
using QnAMakerDialog;

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
        context.Wait(MessageReceived);
    }

    [QnAMakerResponseHandler(50)]
    public async Task LowScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
    {
        await context.PostAsync($"I found an answer that might help...{result.Answer}.");
        context.Wait(MessageReceived);
    }
}