using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using NewXForms = XFormTrans.NewXForms;

namespace XFormTrans
{
    public class XObject
    {
        public readonly Dictionary<string, string> RawAttrs = new Dictionary<string,string>();
        public string this[string attr]
        {
            get { string val; RawAttrs.TryGetValue(attr, out val); return val; }
        }
        public XObject(XmlNode node)
        {
            foreach (XmlElement cn in node.ChildNodes)
                if (!cn.HasChildNodes || cn.FirstChild.NodeType != XmlNodeType.Element)
                    RawAttrs.Add(cn.Name, cn.InnerText);
        }
        public static XObject Load(string xml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            return new XObject(xd.DocumentElement);
        }

    }
    public abstract class XFormObj: XObject
    {
        protected const int scale = 15;
        public readonly List<XControl> childs = new List<XControl>();
        private Control control;
        private NewXForms.XControl xcontrol;
        public XFormObj(XmlNode node) : base(node) { }
        private void PutToNode(XmlNode node)
        {
            XmlDocument doc = node.OwnerDocument;
            XmlElement n1;
            foreach (KeyValuePair<string, string> attr in RawAttrs)
            {
                node.AppendChild(n1 = doc.CreateElement(attr.Key));
                n1.AppendChild(doc.CreateTextNode(attr.Value));
            }
            foreach (XControl ctl in childs)
            {
                node.AppendChild(n1 = doc.CreateElement("XControl"));
                ctl.PutToNode(n1);
            }
        }
        public string XMLValue()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement n1;
            doc.AppendChild(n1 = doc.CreateElement(this.GetType().Name));
            this.PutToNode(n1);
            return doc.InnerXml;
        }
        protected abstract Control CreateControl();
        protected abstract NewXForms.XControl CreateXControl();
        public Control GetControl()
        {
            return GetXControl().GetControl();
            /*if (control == null)
            {
                control = CreateControl();
                foreach (XControl cctl in childs) control.Controls.Add(cctl.GetControl());
            }
            return control;*/
        }
        public NewXForms.XControl GetXControl()
        {
            if (xcontrol == null)
            {
                xcontrol = CreateXControl();
                foreach (XControl cctl in childs) cctl.GetXControl();
            }
            return xcontrol;
        }

