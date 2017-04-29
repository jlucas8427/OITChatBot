using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Text.RegularExpressions;

namespace OITChatBot.Dialogs
{
    /// <summary>
    /// Simple Dialog, that invokes the QnAMaker if the incoming message is a question
    /// </summary>
    [Serializable]
    public class HandoffDialog : IDialog<object>
    {
        private readonly IUserToAgent _userToAgent;

        public HandoffDialog(IUserToAgent userToAgent)
        {
            _userToAgent = userToAgent;
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            //Call the QnAMaker Dialog if the message is a question.
            if (message.Text == "transfer to front desk")
            {
                var agent = await _userToAgent.IntitiateConversationWithAgentAsync(message as Activity, default(CancellationToken));
                if (agent == null)
                    await context.PostAsync("All our customer care representatives are busy at the moment. Please try after some time.");
                context.Done(true);
                //await context.Forward(new EchoDialog(_userToAgent), AfterQnA, message, CancellationToken.None);
                //await context.Forward(new BasicQnAMakerDialog(), AfterQnA, message, CancellationToken.None);
            }
            else
            {
                await context.Forward(new BasicQnAMakerDialog(), AfterQnA, message, CancellationToken.None);
                //await Conversation.SendAsync(activity, () => new BasicQnAMakerDialog());
                context.Wait(MessageReceivedAsync);
            }


        }

        //Callback, after the QnAMaker Dialog returns a result.
        public async Task AfterQnA(IDialogContext context, IAwaitable<object> argument)
        {
            context.Wait(MessageReceivedAsync);
        }
    }
}