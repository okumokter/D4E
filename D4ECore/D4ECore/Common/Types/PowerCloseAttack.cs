﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Common.Types
{
    public class PowerCloseAttack : PowerAttack
    {
        public override PowerAttackType Type() => PowerAttackType.Close;
        public int Range;
    }
}