        public IEnumerable<XControl> EnumerateChilds()
        {
            foreach (XControl ctl in childs)
            {
                yield return ctl;
                foreach (XControl cctl in ctl.EnumerateChilds())
                    yield return cctl;
            }
        }
    }
    public class XForm: XFormObj
    {
        public readonly Dictionary<string, XControl> childsdic = new Dictionary<string, XControl>();
        protected override NewXForms.XControl CreateXControl()
        {
            string caption = RawAttrs["Caption"];
            int width = int.Parse(RawAttrs["Width"]) / scale;
            int height = 0;
            foreach (XControl child in childs)
            {
                try {
                    int ctlbottom = int.Parse(child["Top"])+int.Parse(child["Height"]);
                    if (ctlbottom > height) height = ctlbottom;
                } catch (Exception) {
                }
                
            }
            return new NewXForms.XForm(new Size(width, height),caption);
        }
        protected override Control CreateControl()
        {
            Form form = new Form();
            form.Text = RawAttrs["Caption"];
            form.Width = int.Parse(RawAttrs["Width"]) / scale;
            //form.Height = int.Parse(RawAttrs["Height"]) / scale;
            int height = 0;
            foreach (XControl child in childs)
            {
                try {
                    int ctlbottom = int.Parse(child["Top"])+int.Parse(child["Height"]);
                    if (ctlbottom > height) height = ctlbottom;
                } catch (Exception) {
                }
                
            }
            form.Height = height/scale+40;
            return form;
        }
        public XForm(XmlElement node) : base(node)
        {
            XControl obj;
            foreach (XmlElement cn in node.GetElementsByTagName("XControl"))
            {
                obj = new XControl(cn);
                obj.parent = this;
                childsdic.Add(obj.name, obj);
                if (!obj.RawAttrs.ContainsKey("Parent"))
                    childs.Add(obj);
            }
            foreach (XControl ctl in childsdic.Values)
            {
                string par;
                if (ctl.RawAttrs.TryGetValue("Parent", out par))
                {
                    obj = childsdic[par];
                    ctl.parent = obj;
                    obj.childs.Add(ctl);
                }
            }
            foreach (XControl ctl in childsdic.Values)
            {
                try
                {
                    if ("100".Equals(ctl.RawAttrs["ControlType"])
					    && (!"124".Equals(ctl.parent.RawAttrs["ControlType"])))
                    {
                        obj = (XControl)ctl.parent;
                        obj.childs.Remove(ctl);
                        ctl.parent = obj.parent;
                        obj.parent.childs.Add(ctl);
                    }
                } catch (KeyNotFoundException) { }
            }
        }
    }
    public class XControl : XFormObj
    {
        public XFormObj parent;
        public string name;
        static readonly string[] usedattrs = {
            "Caption",
            "Top",
            "Left",
            "Width",
            "Height",
            "Visible",
            "Parent",
        };
        public XControl(XmlNode node) : base(node)
        {   name = RawAttrs["Name"];
            if (RawAttrs.ContainsKey("Top") && RawAttrs.ContainsKey("Left") && RawAttrs.ContainsKey("Width") && RawAttrs.ContainsKey("Height"))
            {
                y = int.Parse(RawAttrs["Top"])/scale;
                x = int.Parse(RawAttrs["Left"])/scale;
                w = int.Parse(RawAttrs["Width"])/scale;
                h = int.Parse(RawAttrs["Height"])/scale;
            }
        }
        public override string ToString()
        {
            return name;
        }
        public int x, y, w, h;
        public Point Location
        {
            get
            {
                XControl p = parent as XControl;
                if (p != null)
                {
                    return new Point(x - p.x, y - p.y);
                } else return new Point(x, y);

            }
        }
        protected override NewXForms.XControl CreateXControl()
        {
            int type = 0;
            string capt = this["ControlType"];
            if (capt != null) int.TryParse(capt, out type);
            capt = this["Caption"];
            NewXForms.XControl ctl;
            Type ctlType;
            string elemname;
            switch (type)
            {
                case 100:
                    ctlType = typeof(Label);
                    elemname = "label";
                    break;
                case 123:
                    ctlType = typeof(TabControl);
                    elemname = "tabcontrol";
                    break;
                case 124:
                    ctlType = typeof(TabPage);
                    elemname = "tabpage";
                    break;
                case 106:
                    ctlType = typeof(CheckBox);
                    elemname = "checkbox";
                    break;
                case 109:
                case 111:
                    ctlType = typeof(TextBox);
                    elemname = "textbox";
                    break;
                case 104:
                    ctlType = typeof(Button);
                    elemname = "button";
                    break;
                default:
                    ctlType = typeof(GroupBox);
                    elemname = "unknown";
                    break;
            }
            Rectangle bounds = new Rectangle(Location, new Size(w+2, h+2));
            capt = capt ?? name;
            ctl = new NewXForms.XSimpleControl(parent.GetXControl(),bounds,capt,bool.Parse(this["Visible"]),ctlType,elemname);
            foreach (KeyValuePair<string,string> tag in RawAttrs)
            {
                if (tag.Value.Equals("")) continue;
                bool isused = false;
                foreach (string s in usedattrs)
                    if (tag.Key.Equals(s)) {
                        isused = true;
                        break;
                    }
                if (!isused) ctl.Tags.Add(tag.Key,tag.Value);
            }
            
            return ctl;
        }
        protected override Control CreateControl()
        {
            int type = 0;
            string capt = this["ControlType"];
            if (capt != null) int.TryParse(capt, out type);
            capt = this["Caption"];
            Control ctl;
            switch (type)
            {
                case 100:
                    ctl = new Label();
                    break;
                case 123:
                    ctl = new TabControl();
                    break;
                case 124:
                    ctl = new TabPage();
                    break;
                case 106:
                    ctl = new CheckBox();
                    break;
                case 109:
                case 111:
                    ctl = new TextBox();
                    break;
                case 104:
                    ctl = new Button();
                    break;
                default:
                    ctl = new GroupBox();
                    ctl.BackColor = Color.Transparent;
                    break;
            }
            ctl.CausesValidation = true;
            ctl.Bounds = new Rectangle(Location, new Size(w+2, h+2));
            try
            {
                ctl.Font = new Font(this["FontName"], float.Parse(this["FontSize"] ?? ""));
            } catch (FormatException) { } catch (OverflowException) { }
            ctl.Text = capt ?? name;
            if ((this["Visible"] ?? "").ToLowerInvariant() == "false") ctl.Visible = false; else ctl.Visible =  true;
            return ctl;
        }
    }
}
