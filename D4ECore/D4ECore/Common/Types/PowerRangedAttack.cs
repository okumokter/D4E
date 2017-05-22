using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Common.Types
{
    public class PowerRangedAttack : PowerAttack
    {
        public override PowerAttackType Type() => PowerAttackType.Ranged;
        public int Range;
    }
}
