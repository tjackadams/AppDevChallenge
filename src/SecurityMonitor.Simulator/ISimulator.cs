using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Simulator
{
    public interface ISimulator
    {
        Task Simulate();
    }
}
