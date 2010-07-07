using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XFormTrans
{
	public class TableForm : Form
	{
        public TableForm (string name, IDataReader rdr)
		{
			DataGridView dgv = new DataGridView();
			dgv.Dock = DockStyle.Fill;
			DataGridViewTextBoxCell template = new DataGridViewTextBoxCell();
			template.Style.Font = new Font("DejaVu Sans",10);
			for (int i = 0;i < rdr.FieldCount;i++)
			{
				DataGridViewColumn col = new DataGridViewColumn(template);
				col.Name = rdr.GetName(i);
				dgv.Columns.Add(col);
			}
			while (rdr.Read())
			{
				DataGridViewRow row = new DataGridViewRow();
				row.CreateCells(dgv);
    			for (int i = 0;i < rdr.FieldCount;i++)
				{
					object val = rdr[i];
					if (val is byte[])
					{
						val = Encoding.UTF8.GetString((byte[])val);
					}
					row.Cells[i].Value = val;
				}
				dgv.Rows.Add(row);
			}
			
			Controls.Add(dgv);
		}
	}
}
