using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lab1_OIvVS
{
    public partial class PacketsInfoForm : Form
    {
        public PacketsInfoForm()
        {
            InitializeComponent();
        }

        public void RefreshPackets(List<Packet> packets)
        {
            dataGridViewPackets.Rows.Clear();
            if (packets == null) return;

            foreach (var p in packets)
            {
                string status = p.IsDelivered() ? "Доставлен" :
                                p.IsDuplicate() ? "Дубликат" :
                                p.IsExpired() ? "Истёк" :
                                p.IsDropped() ? "Отброшен" :
                                                  "В пути";

                string pathStr = p.GetPathString();
                if (pathStr.Length > 60) pathStr = pathStr.Substring(0, 57) + "...";

                dataGridViewPackets.Rows.Add(
                    p.Id,
                    p.Source.Name,
                    p.Destination.Name,
                    p.Size,
                    pathStr,
                    status,
                    p.TimeToLive.ToString("F1"),
                    p.TimeSpent.ToString("F1")
                );
            }
        }
    }
}