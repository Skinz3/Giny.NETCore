using Giny.IO.DLM;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = SFML.Graphics.Color;

namespace Giny.Rendering.Maps
{
    public class Cell : IComparable<Cell>
    {
        public const uint VerticesCount = 8u;

        private static Color BorderColor = new Color(0, 0, 0, 50);

        public int Id
        {
            get;
            set;
        }

        public bool Walkable
        {
            get;
            set;
        }

        public Vector2f[] VerticalDiagonal
        {
            get
            {
                return new Vector2f[] { Points[1], Points[3] };
            }
        }
        public Vector2f[] HorizontalDiagonal
        {
            get
            {
                return new Vector2f[] { Points[0], Points[2] };
            }
        }
        public Vector2f Center
        {
            get
            {
                return new Vector2f((Points[0].X + Points[2].X) / 2, (Points[1].Y + Points[3].Y) / 2);
            }
        }
        public Vector2f Position
        {
            get
            {
                return Points[0];
            }
        }
        private Vector2f[] Points
        {
            get;
            set;
        }

        public ConvexShape Polygon
        {
            get;
            private set;
        }

        private Map Map
        {
            get;
            set;
        }
        public CellData Data
        {
            get;
            set;
        }

     
        public Cell(Map map, int id)
        {
            Map = map;
            Id = id;
        }

        public void ComputePolygon()
        {
            Polygon = new ConvexShape(4);
            Polygon.FillColor = Color.Transparent;
            Polygon.OutlineThickness = 1f;
            Polygon.OutlineColor = new Color(0, 0, 0, 50);

            for (uint i = 0; i < Points.Length; i++)
            {
                Polygon.SetPoint(i, Points[i]);
            }
        }

        public bool Contains(Vector2f p)
        {
            float xnew, ynew;
            float xold, yold;
            float x1, y1;
            float x2, y2;
            bool inside = false;

            if (Points.Length < 3)
                return false;

            xold = Points[Points.Length - 1].X;
            yold = Points[Points.Length - 1].Y;

            foreach (Vector2f t in Points)
            {
                xnew = t.X;
                ynew = t.Y;

                if (xnew > xold)
                {
                    x1 = xold;
                    x2 = xnew;
                    y1 = yold;
                    y2 = ynew;
                }
                else
                {
                    x1 = xnew;
                    x2 = xold;
                    y1 = ynew;
                    y2 = yold;
                }

                if ((xnew < p.X) == (p.X <= xold) && (p.Y - (long)y1) * (x2 - x1) < (y2 - (long)y1) * (p.X - x1))
                {
                    inside = !inside;
                }
                xold = xnew;
                yold = ynew;
            }
            return inside;
        }

        public void DrawBorders(RenderWindow window)
        {
            window.Draw(Polygon);
        }

        public void SetPoints(Vector2f[] points)
        {
            this.Points = points;
        }
        public Vertex[] GetLineVertices()
        {
            List<Vertex> result = new List<Vertex>();

            for (int i = 0; i < Points.Length - 1; i++)
            {
                result.Add(new Vertex(Points[i], BorderColor));
                result.Add(new Vertex(Points[i + 1], BorderColor));
            }

            result.Add(new Vertex(Points[Points.Length - 1], BorderColor));
            result.Add(new Vertex(Points[0], BorderColor));

            return result.ToArray();
        }


        public int CompareTo(Cell other)
        {
            return Id - other.Id;
        }
    }
}
