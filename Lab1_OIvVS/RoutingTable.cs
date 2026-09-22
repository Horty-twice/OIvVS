using System.Collections.Generic;

namespace Lab1_OIvVS
{
    // Одна запись в таблице маршрутизации
    public class RoutingEntry
    {
        public Vertex NextNode { get; set; } // следующий узел
        public int Hops { get; set; }        // счётчик (сколько узлов до цели)

        public RoutingEntry(Vertex nextNode, int hops)
        {
            NextNode = nextNode;
            Hops = hops;
        }
    }

    // Таблица маршрутизации одного узла
    public class RoutingTable
    {
        public Vertex Node { get; set; }
        public Dictionary<Vertex, RoutingEntry> Entries { get; set; }

        public RoutingTable(Vertex node)
        {
            Node = node;
            Entries = new Dictionary<Vertex, RoutingEntry>();
        }

        // Обновить запись (только если новый маршрут короче или запись отсутствует)
        public bool Update(Vertex destination, Vertex nextNode, int hops)
        {
            if (destination == Node) return false; // до себя не нужно

            if (!Entries.ContainsKey(destination))
            {
                Entries[destination] = new RoutingEntry(nextNode, hops);
                return true;
            }

            if (hops < Entries[destination].Hops)
            {
                Entries[destination] = new RoutingEntry(nextNode, hops);
                return true;
            }

            return false;
        }

        public Vertex GetNext(Vertex destination)
        {
            if (Entries.ContainsKey(destination))
                return Entries[destination].NextNode;
            return null;
        }

        public int GetHops(Vertex destination)
        {
            if (Entries.ContainsKey(destination))
                return Entries[destination].Hops;
            return int.MaxValue;
        }

        public bool HasRoute(Vertex destination)
        {
            return Entries.ContainsKey(destination);
        }

        public void Clear()
        {
            Entries.Clear();
        }
    }
}