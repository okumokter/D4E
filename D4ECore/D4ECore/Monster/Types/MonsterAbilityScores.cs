using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;

namespace D4ECore.Monster.Types
{
    public struct MonsterAbilityScoreProperty
    {
        public int Score;
        public int Modifier;
    }

    public class MonsterAbilityScores
    {
        public readonly IDictionary<AbilityScore, MonsterAbilityScoreProperty> Values;

        public MonsterAbilityScores()
        {
            Values = new Dictionary<AbilityScore, MonsterAbilityScoreProperty>();
        }
    }
}
