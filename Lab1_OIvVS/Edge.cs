namespace Lab1_OIvVS
{
    public class Edge
    {
        public Vertex Source { get; set; }//откуда
        public Vertex Target { get; set; }//куда
        public double Weight { get; set; }

        public Edge(Vertex source, Vertex target, double weight)
        {
            Source = source;
            Target = target;
            Weight = weight;
        }
    }
}
