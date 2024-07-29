using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DGVMerge
{
    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;username=root;password=admin");
        public Form1()
        {
            InitializeComponent();
            GetUsers();
            DGVUsers.Paint += new PaintEventHandler(DGVUsers_Paint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void GetUsers()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM dbpos.users",con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dtset = new DataTable();
            adapter.Fill(dtset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dtset;
            DGVUsers.DataSource = bsource;
        }

        private void DGVUsers_Paint(object sender, PaintEventArgs e)
        {
            Rectangle lnameRect = DGVUsers.GetCellDisplayRectangle(3, -1, true);
            Rectangle fnameRect = DGVUsers.GetCellDisplayRectangle(4, -1, true);
            Rectangle mnameRect = DGVUsers.GetCellDisplayRectangle(5, -1, true);

            int combinedWidth = lnameRect.Width + fnameRect.Width + mnameRect.Width;

            Rectangle mergedHeaderRect = new Rectangle(lnameRect.X, lnameRect.Y, combinedWidth, lnameRect.Height);

            e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), mergedHeaderRect);
            e.Graphics.DrawRectangle(Pens.LightGray, mergedHeaderRect);
            e.Graphics.DrawString("FULL NAME", DGVUsers.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, mergedHeaderRect, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
        }
    }
}
