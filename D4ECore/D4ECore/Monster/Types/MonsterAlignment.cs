using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;

namespace D4ECore.Monster.Types
{
    public class MonsterAlignment
    {
        public readonly IList<Alignment> Values;

        public MonsterAlignment()
        {
            Values = new List<Alignment>();
        }
    }
}
