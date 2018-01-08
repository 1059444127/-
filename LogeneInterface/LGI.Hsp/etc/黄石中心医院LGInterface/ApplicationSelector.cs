using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using LGInterface.Model;

namespace LGInterface
{
    public partial class ApplicationSelector : Form
    {
        public List<T_SQD> ListSqd { get; set; }
        public T_SQD SelectedSqd { get; set; } = null;

        public event Action<T_SQD> ItemSelected;

        public ApplicationSelector()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            T_SQD sqd = (T_SQD)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            OnItemSelected(sqd);
        }

        protected virtual void OnItemSelected(T_SQD obj)
        {
            SelectedSqd = obj;
            ItemSelected?.Invoke(obj);
            Close();
        }

        private void ApplicationSelector_Load(object sender, EventArgs e)
        {
            if (ListSqd == null || ListSqd.Count == 0)
            {
                MessageBox.Show("没有查询到任何申请项目!");
                Close();
            }
            else
            {
                dataGridView1.DataSource = ListSqd;
                dataGridView1.AutoResizeColumns();
            }
        }

        private void ApplicationSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
