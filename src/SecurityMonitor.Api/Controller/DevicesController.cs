using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecurityMonitor.Core.Models.Devices.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMonitor.Api.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DevicesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllDevicesCommand.Request());
            return Ok(response);
        }
    }
}
