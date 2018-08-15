using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ElectronicInvoice
{
    public partial class helpInfo : Form
    {
        public helpInfo()
        {
            InitializeComponent();
        }

        private void helpInfo_Load(object sender, EventArgs e)
        {
            try
            {
                this.lb_help.Items.Add("添加按钮---点击进入选择需要打印的电子发票文件--点击确认即可添加。\r\n");
                this.lb_help.Items.Add("删除按钮---点击将选择不需要的电子发票进行删除。\r\n");
                this.lb_help.Items.Add("打印按钮---点击将所有已经添加在列表中的所有的电子发票进行全部打印，在对应栏的打印结果将	会显示对应的电子发票打印结果。\r\n");
                this.lb_help.Items.Add("清除按钮---点击将所有已经打印过或还未打印的电子发票进行删除清理。\r\n");
                this.lb_help.Items.Add("打印机序列号---将对应的打印机设备的序列号输入替换，则打印会发送到对应的设备中去。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
