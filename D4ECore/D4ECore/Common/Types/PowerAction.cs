using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Common.Types
{
    public enum PowerAction
    {
        Standard = 1,
        Move = 2,
        Minor = 3,
        ImmediateInterrupt = 4,
        ImmediateReaction = 5,
        Free = 6,
        Triggered = 7
    }
}
