using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Monster.Types
{
    public class MonsterSenseTelepathy
    {
        public int Range;
    }

    public class MonsterSenses
    {
        public int Perception;
        public MonsterSenseTelepathy Telepathy;
    }
}
