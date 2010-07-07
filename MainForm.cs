using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;

namespace XFormTrans
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
		MySqlConnection conn;
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
		protected override void OnClosed (EventArgs e)
		{
			base.OnClosed (e);
			conn.Dispose();
		}

        
        void QuitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
        
        void MainFormLoad(object sender, EventArgs e)
        {
			conn = new MySqlConnection(Program.ConnStr);
			conn.Open();

			MySqlCommand cmd = new MySqlCommand("SHOW TABLES",conn);
			using (MySqlDataReader rdr = cmd.ExecuteReader())
			{
				while (rdr.Read()) 
					lvDbObj.Items.Add(new ListViewItem(rdr.GetString(0),lvDbObj.Groups["grTables"]));
			}
			lvDbObj.ItemActivate += new EventHandler(OnDbObjActivate);
        }
		void OnDbObjActivate(object sender, EventArgs e)
		{
			ListViewItem li = lvDbObj.SelectedItems[0];
			using (MySqlDataReader rdr = new MySqlCommand("SELECT * FROM " + li.Text,conn).ExecuteReader())
			{
				new TableForm(li.Text,rdr).Show();
			}
		}
        
        void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(File.ReadAllText(openFileDialog.FileName));
                DBoundXForm xform = new DBoundXForm(xd.DocumentElement,new Recordset(null,null));
                xform.GetForm().Show();
            }
        }
    }
}
