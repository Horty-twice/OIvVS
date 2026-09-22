using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab1_OIvVS
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public void ShowResult(double distance, List<Vertex> path)
        {
            if (double.IsInfinity(distance) || path == null || path.Count == 0)
            {
                labelResult.Text = "Путь не найден!";
                return;
            }

            string pathStr = string.Join(" → ", path.ConvertAll(v => v.Name));
            labelResult.Text = $"Длина кратчайшего пути: {distance}\n\nПуть: {pathStr}";
        }
    }
}