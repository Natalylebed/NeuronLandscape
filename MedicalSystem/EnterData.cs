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
        private List<TextBox> input = new List<TextBox>();
        public EnterData()
        {
            InitializeComponent();

            var profInfo = typeof(Person).GetProperties();

            for (int i = 0; i <= (profInfo.Length - 1); i++)
            {
                var properti = profInfo[i];
                var textbox = CreatTextBox(i, properti);
                Controls.Add(textbox);
                input.Add(textbox);
            }


        }

        public bool? ShowForm()
        {
            var form = new EnterData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var person = new Person();
                foreach(var texbox in form.input)
                {
                    person.GetType().InvokeMember(texbox.Tag.ToString(),
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, 
                        Type.DefaultBinder, person, new object[] { texbox.Text });
                }
                //var result = Program.systemController.DataNetwork.FeedForward().Output;
                return false;
            }
            return null;
        }
        private void EnterData_Load(object sender, EventArgs e)
        {

        }

        private TextBox CreatTextBox(int number, PropertyInfo property)
        {
            var y = number * 25 + 12;

            var textbox = new TextBox
            {
                Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
                Location = new Point(13, y),
                Name = "textBox" + number,
                Size = new Size(304, 23),
                TabIndex = number,
                Text=property.Name,
                Tag=property.Name,
                Font = new Font("Segoe UI Semibold", 9.75F,  FontStyle.Italic, GraphicsUnit.Point),
                ForeColor = Color.Gray

        };

            textbox.GotFocus += Textbox_Gotfocus;
            textbox.LostFocus += TextBox_LostFocus;

            return textbox;
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            var texbox = sender as TextBox;
            if(texbox.Text=="")
            {
                texbox.Text = texbox.Tag.ToString();
                texbox.Font = new Font("Segoe UI Semibold", 9.75F,FontStyle.Italic, GraphicsUnit.Point);
                texbox.ForeColor = Color.Gray;
            }
        }

        private void Textbox_Gotfocus(object sender, EventArgs e)
        {
            var texbox = sender as TextBox;
            if (texbox.Text == texbox.Tag.ToString())
            {
                texbox.Text = "";
                texbox.Font= new Font("Segoe UI Semibold", 9.75F, (FontStyle.Bold | FontStyle.Italic), GraphicsUnit.Point);
                texbox.ForeColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
           
        }
    }
}
