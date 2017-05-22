using D4ECore.Monster.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Monster
{
    public class MonsterPatternFeatViewModel : INotifyPropertyChanged
    {
        private MonsterFeat model;
        public MonsterFeat Model
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

        public string Name => (Model != null ? Model.Name : "Feat name");

        public string MainProperties
        {
            get
            {
                var properties = new List<string>();
                if (Model != null)
                {
                    if (!string.IsNullOrEmpty(Model.Requirement))
                        properties.Add(Model.Requirement);
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
