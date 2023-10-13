using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Enum
{
    [Flags]
    public enum FireArc
    {
        None = 0,
        Fore = 1,
        Port = 2,       // Left
        Starboard = 4,  // Right
        Aft = 8,
        Forward = Fore | Port | Starboard,
        Rear = Aft | Port | Starboard,
        Broadside = Port | Starboard,
        PortSide = Fore | Port | Aft,
        StarboardSide = Fore | Starboard | Aft,
        All = Fore | Aft | Port | Starboard
    }
}
