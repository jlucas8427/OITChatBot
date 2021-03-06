﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http;

namespace OITChatBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterBotDependencies();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void RegisterBotDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AgentModule>();

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            builder.Update(Conversation.Container);

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(Conversation.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);
        }
    }
}
