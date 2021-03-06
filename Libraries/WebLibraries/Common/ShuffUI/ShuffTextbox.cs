using System;
using System.Runtime.CompilerServices;
using CommonLibraries;
using jQueryApi;
namespace WebLibraries.Common.ShuffUI
{
    public class ShuffTextbox : ShuffElement
    {
        public string Text
        {
            get { return Element.GetValue(); }
            set { TextChanged(new TextChangedEvent(value, false)); }
        }
        public ShuffUIEvent<TextChangedEvent> TextChanged { get; set; }
        protected jQueryObject LabelElement { get; set; }
        public Action OnEnter { get; set; }

        public ShuffTextbox(int x, int y, Number width, Number height, string text = "", string label = null, string labelStyle = null)
        {
            var but = jQuery.Select("<input value='" + ( text ?? "" ) + "' />");
            Element = but;
            but.CSS("position", "absolute");
            Text = text;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Visible = true;

            but.Keyup(a => { if (a.Which == 13) if (OnEnter != null) OnEnter(); });

            if (label != null) {
                ParentChanged += (e) => {
                                     if (e.Parent == null) {
                                         LabelElement.Remove();
                                         LabelElement = null;
                                     } else {
                                         //to LabeledElement
                                         var lbl = jQuery.Select("<span style='" + labelStyle + "'></span>");
                                         LabelElement = lbl;
                                         lbl.Text(label);
                                         Parent.Element.Append(lbl);

                                         lbl.CSS("position", "absolute");
                                         lbl.CSS("left", X - lbl.GetWidth() - 15);
                                         lbl.CSS("top", Y + 2);
                                         ImportedExtensionMethods.DisableSelection(lbl);
                                     }
                                 };
            }
        }

        public override void BindCustomEvents()
        {
            TextChanged += (e) => { if (!e.Live) Element.Value(e.Text); };
        }

        public void Focus()
        {
            Element.Focus();
        }
    }
    public class ShuffTextbox<T> : ShuffTextbox
    {
        [IntrinsicProperty]
        public T Data { get; set; }

        public ShuffTextbox(T data, int x, int y, Number width, Number height, string text = "", string label = null, string labelStyle = null)
                : base(x, y, width, height, text, label, labelStyle)
        {
            Data = data;
        }
    }
}