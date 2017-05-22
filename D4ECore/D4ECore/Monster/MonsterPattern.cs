using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;
using D4ECore.Monster.Types;

namespace D4ECore.Monster
{
    public class MonsterPattern
    {
        public int Id;
        public string Name;
        public Size Size;
        public MonsterOrigin Origin;
        public MonsterType Type;
        public int Level;
        public MonsterRole Role;
        public int XP;
        public int Initiative;
        public MonsterSenses Senses;
        public int HealthPoints;
        public int Bloodied => HealthPoints / 2;
        public int ActionPoints;
        public MonsterSavingThrows SavingThrows;
        public MonsterDefense Defenses;
        public MonsterAbilityScores AbilityScores;
        public MonsterSkills Skills;
        public MonsterLanguages Languages;
        public MonsterAlignment Alignment;
        public MonsterMovement Movement;
        public MonsterPowers Powers;
        public MonsterFeats Feats;

        public MonsterPattern()
        {
            Senses = new MonsterSenses();
            SavingThrows = new MonsterSavingThrows();
            Defenses = new MonsterDefense();
            AbilityScores = new MonsterAbilityScores();
            Skills = new MonsterSkills();
            Languages = new MonsterLanguages();
            Alignment = new MonsterAlignment();
            Movement = new MonsterMovement();
            Powers = new MonsterPowers();
            Feats = new MonsterFeats();
        }
    }
}
