using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace RealtickTickerConverter
{
    public partial class MainForm : Form
    {
        KeyboardHook hook = new KeyboardHook();
        public MainForm()
        {
            InitializeComponent();
            // register the event that is fired after the key press.
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(RealtickTickerConverter.ModifierKeys.Control,Keys.T);
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            ClipboardToTicker p = new ClipboardToTicker();
        // show the keys pressed in a label.
	        IDataObject clipData = Clipboard.GetDataObject();
	        string toclipboard;
	        if (clipData.GetDataPresent(DataFormats.Text)) {
                string original = clipData.GetData(DataFormats.Text).ToString();
	            if (p.isClipboardFinancialRelated(clipData.GetData(DataFormats.Text).ToString()))
                {
                    toclipboard = p.generateRealtickTicker(clipData.GetData(DataFormats.Text).ToString());
                    label1.Text = original + " --> " + toclipboard;
                }
	            else {
	                toclipboard = clipData.GetData(DataFormats.Text).ToString();
                    label1.Text = original + " --> NOT a valid ticker";
	            }
	            System.Windows.Forms.Clipboard.SetDataObject(toclipboard, true);
	        }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
