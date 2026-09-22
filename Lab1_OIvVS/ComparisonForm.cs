using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1_OIvVS
{
    public partial class ComparisonForm : Form
    {
        public ComparisonForm()
        {
            InitializeComponent();
        }

        public void ShowComparison(
    List<Tuple<string, string, string, double>> dijkstraResults, double dijkstraTime,
    List<Tuple<string, string, string, double>> floydResults, double floydTime,
    int n, int edgeCount)
        {
            // Заголовок формы
            this.Text = $"Сравнение алгоритмов ({n} вершин, {edgeCount} дуг)";

            // Заполняем таблицу Дейкстры
            dataGridViewDijkstra.Rows.Clear();
            foreach (var r in dijkstraResults)
            {
                string pathStr = r.Item3;
                string distStr = double.IsInfinity(r.Item4) ? "∞" : r.Item4.ToString();
                dataGridViewDijkstra.Rows.Add(r.Item1, r.Item2, pathStr, distStr);
            }

            // Заполняем таблицу Флойда
            dataGridViewFloyd.Rows.Clear();
            foreach (var r in floydResults)
            {
                string pathStr = r.Item3;
                string distStr = double.IsInfinity(r.Item4) ? "∞" : r.Item4.ToString();
                dataGridViewFloyd.Rows.Add(r.Item1, r.Item2, pathStr, distStr);
            }

            // Время
            labelDijkstraTime.Text = $"Дейкстра: {dijkstraTime:F3} мс";
            labelFloydTime.Text = $"Флойд: {floydTime:F3} мс";

            // Количество запусков с подписями
            labelDijkstraRuns.Text = $"Дейкстра: {n * (n - 1)}";
            labelFloydRuns.Text = $"Флойд: 1";
        }
    }
}