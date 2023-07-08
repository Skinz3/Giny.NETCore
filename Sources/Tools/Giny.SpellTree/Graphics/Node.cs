using Giny.Core.Extensions;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Giny.SpellTree.Graphics
{
    public class Node
    {
        public event Action<Node> Clicked;

        public const double DistanceXBetweenNodes = 300d;

        public const double NodeRadius = 40;

        public const double ChipNodeRadius = 15;

        public const double LinkStrokeThickness = 5d;

        public static double InitialDistanceYBetweenNode = 700d;

        public static Color NodeDefaultColor = Color.FromArgb(255, 54, 54, 54);

        public const double StrokeSize = 5d;

        public const double LineOpacityNormal = 0.55d;

        public const double LineOpacityTriggers = 0.3d;

        public Ellipse Circle
        {
            get;
            private set;
        }
        public Ellipse ChipCircle
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
        public List<Line> Lines
        {
            get;
            set;
        }


        public Color? NodeColor
        {
            get;
            private set;
        }
        public Color? ChipColor
        {
            get;
            private set;
        }

        public Color? StrokeColor
        {
            get;
            private set;
        }
        public Popup Popup
        {
            get;
            set;
        }
        public Image Image
        {
            get;
            set;
        }


        public int DeepLevel
        {
            get;
            private set;
        }
        public Node(int deepLevel, Node parent, SpellLevelRecord spellLevel, SpellRecord spellRecord, EffectDice effect, double x, double y)
        {
            this.DeepLevel = deepLevel;
            Parent = parent;
            Parent?.Childs.Add(this);
            Lines = new List<Line>();
            Childs = new List<Node>();
            SpellLevel = spellLevel;
            Spell = spellRecord;
            Popup = new Popup();

            Effect = effect;
            X = x;
            Y = y;

            if (Effect != null && Effect.Random != 0)
            {
                SetNodeColor(Color.FromArgb(255, 211, 220, 232));
            }


        }

        public string GetNodeDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Spell : " + Spell.ToString());
            sb.AppendLine("Level : " + SpellLevel.ToString());

            if (Spell.Description != string.Empty)
            {
                sb.AppendLine();
                sb.AppendLine(Spell.Description.AddLineBreaks(' ',7));
            }


            return sb.ToString();
        }
        public string GetLinkDescription(Node child)
        {
            StringBuilder sb = new StringBuilder();
            string triggers = string.Join(',', child.Effect.Triggers).AddLineBreaks(',', 5);
            sb.AppendLine("Triggers : " + triggers);
            sb.AppendLine("Targets : " + child.Effect.GetTargetsString().AddLineBreaks(',',3));

            if (child.Effect.Triggers.Any(x => x.Type == TriggerTypeEnum.Unknown))
            {
                sb.AppendLine("Raw Triggers : " + child.Effect.RawTriggers);
            }

            sb.AppendLine("Zone : " + child.Effect.RawZone);

            return sb.ToString();
        }
        private void OnNodeMouseEnter(Canvas canvas, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            var mp = e.GetPosition(canvas);
            Popup.Open(canvas, GetNodeDescription(), mp.X, mp.Y + 20);

            SetNodeStroke(Colors.Orange, false);

        }
        private void OnNodeMouseLeave(Canvas canvas)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            Popup.Close(canvas);
            ResetStrokeColor();
        }

        public void SetNodeColor(Color? color)
        {
            this.NodeColor = color;

            if (Circle != null)
            {
                if (!color.HasValue)
                {
                    Circle.Fill = new SolidColorBrush(NodeDefaultColor);
                }
                else
                {
                    Circle.Fill = new SolidColorBrush(color.Value);
                }
            }
        }
        public void SetChipColor(Color? color)
        {
            this.ChipColor = color;

            if (ChipCircle != null)
            {
                if (!color.HasValue)
                {
                    ChipCircle.Fill = new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    ChipCircle.Fill = new SolidColorBrush(color.Value);
                }
            }
        }
        public void SetNodeStroke(Color color, bool keep)
        {
            if (keep)
            {
                this.StrokeColor = color;
            }
            Circle.Stroke = new SolidColorBrush(color);
            Circle.StrokeThickness = StrokeSize;
        }
        public void RemoveStroke()
        {
            Circle.StrokeThickness = 0d;
            StrokeColor = null;
            return;
        }
        public void ResetStrokeColor()
        {
            if (!StrokeColor.HasValue)
            {
                Circle.StrokeThickness = 0d;
                return;
            }
            Circle.Stroke = new SolidColorBrush(StrokeColor.Value);
            Circle.StrokeThickness = StrokeSize;
        }

        public void Draw(Canvas canvas)
        {
            this.Circle = CanvasDraw.Circle(X, Y, NodeRadius, new SolidColorBrush(NodeDefaultColor), canvas);

            if (NodeColor != null)
            {
                Circle.Fill = new SolidColorBrush(NodeColor.Value);
            }


            this.Label = CanvasDraw.Text(Spell.Name + " (" + Spell.Id + "," + SpellLevel.Grade + ")", X, Y - NodeRadius, new SolidColorBrush(Color.FromArgb(255, 100, 100, 100)), canvas, true);


            foreach (var child in Childs)
            {
                var line = CanvasDraw.Line(X, Y, child.X, child.Y, new SolidColorBrush(Colors.Black), canvas, LineOpacityNormal, LinkStrokeThickness); ;

                if (child.Effect.RawTriggers != "I")
                {
                    line.Opacity = LineOpacityTriggers;
                    line.Stroke = new SolidColorBrush(Colors.Black);
                }
                Lines.Add(line);
            }

            this.ChipCircle = CanvasDraw.Circle(X, Y, ChipNodeRadius, new SolidColorBrush(!ChipColor.HasValue ? Colors.Transparent : ChipColor.Value), canvas);
            this.ChipCircle.IsHitTestVisible = false;

            int index = 0;

            Circle.MouseLeftButtonDown += (o, e) =>
            {
                Clicked?.Invoke(this);
            };

            Circle.MouseEnter += (o, e) =>
            {
                OnNodeMouseEnter(canvas, e);
            };

            Circle.MouseLeave += (o, e) =>
            {
                OnNodeMouseLeave(canvas);
            };

            foreach (var line in Lines)
            {
                var child = Childs[index];

                line.MouseEnter += ((object sender, MouseEventArgs e) =>
                {
                    var mp = e.GetPosition(canvas);
                    Mouse.OverrideCursor = Cursors.Hand;
                    Popup.Open(canvas, GetLinkDescription(child), mp.X, mp.Y + 20);
                    line.Opacity = 1;
                    line.Stroke = Brushes.Orange;
                    line.StrokeThickness = LinkStrokeThickness + 3;
                });

                line.MouseLeave += ((object sender, MouseEventArgs e) =>
                {
                    Popup.Close(canvas);
                    Mouse.OverrideCursor = Cursors.Arrow;
                    line.Opacity = child.Effect.RawTriggers != "I" ? LineOpacityTriggers : LineOpacityNormal;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = LinkStrokeThickness;
                });

                index++;
            }





        }
        /// <summary>
        /// Not working
        /// </summary>
        /// <param name="amount"></param>
        public void GrowChildDistance(double amount)
        {


            if (Childs.Count == 0)
            {
                return;
            }





            var minY = Childs.Min(x => x.Y);
            var maxY = Childs.Max(x => x.Y);

            minY -= amount;
            maxY += amount;

            var gap = (maxY - minY) / Childs.Count;

            var currentY = minY;

            foreach (var child in Childs.OrderBy(x => x.Y))
            {
                child.Y = currentY;
                currentY += gap;

                child.GrowChildDistance(amount);

            }

        }

        public Node? FindNode(Func<Node, bool> predicate)
        {
            if (predicate(this))
            {
                return this;
            }
            foreach (var child in Childs)
            {
                if (predicate(child))
                {
                    return child;
                }
            }

            return null;
        }


    }
}
