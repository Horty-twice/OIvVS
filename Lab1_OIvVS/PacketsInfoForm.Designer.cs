namespace Lab1_OIvVS
{
    partial class PacketsInfoForm
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
            this.dataGridViewPackets = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTTL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeSpent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackets)).BeginInit();
            this.SuspendLayout();

            // 
            // dataGridViewPackets
            // 
            this.dataGridViewPackets.AllowUserToAddRows = false;
            this.dataGridViewPackets.AllowUserToDeleteRows = false;
            this.dataGridViewPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPackets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPackets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colSource,
                this.colDestination,
                this.colSize,
                this.colPath,
                this.colStatus,
                this.colTTL,
                this.colTimeSpent});
            this.dataGridViewPackets.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewPackets.Name = "dataGridViewPackets";
            this.dataGridViewPackets.ReadOnly = true;
            this.dataGridViewPackets.Size = new System.Drawing.Size(876, 400);
            this.dataGridViewPackets.TabIndex = 0;

            // 
            // colId
            // 
            this.colId.HeaderText = "№";
            this.colId.Name = "colId";
            this.colId.Width = 40;

            // 
            // colSource
            // 
            this.colSource.HeaderText = "Откуда";
            this.colSource.Name = "colSource";
            this.colSource.Width = 60;

            // 
            // colDestination
            // 
            this.colDestination.HeaderText = "Куда";
            this.colDestination.Name = "colDestination";
            this.colDestination.Width = 60;

            // 
            // colSize
            // 
            this.colSize.HeaderText = "Размер (байт)";
            this.colSize.Name = "colSize";
            this.colSize.Width = 100;

            // 
            // colPath
            // 
            this.colPath.HeaderText = "Маршрут";
            this.colPath.Name = "colPath";
            this.colPath.Width = 280;

            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Статус";
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 90;

            // 
            // colTTL
            // 
            this.colTTL.HeaderText = "TTL";
            this.colTTL.Name = "colTTL";
            this.colTTL.Width = 90;

            // 
            // colTimeSpent
            // 
            this.colTimeSpent.HeaderText = "Время в пути (сек)";
            this.colTimeSpent.Name = "colTimeSpent";
            this.colTimeSpent.Width = 110;

            // 
            // PacketsInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 424);
            this.Controls.Add(this.dataGridViewPackets);
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "PacketsInfoForm";
            this.Text = "Сведения о пакетах";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPackets)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dataGridViewPackets;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTTL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeSpent;
    }
}