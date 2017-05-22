using D4ECore.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Monster.Types
{
    public class MonsterAttackPower : MonsterPower
    {
        public override PowerCategory Category()
        {
            return PowerCategory.Attack;
        }

        public bool Basic;
        private PowerAttackType attackType;
        public PowerAttackType AttackType
        {
            get { return attackType; }
            set
            {
                if (attackType != value)
                {
                    attackType = value;
                    switch (attackType)
                    {
                        case PowerAttackType.Melee:
                            Attack = new PowerMeleeAttack();
                            break;
                        case PowerAttackType.Ranged:
                            Attack = new PowerRangedAttack();
                            break;
                        case PowerAttackType.Close:
                            Attack = new PowerCloseAttack();
                            break;
                        case PowerAttackType.Area:
                            Attack = new PowerAreaAttack();
                            break;
                    }
                }
            }
        }
        public int? AttackBonus;
        public Defense? TargetDefense;
        public PowerAttack Attack;
    }
}
