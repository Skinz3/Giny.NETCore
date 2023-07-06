using Giny.ORM;
using Giny.World;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

namespace Giny.SpellTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TreeDrawer Drawer
        {
            get;
            set;
        }
        public MainWindow()
        {
            InitializeComponent();

            Drawer = new TreeDrawer(canvas);

            DatabaseManager.Instance.Initialize(Assembly.GetAssembly(typeof(SpellRecord)), "127.0.0.1", "giny_world2", "root", "");

            DatabaseManager.Instance.LoadTable<SpellRecord>();
            DatabaseManager.Instance.LoadTable<SpellLevelRecord>();

            SpellRecord.Initialize();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var spellId = short.Parse(spellInput.Text);

            var spell = SpellRecord.GetSpellRecord(spellId);

            if (spell != null )
            {
                Drawer.Draw(spell);
            }
            return;
            Task.Run(() =>
            {
                foreach (var spell in SpellRecord.GetSpellRecords())
                {

                    this.Dispatcher.Invoke(new(() =>
                    {
                        Drawer.Draw(spell);
                    }));


                    Thread.Sleep(1000);

                }
            });



        }
    }
}
