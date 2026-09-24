namespace Lab1_OIvVS
{
    partial class RoutingTablesForm
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
            this.labelNode = new System.Windows.Forms.Label();
            this.comboBoxNodes = new System.Windows.Forms.ComboBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.colDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNextNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHops = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.SuspendLayout();

            // 
            // labelNode
            // 
            this.labelNode.AutoSize = true;
            this.labelNode.Location = new System.Drawing.Point(12, 15);
            this.labelNode.Name = "labelNode";
            this.labelNode.Size = new System.Drawing.Size(40, 13);
            this.labelNode.Text = "Узел:";

            // 
            // comboBoxNodes
            // 
            this.comboBoxNodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNodes.FormattingEnabled = true;
            this.comboBoxNodes.Location = new System.Drawing.Point(60, 12);
            this.comboBoxNodes.Name = "comboBoxNodes";
            this.comboBoxNodes.Size = new System.Drawing.Size(120, 21);
            this.comboBoxNodes.TabIndex = 0;
            this.comboBoxNodes.SelectedIndexChanged += new System.EventHandler(this.comboBoxNodes_SelectedIndexChanged);

            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(12, 45);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(200, 17);
            this.labelTitle.Text = "Таблица маршрутизации";

            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.AllowUserToAddRows = false;
            this.dataGridViewTable.AllowUserToDeleteRows = false;
            this.dataGridViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colNextNode,
                this.colHops});
            this.dataGridViewTable.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.ReadOnly = true;
            this.dataGridViewTable.Size = new System.Drawing.Size(420, 350);
            this.dataGridViewTable.TabIndex = 1;

            // 
            // colNextNode
            // 
            this.colNextNode.HeaderText = "Откуда";
            this.colNextNode.Name = "colNextNode";
            this.colNextNode.Width = 140;

            // 
            // colHops
            // 
            this.colHops.HeaderText = "Счётчик";
            this.colHops.Name = "colHops";
            this.colHops.Width = 80;

            // 
            // RoutingTablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 150);
            this.Controls.Add(this.dataGridViewTable);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.comboBoxNodes);
            this.Controls.Add(this.labelNode);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "RoutingTablesForm";
            this.Text = "Таблицы маршрутизации";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelNode;
        private System.Windows.Forms.ComboBox comboBoxNodes;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNextNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHops;
    }
}