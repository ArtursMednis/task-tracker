using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Application.Contracts.Identity
{
    public interface ICurrentUser
    {
        string UserId { get; }
    }
}
