namespace Lab1_OIvVS
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
            this.dataGridViewMatrix = new System.Windows.Forms.DataGridView();
            this.btnFindPath = new System.Windows.Forms.Button();
            this.btnAddVertex = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExportGraph = new System.Windows.Forms.Button();
            this.btnImportGraph = new System.Windows.Forms.Button();
            this.btnCompareAlgorithms = new System.Windows.Forms.Button();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.groupBoxRouting = new System.Windows.Forms.GroupBox();
            this.panelAlgorithm = new System.Windows.Forms.Panel();
            this.labelAlgorithm = new System.Windows.Forms.Label();
            this.radioButtonRandom = new System.Windows.Forms.RadioButton();
            this.radioButtonFlooding = new System.Windows.Forms.RadioButton();
            this.radioButtonExperience = new System.Windows.Forms.RadioButton();
            this.panelMethod = new System.Windows.Forms.Panel();
            this.labelMethod = new System.Windows.Forms.Label();
            this.radioButtonVirtual = new System.Windows.Forms.RadioButton();
            this.radioButtonDatagram = new System.Windows.Forms.RadioButton();
            this.labelPackets = new System.Windows.Forms.Label();
            this.textBoxPackets = new System.Windows.Forms.TextBox();
            this.labelTTL = new System.Windows.Forms.Label();
            this.textBoxTTL = new System.Windows.Forms.TextBox();
            this.btnStartRouting = new System.Windows.Forms.Button();
            this.btnStopRouting = new System.Windows.Forms.Button();
            this.btnShowTables = new System.Windows.Forms.Button();
            this.btnShowPackets = new System.Windows.Forms.Button();
            this.contextMenuStripVertex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.makeStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripEdge = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeWeightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripEmpty = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMatrixHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVertexFromMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteVertexFromMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMatrix)).BeginInit();
            this.groupBoxRouting.SuspendLayout();
            this.panelAlgorithm.SuspendLayout();
            this.panelMethod.SuspendLayout();
            this.contextMenuStripVertex.SuspendLayout();
            this.contextMenuStripEdge.SuspendLayout();
            this.contextMenuStripEmpty.SuspendLayout();
            this.contextMenuStripMatrixHeader.SuspendLayout();
            this.SuspendLayout();

            // 
            // pictureBoxGraph
            // 
            this.pictureBoxGraph.BackColor = System.Drawing.Color.White;
            this.pictureBoxGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGraph.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxGraph.Name = "pictureBoxGraph";
            this.pictureBoxGraph.Size = new System.Drawing.Size(500, 400);
            this.pictureBoxGraph.TabIndex = 0;
            this.pictureBoxGraph.TabStop = false;
            this.pictureBoxGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGraph_Paint);
            this.pictureBoxGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseDown);
            this.pictureBoxGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseMove);
            this.pictureBoxGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseUp);

            // 
            // dataGridViewMatrix
            // 
            this.dataGridViewMatrix.AllowUserToAddRows = false;
            this.dataGridViewMatrix.AllowUserToDeleteRows = false;
            this.dataGridViewMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMatrix.Location = new System.Drawing.Point(518, 12);
            this.dataGridViewMatrix.Name = "dataGridViewMatrix";
            this.dataGridViewMatrix.RowHeadersWidth = 60;
            this.dataGridViewMatrix.Size = new System.Drawing.Size(400, 400);
            this.dataGridViewMatrix.TabIndex = 1;
            this.dataGridViewMatrix.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMatrix_CellValueChanged);
            this.dataGridViewMatrix.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMatrix_RowHeaderMouseClick);
            this.dataGridViewMatrix.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMatrix_ColumnHeaderMouseClick);

            // 
            // btnFindPath
            // 
            this.btnFindPath.Location = new System.Drawing.Point(12, 418);
            this.btnFindPath.Name = "btnFindPath";
            this.btnFindPath.Size = new System.Drawing.Size(100, 30);
            this.btnFindPath.TabIndex = 2;
            this.btnFindPath.Text = "Найти путь";
            this.btnFindPath.UseVisualStyleBackColor = true;
            this.btnFindPath.Click += new System.EventHandler(this.btnFindPath_Click);

            // 
            // btnAddVertex
            // 
            this.btnAddVertex.Location = new System.Drawing.Point(118, 418);
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(120, 30);
            this.btnAddVertex.TabIndex = 3;
            this.btnAddVertex.Text = "Добавить вершину";
            this.btnAddVertex.UseVisualStyleBackColor = true;
            this.btnAddVertex.Click += new System.EventHandler(this.btnAddVertex_Click);

            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(244, 418);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Очистить граф";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // 
            // btnExportGraph
            // 
            this.btnExportGraph.Location = new System.Drawing.Point(350, 418);
            this.btnExportGraph.Name = "btnExportGraph";
            this.btnExportGraph.Size = new System.Drawing.Size(120, 30);
            this.btnExportGraph.TabIndex = 5;
            this.btnExportGraph.Text = "Скачать граф";
            this.btnExportGraph.UseVisualStyleBackColor = true;
            this.btnExportGraph.Click += new System.EventHandler(this.btnExportGraph_Click);

            // 
            // btnImportGraph
            // 
            this.btnImportGraph.Location = new System.Drawing.Point(476, 418);
            this.btnImportGraph.Name = "btnImportGraph";
            this.btnImportGraph.Size = new System.Drawing.Size(130, 30);
            this.btnImportGraph.TabIndex = 6;
            this.btnImportGraph.Text = "Импортировать граф";
            this.btnImportGraph.UseVisualStyleBackColor = true;
            this.btnImportGraph.Click += new System.EventHandler(this.btnImportGraph_Click);

            // 
            // btnCompareAlgorithms
            // 
            this.btnCompareAlgorithms.Location = new System.Drawing.Point(612, 418);
            this.btnCompareAlgorithms.Name = "btnCompareAlgorithms";
            this.btnCompareAlgorithms.Size = new System.Drawing.Size(150, 30);
            this.btnCompareAlgorithms.TabIndex = 7;
            this.btnCompareAlgorithms.Text = "Сравнить алгоритмы";
            this.btnCompareAlgorithms.UseVisualStyleBackColor = true;
            this.btnCompareAlgorithms.Click += new System.EventHandler(this.btnCompareAlgorithms_Click);

            // 
            // groupBoxRouting
            // 
            this.groupBoxRouting.Controls.Add(this.panelAlgorithm);
            this.groupBoxRouting.Controls.Add(this.panelMethod);
            this.groupBoxRouting.Controls.Add(this.labelPackets);
            this.groupBoxRouting.Controls.Add(this.textBoxPackets);
            this.groupBoxRouting.Controls.Add(this.labelTTL);
            this.groupBoxRouting.Controls.Add(this.textBoxTTL);
            this.groupBoxRouting.Controls.Add(this.btnStartRouting);
            this.groupBoxRouting.Controls.Add(this.btnStopRouting);
            this.groupBoxRouting.Controls.Add(this.btnShowTables);
            this.groupBoxRouting.Controls.Add(this.btnShowPackets);
            this.groupBoxRouting.Location = new System.Drawing.Point(12, 455);
            this.groupBoxRouting.Name = "groupBoxRouting";
            this.groupBoxRouting.Size = new System.Drawing.Size(906, 125);
            this.groupBoxRouting.TabIndex = 8;
            this.groupBoxRouting.TabStop = false;
            this.groupBoxRouting.Text = "Маршрутизация";

            // 
            // panelAlgorithm
            // 
            this.panelAlgorithm.Controls.Add(this.labelAlgorithm);
            this.panelAlgorithm.Controls.Add(this.radioButtonRandom);
            this.panelAlgorithm.Controls.Add(this.radioButtonFlooding);
            this.panelAlgorithm.Controls.Add(this.radioButtonExperience);
            this.panelAlgorithm.Location = new System.Drawing.Point(6, 15);
            this.panelAlgorithm.Name = "panelAlgorithm";
            this.panelAlgorithm.Size = new System.Drawing.Size(500, 30);
            this.panelAlgorithm.TabIndex = 0;

            // 
            // labelAlgorithm
            // 
            this.labelAlgorithm.AutoSize = true;
            this.labelAlgorithm.Location = new System.Drawing.Point(4, 7);
            this.labelAlgorithm.Name = "labelAlgorithm";
            this.labelAlgorithm.Size = new System.Drawing.Size(70, 13);
            this.labelAlgorithm.TabIndex = 0;
            this.labelAlgorithm.Text = "Алгоритм:";

            // 
            // radioButtonRandom
            // 
            this.radioButtonRandom.AutoSize = true;
            this.radioButtonRandom.Checked = true;
            this.radioButtonRandom.Location = new System.Drawing.Point(84, 5);
            this.radioButtonRandom.Name = "radioButtonRandom";
            this.radioButtonRandom.Size = new System.Drawing.Size(80, 17);
            this.radioButtonRandom.TabIndex = 1;
            this.radioButtonRandom.TabStop = true;
            this.radioButtonRandom.Text = "Случайная";

            // 
            // radioButtonFlooding
            // 
            this.radioButtonFlooding.AutoSize = true;
            this.radioButtonFlooding.Location = new System.Drawing.Point(174, 5);
            this.radioButtonFlooding.Name = "radioButtonFlooding";
            this.radioButtonFlooding.Size = new System.Drawing.Size(75, 17);
            this.radioButtonFlooding.TabIndex = 2;
            this.radioButtonFlooding.Text = "Лавинная";

            // 
            // radioButtonExperience
            // 
            this.radioButtonExperience.AutoSize = true;
            this.radioButtonExperience.Location = new System.Drawing.Point(259, 5);
            this.radioButtonExperience.Name = "radioButtonExperience";
            this.radioButtonExperience.Size = new System.Drawing.Size(175, 17);
            this.radioButtonExperience.TabIndex = 3;
            this.radioButtonExperience.Text = "По предыдущему опыту";

            // 
            // panelMethod
            // 
            this.panelMethod.Controls.Add(this.labelMethod);
            this.panelMethod.Controls.Add(this.radioButtonVirtual);
            this.panelMethod.Controls.Add(this.radioButtonDatagram);
            this.panelMethod.Location = new System.Drawing.Point(6, 48);
            this.panelMethod.Name = "panelMethod";
            this.panelMethod.Size = new System.Drawing.Size(500, 30);
            this.panelMethod.TabIndex = 1;

            // 
            // labelMethod
            // 
            this.labelMethod.AutoSize = true;
            this.labelMethod.Location = new System.Drawing.Point(4, 7);
            this.labelMethod.Name = "labelMethod";
            this.labelMethod.Size = new System.Drawing.Size(55, 13);
            this.labelMethod.TabIndex = 0;
            this.labelMethod.Text = "Метод:";

            // 
            // radioButtonVirtual
            // 
            this.radioButtonVirtual.AutoSize = true;
            this.radioButtonVirtual.Checked = true;
            this.radioButtonVirtual.Location = new System.Drawing.Point(84, 5);
            this.radioButtonVirtual.Name = "radioButtonVirtual";
            this.radioButtonVirtual.Size = new System.Drawing.Size(130, 17);
            this.radioButtonVirtual.TabIndex = 1;
            this.radioButtonVirtual.TabStop = true;
            this.radioButtonVirtual.Text = "Виртуальный канал";

            // 
            // radioButtonDatagram
            // 
            this.radioButtonDatagram.AutoSize = true;
            this.radioButtonDatagram.Location = new System.Drawing.Point(224, 5);
            this.radioButtonDatagram.Name = "radioButtonDatagram";
            this.radioButtonDatagram.Size = new System.Drawing.Size(115, 17);
            this.radioButtonDatagram.TabIndex = 2;
            this.radioButtonDatagram.Text = "Дейтаграммный";

            // 
            // labelPackets
            // 
            this.labelPackets.AutoSize = true;
            this.labelPackets.Location = new System.Drawing.Point(10, 88);
            this.labelPackets.Name = "labelPackets";
            this.labelPackets.Size = new System.Drawing.Size(60, 13);
            this.labelPackets.TabIndex = 2;
            this.labelPackets.Text = "Пакетов:";

            // 
            // textBoxPackets
            // 
            this.textBoxPackets.Location = new System.Drawing.Point(90, 85);
            this.textBoxPackets.Name = "textBoxPackets";
            this.textBoxPackets.Size = new System.Drawing.Size(60, 20);
            this.textBoxPackets.TabIndex = 3;
            this.textBoxPackets.Text = "3";

            // 
            // labelTTL
            // 
            this.labelTTL.AutoSize = true;
            this.labelTTL.Location = new System.Drawing.Point(170, 88);
            this.labelTTL.Name = "labelTTL";
            this.labelTTL.Size = new System.Drawing.Size(50, 13);
            this.labelTTL.TabIndex = 4;
            this.labelTTL.Text = "TTL (сек):";

            // 
            // textBoxTTL
            // 
            this.textBoxTTL.Location = new System.Drawing.Point(240, 85);
            this.textBoxTTL.Name = "textBoxTTL";
            this.textBoxTTL.Size = new System.Drawing.Size(60, 20);
            this.textBoxTTL.TabIndex = 5;
            this.textBoxTTL.Text = "10";

            // 
            // btnStartRouting
            // 
            this.btnStartRouting.Location = new System.Drawing.Point(400, 80);
            this.btnStartRouting.Name = "btnStartRouting";
            this.btnStartRouting.Size = new System.Drawing.Size(100, 30);
            this.btnStartRouting.TabIndex = 6;
            this.btnStartRouting.Text = "Запустить";
            this.btnStartRouting.UseVisualStyleBackColor = true;
            this.btnStartRouting.Click += new System.EventHandler(this.btnStartRouting_Click);

            // 
            // btnStopRouting
            // 
            this.btnStopRouting.Location = new System.Drawing.Point(510, 80);
            this.btnStopRouting.Name = "btnStopRouting";
            this.btnStopRouting.Size = new System.Drawing.Size(100, 30);
            this.btnStopRouting.TabIndex = 7;
            this.btnStopRouting.Text = "Остановить";
            this.btnStopRouting.UseVisualStyleBackColor = true;
            this.btnStopRouting.Click += new System.EventHandler(this.btnStopRouting_Click);

            // 
            // btnShowTables
            // 
            this.btnShowTables.Location = new System.Drawing.Point(620, 80);
            this.btnShowTables.Name = "btnShowTables";
            this.btnShowTables.Size = new System.Drawing.Size(130, 30);
            this.btnShowTables.TabIndex = 8;
            this.btnShowTables.Text = "Показать таблицы";
            this.btnShowTables.UseVisualStyleBackColor = true;
            this.btnShowTables.Click += new System.EventHandler(this.btnShowTables_Click);

            // 
            // btnShowPackets
            // 
            this.btnShowPackets.Location = new System.Drawing.Point(760, 80);
            this.btnShowPackets.Name = "btnShowPackets";
            this.btnShowPackets.Size = new System.Drawing.Size(130, 30);
            this.btnShowPackets.TabIndex = 9;
            this.btnShowPackets.Text = "Показать пакеты";
            this.btnShowPackets.UseVisualStyleBackColor = true;
            this.btnShowPackets.Click += new System.EventHandler(this.btnShowPackets_Click);

            // 
            // listBoxLogs
            // 
            this.listBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLogs.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.ItemHeight = 13;
            this.listBoxLogs.Location = new System.Drawing.Point(12, 590);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(800, 69);
            this.listBoxLogs.TabIndex = 10;

            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLogs.Location = new System.Drawing.Point(818, 590);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(100, 23);
            this.btnClearLogs.TabIndex = 11;
            this.btnClearLogs.Text = "Очистить логи";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);

            // 
            // contextMenuStripVertex
            // 
            this.contextMenuStripVertex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeStartToolStripMenuItem,
            this.makeEndToolStripMenuItem,
            this.deleteVertexToolStripMenuItem});
            this.contextMenuStripVertex.Name = "contextMenuStripVertex";
            this.contextMenuStripVertex.Size = new System.Drawing.Size(190, 70);

            // 
            // makeStartToolStripMenuItem
            // 
            this.makeStartToolStripMenuItem.Name = "makeStartToolStripMenuItem";
            this.makeStartToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.makeStartToolStripMenuItem.Text = "Сделать начальной";
            this.makeStartToolStripMenuItem.Click += new System.EventHandler(this.makeStartToolStripMenuItem_Click);

            // 
            // makeEndToolStripMenuItem
            // 
            this.makeEndToolStripMenuItem.Name = "makeEndToolStripMenuItem";
            this.makeEndToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.makeEndToolStripMenuItem.Text = "Сделать конечной";
            this.makeEndToolStripMenuItem.Click += new System.EventHandler(this.makeEndToolStripMenuItem_Click);

            // 
            // deleteVertexToolStripMenuItem
            // 
            this.deleteVertexToolStripMenuItem.Name = "deleteVertexToolStripMenuItem";
            this.deleteVertexToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.deleteVertexToolStripMenuItem.Text = "Удалить вершину";
            this.deleteVertexToolStripMenuItem.Click += new System.EventHandler(this.deleteVertexToolStripMenuItem_Click);

            // 
            // contextMenuStripEdge
            // 
            this.contextMenuStripEdge.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeWeightToolStripMenuItem,
            this.deleteEdgeToolStripMenuItem});
            this.contextMenuStripEdge.Name = "contextMenuStripEdge";
            this.contextMenuStripEdge.Size = new System.Drawing.Size(153, 48);

            // 
            // changeWeightToolStripMenuItem
            // 
            this.changeWeightToolStripMenuItem.Name = "changeWeightToolStripMenuItem";
            this.changeWeightToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.changeWeightToolStripMenuItem.Text = "Изменить вес";
            this.changeWeightToolStripMenuItem.Click += new System.EventHandler(this.changeWeightToolStripMenuItem_Click);

            // 
            // deleteEdgeToolStripMenuItem
            // 
            this.deleteEdgeToolStripMenuItem.Name = "deleteEdgeToolStripMenuItem";
            this.deleteEdgeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteEdgeToolStripMenuItem.Text = "Удалить ребро";
            this.deleteEdgeToolStripMenuItem.Click += new System.EventHandler(this.deleteEdgeToolStripMenuItem_Click);

            // 
            // contextMenuStripEmpty
            // 
            this.contextMenuStripEmpty.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem});
            this.contextMenuStripEmpty.Name = "contextMenuStripEmpty";
            this.contextMenuStripEmpty.Size = new System.Drawing.Size(167, 26);

            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addVertexToolStripMenuItem.Text = "Добавить вершину";
            this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);

            // 
            // contextMenuStripMatrixHeader
            // 
            this.contextMenuStripMatrixHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexFromMatrixToolStripMenuItem,
            this.deleteVertexFromMatrixToolStripMenuItem});
            this.contextMenuStripMatrixHeader.Name = "contextMenuStripMatrixHeader";
            this.contextMenuStripMatrixHeader.Size = new System.Drawing.Size(181, 48);

            // 
            // addVertexFromMatrixToolStripMenuItem
            // 
            this.addVertexFromMatrixToolStripMenuItem.Name = "addVertexFromMatrixToolStripMenuItem";
            this.addVertexFromMatrixToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addVertexFromMatrixToolStripMenuItem.Text = "Добавить вершину";
            this.addVertexFromMatrixToolStripMenuItem.Click += new System.EventHandler(this.addVertexFromMatrixToolStripMenuItem_Click);

            // 
            // deleteVertexFromMatrixToolStripMenuItem
            // 
            this.deleteVertexFromMatrixToolStripMenuItem.Name = "deleteVertexFromMatrixToolStripMenuItem";
            this.deleteVertexFromMatrixToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteVertexFromMatrixToolStripMenuItem.Text = "Удалить вершину";
            this.deleteVertexFromMatrixToolStripMenuItem.Click += new System.EventHandler(this.deleteVertexFromMatrixToolStripMenuItem_Click);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 680);
            this.Controls.Add(this.btnClearLogs);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.groupBoxRouting);
            this.Controls.Add(this.btnCompareAlgorithms);
            this.Controls.Add(this.btnImportGraph);
            this.Controls.Add(this.btnExportGraph);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddVertex);
            this.Controls.Add(this.btnFindPath);
            this.Controls.Add(this.dataGridViewMatrix);
            this.Controls.Add(this.pictureBoxGraph);
            this.Name = "MainForm";
            this.Text = "Лабораторная работа №3 - Алгоритмы маршрутизации";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMatrix)).EndInit();
            this.groupBoxRouting.ResumeLayout(false);
            this.groupBoxRouting.PerformLayout();
            this.panelAlgorithm.ResumeLayout(false);
            this.panelAlgorithm.PerformLayout();
            this.panelMethod.ResumeLayout(false);
            this.panelMethod.PerformLayout();
            this.contextMenuStripVertex.ResumeLayout(false);
            this.contextMenuStripEdge.ResumeLayout(false);
            this.contextMenuStripEmpty.ResumeLayout(false);
            this.contextMenuStripMatrixHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // -------------------- Объявления элементов управления --------------------
        private System.Windows.Forms.PictureBox pictureBoxGraph;
        private System.Windows.Forms.DataGridView dataGridViewMatrix;
        private System.Windows.Forms.Button btnFindPath;
        private System.Windows.Forms.Button btnAddVertex;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExportGraph;
        private System.Windows.Forms.Button btnImportGraph;
        private System.Windows.Forms.Button btnCompareAlgorithms;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.GroupBox groupBoxRouting;
        private System.Windows.Forms.Panel panelAlgorithm;
        private System.Windows.Forms.Panel panelMethod;
        private System.Windows.Forms.Label labelAlgorithm;
        private System.Windows.Forms.RadioButton radioButtonRandom;
        private System.Windows.Forms.RadioButton radioButtonFlooding;
        private System.Windows.Forms.RadioButton radioButtonExperience;
        private System.Windows.Forms.Label labelMethod;
        private System.Windows.Forms.RadioButton radioButtonVirtual;
        private System.Windows.Forms.RadioButton radioButtonDatagram;
        private System.Windows.Forms.Label labelPackets;
        private System.Windows.Forms.TextBox textBoxPackets;
        private System.Windows.Forms.Label labelTTL;
        private System.Windows.Forms.TextBox textBoxTTL;
        private System.Windows.Forms.Button btnStartRouting;
        private System.Windows.Forms.Button btnStopRouting;
        private System.Windows.Forms.Button btnShowTables;
        private System.Windows.Forms.Button btnShowPackets;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripVertex;
        private System.Windows.Forms.ToolStripMenuItem makeStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeEndToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteVertexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEdge;
        private System.Windows.Forms.ToolStripMenuItem changeWeightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEdgeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEmpty;
        private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMatrixHeader;
        private System.Windows.Forms.ToolStripMenuItem addVertexFromMatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteVertexFromMatrixToolStripMenuItem;
    }
}