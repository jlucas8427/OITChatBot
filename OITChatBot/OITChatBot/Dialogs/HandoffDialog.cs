using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

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

            //Send activity to QnA dialog if a transfer is not specified
            if (message.Text == "transfer to front desk")
            {
                var agent = await _userToAgent.IntitiateConversationWithAgentAsync(message as Activity, default(CancellationToken));
                if (agent == null)
                    await context.PostAsync("All of our front desk staff are currently busy at the moment. Please wait and try again.");
                context.Done(true);
            }
            else
            {
                await context.Forward(new BasicQnAMakerDialog(), AfterQnA, message, CancellationToken.None);
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