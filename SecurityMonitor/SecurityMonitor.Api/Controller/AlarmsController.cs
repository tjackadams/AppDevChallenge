using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecurityMonitor.Api.Models;
using SecurityMonitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMonitor.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAlarmRepository _alarmRepository;

        public AlarmsController(IMapper mapper, IAlarmRepository alarmRepository)
        {
            _mapper = mapper;
            _alarmRepository = alarmRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var alarms = await _alarmRepository.GetAllLatest();

            return Ok(_mapper.Map<IEnumerable<AlarmViewModel>>(alarms));

        }
    }
}
