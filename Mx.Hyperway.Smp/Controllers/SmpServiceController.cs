using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Controllers
{
    using Autofac;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Rest;

    using Mx.Hyperway.Smp.Data;
    using Mx.Hyperway.Smp.Models;
    using Mx.Peppol.Common.Util;

    public class SmpServiceController : Controller
    {
        private readonly SmpContext context;

        public SmpServiceController(SmpContext context)
        {
            this.context = context;
        }

        public IActionResult ListByParticipant(string participant)
        {
            var services =
                (from x in this.context.SmpServices.Include(x => x.PeppolParticipant).Include(x => x.PeppolDocument)
                     .Include(x => x.PeppolProcess)
                 where x.PeppolParticipant.Identifier == participant
                 select x).ToList();
            var model = new SmpServiceListModel();
            model.ParticipantIdentifier = participant;
            model.Services = new List<SmpServiceModel>();
            foreach (var service in services)
            {
                var serviceModel = new SmpServiceModel();
                serviceModel.DocumentIdentifier = service.PeppolDocument.Identifier;
                serviceModel.ParticipantIdentifier = service.PeppolParticipant.Identifier;
                serviceModel.ProcessIdentifier = service.PeppolProcess.Identifier;
                var metadataPath = ModelUtils.Urlencode(
                    "{0}/services/{1}",
                    serviceModel.ParticipantIdentifier,
                    serviceModel.DocumentIdentifier);
                serviceModel.ServiceMetadataUrl = $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host}/{metadataPath}";
                model.Services.Add(serviceModel);
            }

            return this.View(model);
        }
    }
}
