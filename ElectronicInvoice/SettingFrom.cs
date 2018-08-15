using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ElectronicInvoice
{
    public partial class SettingFrom : Form
    {
        public SettingFrom()
        {
            InitializeComponent();
        }

        private void SettingFrom_Load(object sender, EventArgs e)
        {
           try
            {
                using (FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ElectronicInvoice\\PrintUrl.xml", FileMode.OpenOrCreate))
                {
                    if (file.Length > 0)
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(PrintUrl));
                        var result = xml.Deserialize(file) as PrintUrl;
                        this.txb_url.Text = result.url;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string url = this.txb_url.Text;
                using (FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ElectronicInvoice\\PrintUrl.xml", FileMode.OpenOrCreate))
                {
                    if (file.Length > 0)
                    {
                        file.SetLength(0);
                        file.Seek(0, 0);
                    }
                    XmlSerializer xml = new XmlSerializer(typeof(PrintUrl));
                    PrintUrl p = new PrintUrl()
                    {
                        url = url
                    };
                    xml.Serialize(file, p);
                    MessageBox.Show("修改成功!");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
