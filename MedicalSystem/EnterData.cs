using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem
{
    public partial class EnterData : Form
    {
        public EnterData()
        {
            InitializeComponent();
            var profInfo = typeof(Person).GetProperties(); 
            for(int i = 0; i <= (profInfo.Length-1); i++)
            {
                var properti = profInfo[i];
                var textbox = CreatTextBox(i,properti);
                Controls.Add(textbox);
            }

             
        }

        public bool? ShowForm()
        {
            var form = new EnterData();
            if(form.ShowDialog()==DialogResult.OK)
            {
                var person = new Person();
                var result = Program.systemController.DataNetwork.FeedForward().Output;
               return result == 1.0;
            }
            return null;
        }
        private void EnterData_Load(object sender, EventArgs e)
        {

        }

        private TextBox CreatTextBox (int number, PropertyInfo property)
        {
            var y = number * 25+12;

            var textbox = new TextBox();
            {
               Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
               Location = new Point(13, y);
               Name = "textBox" + number;
               Size = new Size(304, 23);
               TabIndex = number;
            }

            return textbox;
        }

    }
}
