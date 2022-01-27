using NeuronLandscape;
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


            ToolStripMenuItem ImagefileMenuItem = new ToolStripMenuItem("Проверить изображение");
            fileItem.DropDownItems.Add(ImagefileMenuItem);
            ImagefileMenuItem.Click += ImagefileMenuItem_Clik;
            
            fileItem.DropDownItems.Add(new ToolStripMenuItem("Ввести данные"));
           
            fileItem.DropDownItems.Add(new ToolStripMenuItem("Выход"));

            File.Items.Add(fileItem);

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("О программе");
            aboutItem.Click += aboutItem_Click;
            File.Items.Add(aboutItem);
        }

        private void ImagefileMenuItem_Clik(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var pathimage = openFileDialog.FileName;
                var pictureconvert = new ConvertPicture();
                var input=pictureconvert.ConvertInPixel(pathimage);

                var result=Program.systemController.ImageNetwork.FeedForward(input.ToArray()).Output;

            }
           
        }

        private void aboutItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutBox1();
            aboutForm.ShowDialog();


        }


    }
}


