using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace D4ECore.Monster.WPF
{
    /// <summary>
    /// Interaktionslogik für MonsterPatternControl.xaml
    /// </summary>
    public partial class MonsterPatternControl : UserControl
    {
        public MonsterPatternControl()
        {
            InitializeComponent();
        }

        public MonsterPatternViewModel ViewModel => (MonsterPatternViewModel)DataContext;
    }
}
