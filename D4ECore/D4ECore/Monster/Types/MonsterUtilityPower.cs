using D4ECore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Monster.Types
{
    public class MonsterUtilityPower : MonsterPower
    {
        public override PowerCategory Category()
        {
            return PowerCategory.Utility;
        }
    }
}
