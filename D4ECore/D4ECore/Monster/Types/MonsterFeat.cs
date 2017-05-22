using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Monster.Types
{
    public class MonsterFeat
    {
        public string Name;
        public string Detail;
        public string Requirement;
    }

    public class MonsterFeats : List<MonsterFeat> { }
}
