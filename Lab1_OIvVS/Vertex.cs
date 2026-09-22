namespace Lab1_OIvVS
{
    public class Vertex
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }

        //поля для алгоритма Дейкстры
        public double Distance { get; set; }//текущая пометка (расстояние)
        public bool IsPermanent { get; set; }//постоянная ли пометка
        public Vertex Previous { get; set; }//предыдущая вершина на пути

        public Vertex(string name, int x, int y)
        {
            Name = name;
            Location = new Point(x, y);
            IsStart = false;
            IsEnd = false;
            Distance = double.PositiveInfinity;
            IsPermanent = false;
            Previous = null;
        }

        //получить прямоугольник для клика/рисования (размер 40x40)
        public Rectangle GetRectangle()
        {
            return new Rectangle(Location.X - 20, Location.Y - 20, 40, 40);
        }

        //сброс пометок перед новым запуском алгоритма
        public void ResetDijkstra()
        {
            Distance = double.PositiveInfinity;
            IsPermanent = false;
            Previous = null;
        }
    }
}
