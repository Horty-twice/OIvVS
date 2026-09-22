using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab1_OIvVS
{
    public enum PacketStatus
    {
        InTransit,
        Delivered,
        Duplicate,   // копия, дошедшая после первой
        Expired,
        Dropped      // отброшен по пути (узел уже видел ID)
    }

    public class Packet
    {
        private static int _nextId = 0;

        public int Id { get; private set; }
        public Vertex Source { get; set; }
        public Vertex Destination { get; set; }
        public int Size { get; set; }
        public Vertex CurrentNode { get; set; }
        public Vertex NextNode { get; set; }
        public List<Vertex> Path { get; set; }
        public PacketStatus Status { get; set; }
        public double TimeToLive { get; set; }
        public double TimeSpent { get; set; }
        public double CurrentEdgeWeight { get; set; }
        public double Progress { get; set; }
        public int ParentId { get; set; }
        public int Depth { get; set; }
        public Color Color { get; set; }
        public PointF Position { get; set; }
        public List<Vertex> PrecomputedRoute { get; set; }
        public int RouteIndex { get; set; }

        // Основной конструктор (корневой пакет)
        public Packet(Vertex source, Vertex destination, int size, int parentId = -1, int depth = 0)
        {
            Id = _nextId++;
            Source = source;
            Destination = destination;
            Size = size;
            CurrentNode = source;
            NextNode = null;
            Path = new List<Vertex> { source };
            Status = PacketStatus.InTransit;
            TimeToLive = 10.0;
            TimeSpent = 0;
            CurrentEdgeWeight = 0;
            Progress = 0;
            ParentId = parentId;
            Depth = depth;
            Position = new PointF(source.Location.X, source.Location.Y);
            Color = Color.Red;
            PrecomputedRoute = null;
            RouteIndex = 0;
        }

        // Конструктор копии (для лавинной): тот же ID
        public Packet(Packet original, Vertex nextNode)
        {
            Id = original.Id;                  // сохраняем ID
            Source = original.Source;
            Destination = original.Destination;
            Size = original.Size;
            CurrentNode = original.CurrentNode;
            NextNode = nextNode;
            Path = new List<Vertex>(original.Path);
            Status = PacketStatus.InTransit;
            TimeToLive = original.TimeToLive;
            TimeSpent = original.TimeSpent;
            CurrentEdgeWeight = 0;             // установит вызывающий код
            Progress = 0;
            ParentId = original.Id;
            Depth = original.Depth + 1;
            Position = new PointF(original.CurrentNode.Location.X, original.CurrentNode.Location.Y);
            Color = original.Color;
            PrecomputedRoute = null;
            RouteIndex = 0;
        }

        public static void ResetIdCounter()
        {
            _nextId = 0;
        }

        public bool IsActive() => Status == PacketStatus.InTransit;
        public bool IsDelivered() => Status == PacketStatus.Delivered;
        public bool IsDuplicate() => Status == PacketStatus.Duplicate;
        public bool IsExpired() => Status == PacketStatus.Expired;
        public bool IsDropped() => Status == PacketStatus.Dropped;

        public string GetPathString()
        {
            return string.Join(" → ", Path.ConvertAll(v => v.Name));
        }
    }
}