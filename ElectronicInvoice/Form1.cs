using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ElectronicInvoice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string url = "";
        ToolTip tool = new ToolTip();
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string flod = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ElectronicInvoice";
                if (!System.IO.File.Exists(flod))
                    Directory.CreateDirectory(flod);
                using (FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ElectronicInvoice\\PrintNumber.xml", FileMode.OpenOrCreate))
                {
                    if (file.Length > 0)
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(PrintNumber));
                        var result = xml.Deserialize(file) as PrintNumber;
                        this.txb_number.Text = result.number;
                        printNumber = result.number;
                    }
                }
                PrintSet.amend = true;
                System.Timers.Timer ti = new System.Timers.Timer(2000);
                ti.Enabled = true;
                ti.Elapsed += Ti_Elapsed;
                
                
                this.btn_min.Image = this.imageList1.Images[2];
                this.btn_out.Image = this.imageList1.Images[3];
                
                tool.SetToolTip(this.btn_min, "缩小");
                tool.SetToolTip(this.btn_out, "退出");
                tool.SetToolTip(this.btn_setting, "设置");
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.btn_max.Image = this.imageList1.Images[0];
                    tool.SetToolTip(this.btn_max, "放大");
                }else if(this.WindowState == FormWindowState.Maximized)
                {
                    this.btn_max.Image = this.imageList1.Images[1];
                    tool.SetToolTip(this.btn_max, "向下还原");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ti_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (IsHandleCreated)
            {
                if (PrintSet.amend)
                {
                    PrintSet.amend = false;
                    using (FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ElectronicInvoice\\PrintUrl.xml", FileMode.OpenOrCreate))
                    {
                        if (file.Length > 0)
                        {
                            XmlSerializer xml = new XmlSerializer(typeof(PrintUrl));
                            var result = xml.Deserialize(file) as PrintUrl;
                            url = result.url;
                        }else
                        {
                            url = "https://yun.dascomyun.cn/cloudprint/";
                        }

                    }
                }
            }
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                this.btn_max.Image = this.imageList1.Images[1];
                tool.SetToolTip(this.btn_max, "向下还原");
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.btn_max.Image = this.imageList1.Images[0];
                tool.SetToolTip(this.btn_max, "放大");
            }
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int count = 1;
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.ShowDialog();
                if (open.FileName != "")
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = (count).ToString();
                    item.SubItems.Add(open.FileName);
                    item.SubItems.Add(open.SafeFileName);
                    this.lv_img.Items.Add(item);
                    count++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_goOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv_img.Items.Count > 0)
                {
                    if (lv_img.SelectedItems.Count > 0)
                    {
                        int selected = lv_img.SelectedItems[0].Index;
                        lv_img.Items.Remove(lv_img.SelectedItems[0]);
                        int i = 1;
                        foreach (ListViewItem item in lv_img.Items)
                        {
                            item.Text = i.ToString();
                            i++;
                        }
                        if (lv_log.Items.Count >= (selected + 1))
                        {
                            lv_log.Items[selected].Remove();
                        }
                        count--;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            try
            {
                lv_log.Items.Clear();
                lv_img.Items.Clear();
                count = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string printNumber = "";
        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                //https://yun.dascomyun.cn/cloudprint/
                if (this.lv_img.Items.Count > 0)
                {
                    if (this.txb_number.Text == "")
                    {
                        MessageBox.Show("打印机序列号值不能为空,请输入！");
                        return;
                    }
                    var number = txb_number.Text.Trim();
                    if (number != printNumber)
                    {
                        using (FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ElectronicInvoice\\PrintNumber.xml", FileMode.OpenOrCreate))
                        {
                            if (file.Length > 0)
                            {
                                file.SetLength(0);
                                file.Seek(0, 0);
                            }
                            XmlSerializer xml = new XmlSerializer(typeof(PrintNumber));
                            PrintNumber p = new PrintNumber()
                            {
                                number = number
                            };
                            xml.Serialize(file, p);
                            printNumber = number;
                        }
                    }
                    if (lv_log.Items.Count > 0)
                    {
                        //当再次打印时清除所有的打印结果
                        lv_log.Items.Clear();
                    }
                    Uri uri = new Uri(url + "v1.0/print/" + number);
                    for (int i = 0; i < lv_img.Items.Count; i++)
                    {
                        var conect = lv_img.Items[i].SubItems[1].Text.Split('.');
                        string filetype = "";
                        if (conect.Length > 0)
                        {
                            filetype = conect[conect.Length - 1];
                            filetype = filetype.ToLower();
                        }
                        var request = WebRequest.Create(uri);
                        request.Method = "Post";
                        request.Headers.Add("File-Type", "application/" + filetype);
                        request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                        var guid = Guid.NewGuid().ToString("N");
                        string strbound = "---" + guid;
                        request.ContentType = "multipart/form-data;boundary=" + strbound;
                        formDataType fd = new formDataType(strbound);
                        string filenumber = fd.formDataAppendName("file", lv_img.Items[i].SubItems[2].Text, "application/" + filetype);
                        byte[] data = Encoding.GetEncoding("utf-8").GetBytes(filenumber);
                        //发送formdata数据
                        using (Stream stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                            data = System.IO.File.ReadAllBytes(lv_img.Items[i].SubItems[1].Text);
                            stream.Write(data, 0, data.Length);
                            data = Encoding.UTF8.GetBytes(fd.formDataAppendName());
                            stream.Write(data, 0, data.Length);
                        }

                        var response = request.GetResponse() as HttpWebResponse;
                        using (var strR = new StreamReader(response.GetResponseStream()))
                        {
                            string json = strR.ReadToEnd();
                            JObject str = JObject.Parse(json);
                            string codePrint = (string)str["code"];
                            var jds = str["data"];
                            ListViewItem itemv = new ListViewItem();
                            switch (codePrint)
                            {
                                case "0":
                                    string it = "";
                                    foreach (var item in jds)
                                    {
                                        it += item + ";";
                                    }
                                    itemv.Text = it;
                                    break;
                                case "1000":
                                    itemv.Text = "服务打印失败";
                                    break;
                                case "1001":
                                    itemv.Text = "文件类型错误";
                                    break;
                                case "1002":
                                    itemv.Text = "缺少请求头";
                                    break;
                                case "1003":
                                    itemv.Text = "源文件为空";
                                    break;
                                case "1100":
                                    itemv.Text = "设备不存在";
                                    break;
                                case "1101":
                                    itemv.Text = "设备繁忙，被占用";
                                    break;
                                case "1102":
                                    itemv.Text = "设备状态错误";
                                    break;
                                case "1103":
                                    itemv.Text = "设备状态不能打印";
                                    break;
                                case "1104":
                                    itemv.Text = "设备暂停打印";
                                    break;
                                case "1105":
                                    itemv.Text = "设备不在工作时间";
                                    break;
                                case "1106":
                                    itemv.Text = "设备信息有误";
                                    break;
                                case "1121":
                                    itemv.Text = "转换服务错误";
                                    break;
                                default:
                                    itemv.Text = "未知状态码" + codePrint;
                                    break;
                            }
                            lv_log.Items.Add(itemv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            try
            {
                helpInfo h = new helpInfo();
                h.Owner = this;
                h.StartPosition = FormStartPosition.CenterParent;
                h.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        Point p = new Point(0, 0);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                p = e.Location;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Point up = e.Location;
                Point move = new Point(up.X - p.X, up.Y - p.Y);
                Point ld = this.Location;
                this.Location = new Point(ld.X + move.X, ld.Y + move.Y);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_help_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.lb_color.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_help_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.lb_color.ForeColor = Color.FromArgb(128, 128, 255);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            try
            {
                SettingFrom s = new SettingFrom();
                s.Owner = this;
                s.StartPosition = FormStartPosition.CenterParent;
                s.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_setting_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.lb_color2.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_setting_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.lb_color2.ForeColor = Color.FromArgb(128, 128, 255);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public class PrintNumber
    {
        public string number { get; set; }
    }
    public class PrintUrl
    {
        public string url { get; set; }
    }
    public class PrintSet
    {
        public static bool amend { get; set; }
    }
}
