using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Mx.Hyperway.Smp.Data;
    using Mx.Hyperway.Smp.Models;

    public class SmpParticipantController : Controller
    {
        private readonly SmpContext context;

        public SmpParticipantController(SmpContext context)
        {
            this.context = context;
        }
        public IActionResult List()
        {
            var participants = this.context.PeppolParticipants
                .Select(x => new SmpParticipant { Id = x.Id, Identifier = x.Identifier }).ToList();
            var model = new SmpParticipantListModel();
            model.Participants = participants;
            return this.View(model);
        }
    }
}
