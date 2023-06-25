using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Maps.Paths
{
    public class Pathfinding 
    {
        private Dictionary<short, Node> m_nodes;
        private Node m_start;
        private Node m_end;

        private SortedNodeList<Node> m_openList;

        private NodeList<Node> m_closeList;

        public Pathfinding(MapRecord map)
        {
            this.m_nodes = new Dictionary<short, Node>();

            foreach (var point in MapPoint.GetOrthogonalGridReference())
            {
                var node = new Node(point);
                node.Walkable = map.IsCellWalkable(point.CellId);
                m_nodes.Add(node.CellId, node);
            }

            this.m_openList = new SortedNodeList<Node>();
            this.m_closeList = new NodeList<Node>();
        }

        public List<short> FindPath(short start, short target)
        {
            this.m_start = this.m_nodes[start];

            this.m_end = this.m_nodes[target];

            this.m_end.Walkable = true;

            this.m_openList.Add(this.m_start);

            while (this.m_openList.Count > 0)
            {
                var bestCell = this.m_openList.RemoveFirst();
                if (bestCell.CellId == this.m_end.CellId)
                {
                    var sol = new List<short>();
                    while (bestCell.Parent != null && bestCell != this.m_start)
                    {
                        sol.Add(bestCell.CellId);
                        bestCell = bestCell.Parent;
                    }
                    sol.Reverse();

                    this.m_end.Walkable = false;
                    return sol;
                }
                this.m_closeList.Add(bestCell);
                this.AddToOpen(bestCell, this.GetNeighbors(bestCell));
            }
            this.m_end.Walkable = false;
            return null;
        }

        private List<Node> GetNeighbors(Node node)
        {
            var nodes = new List<Node>();

            foreach (var n in node.Neighbors)
            {
                var cell = this.m_nodes[n.CellId];

                if (cell.Walkable)
                {
                    if (cell.Parent == null)
                        cell.Parent = node;

                    nodes.Add(cell);
                }
            }


            return nodes;
        }

        private Node GetBestNode()
        {
            this.m_openList.OrderBy(x => x.H);
            return this.m_openList.First();
        }
        public void PutEntities(IEnumerable<Fighter> fighters)
        {
            foreach (var fighter in fighters)
            {
                var node = this.m_nodes[fighter.Cell.Id];
                if (node != this.m_start)
                    node.Walkable = false;
            }
        }


        private void AddToOpen(Node current, IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                if (!this.m_openList.Contains(node))
                {
                    if (!this.m_closeList.Contains(node))
                        this.m_openList.AddDichotomic(node);
                }
                else
                {
                    if (node.CostWillBe() < this.m_openList[node].G)
                        node.Parent = current;
                }
            }
        }
    }
}
