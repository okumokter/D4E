using D4ECore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Monster.Types
{
    public class MonsterDefense
    {
        public Dictionary<Defense, int> Values;

        public MonsterDefense()
        {
            Values = new Dictionary<Defense, int>();
        }

        public int ArmorClass
        {
            get { return Values[Defense.ArmorClass]; }
            set { Values[Defense.ArmorClass] = value; }
        }

        public int Fortitude
        {
            get { return Values[Defense.Fortitude]; }
            set { Values[Defense.Fortitude] = value; }
        }

        public int Reflexes
        {
            get { return Values[Defense.Reflexes]; }
            set { Values[Defense.Reflexes] = value; }
        }

        public int Willpower
        {
            get { return Values[Defense.Willpower]; }
            set { Values[Defense.Willpower] = value; }
        }
    }
}
