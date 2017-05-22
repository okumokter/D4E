using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D4ECore.Common.Types;
using D4ECore.Monster.Types;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;

namespace D4ECore.Monster
{
    public class MonsterPatternViewModel : INotifyPropertyChanged
    {
        private MonsterPattern model;
        public MonsterPattern Model
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

        public readonly List<MonsterPatternPowerViewModel> PowerViewModels;

        public IEnumerable<MonsterPatternPowerViewModel> Powers => Model?.Powers.Select(x => new MonsterPatternPowerViewModel() { Model = x });
        public IEnumerable<MonsterPatternFeatViewModel> Feats => Model?.Feats.Select(x => new MonsterPatternFeatViewModel() { Model = x });

        public string Name => (Model != null ? Model.Name : "Monster name");
        public string Category
        {
            get
            {
                var list = new List<string>();
                list.Add("Level");
                list.Add((Model != null ? Model.Level.ToString() : "#"));
                list.Add((Model != null ? Model.Role.ToString() : "Role"));
                return string.Join(" ", list);
            }
        }
        public string Type
        {
            get
            {
                var list = new List<string>();
                list.Add((Model != null ? Model.Size.ToString() : "Size"));
                list.Add((Model != null ? Model.Origin.ToString() : "origin"));
                list.Add((Model != null ? Model.Type.ToString() : "type"));
                return string.Join(" ", list);
            }
        }
        public string XP => $"XP {(Model != null ? Model.XP.ToString("N0") : "#")}";
        public string Initiative => $"{(Model != null ? Model.Initiative.Signed() : "+#")}";
        public string HealthPoints => $"{(Model != null ? (Model.Role == MonsterRole.Minion ? $"{Model.HealthPoints.ToString()}; a missed attack never damages a minion." : Model.HealthPoints.ToString()) : "#")}";
        public string Bloodied => $"{(Model != null ? Model.Bloodied.ToString() : "#")}";
        public Visibility BloodiedVisible => ((Model != null) && (Model.Role == MonsterRole.Minion) ? Visibility.Collapsed : Visibility.Visible);
        public string ArmorClass => $"{(Model != null ? Model.Defenses.ArmorClass.ToString() : "#")}";
        public string Fortitude => $"{(Model != null ? Model.Defenses.Fortitude.ToString() : "#")}";
        public string Reflexes => $"{(Model != null ? Model.Defenses.Reflexes.ToString() : "#")}";
        public string Willpower => $"{(Model != null ? Model.Defenses.Willpower.ToString() : "#")}";
        public string Senses
        {
            get
            {
                if (Model != null)
                {
                    var list = new List<string>();
                    list.Add($"Perception {Model.Senses.Perception.Signed()}");
                    return string.Join(" ", list);
                }
                return "";
            }
        }
        public string ActionPoints => $"{(Model != null ? Model.ActionPoints.ToString() : "#")}";
        public Visibility ActionPointsVisible => ((Model != null) && (Model.ActionPoints > 0) ? Visibility.Visible : Visibility.Collapsed);
        public string SavingThrows
        {
            get
            {
                if (Model != null)
                {
                    var list = new List<string>();
                    if (Model.SavingThrows.General > 0)
                        list.Add(Model.SavingThrows.General.Signed());
                    foreach (var effect in Model.SavingThrows.AgainstEffects.Keys)
                    {
                        if (Model.SavingThrows.AgainstEffects[effect] > 0)
                            list.Add($"{Model.SavingThrows.AgainstEffects[effect].Signed()} against {effect.ToString()} effects.");
                    }
                    return string.Join(", ", list);
                }
                return "";
            }
        }
        public Visibility SavingThrowsVisible => (
            (Model != null) && (Model.SavingThrows.General + Model.SavingThrows.AgainstEffects.Sum(x => x.Value) > 0) 
            ? Visibility.Visible 
            : Visibility.Collapsed
        );
        public string Alignment
        {
            get
            {
                if (Model != null)
                {
                    if (Model.Alignment.Values.Count == 5)
                        return "Any";
                    else
                        return string.Join(", ", Model.Alignment.Values.Select(x => x.ToString()));

                }
                return "";
            }
        }
        public string Movement
        {
            get
            {
                if (Model != null)
                {
                    if (Model.Movement.Values.Count > 0)
                    {
                        var list = new List<string>();
                        foreach (var m in Model.Movement.Values)
                        {
                            string s = $"{((m.Key != MonsterMovementType.Normal) ? $"{m.Key.ToString()} " : "")}{m.Value.Rate}";
                            if (!string.IsNullOrEmpty(m.Value.Remark))
                                s += $" ({m.Value.Remark})";
                            list.Add(s);
                        }
                        return string.Join(", ", list);
                    }
                    return "(none)";
                }
                return "";
            }
        }
        public string Languages
        {
            get
            {
                if (Model != null)
                {
                    return string.Join(", ", Model.Languages.Values.Select(x => x.ToString()));
                }
                return "-";
            }
        }
        public string Skills
        {
            get
            {
                if (Model != null)
                {
                    return string.Join(", ", Model.Skills.Values.Where(x => x.Value.IsSpecial)
                        .Select(x => string.Join(" ", new string[]
                        {
                            x.Key.ToString(),
                            x.Value.Value.Signed()
                        })));
                }
                return "";
            }
        }
        public Visibility SkillsVisible => ((Model == null) || Model.Skills.Values.Where(x => x.Value.IsSpecial).Count() > 0 ? Visibility.Visible : Visibility.Collapsed);
        public IEnumerable<string> AbilityScores
        {
            get
            {
                foreach(AbilityScore score in Enum.GetValues(typeof(AbilityScore)))
                {
                    yield return (Model != null
                        ? $"{Model.AbilityScores.Values[score].Score} ({Model.AbilityScores.Values[score].Modifier.Signed()})"
                        : "# (#)");
                }
            }
        }
        public string Strength => AbilityScores.ElementAt(0);
        public string Constitution => AbilityScores.ElementAt(1);
        public string Dexterity => AbilityScores.ElementAt(2);
        public string Intelligence => AbilityScores.ElementAt(3);
        public string Wisdom => AbilityScores.ElementAt(4);
        public string Charisma => AbilityScores.ElementAt(5);
    }
}
