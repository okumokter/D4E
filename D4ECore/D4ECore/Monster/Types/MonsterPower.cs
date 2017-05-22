using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;

namespace D4ECore.Monster.Types
{
    public abstract class MonsterPower
    {
        public string Name;
        public string Detail;
        public PowerAction Action;
        public PowerRecharge Recharge;
        public abstract PowerCategory Category();
        public string Requirement;
        public string RechargeCondition;
    }

    public class MonsterPowers : List<MonsterPower> { }
}
