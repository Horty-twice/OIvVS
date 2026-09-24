using System.Collections.Generic;

namespace Lab1_OIvVS
{
    // Одна запись в таблице маршрутизации
    public class RoutingEntry
    {
        public Vertex NextNode { get; set; }
        public Vertex PreviousNode { get; set; }   // <-- откуда пришёл
        public int Hops { get; set; }

        public RoutingEntry(Vertex nextNode, int hops, Vertex previousNode = null)
        {
            NextNode = nextNode;
            Hops = hops;
            PreviousNode = previousNode;
        }
    }

    public class RoutingTable
    {
        public Vertex Node { get; set; }
        public Dictionary<Vertex, RoutingEntry> Entries { get; set; }

        public RoutingTable(Vertex node)
        {
            Node = node;
            Entries = new Dictionary<Vertex, RoutingEntry>();
        }

        public bool Update(Vertex destination, Vertex nextNode, int hops, Vertex previousNode = null)
        {
            // Убираем проверку destination == Node — узел тоже учится

            if (!Entries.ContainsKey(destination))
            {
                Entries[destination] = new RoutingEntry(nextNode, hops, previousNode);
                return true;
            }

            if (hops < Entries[destination].Hops)
            {
                Entries[destination] = new RoutingEntry(nextNode, hops, previousNode);
                return true;
            }

            return false;
        }

        public Vertex GetNext(Vertex destination)
        {
            if (Entries.ContainsKey(destination)) return Entries[destination].NextNode;
            return null;
        }

        public int GetHops(Vertex destination)
        {
            if (Entries.ContainsKey(destination)) return Entries[destination].Hops;
            return int.MaxValue;
        }

        public bool HasRoute(Vertex destination) => Entries.ContainsKey(destination);
        public void Clear() => Entries.Clear();
    }
}