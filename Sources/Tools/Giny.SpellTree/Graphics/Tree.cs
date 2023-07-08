using Giny.World.Managers.Effects;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Giny.SpellTree.Graphics
{
    public class Tree
    {
        private Canvas Canvas
        {
            get;
            set;
        }

        public List<Node> Nodes
        {
            get;
            private set;
        } = new List<Node>();

        public Tree(Canvas canvas)
        {
            this.Canvas = canvas;
        }

        public event Action<Node> NodeClick;

        public void Draw(SpellRecord record, SpellLevelRecord level)
        {
            Canvas.Children.Clear();

            foreach (var n in Nodes)
            {
                n.Clicked -= NodeClick;
            }

            Nodes.Clear();

            double xCurrent = Canvas.ActualWidth / 2d;
            double yCurrent = Canvas.ActualHeight / 2d;


            Node node = new Node(0, null, level, record, null, xCurrent, yCurrent);


            Nodes.Add(node);

            BuildTreeLevel(1, node, xCurrent, yCurrent, level, Node.InitialDistanceYBetweenNode);

            foreach (var n in Nodes)
            {
                n.Draw(Canvas);
            }

            var scrollViewer = (ScrollViewer)Canvas.Parent;

            double contentHeight = scrollViewer.ExtentHeight;
            double viewportHeight = scrollViewer.ViewportHeight;
            double middleOffset = (contentHeight - viewportHeight) / 2;

            scrollViewer.ScrollToVerticalOffset(middleOffset);
            scrollViewer.ScrollToHorizontalOffset(Canvas.ActualWidth / 2 - 200);

            foreach (var n in Nodes)
            {
                n.Clicked += NodeClick;
            }


            ReduceCircles();

        }
        private void ReduceNodeCircle(Node node)
        {
            node.Circle.Width = Node.NodeRadius / 2;
            node.Circle.Height = Node.NodeRadius / 2;

            Canvas.SetLeft(node.Circle, node.X - node.Circle.Width / 2);
            Canvas.SetTop(node.Circle, node.Y - node.Circle.Height / 2);

            node.ChipCircle.Width = Node.ChipNodeRadius / 2;
            node.ChipCircle.Height = Node.ChipNodeRadius / 2;

            Canvas.SetLeft(node.ChipCircle, node.X - node.ChipCircle.Width / 2);
            Canvas.SetTop(node.ChipCircle, node.Y - node.ChipCircle.Height / 2);

            node.Label.Visibility = System.Windows.Visibility.Hidden;

        }
        private void ReduceCircles()
        {
            var maxLevel = Nodes.Max(x => x.DeepLevel);

            for (int i = 0; i <= maxLevel; i++)
            {
                var levelNodes = Nodes.Where(x => x.DeepLevel == i).ToArray();

                if (levelNodes.Length >= 2)
                {
                    for (int y = 0; y < levelNodes.Length - 1; y += 1)
                    {
                        var n1 = levelNodes[y];
                        var n2 = levelNodes[y + 1];

                        var distance = Math.Abs(n1.Y - n2.Y);

                        if (distance < Node.NodeRadius)
                        {
                            ReduceNodeCircle(n1);
                            ReduceNodeCircle(n2);
                        }
                    }

                }


            }
        }

        private Node CreateNode(int deepLevel, Node parent, double x, double y, EffectDice effect, SpellRecord targetSpell, SpellLevelRecord level)
        {

            if (Nodes.Count > 100)
            {
                return null;
            }



            if (parent.Effect == effect)
            {

                parent.SetChipColor(Colors.Red);
                return null;
            }

            var similars = Nodes.Where(x => x.Effect == effect && x.SpellLevel == level);

            if (similars.Count() > 0)
            {

                var n2 = new Node(deepLevel, parent, level, targetSpell, effect, x, y);
                n2.SetChipColor(Colors.Snow);
                Nodes.Add(n2);

                foreach (var similar in similars)
                {
                    similar.SetChipColor(Colors.Snow);
                }

                var sources = Nodes.Where(x => x.SpellLevel.Grade == effect.Max && x.Spell.Id == effect.Min);

                foreach (var source in sources)
                {
                    if (source != null && source.Parent != null)
                    {
                        source.Parent.SetChipColor(Colors.CornflowerBlue);
                    }
                }


                return null;
            }

            var node = new Node(deepLevel, parent, level, targetSpell, effect, x, y);

            Nodes.Add(node);
            return node;

        }
        private void BuildTreeLevel(int deepLevel, Node parent, double xCurrent, double yCurrent, SpellLevelRecord level, double yOffset)
        {
            xCurrent += Node.DistanceXBetweenNodes;

            var castEffects = level.Effects.Where(x => x.IsSpellCastEffect()).Select(x => (EffectDice)x).ToArray();

            if (castEffects.Length == 0)
            {
                return;
            }
            var minY = yCurrent;
            var maxY = yCurrent;

            if (castEffects.Length > 1)
            {
                minY = yCurrent - (yOffset / 2);
                maxY = yCurrent + (yOffset / 2);
            }

            double yCurrentNode = minY;

            double gapBetweenNode = (maxY - minY) / (castEffects.Length - 1);

            foreach (var effect in castEffects)
            {
                var targetSpell = SpellRecord.GetSpellRecord((short)effect.Min);

                if (targetSpell != null)
                {
                    var targetLevel = targetSpell.GetLevel((byte)effect.Max);

                    var newNode = CreateNode(deepLevel, parent, xCurrent, yCurrentNode, effect, targetSpell, targetLevel);

                    if (newNode == null)
                    {
                        break;
                    }

                  
                    BuildTreeLevel(deepLevel + 1, newNode, xCurrent, yCurrentNode, targetLevel, yOffset / castEffects.Length);

                   
                    yCurrentNode += gapBetweenNode;
                }


            }

        }


    }
}
