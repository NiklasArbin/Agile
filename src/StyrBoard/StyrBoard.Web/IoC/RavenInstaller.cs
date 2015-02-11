﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Embedded;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.IoC
{
    public class RavenInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var documentStore = new EmbeddableDocumentStore
            {
                DataDirectory = "Data",
                UseEmbeddedHttpServer = false
            };

            documentStore.Initialize();

            container.Register(
                Component.For<IDocumentStore>().Instance(documentStore));
        }
    }
}