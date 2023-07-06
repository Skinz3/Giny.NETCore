using Giny.World.Managers.Effects;
using Giny.World.Records.Spells;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Giny.SpellTree
{
    public class Node
    {
        public const double DistanceXBetweenNodes = 300d;

        public const double NodeRadius = 30;

        public Ellipse Circle
        {
            get;
            private set;
        }
        public TextBlock Label
        {
            get;
            private set;
        }
        public SpellLevelRecord SpellLevel
        {
            get;
            private set;
        }
        public SpellRecord Spell
        {
            get;
            private set;
        }
        public EffectDice Effect
        {
            get;
            private set;
        }
        public double X
        {
            get;
            private set;
        }
        public double Y
        {
            get;
            private set;
        }
        public Node Parent
        {
            get;
            set;
        }
        public List<Node> Childs
        {
            get;
            set;
        }

        public Node(Node parent, SpellLevelRecord spellLevel, SpellRecord spellRecord, EffectDice effect, double x, double y)
        {
            Parent = parent;
            Parent?.Childs.Add(this);

            Childs = new List<Node>();
            SpellLevel = spellLevel;
            Spell = spellRecord;
            Effect = effect;
            X = x;
            Y = y;
        }

        public void Draw(Canvas canvas, double yCurrentNode)
        {
            CanvasDraw.Line(X - DistanceXBetweenNodes, Y, X, yCurrentNode, Brushes.Black, canvas, 0.2d);
            var circle = CanvasDraw.Circle(X, yCurrentNode, NodeRadius, new SolidColorBrush(Color.FromArgb(255, 54, 54, 54)), canvas);
            CanvasDraw.Text(Spell.Name + " Grade " + SpellLevel.Grade, X + NodeRadius, yCurrentNode - NodeRadius, Brushes.Black, canvas, false);
        }

    }

    public class TreeDrawer
    {


        private const double InitialDistanceYBetweenNode = 500d;



        private static Brush[] LinkBrushes = new Brush[]
        {
            Brushes.Blue,
            Brushes.Violet,
            Brushes.Red,
            Brushes.Yellow,
            Brushes.Pink,
            Brushes.Magenta,
            Brushes.Aqua,
            Brushes.Aquamarine,
            Brushes.Chartreuse,
            Brushes.Chocolate,
        };
        private Canvas Canvas
        {
            get;
            set;
        }

        private List<Node> Nodes
        {
            get;
            set;
        } = new List<Node>();

        public TreeDrawer(Canvas canvas)
        {
            this.Canvas = canvas;
        }

        public void Draw(SpellRecord record)
        {
            var level = record.Levels[0];

            Canvas.Children.Clear();
            Nodes.Clear();

            double xCurrent = 50;
            double yCurrent = Canvas.ActualHeight / 2;

            Node node = new Node(null, level, record, null, xCurrent, yCurrent);

            Nodes.Add(node);
            node.Draw(Canvas, yCurrent);


            DrawTreeLevel(null, xCurrent, yCurrent, level, InitialDistanceYBetweenNode);

        }

        private Node CreateNode(Node parent, double xCurrent, double yCurrent, double yCurrentNode, EffectDice effect, SpellRecord targetSpell, SpellLevelRecord level)
        {
            var node = new Node(parent, level, targetSpell, effect, xCurrent, yCurrent);
            node.Draw(Canvas, yCurrentNode);
            Nodes.Add(node);
            return node;

        }
        private void DrawTreeLevel(Node parent, double xCurrent, double yCurrent, SpellLevelRecord level, double yOffset)
        {
            xCurrent += Node.DistanceXBetweenNodes;

            var castEffects = level.Effects.Where(x => x.IsSpellCastEffect()).Select(x => (EffectDice)x).ToArray();


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

                var targetLevel = targetSpell.GetLevel((byte)effect.Max);

                var newNode = CreateNode(parent, xCurrent, yCurrent, yCurrentNode, effect, targetSpell, targetLevel);

                DrawTreeLevel(newNode, xCurrent, yCurrentNode, targetLevel, yOffset / 2);

                yCurrentNode += gapBetweenNode;

            }

        }
    }
}
