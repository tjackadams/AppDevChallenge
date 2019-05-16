using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityMonitor.Core.Models.Devices.Commands
{
    public class GetAllDevicesCommand
    {
        public class Request : IRequest<IEnumerable<DeviceViewModel>>
        {

        }

        public class Handler : IRequestHandler<Request, IEnumerable<DeviceViewModel>>
        {
            private readonly IDeviceRepository _repository;
            private readonly IMapper _mapper;
            public Handler(IDeviceRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<IEnumerable<DeviceViewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                var devices = await _repository.GetAll();
                return _mapper.Map<IEnumerable<DeviceViewModel>>(devices);
            }
        }
    }
}
