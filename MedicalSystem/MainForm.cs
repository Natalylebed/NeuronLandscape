using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ToolStripMenuItem fileItem = new ToolStripMenuItem("Файл");

            fileItem.DropDownItems.Add("Изображение");
            fileItem.DropDownItems.Add(new ToolStripMenuItem("Создать"));
            fileItem.DropDownItems.Add(new ToolStripMenuItem("Выход"));

            File.Items.Add(fileItem);

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("О программе");
            aboutItem.Click += aboutItem_Click;
           File.Items.Add(aboutItem);
        }
        void aboutItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutBox1();
            aboutForm.ShowDialog();


    }
}
        
