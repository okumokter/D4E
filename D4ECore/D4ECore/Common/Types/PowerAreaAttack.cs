using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Common.Types
{
    public class PowerAreaAttack : PowerAttack
    {
        public override PowerAttackType Type() => PowerAttackType.Area;
        public int Range;
    }
}
