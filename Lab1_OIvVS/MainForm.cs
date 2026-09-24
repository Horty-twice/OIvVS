using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab1_OIvVS
{
    public partial class MainForm : Form
    {
        private bool sharedRouteNotYetSet = false;  // true, если виртуальный "по опыту" и маршрут ещё не зафиксирован
        private HashSet<int> deliveredIds = new HashSet<int>();
        private Dictionary<Vertex, HashSet<int>> seenPackets = new Dictionary<Vertex, HashSet<int>>();

        // --- Основные поля ---
        private Graph graph;
        private Vertex selectedVertex;
        private Edge selectedEdge;
        private Vertex movingVertex;
        private bool isDragging = false;
        private List<Vertex> pathVertices;
        private List<Edge> pathEdgesList;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
        private int selectedMatrixRowIndex = -1;
        private int selectedMatrixColumnIndex = -1;
        private List<string> logEntries = new List<string>();

        // --- Поля для маршрутизации ---
        private List<Packet> packets = new List<Packet>();
        private Dictionary<Vertex, RoutingTable> routingTables;
        private RoutingTablesForm routingTablesForm;
        private PacketsInfoForm packetsInfoForm;
        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer launchTimer;
        private Random packetRandom = new Random();
        private const int TickIntervalMs = 50;

        // --- Поля для последовательного запуска ---
        private int packetsToLaunch = 0;
        private int packetsLaunched = 0;
        private Vertex launchSource, launchDest;
        private double launchTTL;
        private List<Vertex> sharedRouteForLaunch;
        private bool launchIsVirtual, launchIsFlooding, launchIsRandom, launchIsExperience;

        public MainForm()
        {
            InitializeComponent();
            graph = new Graph();
            pathVertices = null;
            pathEdgesList = null;

            // Настройка DataGridView
            dataGridViewMatrix.AllowUserToAddRows = false;
            dataGridViewMatrix.AllowUserToDeleteRows = false;
            dataGridViewMatrix.ReadOnly = false;
            dataGridViewMatrix.RowHeadersVisible = true;
            dataGridViewMatrix.RowHeadersWidth = 60;
            dataGridViewMatrix.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;

            // Настройка диалогов
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "graph";
            saveFileDialog.Title = "Сохранить граф";

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*";
            openFileDialog.Title = "Открыть граф";

            // Подписка на клики по заголовкам матрицы
            this.dataGridViewMatrix.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMatrix_RowHeaderMouseClick);
            this.dataGridViewMatrix.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMatrix_ColumnHeaderMouseClick);

            // Таймер анимации (движение пакетов)
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = TickIntervalMs;
            animationTimer.Tick += new EventHandler(this.animationTimer_Tick);

            // Таймер запуска пакетов (по одному)
            launchTimer = new System.Windows.Forms.Timer();
            launchTimer.Interval = 1000; // 1 секунда между пакетами
            launchTimer.Tick += new EventHandler(this.launchTimer_Tick);

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            UpdateMatrix();
        }

        // -------------------- Обновление матрицы --------------------
        private void UpdateMatrix()
        {
            int n = graph.Vertices.Count;
            dataGridViewMatrix.Rows.Clear();
            dataGridViewMatrix.Columns.Clear();

            foreach (var v in graph.Vertices)
            {
                dataGridViewMatrix.Columns.Add(v.Name, v.Name);
                dataGridViewMatrix.Columns[dataGridViewMatrix.Columns.Count - 1].Width = 40;
            }

            for (int i = 0; i < n; i++)
            {
                dataGridViewMatrix.Rows.Add();
                dataGridViewMatrix.Rows[i].HeaderCell.Value = graph.Vertices[i].Name;
                for (int j = 0; j < n; j++)
                {
                    double w = graph.GetWeight(graph.Vertices[i], graph.Vertices[j]);
                    dataGridViewMatrix[j, i].Value = double.IsInfinity(w) ? "" : w.ToString();
                }
            }

            pictureBoxGraph.Invalidate();
        }

        // -------------------- Обработка изменения матрицы --------------------
        private void dataGridViewMatrix_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (graph.Vertices.Count == 0) return;

            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= graph.Vertices.Count || col >= graph.Vertices.Count) return;

            Vertex source = graph.Vertices[row];
            Vertex target = graph.Vertices[col];
            string val = dataGridViewMatrix[col, row].Value?.ToString().Trim();

            if (string.IsNullOrEmpty(val))
            {
                var edge = graph.Edges.FirstOrDefault(ed => ed.Source == source && ed.Target == target);
                if (edge != null)
                    graph.RemoveEdge(edge);
                pictureBoxGraph.Invalidate();
                return;
            }

            if (double.TryParse(val, out double weight))
            {
                if (weight < 0)
                {
                    MessageBox.Show("Вес не может быть отрицательным!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    double oldW = graph.GetWeight(source, target);
                    dataGridViewMatrix[col, row].Value = double.IsInfinity(oldW) ? "" : oldW.ToString();
                    return;
                }

                var existing = graph.Edges.FirstOrDefault(ed => ed.Source == source && ed.Target == target);
                if (existing == null)
                {
                    try { graph.AddEdge(source, target, weight); }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridViewMatrix[col, row].Value = "";
                    }
                }
                else graph.UpdateWeight(source, target, weight);

                pictureBoxGraph.Invalidate();
            }
            else
            {
                MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                double oldW = graph.GetWeight(source, target);
                dataGridViewMatrix[col, row].Value = double.IsInfinity(oldW) ? "" : oldW.ToString();
            }
        }

        // -------------------- Клики по заголовкам матрицы --------------------
        private void dataGridViewMatrix_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < graph.Vertices.Count)
            {
                selectedMatrixRowIndex = e.RowIndex;
                selectedMatrixColumnIndex = -1;
                contextMenuStripMatrixHeader.Show(dataGridViewMatrix, e.Location);
            }
        }

        private void dataGridViewMatrix_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.ColumnIndex < graph.Vertices.Count)
            {
                selectedMatrixColumnIndex = e.ColumnIndex;
                selectedMatrixRowIndex = -1;
                contextMenuStripMatrixHeader.Show(dataGridViewMatrix, e.Location);
            }
        }

        // -------------------- Уникальное имя --------------------
        private string GetUniqueVertexName()
        {
            int index = 0;
            while (true)
            {
                string name = "v" + index;
                if (!graph.Vertices.Any(v => v.Name == name))
                    return name;
                index++;
            }
        }

        // -------------------- Вершины через матрицу --------------------
        private void addVertexFromMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graph.Vertices.Count >= 10)
            {
                MessageBox.Show("Максимум 10 вершин.", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Random rnd = new Random();
            int x = 30 + rnd.Next(0, pictureBoxGraph.Width - 60);
            int y = 30 + rnd.Next(0, pictureBoxGraph.Height - 60);
            string name = GetUniqueVertexName();
            graph.AddVertex(name, x, y);
            selectedMatrixRowIndex = -1;
            selectedMatrixColumnIndex = -1;
            UpdateMatrix();
            pictureBoxGraph.Invalidate();
        }

        private void deleteVertexFromMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int indexToDelete = -1;
            if (selectedMatrixRowIndex >= 0 && selectedMatrixRowIndex < graph.Vertices.Count)
                indexToDelete = selectedMatrixRowIndex;
            else if (selectedMatrixColumnIndex >= 0 && selectedMatrixColumnIndex < graph.Vertices.Count)
                indexToDelete = selectedMatrixColumnIndex;

            if (indexToDelete == -1)
            {
                MessageBox.Show("Не выбрана вершина.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (graph.Vertices.Count <= 1)
            {
                MessageBox.Show("Нельзя удалить последнюю вершину.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить {graph.Vertices[indexToDelete].Name}?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                graph.RemoveVertex(graph.Vertices[indexToDelete]);
                pathVertices = null;
                pathEdgesList = null;
                selectedMatrixRowIndex = -1;
                selectedMatrixColumnIndex = -1;
                UpdateMatrix();
                pictureBoxGraph.Invalidate();
            }
        }

        // -------------------- Логи --------------------
        private void AddLogEntry(string entry)
        {
            string fullEntry = $"[{DateTime.Now:HH:mm:ss}] {entry}";
            logEntries.Insert(0, fullEntry);
            if (logEntries.Count > 100) logEntries.RemoveAt(logEntries.Count - 1);
            UpdateLogDisplay();
        }

        private void UpdateLogDisplay()
        {
            listBoxLogs.Items.Clear();
            foreach (var entry in logEntries) listBoxLogs.Items.Add(entry);
            if (listBoxLogs.Items.Count > 0) listBoxLogs.SelectedIndex = 0;
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            logEntries.Clear();
            UpdateLogDisplay();
        }

        // -------------------- Кнопки графа --------------------
        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            if (graph.Vertices.Count >= 10)
            {
                MessageBox.Show("Максимум 10 вершин.", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Random rnd = new Random();
            int x = 30 + rnd.Next(0, pictureBoxGraph.Width - 60);
            int y = 30 + rnd.Next(0, pictureBoxGraph.Height - 60);
            string name = GetUniqueVertexName();
            graph.AddVertex(name, x, y);
            UpdateMatrix();
            pictureBoxGraph.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            animationTimer.Stop();
            launchTimer.Stop();
            packets.Clear();
            graph = new Graph();
            pathVertices = null;
            pathEdgesList = null;
            routingTables = null;
            UpdateMatrix();
            pictureBoxGraph.Invalidate();

            if (packetsInfoForm != null && !packetsInfoForm.IsDisposed)
                packetsInfoForm.RefreshPackets(packets);

            // Закрываем форму таблиц — при следующем запуске создастся заново
            if (routingTablesForm != null && !routingTablesForm.IsDisposed)
                routingTablesForm.Close();
            routingTablesForm = null;
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            var start = graph.Vertices.FirstOrDefault(v => v.IsStart);
            var end = graph.Vertices.FirstOrDefault(v => v.IsEnd);
            if (start == null || end == null)
            {
                MessageBox.Show("Сначала задайте начальную и конечную вершины.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool found = graph.Dijkstra(start.Name, end.Name, out double distance, out List<Vertex> path, out List<Edge> pathEdges);
            pathVertices = path;
            pathEdgesList = pathEdges;

            ResultForm resultForm = new ResultForm();
            if (found)
            {
                string pathStr = string.Join(" → ", path.ConvertAll(v => v.Name));
                resultForm.ShowResult(distance, path);
                AddLogEntry($"Путь найден: {start.Name} → {end.Name} | Длина: {distance} | {pathStr}");
            }
            else
            {
                resultForm.ShowResult(double.PositiveInfinity, null);
                AddLogEntry($"Путь не найден: {start.Name} → {end.Name}");
            }
            resultForm.ShowDialog();
            pictureBoxGraph.Invalidate();
        }

        // -------------------- Сравнение алгоритмов --------------------
        private void btnCompareAlgorithms_Click(object sender, EventArgs e)
        {
            if (graph.Vertices.Count < 2)
            {
                MessageBox.Show("Нужно минимум 2 вершины.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var n = graph.Vertices.Count;

                var swDijkstra = System.Diagnostics.Stopwatch.StartNew();
                var (dijkstraDist, dijkstraPrev) = graph.DijkstraAllPairs();
                swDijkstra.Stop();

                var swFloyd = System.Diagnostics.Stopwatch.StartNew();
                var (floydDist, floydNext) = graph.Floyd();
                swFloyd.Stop();

                var dijkstraResults = new List<Tuple<string, string, string, double>>();
                var floydResults = new List<Tuple<string, string, string, double>>();

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) continue;

                        string dPath = "Путь не найден";
                        double dDist = dijkstraDist[i, j];
                        if (!double.IsInfinity(dDist))
                            dPath = string.Join(" → ", graph.GetPathDijkstra(i, j, dijkstraPrev).ConvertAll(v => v.Name));
                        dijkstraResults.Add(Tuple.Create(graph.Vertices[i].Name, graph.Vertices[j].Name, dPath, dDist));

                        string fPath = "Путь не найден";
                        double fDist = floydDist[i, j];
                        if (!double.IsInfinity(fDist))
                            fPath = string.Join(" → ", graph.GetPathFloyd(i, j, floydNext).ConvertAll(v => v.Name));
                        floydResults.Add(Tuple.Create(graph.Vertices[i].Name, graph.Vertices[j].Name, fPath, fDist));
                    }
                }

                ComparisonForm form = new ComparisonForm();
                form.ShowComparison(dijkstraResults, swDijkstra.Elapsed.TotalMilliseconds,
                                    floydResults, swFloyd.Elapsed.TotalMilliseconds, n, graph.Edges.Count);
                form.ShowDialog();

                AddLogEntry($"Сравнение: Дейкстра {swDijkstra.Elapsed.TotalMilliseconds:F3} мс, Флойд {swFloyd.Elapsed.TotalMilliseconds:F3} мс");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------- Экспорт / импорт --------------------
        private void btnExportGraph_Click(object sender, EventArgs e) => ExportGraph();
        private void btnImportGraph_Click(object sender, EventArgs e) => ImportGraph();

        private void ExportGraph()
        {
            if (graph.Vertices.Count == 0)
            {
                MessageBox.Show("Граф пуст.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        string header = "";
                        foreach (var v in graph.Vertices) header += $"{v.Name}_{v.Location.X}_{v.Location.Y}\t";
                        sw.WriteLine(header.TrimEnd('\t'));

                        int n = graph.Vertices.Count;
                        for (int i = 0; i < n; i++)
                        {
                            string row = "";
                            for (int j = 0; j < n; j++)
                            {
                                double weight = graph.GetWeight(graph.Vertices[i], graph.Vertices[j]);
                                row += (double.IsInfinity(weight) ? "INF" : weight.ToString()) + "\t";
                            }
                            sw.WriteLine(row.TrimEnd('\t'));
                        }
                    }
                    MessageBox.Show("Граф сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ImportGraph()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        string headerLine = sr.ReadLine();
                        if (string.IsNullOrEmpty(headerLine))
                        {
                            MessageBox.Show("Файл пуст.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string[] headerParts = headerLine.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        List<(string name, int x, int y)> vertexData = new List<(string, int, int)>();

                        foreach (var part in headerParts)
                        {
                            string[] parts = part.Split('_');
                            if (parts.Length == 3 && parts[0].StartsWith("v") && int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                                vertexData.Add((parts[0], x, y));
                            else
                            {
                                MessageBox.Show($"Неверный формат: {part}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        if (vertexData.Count == 0 || vertexData.Count > 10)
                        {
                            MessageBox.Show("Количество вершин 1-10.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        List<List<double>> matrix = new List<List<double>>();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;
                            string[] parts = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length != vertexData.Count)
                            {
                                MessageBox.Show("Неверное число столбцов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            List<double> row = new List<double>();
                            foreach (var part in parts)
                            {
                                if (part == "INF") row.Add(double.PositiveInfinity);
                                else if (double.TryParse(part, out double weight))
                                {
                                    if (weight < 0) { MessageBox.Show("Отрицательный вес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                                    row.Add(weight);
                                }
                                else { MessageBox.Show($"Неверное число: {part}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            }
                            matrix.Add(row);
                        }

                        if (matrix.Count != vertexData.Count)
                        {
                            MessageBox.Show("Число строк не совпадает.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        graph = new Graph();
                        foreach (var data in vertexData) graph.AddVertex(data.name, data.x, data.y);
                        for (int i = 0; i < matrix.Count; i++)
                            for (int j = 0; j < matrix[i].Count; j++)
                            {
                                double weight = matrix[i][j];
                                if (!double.IsInfinity(weight) && weight > 0)
                                    graph.AddEdge(graph.Vertices[i], graph.Vertices[j], weight);
                            }

                        pathVertices = null;
                        pathEdgesList = null;
                        UpdateMatrix();
                        pictureBoxGraph.Invalidate();

                        MessageBox.Show($"Граф импортирован! {graph.Vertices.Count} вершин.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================================================================
        //                    МАРШРУТИЗАЦИЯ
        // ================================================================

        private void btnStartRouting_Click(object sender, EventArgs e)
        {
            var source = graph.Vertices.FirstOrDefault(v => v.IsStart);
            var dest = graph.Vertices.FirstOrDefault(v => v.IsEnd);
            if (source == null || dest == null)
            {
                MessageBox.Show("Задайте начальную и конечную вершины.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (source == dest)
            {
                MessageBox.Show("Начальная и конечная должны различаться.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxPackets.Text, out int count) || count < 1 || count > 20)
            {
                MessageBox.Show("Количество пакетов 1-20.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(textBoxTTL.Text, out double ttl) || ttl < 1 || ttl > 120)
            {
                MessageBox.Show("TTL 1-120 сек.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Останавливаем старые таймеры
            animationTimer.Stop();
            launchTimer.Stop();
            packets.Clear();
            Packet.ResetIdCounter();

            // Сохраняем параметры
            launchSource = source;
            launchDest = dest;
            launchTTL = ttl;
            packetsToLaunch = count;
            packetsLaunched = 0;
            launchIsVirtual = radioButtonVirtual.Checked;
            launchIsFlooding = radioButtonFlooding.Checked;
            launchIsRandom = radioButtonRandom.Checked;
            launchIsExperience = radioButtonExperience.Checked;

            // Инициализируем таблицы и отсечение дубликатов
            routingTables = graph.InitRoutingTables();
            deliveredIds.Clear();
            seenPackets.Clear();

            // --- Предвычисление маршрута ---
            sharedRouteForLaunch = null;
            sharedRouteNotYetSet = false;

            if (launchIsVirtual && !launchIsFlooding)
            {
                if (launchIsRandom)
                {
                    // Для случайной виртуальный канал — предвычисляем случайный маршрут
                    sharedRouteForLaunch = BuildRandomRoute(source, dest, ttl);
                    if (sharedRouteForLaunch == null)
                    {
                        MessageBox.Show("Не удалось построить маршрут.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (launchIsExperience)
                {
                    // Для "по опыту" + виртуальный: маршрут будет зафиксирован
                    // после первого успешно доставленного пакета
                    sharedRouteNotYetSet = true;
                }
            }

            // Настройка интервала запуска: для виртуального "по опыту" — 2 сек,
            // чтобы первый пакет успел дойти и зафиксировать маршрут
            if (sharedRouteNotYetSet)
                launchTimer.Interval = 2000;
            else
                launchTimer.Interval = 1000;

            // Открываем/обновляем формы
            EnsurePacketsInfoFormVisible();
            if (launchIsExperience)
                EnsureRoutingTablesFormVisible();

            // Запускаем: сначала сразу первый пакет, потом остальные по таймеру
            launchTimer_Tick(null, null);
            animationTimer.Start();
            launchTimer.Start();

            AddLogEntry($"Запуск: {count} пакетов, алгоритм " +
                        $"{(launchIsRandom ? "Случайная" : launchIsFlooding ? "Лавинная" : "По опыту")}, " +
                        $"метод {(launchIsVirtual ? "виртуальный" : "дейтаграммный")}");
        }

        private void btnStopRouting_Click(object sender, EventArgs e)
        {
            animationTimer.Stop();
            launchTimer.Stop();
            packets.Clear();
            pictureBoxGraph.Invalidate();

            if (packetsInfoForm != null && !packetsInfoForm.IsDisposed)
                packetsInfoForm.RefreshPackets(packets);

            // Очищаем таблицы маршрутизации, но не закрываем форму
            if (routingTablesForm != null && !routingTablesForm.IsDisposed)
            {
                routingTables = graph.InitRoutingTables(); // пустые таблицы
                routingTablesForm.Setup(graph, routingTables);
                routingTablesForm.RefreshTable();
            }

            AddLogEntry("Маршрутизация остановлена пользователем");
        }

        private void btnShowTables_Click(object sender, EventArgs e) => EnsureRoutingTablesFormVisible();
        private void btnShowPackets_Click(object sender, EventArgs e) => EnsurePacketsInfoFormVisible();

        // Исправление: всегда обновляем ссылки формы на актуальные данные
        private void EnsureRoutingTablesFormVisible()
        {
            if (routingTables == null) routingTables = graph.InitRoutingTables();

            if (routingTablesForm == null || routingTablesForm.IsDisposed)
            {
                routingTablesForm = new RoutingTablesForm();
                routingTablesForm.StartPosition = FormStartPosition.Manual;

                // Правый край экрана, ниже формы пакетов
                Screen screen = Screen.FromControl(this);
                int rightX = screen.WorkingArea.Right - routingTablesForm.Width - 10;
                int topY = screen.WorkingArea.Top + 10;

                // Если форма пакетов уже открыта — ставим ниже её
                if (packetsInfoForm != null && !packetsInfoForm.IsDisposed)
                    topY = packetsInfoForm.Bottom + 10;

                routingTablesForm.Location = new Point(rightX, topY);
                routingTablesForm.Show(this);
            }

            routingTablesForm.Setup(graph, routingTables);
        }

        private void EnsurePacketsInfoFormVisible()
        {
            if (packetsInfoForm == null || packetsInfoForm.IsDisposed)
            {
                packetsInfoForm = new PacketsInfoForm();
                packetsInfoForm.StartPosition = FormStartPosition.Manual;

                // Правый край экрана
                Screen screen = Screen.FromControl(this);
                int rightX = screen.WorkingArea.Right - packetsInfoForm.Width - 10;
                int topY = screen.WorkingArea.Top + 10;
                packetsInfoForm.Location = new Point(rightX, topY);

                packetsInfoForm.Show(this);
            }
            packetsInfoForm.RefreshPackets(packets);
        }

        // -------------------- Запуск пакетов по одному --------------------
        private void launchTimer_Tick(object sender, EventArgs e)
        {
            if (packetsLaunched >= packetsToLaunch)
            {
                launchTimer.Stop();
                return;
            }

            int size = packetRandom.Next(64, 1501);
            Packet p = new Packet(launchSource, launchDest, size) { TimeToLive = launchTTL };

            if (sharedRouteForLaunch != null)
            {
                p.PrecomputedRoute = sharedRouteForLaunch;
                p.RouteIndex = 0;
            }

            p.Color = Color.FromArgb(255, packetRandom.Next(50, 200), packetRandom.Next(50, 200));
            packets.Add(p);
            packetsLaunched++;
        }

        // -------------------- Таймер анимации --------------------
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (packets.Count == 0 && !launchTimer.Enabled)
            {
                animationTimer.Stop();
                return;
            }

            List<Packet> newPackets = new List<Packet>();

            foreach (var p in packets.ToList())
            {
                if (!p.IsActive()) continue;

                p.TimeToLive -= TickIntervalMs / 1000.0;
                p.TimeSpent += TickIntervalMs / 1000.0;
                if (p.TimeToLive <= 0)
                {
                    p.Status = PacketStatus.Expired;
                    continue;
                }

                if (p.NextNode == null)
                {
                    Vertex next = ChooseNextNode(p, newPackets);
                    if (next == null)
                    {
                        if (p.IsActive()) p.Status = PacketStatus.Expired;
                        continue;
                    }
                    p.NextNode = next;
                    p.CurrentEdgeWeight = graph.GetWeight(p.CurrentNode, next);
                    p.Progress = 0;
                }

                double timePerEdgeSec = Math.Max(0.3, Math.Min(3.0, p.CurrentEdgeWeight * 0.1));
                p.Progress += (TickIntervalMs / 1000.0) / timePerEdgeSec;

                if (p.Progress >= 1.0)
                {
                    Vertex arrivedAt = p.NextNode;
                    p.CurrentNode = arrivedAt;
                    p.NextNode = null;
                    p.Progress = 0;
                    p.Path.Add(arrivedAt);
                    p.Position = new PointF(arrivedAt.Location.X, arrivedAt.Location.Y);

                    if (arrivedAt == p.Destination)
                    {
                        if (deliveredIds.Contains(p.Id)) { p.Status = PacketStatus.Duplicate; }
                        else
                        {
                            p.Status = PacketStatus.Delivered;
                            deliveredIds.Add(p.Id);
                            if (routingTables != null && launchIsExperience)
                                graph.LearnRouteFromPacket(routingTables, p);

                            // Если виртуальный "по опыту" и маршрут ещё не зафиксирован — фиксируем
                            if (sharedRouteNotYetSet)
                            {
                                sharedRouteForLaunch = new List<Vertex>(p.Path);
                                sharedRouteNotYetSet = false;

                                // Обновляем ТОЛЬКО те пакеты, которые ещё не начали движение
                                // (Path.Count <= 1 и NextNode == null — значит, они ещё в стартовом узле)
                                foreach (var other in packets)
                                {
                                    if (other != p && other.IsActive() && other.NextNode == null && other.Path.Count <= 1)
                                    {
                                        other.PrecomputedRoute = sharedRouteForLaunch;
                                        other.RouteIndex = 0;
                                    }
                                    // Пакеты, которые уже в пути (NextNode != null), НЕ трогаем
                                }

                                foreach (var other in newPackets)
                                {
                                    if (other != p && other.IsActive() && other.NextNode == null && other.Path.Count <= 1)
                                    {
                                        other.PrecomputedRoute = sharedRouteForLaunch;
                                        other.RouteIndex = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    float x1 = p.CurrentNode.Location.X;
                    float y1 = p.CurrentNode.Location.Y;
                    float x2 = p.NextNode.Location.X;
                    float y2 = p.NextNode.Location.Y;
                    p.Position = new PointF(x1 + (x2 - x1) * (float)p.Progress, y1 + (y2 - y1) * (float)p.Progress);
                }
            }

            packets.AddRange(newPackets);

            pictureBoxGraph.Invalidate();
            if (packetsInfoForm != null && !packetsInfoForm.IsDisposed)
                packetsInfoForm.RefreshPackets(packets);
            if (routingTablesForm != null && !routingTablesForm.IsDisposed)
                routingTablesForm.RefreshTable();

            if (packets.All(p => !p.IsActive()) && !launchTimer.Enabled)
            {
                animationTimer.Stop();
                int delivered = packets.Count(p => p.IsDelivered());
                int duplicates = packets.Count(p => p.IsDuplicate());
                int expired = packets.Count(p => p.IsExpired());
                int dropped = packets.Count(p => p.IsDropped());
                AddLogEntry($"Завершено: доставлено {delivered}, дубликатов {duplicates}, истекло {expired}, отброшено {dropped}");
            }
        }

        // -------------------- Выбор следующего узла --------------------
        private Vertex ChooseNextNode(Packet p, List<Packet> newPackets)
        {
            Vertex current = p.CurrentNode;
            Vertex previous = p.Path.Count >= 2 ? p.Path[p.Path.Count - 2] : null;

            // Отсечение дубликатов для лавинной
            if (radioButtonFlooding.Checked)
            {
                if (graph.NodeHasSeenPacket(current, p.Id, seenPackets))
                {
                    p.Status = PacketStatus.Dropped;
                    return null;
                }
                graph.MarkPacketSeen(current, p.Id, seenPackets);
            }

            // Виртуальный канал
            if (p.PrecomputedRoute != null)
            {
                int nextIdx = p.RouteIndex + 1;
                if (nextIdx < p.PrecomputedRoute.Count)
                {
                    p.RouteIndex = nextIdx;
                    return p.PrecomputedRoute[nextIdx];
                }
                return null;
            }

            // Лавинная
            if (radioButtonFlooding.Checked)
            {
                var neighbors = graph.GetAllNeighbors(current, previous);
                if (neighbors.Count == 0) return null;

                Vertex first = neighbors[0];

                for (int i = 1; i < neighbors.Count; i++)
                {
                    Packet child = new Packet(p, neighbors[i]);
                    child.CurrentEdgeWeight = graph.GetWeight(current, neighbors[i]); // <-- фикс скорости
                    child.Color = Color.FromArgb(0, 100, Math.Min(255, 100 + child.Depth * 30));
                    newPackets.Add(child);
                }
                return first;
            }

            // Случайная
            if (radioButtonRandom.Checked)
                return graph.GetRandomNeighbor(current, previous);

            // По опыту
            if (radioButtonExperience.Checked && routingTables != null && routingTables.ContainsKey(current))
            {
                Vertex next = routingTables[current].GetNext(p.Destination);
                if (next != null) return next;
                return graph.GetRandomNeighbor(current, previous);
            }

            return null;
        }

        private List<Vertex> BuildRandomRoute(Vertex source, Vertex dest, double ttl)
        {
            List<Vertex> route = new List<Vertex> { source };
            Vertex current = source;
            Vertex previous = null;
            int maxSteps = (int)(ttl / 0.5);
            for (int i = 0; i < maxSteps; i++)
            {
                if (current == dest) return route;
                Vertex next = graph.GetRandomNeighbor(current, previous);
                if (next == null) return null;
                route.Add(next);
                previous = current;
                current = next;
            }
            return current == dest ? route : null;
        }

        // -------------------- Рисование --------------------
        private void pictureBoxGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen penVertex = new Pen(Color.Black, 2);
            Brush brushVertex = Brushes.LightGray;
            Pen penEdge = new Pen(Color.Black, 1);
            Font font = new Font("Arial", 10);

            List<Vertex> highlight = pathVertices;
            if (highlight != null && highlight.Any(v => !graph.Vertices.Contains(v)))
                highlight = null;

            graph.Draw(g, penVertex, brushVertex, penEdge, font,
                       startHighlight: graph.Vertices.FirstOrDefault(v => v.IsStart),
                       endHighlight: graph.Vertices.FirstOrDefault(v => v.IsEnd),
                       pathVertices: pathVertices,
                       pathEdges: pathEdgesList);

            foreach (var p in packets)
            {
                if (p.IsExpired() || p.IsDropped()) continue;

                float radius = 5f + p.Size / 500f;
                Color drawColor = p.IsDuplicate() ? Color.FromArgb(150, p.Color) : p.Color;
                Brush b = new SolidBrush(drawColor);
                g.FillEllipse(b, p.Position.X - radius, p.Position.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(Pens.Black, p.Position.X - radius, p.Position.Y - radius, radius * 2, radius * 2);
                g.DrawString(p.Id.ToString(), new Font("Arial", 7), Brushes.White, p.Position.X - 4, p.Position.Y - 5);
            }
        }

        // -------------------- Мышь --------------------
        private Vertex GetVertexAt(Point p)
        {
            foreach (var v in graph.Vertices)
                if (v.GetRectangle().Contains(p)) return v;
            return null;
        }

        private Edge GetEdgeAt(Point p)
        {
            foreach (var e in graph.Edges)
            {
                Point p1 = e.Source.Location;
                Point p2 = e.Target.Location;
                Point mid = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                if (Math.Abs(p.X - mid.X) < 20 && Math.Abs(p.Y - mid.Y) < 20) return e;
            }
            return null;
        }

        private void pictureBoxGraph_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                movingVertex = GetVertexAt(e.Location);
                if (movingVertex != null) isDragging = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                selectedVertex = GetVertexAt(e.Location);
                if (selectedVertex != null)
                    contextMenuStripVertex.Show(pictureBoxGraph, e.Location);
                else
                {
                    selectedEdge = GetEdgeAt(e.Location);
                    if (selectedEdge != null)
                        contextMenuStripEdge.Show(pictureBoxGraph, e.Location);
                    else
                        contextMenuStripEmpty.Show(pictureBoxGraph, e.Location);
                }
            }
        }

        private void pictureBoxGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && movingVertex != null)
            {
                movingVertex.Location = new Point(e.X, e.Y);
                pictureBoxGraph.Invalidate();
            }
        }

        private void pictureBoxGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { isDragging = false; movingVertex = null; }
        }

        // -------------------- Контекстные меню --------------------
        private void makeStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedVertex == null) return;
            foreach (var v in graph.Vertices) v.IsStart = false;
            selectedVertex.IsStart = true;
            selectedVertex.IsEnd = false;
            pictureBoxGraph.Invalidate();
        }

        private void makeEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedVertex == null) return;
            foreach (var v in graph.Vertices) v.IsEnd = false;
            selectedVertex.IsEnd = true;
            selectedVertex.IsStart = false;
            pictureBoxGraph.Invalidate();
        }

        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedVertex == null) return;
            graph.RemoveVertex(selectedVertex);
            pathVertices = null;
            pathEdgesList = null;
            UpdateMatrix();
            pictureBoxGraph.Invalidate();
        }

        private void changeWeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedEdge == null) return;
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите вес:", "Изменение веса", selectedEdge.Weight.ToString());
            if (double.TryParse(input, out double newWeight))
            {
                if (newWeight < 0)
                {
                    MessageBox.Show("Вес не может быть отрицательным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    graph.UpdateWeight(selectedEdge.Source, selectedEdge.Target, newWeight);
                    int row = graph.Vertices.IndexOf(selectedEdge.Source);
                    int col = graph.Vertices.IndexOf(selectedEdge.Target);
                    if (row >= 0 && col >= 0)
                        dataGridViewMatrix[col, row].Value = newWeight.ToString();
                    pictureBoxGraph.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Некорректный ввод.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void deleteEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedEdge == null) return;
            graph.RemoveEdge(selectedEdge);
            int row = graph.Vertices.IndexOf(selectedEdge.Source);
            int col = graph.Vertices.IndexOf(selectedEdge.Target);
            if (row >= 0 && col >= 0)
                dataGridViewMatrix[col, row].Value = "";
            pictureBoxGraph.Invalidate();
        }

        private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point mousePos = pictureBoxGraph.PointToClient(Cursor.Position);
            if (graph.Vertices.Count >= 10)
            {
                MessageBox.Show("Максимум 10 вершин.", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string name = GetUniqueVertexName();
            graph.AddVertex(name, mousePos.X, mousePos.Y);
            UpdateMatrix();
            pictureBoxGraph.Invalidate();
        }
    }
}
