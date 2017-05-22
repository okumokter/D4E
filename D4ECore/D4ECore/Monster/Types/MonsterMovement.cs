using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Monster.Types
{
    public class MonsterMovementProperty
    {
        public int Rate;
        public string Remark;
    }

    public class MonsterMovement
    {
        public readonly Dictionary<MonsterMovementType, MonsterMovementProperty> Values;
        
        public MonsterMovement()
        {
            Values = new Dictionary<MonsterMovementType, MonsterMovementProperty>();
        }
    }
}
