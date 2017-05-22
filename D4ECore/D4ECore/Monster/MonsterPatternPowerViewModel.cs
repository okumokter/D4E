using D4ECore.Common.Types;
using D4ECore.Monster.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace D4ECore.Monster
{
    public class MonsterPatternPowerViewModel : INotifyPropertyChanged
    {
        private MonsterPower model;
        public MonsterPower Model
        {
            get { return model; }
            set
            {
                model = value;
                NotifyPropertyChanged("");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string AttackType
        {
            get
            {
                MonsterAttackPower attackPower = (Model as MonsterAttackPower);
                string code = "";
                if (attackPower != null)
                {
                    switch (attackPower.AttackType)
                    {
                        case PowerAttackType.Melee:
                            code = "M";
                            break;
                        case PowerAttackType.Ranged:
                            code = "R";
                            break;
                        case PowerAttackType.Close:
                            code = "C";
                            break;
                        case PowerAttackType.Area:
                            code = "A";
                            break;
                    }
                    if (attackPower.Basic)
                        code = code.ToLower();
                }
                return code;
            }
        }

        public Visibility AttackTypeVisibility => (string.IsNullOrEmpty(AttackType) ? Visibility.Collapsed : Visibility.Visible);

        public string Name => (Model != null ? Model.Name : "Attack name");

        public string MainProperties
        {
            get
            {
                var properties = new List<string>();
                if (Model != null)
                {
                    properties.Add(Model.Action.ToString());
                    properties.Add(Model.Recharge.ToString());
                    if (!string.IsNullOrEmpty(Model.Requirement))
                        properties.Add(Model.Requirement);
                    if (!string.IsNullOrEmpty(Model.RechargeCondition))
                        properties.Add(Model.RechargeCondition);
                }
                if (properties.Count > 0)
                    return $"({string.Join("; ", properties)})";
                else
                    return "";
            }
        }

        public string Detail => (Model != null ? Model.Detail : "<details>");
    }
}
