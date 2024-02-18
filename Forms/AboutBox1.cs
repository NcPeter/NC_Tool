using System;

namespace NC_Tool
{
    public sealed partial class AboutBox1
    {
        public AboutBox1()
        {
            InitializeComponent();
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
            // Titel des Formulars fest.
            string ApplicationTitle;
            if (!string.IsNullOrEmpty(My.MyProject.Application.Info.Title))
            {
                ApplicationTitle = My.MyProject.Application.Info.Title;
            }
            else
            {
                ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.MyProject.Application.Info.AssemblyName);
            }
            Text = My.MyProject.Forms.Form1.rm.GetString("String6") + " " + ApplicationTitle;
            // Info
            LabelProductName.Text = My.MyProject.Forms.Form1.rm.GetString("String397") + My.MyProject.Application.Info.ProductName;
            LabelVersion.Text = My.MyProject.Forms.Form1.rm.GetString("String398") + My.MyProject.Application.Info.Version;
            LabelCopyright.Text = My.MyProject.Forms.Form1.rm.GetString("String399");
            LabelCompanyName.Text = My.MyProject.Forms.Form1.rm.GetString("String400") + My.MyProject.Application.Info.CompanyName;
            // Beschreibung
            TextBoxDescription.Text = My.MyProject.Forms.Form1.rm.GetString("String401");
            // Informationen
            TextBoxInfo.Text = My.MyProject.Forms.Form1.rm.GetString("String402");
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}