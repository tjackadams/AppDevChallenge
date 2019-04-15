using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.Core.Domain
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; }
    }
}
