using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XFormTrans
{
    class DBoundXForm : XForm
    {
        /*struct Binding
        {
            public string ctlSource;
            public XControl ctl;
            public Binding(string ctlSource, XControl ctl)
            {
                this.ctlSource = ctlSource;
                this.ctl = ctl;
            }
        }
        List<Binding> bindings = new List<Binding>();*/
        bool bind = false;
        Dictionary<Control, XControl> ctls = new Dictionary<Control,XControl>();
        Form form;
        Recordset rs;
        public Form GetForm()
        {
            return form;
        }
        private void BuildForm()
        {
            form = (Form)GetControl();
            foreach (XControl ctl in EnumerateChilds())
            {
                if (ctl.GetControl() is GroupBox) ctl.GetControl().SendToBack();
                //if (ctl.GetControl() is Label) ctl.GetControl().BringToFront();
                /*string src = ctl["ControlSource"];
                if (!string.IsNullOrEmpty(src) && !src.StartsWith("="))
                {
                    //bindings.Add(new Binding(src,ctl));*/
                ctls[ctl.GetControl()] = ctl;
                InstallEvents(ctl.GetControl());
                //}
            }
            if (rs.BOF) rs.MoveFirst();
            UpdateBindings();
            CreateNavigationPanel();
            
        }
        public DBoundXForm(XmlElement node, IDbConnection conn) : base(node)
        {
            string recordSource = this["RecordSource"];
            if (string.IsNullOrEmpty(recordSource)) {
                recordSource = "SELECT NULL";
            }
            if (!recordSource.StartsWith("SELECT")) {
                recordSource = "SELECT * FROM [" + recordSource + "]";
            }
            this.rs = new Recordset(conn, recordSource);
            BuildForm();
        }
        public DBoundXForm(XmlElement node, Recordset rs) : base(node)
        {
            this.rs = rs;
            BuildForm();
        }

        Dictionary<object, MethodInvoker> actionsMap = new Dictionary<object,MethodInvoker>();

        private Button CreateNavButton(int x, int y, int w, int h, string name, MethodInvoker action)
        {
            Button btn = new Button();
            btn.Bounds = new Rectangle(x, y, w, h);
            btn.Text = name;
            actionsMap[btn] = action;
            btn.Click += new EventHandler(OnNavButtonClick);
            return btn;
        }

        private void CreateNavigationPanel()
        {
            Panel pnl = new Panel();
            pnl.Bounds = new Rectangle(0, form.ClientSize.Height - 32, form.ClientSize.Width, 32);
            pnl.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

            pnl.Controls.Add(CreateNavButton(0  , 0, 64, 32, "<<<<", new MethodInvoker(rs.MoveFirst)));
            pnl.Controls.Add(CreateNavButton(64 , 0, 64, 32, "Prev", new MethodInvoker(rs.MovePrev)));
            pnl.Controls.Add(CreateNavButton(128, 0, 64, 32, "Next", new MethodInvoker(rs.MoveNext)));
            pnl.Controls.Add(CreateNavButton(192, 0, 64, 32, ">>>>", new MethodInvoker(rs.MoveLast)));
            form.Controls.Add(pnl);
            pnl.BringToFront();
        }

        void OnNavButtonClick(object sender, EventArgs e)
        {
            actionsMap[sender]();
            UpdateBindings();
        }

        private void UpdateBindings()
        {
            if (bind)
                throw new InvalidOperationException("Re-entered in UpdateBindings!");
            bind = true;
            foreach (KeyValuePair<Control, XControl> ctl in ctls)
            {
				if (string.IsNullOrEmpty(ctl.Value["ControlSource"])) continue;
                try
                {
                    ctl.Key.Text = string.Concat(rs[ctl.Value["ControlSource"]]);
                }
                catch (KeyNotFoundException) { }
            }
            bind = false;
        }
        private void InstallEvents(Control ctl)
        {
            ctl.TextChanged += new EventHandler(OnControlTextChanged);
            ctl.Validating += new CancelEventHandler(OnControlValidating);
            ctl.KeyPress += new KeyPressEventHandler(OnControlKeyPress);
        }

        void OnControlKeyPress(object sender, KeyPressEventArgs e)
        {
            if (bind) return;
            if (e.KeyChar != '\r') return;
            e.Handled = true;
        }
        private void OnControlTextChanged(object sender,EventArgs e)
        {
            if (bind) return;
			//TODO: update the recordset
        }
        private void OnControlValidating(object sender, CancelEventArgs e)
        {
            if (bind) return;
			//TODO: update the recordset
        }

    }
}
