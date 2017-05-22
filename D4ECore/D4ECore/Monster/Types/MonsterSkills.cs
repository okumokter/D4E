using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;

namespace D4ECore.Monster.Types
{
    public struct MonsterSkillProperty
    {
        public int Value;
        public bool IsSpecial;
    }

    public class MonsterSkills
    {
        public readonly IDictionary<Skill, MonsterSkillProperty> Values;

        public MonsterSkills()
        {
            Values = new Dictionary<Skill, MonsterSkillProperty>();
        }
    }
}
