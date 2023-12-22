using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Maps.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Giny.SpellTree.Graphics
{
    public class NodeLine
    {
        public const double LinkStrokeThickness = 5d;

        public const double LineOpacityNormal = 0.6d;

        public event Action<MouseEventArgs> OnMouseEnter;

        public event Action<MouseEventArgs> OnMouseLeave;

        private Line Line
        {
            get;
            set;
        }

        private Node Parent
        {
            get;
            set;
        }
        private Node Child
        {
            get;
            set;
        }
        public EffectDice Effect => Child.Effect;

        public NodeLine(Node parent, Node child)
        {
            this.Parent = parent;
            this.Child = child;

        }

        public void Draw(Canvas canvas)
        {


            Line = CanvasDraw.Line(Parent.X, Parent.Y, Child.X, Child.Y, new SolidColorBrush(GetBaseColor()), canvas, LineOpacityNormal, LinkStrokeThickness); ;

            Line.MouseEnter += MouseEnter;
            Line.MouseLeave += MouseLeave;


        }

        private Color GetBaseColor()
        {
            if (Child.Effect.RawTriggers != "I")
            {
                return Colors.CornflowerBlue;

            }
            else
            {
                return Colors.Black;
            }
            /*
            if (Child.Effect.EffectEnum == EffectsEnum.Effect_Rune)
            {
                color = Colors.BlueViolet;
            }
            else if (Child.Effect.EffectEnum == EffectsEnum.Effect_Trap)
            {
                color = Colors.Green;
            }
            else if (Child.Effect.EffectEnum == EffectsEnum.Effect_TurnBeginGlyph ||
                Child.Effect.EffectEnum == EffectsEnum.Effect_TurnEndGlyph ||
                Child.Effect.EffectEnum == EffectsEnum.Effect_GlyphAura
                || Child.Effect.EffectEnum == EffectsEnum.Effect_Glyph_CastingSpellImmediate)
            {
                color = Colors.Red;
            }

            return color; */
        }
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            OnMouseEnter?.Invoke(e);
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            OnMouseLeave?.Invoke(e);
        }

        public void Select()
        {
            Line.Opacity = 1;
            Line.Stroke = Brushes.Orange;
            Line.StrokeThickness = LinkStrokeThickness + 3;
        }
        public void Unselect()
        {
            Line.Opacity = LineOpacityNormal;
            Line.Stroke = new SolidColorBrush(GetBaseColor());
            Line.StrokeThickness = LinkStrokeThickness;
        }

    }
}
