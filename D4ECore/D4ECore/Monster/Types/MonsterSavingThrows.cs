using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;

namespace D4ECore.Monster.Types
{
    public class MonsterSavingThrows
    {
        public int General;
        public readonly Dictionary<EffectType, int> AgainstEffects;

        public MonsterSavingThrows()
        {
            AgainstEffects = new Dictionary<EffectType, int>();
        }
    }
}
