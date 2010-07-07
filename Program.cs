using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;


namespace XFormTrans
{
    class Program
    {
		public static string ConnStr;
        [STAThread]
        static void Main(string[] args)
        {
            XObject settings = XObject.Load(File.ReadAllText("settings.xml"));
            ConnStr = settings["ConnectionString"];
            //conn.Open();
            //Recordset rs = new Recordset(conn, settings["Query"]);

            /*XmlDocument xd = new XmlDocument();
            xd.LoadXml(File.ReadAllText("form.xml"));
            XForm XForm = new DBoundXForm(xd.DocumentElement, rs);*/
            

           // File.WriteAllText("2.xml", XForm.XMLValue());
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
