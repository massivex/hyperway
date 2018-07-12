using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer
{
    using Autofac;

    using Mx.Oxalis.Api.Transformer;
    using Mx.Oxalis.Commons.IoC;
    using Mx.Oxalis.DocumentSniffer.Document;

    public class SnifferModule : OxalisModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NoSbdhParser>().Named<ContentDetector>("legacy").InstancePerLifetimeScope();
        }
    }
}
