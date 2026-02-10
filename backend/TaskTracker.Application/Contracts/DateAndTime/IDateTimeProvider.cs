using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Application.Contracts.DateAndTime
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
