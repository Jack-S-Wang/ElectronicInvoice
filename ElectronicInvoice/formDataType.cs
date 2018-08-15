using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicInvoice
{
    class formDataType
    {
        private string gud = "";
        /// <summary>
        /// 数据格式（form-data）
        /// </summary>
        /// <param name="guid">必须传入正规的格式如：--fasfdsg，开头加几个横杠才行</param>
        public formDataType(string guid)
        {
            this.gud = guid;
        }
        /// <summary>
        /// 添加文件信息的表数据格式(form-data)
        /// </summary>
        /// <param name="name">标识名</param>
        /// <param name="filename">文件名称（也可随便定义一个假名，但必须得定义）</param>
        /// <param name="contentType">文件格式定义名称（如：image/jpg或text/plain或text/html）</param>
        /// <returns></returns>
        public string formDataAppendName(string name,string filename,string contentType)
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("\r\n--{0}\r\n", gud);
                strBuild.AppendFormat("Content-Disposition: form-data; name=\"{0}\"" + "; filename=\"{1}\"\r\n",name,filename);
                strBuild.AppendFormat("Content-Type:{0}\r\n",contentType);
                strBuild.AppendLine("");
                return strBuild.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 添加简单的字符串格式内容(form-data)
        /// </summary>
        /// <param name="name">标识名</param>
        /// <param name="value">内容信息</param>
        /// <returns></returns>
        public string formDataAppendName(string name,string value)
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.AppendFormat("\r\n--{0}\r\n", gud);
                strBuild.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n",name);
                strBuild.AppendLine("");
                strBuild.AppendFormat("{0}",value);
                return strBuild.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加表数据格式的结尾信息(form-data)
        /// </summary>
        /// <returns></returns>
        public string formDataAppendName()
        {
            try
            {
                StringBuilder strBuild = new StringBuilder();
                strBuild.Append("\r\n--" + gud + "--\r\n");
                return strBuild.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
