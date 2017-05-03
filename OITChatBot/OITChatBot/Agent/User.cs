﻿using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Connector;
using System;

namespace OITChatBot
{
    [Serializable]
    public class User
    {
        public User()
        {

        }
        public User(IActivity message)
        {
            ConversationReference = message.ToConversationReference();
        }

        public ConversationReference ConversationReference { get; set; }
    }
}
