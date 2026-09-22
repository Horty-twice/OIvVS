using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lab1_OIvVS
{
    public partial class RoutingTablesForm : Form
    {
        private Dictionary<Vertex, RoutingTable> tables;
        private Graph graph;

        public RoutingTablesForm()
        {
            InitializeComponent();
        }

        // Инициализация: сохраняем ссылки на граф и таблицы
        public void Setup(Graph graph, Dictionary<Vertex, RoutingTable> tables)
        {
            this.graph = graph;
            this.tables = tables;
            RefreshNodeList();
        }

        // Обновить список узлов в ComboBox
        public void RefreshNodeList()
        {
            if (graph == null) return;

            string selectedName = comboBoxNodes.SelectedItem?.ToString();
            comboBoxNodes.Items.Clear();
            foreach (var v in graph.Vertices)
            {
                comboBoxNodes.Items.Add(v.Name);
            }
            if (comboBoxNodes.Items.Count > 0)
            {
                if (selectedName != null && comboBoxNodes.Items.Contains(selectedName))
                    comboBoxNodes.SelectedItem = selectedName;
                else
                    comboBoxNodes.SelectedIndex = 0;
            }
        }

        // Обновить таблицу для выбранного узла
        public void RefreshTable()
        {
            dataGridViewTable.Rows.Clear();
            if (graph == null || tables == null) return;
            if (comboBoxNodes.SelectedIndex < 0) return;

            string nodeName = comboBoxNodes.SelectedItem.ToString();
            Vertex node = graph.Vertices.FirstOrDefault(v => v.Name == nodeName);
            if (node == null) return;

            if (!tables.ContainsKey(node)) return;
            var table = tables[node];

            // Заголовок
            labelTitle.Text = $"Таблица маршрутизации узла {node.Name}";

            // Заполняем строки
            foreach (var kvp in table.Entries.OrderBy(e => e.Key.Name))
            {
                Vertex destination = kvp.Key;
                RoutingEntry entry = kvp.Value;
                dataGridViewTable.Rows.Add(destination.Name, entry.NextNode.Name, entry.Hops);
            }
        }

        private void comboBoxNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTable();
        }
    }
}