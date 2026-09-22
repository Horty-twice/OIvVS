namespace Lab1_OIvVS
{
    partial class ComparisonForm
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
            this.dataGridViewDijkstra = new System.Windows.Forms.DataGridView();
            this.dataGridViewFloyd = new System.Windows.Forms.DataGridView();
            this.labelDijkstraTime = new System.Windows.Forms.Label();
            this.labelFloydTime = new System.Windows.Forms.Label();
            this.labelDijkstraRuns = new System.Windows.Forms.Label();
            this.labelFloydRuns = new System.Windows.Forms.Label();
            this.labelRunsHeader = new System.Windows.Forms.Label();
            this.labelTitleDijkstra = new System.Windows.Forms.Label();
            this.labelTitleFloyd = new System.Windows.Forms.Label();
            this.colFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFrom2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDist2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDijkstra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFloyd)).BeginInit();
            this.SuspendLayout();

            // 
            // labelTitleDijkstra
            // 
            this.labelTitleDijkstra.AutoSize = true;
            this.labelTitleDijkstra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelTitleDijkstra.Location = new System.Drawing.Point(12, 9);
            this.labelTitleDijkstra.Name = "labelTitleDijkstra";
            this.labelTitleDijkstra.Size = new System.Drawing.Size(150, 17);
            this.labelTitleDijkstra.Text = "Алгоритм Дейкстры";

            // 
            // labelTitleFloyd
            // 
            this.labelTitleFloyd.AutoSize = true;
            this.labelTitleFloyd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelTitleFloyd.Location = new System.Drawing.Point(480, 9);
            this.labelTitleFloyd.Name = "labelTitleFloyd";
            this.labelTitleFloyd.Size = new System.Drawing.Size(130, 17);
            this.labelTitleFloyd.Text = "Алгоритм Флойда";

            // 
            // labelDijkstraTime
            // 
            this.labelDijkstraTime.AutoSize = true;
            this.labelDijkstraTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelDijkstraTime.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelDijkstraTime.Location = new System.Drawing.Point(12, 35);
            this.labelDijkstraTime.Name = "labelDijkstraTime";
            this.labelDijkstraTime.Size = new System.Drawing.Size(120, 15);
            this.labelDijkstraTime.Text = "Дейкстра: 0.000 мс";

            // 
            // labelFloydTime
            // 
            this.labelFloydTime.AutoSize = true;
            this.labelFloydTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelFloydTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelFloydTime.Location = new System.Drawing.Point(480, 35);
            this.labelFloydTime.Name = "labelFloydTime";
            this.labelFloydTime.Size = new System.Drawing.Size(100, 15);
            this.labelFloydTime.Text = "Флойд: 0.000 мс";

            // 
            // dataGridViewDijkstra
            // 
            this.dataGridViewDijkstra.AllowUserToAddRows = false;
            this.dataGridViewDijkstra.AllowUserToDeleteRows = false;
            this.dataGridViewDijkstra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewDijkstra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDijkstra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFrom,
            this.colTo,
            this.colPath,
            this.colDist});
            this.dataGridViewDijkstra.Location = new System.Drawing.Point(12, 60);
            this.dataGridViewDijkstra.Name = "dataGridViewDijkstra";
            this.dataGridViewDijkstra.ReadOnly = true;
            this.dataGridViewDijkstra.Size = new System.Drawing.Size(450, 400);
            this.dataGridViewDijkstra.TabIndex = 0;

            // 
            // dataGridViewFloyd
            // 
            this.dataGridViewFloyd.AllowUserToAddRows = false;
            this.dataGridViewFloyd.AllowUserToDeleteRows = false;
            this.dataGridViewFloyd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewFloyd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFloyd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFrom2,
            this.colTo2,
            this.colPath2,
            this.colDist2});
            this.dataGridViewFloyd.Location = new System.Drawing.Point(480, 60);
            this.dataGridViewFloyd.Name = "dataGridViewFloyd";
            this.dataGridViewFloyd.ReadOnly = true;
            this.dataGridViewFloyd.Size = new System.Drawing.Size(450, 400);
            this.dataGridViewFloyd.TabIndex = 1;

            // 
            // colFrom
            // 
            this.colFrom.HeaderText = "Откуда";
            this.colFrom.Name = "colFrom";
            this.colFrom.Width = 60;

            // 
            // colTo
            // 
            this.colTo.HeaderText = "Куда";
            this.colTo.Name = "colTo";
            this.colTo.Width = 60;

            // 
            // colPath
            // 
            this.colPath.HeaderText = "Путь";
            this.colPath.Name = "colPath";
            this.colPath.Width = 200;

            // 
            // colDist
            // 
            this.colDist.HeaderText = "Длина";
            this.colDist.Name = "colDist";
            this.colDist.Width = 60;

            // 
            // colFrom2
            // 
            this.colFrom2.HeaderText = "Откуда";
            this.colFrom2.Name = "colFrom2";
            this.colFrom2.Width = 60;

            // 
            // colTo2
            // 
            this.colTo2.HeaderText = "Куда";
            this.colTo2.Name = "colTo2";
            this.colTo2.Width = 60;

            // 
            // colPath2
            // 
            this.colPath2.HeaderText = "Путь";
            this.colPath2.Name = "colPath2";
            this.colPath2.Width = 200;

            // 
            // colDist2
            // 
            this.colDist2.HeaderText = "Длина";
            this.colDist2.Name = "colDist2";
            this.colDist2.Width = 60;

            // 
            // labelRunsHeader
            // 
            this.labelRunsHeader.AutoSize = true;
            this.labelRunsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelRunsHeader.Location = new System.Drawing.Point(12, 475);
            this.labelRunsHeader.Name = "labelRunsHeader";
            this.labelRunsHeader.Size = new System.Drawing.Size(120, 15);
            this.labelRunsHeader.Text = "Количество запусков:";

            // 
            // labelDijkstraRuns
            // 
            this.labelDijkstraRuns.AutoSize = true;
            this.labelDijkstraRuns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelDijkstraRuns.Location = new System.Drawing.Point(150, 475);
            this.labelDijkstraRuns.Name = "labelDijkstraRuns";
            this.labelDijkstraRuns.Size = new System.Drawing.Size(30, 15);
            this.labelDijkstraRuns.Text = "0";

            // 
            // labelFloydRuns
            // 
            this.labelFloydRuns.AutoSize = true;
            this.labelFloydRuns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.labelFloydRuns.Location = new System.Drawing.Point(500, 475);
            this.labelFloydRuns.Name = "labelFloydRuns";
            this.labelFloydRuns.Size = new System.Drawing.Size(30, 15);
            this.labelFloydRuns.Text = "1";

            // 
            // ComparisonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 520);
            this.Controls.Add(this.labelFloydRuns);
            this.Controls.Add(this.labelDijkstraRuns);
            this.Controls.Add(this.labelRunsHeader);
            this.Controls.Add(this.dataGridViewFloyd);
            this.Controls.Add(this.dataGridViewDijkstra);
            this.Controls.Add(this.labelFloydTime);
            this.Controls.Add(this.labelDijkstraTime);
            this.Controls.Add(this.labelTitleFloyd);
            this.Controls.Add(this.labelTitleDijkstra);
            this.Name = "ComparisonForm";
            this.Text = "Сравнение алгоритмов Дейкстры и Флойда";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDijkstra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFloyd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dataGridViewDijkstra;
        private System.Windows.Forms.DataGridView dataGridViewFloyd;
        private System.Windows.Forms.Label labelDijkstraTime;
        private System.Windows.Forms.Label labelFloydTime;
        private System.Windows.Forms.Label labelDijkstraRuns;
        private System.Windows.Forms.Label labelFloydRuns;
        private System.Windows.Forms.Label labelRunsHeader;
        private System.Windows.Forms.Label labelTitleDijkstra;
        private System.Windows.Forms.Label labelTitleFloyd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrom2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDist2;
    }
}