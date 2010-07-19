using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XFormTrans.NewXForms
{
    public abstract class XControl
    {
        protected XControl parent;
        protected Rectangle bounds;
        protected string caption;
        protected bool visible;
        
        private readonly List<XControl> children = new List<XControl>();
        private readonly Dictionary<string, string> tags = new Dictionary<string, string>();
        private Control control;
        
        protected XControl(XControl parent, Rectangle bounds, string caption, bool visible)
        {
            this.parent = parent;
            this.bounds = bounds;
            this.caption = caption;
            this.visible = visible;
            if (parent != null) {
                parent.children.Add(this);
            }
        }
        protected abstract Control CreateControl();
        public Control GetControl()
        {
            if (control == null)
            {
                control = CreateControl();
                foreach (XControl cctl in children) control.Controls.Add(cctl.GetControl());
            }
            return control;
        }
        public abstract string ElementName { get; }
        public virtual string Caption { get { return caption; } }
        public virtual Rectangle Bounds
        { 
            get
            {
                if (control != null) return control.Bounds;
                return bounds;
            }
        }
        public virtual bool Visible
        { 
            get
            {
                if (control != null) return control.Visible;
                return visible;
            }
        }
        public Dictionary<string, string> Tags { get { return tags; } }
        public XControl Parent { get { return parent; } }
        public XmlElement ToXml(XmlDocument doc)
        {
            XmlElement n = doc.CreateElement(ElementName);
            caption = Caption;
            bounds = Bounds;
            visible = Visible;
            n.SetAttribute("x",bounds.X.ToString());
            n.SetAttribute("y",bounds.Y.ToString());
            n.SetAttribute("w",bounds.Width.ToString());
            n.SetAttribute("h",bounds.Height.ToString());
            n.SetAttribute("text",caption);
            n.SetAttribute("visible",visible.ToString().ToLower());
            foreach (KeyValuePair<string,string> tag in tags)
            {
                XmlElement n2 = doc.CreateElement("tag");
                n2.SetAttribute("k",tag.Key);
                n2.SetAttribute("v",tag.Value);
                n.AppendChild(n2);
            }
            foreach (XControl ctl in children)
            {
                n.AppendChild(ctl.ToXml(doc));
            }
            return n;
        }
        public IEnumerable<XControl> EnumerateChildren()
        {
            foreach (XControl ctl in children)
            {
                yield return ctl;
                foreach (XControl cctl in ctl.EnumerateChildren())
                    yield return cctl;
            }
        }
    }
    public class XForm : XControl
    {
        public XForm(Size size, string caption)
            : base(null,new Rectangle(0,0,size.Width,size.Height),caption,true)
        {
        }
        public Form CreateForm()
        {
            Form form = new Form();
            form.Text = caption;
            form.Size = bounds.Size;
            return form;
        }
        protected override Control CreateControl()
        {
            return CreateForm();
        }
        public override string ElementName { get { return "form"; }}
    }
    public class XSimpleControl : XControl
    {
        readonly Type type;
        readonly string elementName;
        public XSimpleControl(XControl parent, Rectangle bounds, string caption, bool visible, Type type, string elementName)
            : base(parent,bounds,caption,visible)
        {
            if (!type.IsSubclassOf(typeof(Control)))
                throw new ArgumentException("'type' must be subclass of Control","type");
            this.type = type;
            this.elementName = elementName;
        }
        protected override Control CreateControl()
        {
            Control ctl = (Control)Activator.CreateInstance(type);
            ctl.Bounds = bounds;
            ctl.Text = caption;
            ctl.Visible = visible;
            return ctl;
        }
        public override string ElementName { get { return elementName; }}
    }
}
