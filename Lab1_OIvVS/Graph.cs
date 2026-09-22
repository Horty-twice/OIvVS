using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Lab1_OIvVS
{
    public class Graph
    {
        public List<Vertex> Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }

        public Graph()
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }

        public void AddVertex(string name, int x, int y)
        {
            if (Vertices.Any(v => v.Name == name))
                throw new Exception("Вершина с таким именем уже существует.");
            if (Vertices.Count >= 10)
                throw new Exception("Не более 10 вершин.");
            Vertices.Add(new Vertex(name, x, y));
        }

        public void RemoveVertex(Vertex v)
        {
            Edges.RemoveAll(e => e.Source == v || e.Target == v);
            Vertices.Remove(v);
        }

        public void AddEdge(Vertex source, Vertex target, double weight)
        {
            if (source == null || target == null)
                throw new Exception("Вершины не существуют.");
            if (weight < 0)
                throw new Exception("Вес не может быть отрицательным.");
            if (Edges.Any(e => e.Source == source && e.Target == target))
                throw new Exception("Такая дуга уже существует.");
            Edges.Add(new Edge(source, target, weight));
        }

        public void UpdateWeight(Vertex source, Vertex target, double newWeight)
        {
            var edge = Edges.FirstOrDefault(e => e.Source == source && e.Target == target);
            if (edge == null)
                throw new Exception("Дуга не найдена.");
            if (newWeight < 0)
                throw new Exception("Вес не может быть отрицательным.");
            edge.Weight = newWeight;
        }

        public void RemoveEdge(Edge edge)
        {
            Edges.Remove(edge);
        }

        public List<Vertex> GetAdjacent(Vertex v)
        {
            return Edges.Where(e => e.Source == v).Select(e => e.Target).ToList();
        }

        public double GetWeight(Vertex source, Vertex target)
        {
            var edge = Edges.FirstOrDefault(e => e.Source == source && e.Target == target);
            return edge == null ? double.PositiveInfinity : edge.Weight;
        }

        private void ResetDijkstra()
        {
            foreach (var v in Vertices)
                v.ResetDijkstra();
        }

        // -------------------- Алгоритм Дейкстры --------------------
        public bool Dijkstra(string startName, string endName, out double distance, out List<Vertex> path, out List<Edge> pathEdges)
        {
            distance = 0;
            path = new List<Vertex>();
            pathEdges = new List<Edge>();

            Vertex start = Vertices.FirstOrDefault(v => v.Name == startName);
            Vertex end = Vertices.FirstOrDefault(v => v.Name == endName);
            if (start == null || end == null)
                throw new Exception("Начальная или конечная вершина не найдена.");

            ResetDijkstra();

            start.Distance = 0;
            Vertex current = start;

            while (true)
            {
                current.IsPermanent = true;
                if (current == end) break;

                foreach (var neighbor in GetAdjacent(current))
                {
                    if (neighbor.IsPermanent) continue;
                    double newDist = current.Distance + GetWeight(current, neighbor);
                    if (newDist < neighbor.Distance)
                    {
                        neighbor.Distance = newDist;
                        neighbor.Previous = current;
                    }
                }

                double minDist = double.PositiveInfinity;
                Vertex next = null;
                foreach (var v in Vertices)
                {
                    if (!v.IsPermanent && v.Distance < minDist)
                    {
                        minDist = v.Distance;
                        next = v;
                    }
                }

                if (next == null) break;
                current = next;
            }

            if (!end.IsPermanent || double.IsInfinity(end.Distance))
            {
                distance = double.PositiveInfinity;
                return false;
            }

            distance = end.Distance;
            Vertex step = end;
            while (step != null)
            {
                path.Insert(0, step);
                step = step.Previous;
            }

            var pathCopy = path;
            for (int i = 0; i < pathCopy.Count - 1; i++)
            {
                var edge = Edges.FirstOrDefault(e => e.Source == pathCopy[i] && e.Target == pathCopy[i + 1]);
                if (edge != null) pathEdges.Add(edge);
            }

            return path.Count > 0 && path[0] == start;
        }

        // -------------------- Дейкстра для всех пар --------------------
        public (double[,] distances, Vertex[,] previous) DijkstraAllPairs()
        {
            int n = Vertices.Count;
            double[,] distances = new double[n, n];
            Vertex[,] previous = new Vertex[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = double.PositiveInfinity;
                    previous[i, j] = null;
                }

            for (int startIdx = 0; startIdx < n; startIdx++)
            {
                Vertex start = Vertices[startIdx];
                ResetDijkstra();
                start.Distance = 0;
                Vertex current = start;

                while (true)
                {
                    current.IsPermanent = true;
                    foreach (var neighbor in GetAdjacent(current))
                    {
                        if (neighbor.IsPermanent) continue;
                        double newDist = current.Distance + GetWeight(current, neighbor);
                        if (newDist < neighbor.Distance)
                        {
                            neighbor.Distance = newDist;
                            neighbor.Previous = current;
                        }
                    }
                    double minDist = double.PositiveInfinity;
                    Vertex next = null;
                    foreach (var v in Vertices)
                        if (!v.IsPermanent && v.Distance < minDist) { minDist = v.Distance; next = v; }
                    if (next == null) break;
                    current = next;
                }

                for (int j = 0; j < n; j++)
                {
                    distances[startIdx, j] = Vertices[j].Distance;
                    previous[startIdx, j] = Vertices[j].Previous;
                }
            }

            return (distances, previous);
        }

        // -------------------- Алгоритм Флойда --------------------
        public (double[,] distances, int[,] next) Floyd()
        {
            int n = Vertices.Count;
            double[,] dist = new double[n, n];
            int[,] next = new int[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == j) { dist[i, j] = 0; next[i, j] = j; }
                    else
                    {
                        double w = GetWeight(Vertices[i], Vertices[j]);
                        if (double.IsInfinity(w)) { dist[i, j] = double.PositiveInfinity; next[i, j] = -1; }
                        else { dist[i, j] = w; next[i, j] = j; }
                    }
                }

            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                {
                    if (double.IsInfinity(dist[i, k])) continue;
                    for (int j = 0; j < n; j++)
                    {
                        if (double.IsInfinity(dist[k, j])) continue;
                        double nd = dist[i, k] + dist[k, j];
                        if (nd < dist[i, j]) { dist[i, j] = nd; next[i, j] = next[i, k]; }
                    }
                }

            for (int i = 0; i < n; i++)
                if (dist[i, i] < 0)
                    throw new Exception("Обнаружен цикл отрицательного веса. Решения нет.");

            return (dist, next);
        }

        public List<Vertex> GetPathFloyd(int startIdx, int endIdx, int[,] next)
        {
            List<Vertex> path = new List<Vertex>();
            if (next[startIdx, endIdx] == -1) return path;
            int current = startIdx;
            path.Add(Vertices[current]);
            while (current != endIdx)
            {
                current = next[current, endIdx];
                if (current == -1) return new List<Vertex>();
                path.Add(Vertices[current]);
            }
            return path;
        }

        public List<Vertex> GetPathDijkstra(int startIdx, int endIdx, Vertex[,] previous)
        {
            List<Vertex> path = new List<Vertex>();
            if (startIdx == endIdx) { path.Add(Vertices[startIdx]); return path; }
            if (previous[startIdx, endIdx] == null) return path;

            Vertex step = Vertices[endIdx];
            while (step != null)
            {
                path.Insert(0, step);
                if (step == Vertices[startIdx]) break;
                int stepIdx = Vertices.IndexOf(step);
                if (stepIdx < 0) break;
                step = previous[startIdx, stepIdx];
            }
            if (path.Count == 0 || path[0] != Vertices[startIdx]) return new List<Vertex>();
            return path;
        }

        // -------------------- Методы маршрутизации --------------------
        public Vertex GetRandomNeighbor(Vertex v, Vertex exclude)
        {
            var neighbors = GetAdjacent(v);
            if (exclude != null) neighbors.Remove(exclude);
            if (neighbors.Count == 0) return null;
            Random rnd = new Random();
            return neighbors[rnd.Next(neighbors.Count)];
        }

        public List<Vertex> GetAllNeighbors(Vertex v, Vertex exclude)
        {
            var neighbors = GetAdjacent(v);
            if (exclude != null) neighbors.Remove(exclude);
            return neighbors;
        }

        public Dictionary<Vertex, RoutingTable> InitRoutingTables()
        {
            var tables = new Dictionary<Vertex, RoutingTable>();
            foreach (var v in Vertices) tables[v] = new RoutingTable(v);
            return tables;
        }

        public void LearnRouteFromPacket(Dictionary<Vertex, RoutingTable> tables, Packet packet)
        {
            if (!packet.IsDelivered()) return;

            var path = packet.Path;
            if (path.Count < 2) return;

            for (int i = 0; i < path.Count - 1; i++)
            {
                Vertex currentNode = path[i];
                Vertex nextHop = path[i + 1];
                int hops = path.Count - 1 - i;
                if (tables.ContainsKey(currentNode))
                    tables[currentNode].Update(packet.Destination, nextHop, hops);
            }
        }

        // --- Отсечение дубликатов ---
        public bool NodeHasSeenPacket(Vertex node, int packetId, Dictionary<Vertex, HashSet<int>> seenPackets)
        {
            if (!seenPackets.ContainsKey(node))
                seenPackets[node] = new HashSet<int>();
            return seenPackets[node].Contains(packetId);
        }

        public void MarkPacketSeen(Vertex node, int packetId, Dictionary<Vertex, HashSet<int>> seenPackets)
        {
            if (!seenPackets.ContainsKey(node))
                seenPackets[node] = new HashSet<int>();
            seenPackets[node].Add(packetId);
        }

        // -------------------- Отрисовка --------------------
        private Point GetPointOnCircle(Point center, Point other, float radius)
        {
            float dx = other.X - center.X;
            float dy = other.Y - center.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            if (dist == 0) return center;
            float ratio = radius / dist;
            return new Point((int)(center.X + dx * ratio), (int)(center.Y + dy * ratio));
        }

        public void Draw(Graphics g, Pen penVertex, Brush brushVertex, Pen penEdge, Font font,
                         Vertex startHighlight = null, Vertex endHighlight = null,
                         List<Vertex> pathVertices = null, List<Edge> pathEdges = null)
        {
            float vertexRadius = 20f;

            foreach (var v in Vertices)
            {
                Rectangle rect = v.GetRectangle();
                Brush brush = brushVertex;
                if (v.IsStart) brush = Brushes.Green;
                else if (v.IsEnd) brush = Brushes.Blue;
                else if (pathVertices != null && pathVertices.Contains(v)) brush = Brushes.Orange;

                g.FillEllipse(brush, rect);
                g.DrawEllipse(penVertex, rect);
                g.DrawString(v.Name, font, Brushes.Black, rect.X + 10, rect.Y + 10);
            }

            foreach (var edge in Edges)
            {
                Point p1 = edge.Source.Location;
                Point p2 = edge.Target.Location;
                Point startPoint = GetPointOnCircle(p1, p2, vertexRadius);
                Point endPoint = GetPointOnCircle(p2, p1, vertexRadius);
                bool inPath = pathEdges != null && pathEdges.Contains(edge);
                Pen pen = inPath ? new Pen(Color.Red, 3) : penEdge;
                g.DrawLine(pen, startPoint, endPoint);
                DrawArrow(g, pen, startPoint, endPoint);
                Point mid = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
                g.DrawString(edge.Weight.ToString(), font, Brushes.Black, mid);
            }
        }

        private void DrawArrow(Graphics g, Pen pen, Point from, Point to)
        {
            float arrowSize = 12f;
            float angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            float offset = 2f;
            PointF tip = new PointF(to.X - offset * (float)Math.Cos(angle), to.Y - offset * (float)Math.Sin(angle));
            PointF p1 = new PointF(tip.X - arrowSize * (float)Math.Cos(angle - (float)Math.PI / 6),
                                   tip.Y - arrowSize * (float)Math.Sin(angle - (float)Math.PI / 6));
            PointF p2 = new PointF(tip.X - arrowSize * (float)Math.Cos(angle + (float)Math.PI / 6),
                                   tip.Y - arrowSize * (float)Math.Sin(angle + (float)Math.PI / 6));
            g.DrawLine(pen, tip, p1);
            g.DrawLine(pen, tip, p2);
        }
    }
}