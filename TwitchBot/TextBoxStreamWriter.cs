using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;

namespace TwitchBot
{
    class TextBoxStreamWriter : TextWriter
    {
        RichTextBox textBox = null;

        public TextBoxStreamWriter(RichTextBox output)
        {
            textBox = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            textBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (value == '\n')
                    return;
                else
                {
                    textBox.AppendText(value.ToString());
                    textBox.ScrollToEnd();
                }
            }));
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
