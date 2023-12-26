using Giny.ORM;
using Giny.Protocol.Enums;
using Giny.SpellTree.Graphics;
using Giny.World;
using Giny.World.Managers.Effects;
using Giny.World.Records.Effects;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Giny.IO.D2OClasses;

namespace Giny.SpellTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private Tree Tree
        {
            get;
            set;
        }

        private EffectDice CurrentEffect
        {
            get;
            set;
        }

        private Node SelectedNode
        {
            get;
            set;
        }

        public FontFamily AppFont;

        public static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            AppFont = (FontFamily)FindResource("Jost");

            Tree = new Tree(canvas);

            LoadApp();

            Tree.NodeClick += TreeNodeClick;



            DisplaySearchResults(SpellRecord.GetSpellRecords());

            this.canvas.MouseDown += Canvas_MouseDown;
            this.canvas.MouseMove += Canvas_MouseMove;
            this.canvas.MouseUp += Canvas_MouseUp;

        }



        Point lastMousePosition;

        private List<Node> PreviousNodes
        {
            get;
            set;
        } = new List<Node>();

        private void LoadApp()
        {
            DatabaseManager.Instance.Initialize(Assembly.GetAssembly(typeof(SpellRecord)), "127.0.0.1", "giny_world", "root", "");

            search.Visibility = Visibility.Hidden;
            spells.Visibility = Visibility.Hidden;
            spellName.Visibility = Visibility.Hidden;
            gradeSelect.Visibility = Visibility.Hidden;
            effectProps.Visibility = Visibility.Hidden;
            effects.Visibility = Visibility.Hidden;

            Task.Run(() =>
            {


                DatabaseManager.Instance.LoadTable<SpellRecord>();
                DatabaseManager.Instance.LoadTable<SpellLevelRecord>();
                DatabaseManager.Instance.LoadTable<SpellStateRecord>();
                DatabaseManager.Instance.LoadTable<MonsterRecord>();
                SpellRecord.Initialize();

                Dispatcher.Invoke(() =>
                {
                    spells.Visibility = Visibility.Visible;
                    search.Visibility = Visibility.Visible;
                    loadingLabel.Visibility = Visibility.Hidden;
                    spellName.Visibility = Visibility.Visible;
                    gradeSelect.Visibility = Visibility.Visible;
                    effectProps.Visibility = Visibility.Visible;
                    effects.Visibility = Visibility.Visible;
                    OnSearchChange();
                });
            });

        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.ScrollAll;
            // Store the current mouse position
            lastMousePosition = e.GetPosition(scrollView);

            // Capture the mouse to receive the mouse move events even if the mouse leaves the canvas
            canvas.CaptureMouse();
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            // Release the mouse capture
            canvas.ReleaseMouseCapture();
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && canvas.IsMouseCaptured)
            {
                // Calculate the distance moved by the mouse
                Point newMousePosition = e.GetPosition(scrollView);
                double deltaX = newMousePosition.X - lastMousePosition.X;
                double deltaY = newMousePosition.Y - lastMousePosition.Y;


                deltaX *= 1.2d;
                deltaY *= 1.2d;

                var scrollViewer = (ScrollViewer)canvas.Parent;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - (deltaX));
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - (deltaY)); ;

                // Update the last mouse position
                lastMousePosition = newMousePosition;
            }
        }
        private void TreeNodeClick(Node obj)
        {
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (obj.Parent == null)
                {
                    var node = PreviousNodes.LastOrDefault();

                    if (node != null)
                    {
                        DrawSpell(node.Spell, node.SpellLevel);
                        PreviousNodes.RemoveAt(PreviousNodes.Count - 1);

                    }
                }
                else
                {
                    PreviousNodes.Add(Tree.Nodes.First());
                    DrawSpell(obj.Spell, obj.SpellLevel);
                }
            }


            SelectNode(obj);
        }



        public void DrawSpell(SpellRecord spell, SpellLevelRecord level)
        {
            Tree.Draw(spell, level);



            SelectNode(Tree.Nodes.First());
            UpdateDisplayNames();

        }
        public void SelectNode(Node node)
        {
            if (SelectedNode != null)
                SelectedNode.RemoveStroke();

            SelectedNode = node;

            spellName.Content = node.Spell.ToString();

            effects.Items.Clear();
            effectProps.Items.Clear();

            foreach (var effect in node.SpellLevel.Effects)
            {
                effects.Items.Add(effect);
            }
            SelectedNode.SetNodeStroke(Colors.Orange, true);

            gradeSelect.SelectionChanged -= gradeSelect_SelectionChanged;

            gradeSelect.Items.Clear();

            foreach (var spellLevel in node.Spell.Levels)
            {
                gradeSelect.Items.Add(spellLevel);
            }

            gradeSelect.SelectedItem = node.SpellLevel;

            gradeSelect.SelectionChanged += gradeSelect_SelectionChanged;

        }
        private void effects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var n in Tree.Nodes)
            {
                foreach (var line in n.Lines)
                {
                    line.Unselect();
                }
            }


            CurrentEffect = (EffectDice)effects.SelectedItem;

            effectProps.Items.Clear();

            var node = Tree.Nodes.First().FindNode(x => x.Effect == CurrentEffect);

            if (CurrentEffect == null)
            {
                return;
            }
            if (node != null)
            {
                var line = node.Parent.Lines.FirstOrDefault(x => x.Effect == CurrentEffect);
                line.Select();
            }





            effectProps.Items.Add("Effect : " + CurrentEffect.EffectEnum);
            effectProps.Items.Add("Min,Max : (" + CurrentEffect.Min + "," + CurrentEffect.Max + ")");
            effectProps.Items.Add("Value : " + CurrentEffect.Value);
            effectProps.Items.Add("Duration : " + CurrentEffect.Duration);
            effectProps.Items.Add("Delay : " + CurrentEffect.Delay);
            effectProps.Items.Add("TargetMask : " + CurrentEffect.TargetMask);

            string targets = string.Join(",", CurrentEffect.GetTargets());

            if (targets == string.Empty)
            {
                targets = "ALL";
            }
            effectProps.Items.Add("Targets : " + targets);
            effectProps.Items.Add("Triggers : " + CurrentEffect.RawTriggers);
            effectProps.Items.Add("Triggers Enum : " + string.Join(',', CurrentEffect.Triggers));
            effectProps.Items.Add("Raw Zone : " + CurrentEffect.RawZone);
            effectProps.Items.Add("Order : " + CurrentEffect.Order);

            effectProps.Items.Add("Group : " + CurrentEffect.Group);
            effectProps.Items.Add("TargetId : " + CurrentEffect.TargetId);
            effectProps.Items.Add("Dispellable : " + (FightDispellableEnum)CurrentEffect.Dispellable);
            effectProps.Items.Add("Trigger : " + CurrentEffect.Trigger);
            effectProps.Items.Add("Random : " + CurrentEffect.Random);


            switch (CurrentEffect.EffectEnum)
            {
                case EffectsEnum.Effect_Summon:
                case EffectsEnum.Effect_SummonSlave:
                case EffectsEnum.Effect_KillAndSummon:
                case EffectsEnum.Effect_KillAndSummonSlave:
                case EffectsEnum.Effect_SummonsBomb:
                    MonsterRecord monster = MonsterRecord.GetMonsterRecord((short)CurrentEffect.Min);

                    if (monster != null)
                    {
                        effectProps.Items.Add("Summoned : " + monster.Name + " (" + CurrentEffect.Max + ")");

                        var grade = monster.GetGrade((byte)CurrentEffect.Max);

                        SpellLevelRecord level = SpellLevelRecord.GetSpellLevel(grade.StartingSpellLevelId);

                        if (level != null)
                        {
                            SpellRecord record = SpellRecord.GetSpellRecord(level.SpellId);
                            effectProps.Items.Add($"Summon cast : ({record.Id}) {record.Name} ({level.Grade})");
                        }
                    }
                    else
                    {
                        effectProps.Items.Add("Unknown Summon.");
                    }
                    break;
            }


            if (CurrentEffect.EffectEnum == EffectsEnum.Effect_AddState || CurrentEffect.EffectEnum == EffectsEnum.Effect_DispelState)
            {
                var state = SpellStateRecord.GetSpellStateRecord(CurrentEffect.Value);
                effectProps.Items.Add("State : " + state.Name);
            }

            if (CurrentEffect.EffectEnum == EffectsEnum.Effect_SpellBoostBaseDamage)
            {
                effectProps.Items.Add("Boosted Spell : " + SpellRecord.GetSpellRecord((short)CurrentEffect.Min).Name);
            }
            if (CurrentEffect.EffectEnum == EffectsEnum.Effect_RemoveSpellEffects)
            {
                var spell = SpellRecord.GetSpellRecord((short)CurrentEffect.Value);

                if (spell != null)
                {
                    effectProps.Items.Add("Dispelled Spell : " + spell.Name);
                }
                else
                {
                    effectProps.Items.Add("Dispelled Spell : Not found");
                }
            }
        }

        private void spellInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnSearchChange();
        }

        private void OnSearchChange()
        {
            var searchText = search.Text.ToLower();

            var results = SpellRecord.GetSpellRecords().Where(x => x.ToString().ToLower().Contains(searchText));

            DisplaySearchResults(results);
        }

        public void DisplaySearchResults(IEnumerable<SpellRecord> results)
        {
            spells.Items.Clear();

            foreach (var result in results)
            {
                spells.Items.Add(result);
            }
        }

        private void spells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var spell = spells.SelectedItem as SpellRecord;

            if (spell == null)
            {
                return;
            }

            DrawSpell(spell, spell.Levels.First());
        }

        private void UpdateDisplayNames()
        {
            foreach (var element in canvas.Children)
            {
                if (element is TextBlock)
                {
                    var block = (TextBlock)element;

                    if (displayNames.IsChecked.Value)
                    {
                        block.Opacity = 1;
                    }
                    else
                    {
                        block.Opacity = 0;
                    }
                }
            }
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplayNames();
        }

        private void gradeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLevel = (SpellLevelRecord)gradeSelect.SelectedItem;

            if (selectedLevel != null)
            {
                var selectedRecord = SpellRecord.GetSpellRecord(selectedLevel.SpellId);

                DrawSpell(selectedRecord, selectedLevel);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SpellSearch search = new SpellSearch();
            search.Show();
        }
    }
}
