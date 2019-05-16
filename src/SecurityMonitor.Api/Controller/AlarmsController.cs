using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecurityMonitor.Api.Models;
using SecurityMonitor.Core.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SecurityMonitor.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAlarmRepository _alarmRepository;

        public AlarmsController(IMapper mapper, IAlarmRepository alarmRepository)
        {
            _mapper = mapper;
            _alarmRepository = alarmRepository;
        }

        /// <summary>
        /// Gets all the latest Alarms
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all the latest Alarms</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlarmViewModel>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var alarms = await _alarmRepository.GetAllLatest();

            return Ok(_mapper.Map<IEnumerable<AlarmViewModel>>(alarms));
        }
    }
}
