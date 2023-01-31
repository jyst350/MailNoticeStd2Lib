using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace MailNoticeStd2Lib
{

    /// <summary>
    /// 邮件参数配置类。
    /// </summary>
    [JsonObject]
    public class MailConfig
    {
        /// <summary>
        /// 读取根目录下的配置文件
        /// </summary>
        /// <returns></returns>
        public MailConfig Read(string fileName)
        {
            try
            {
                
                using (var sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject<MailConfig>(sr.ReadToEnd());
                }
            }
            catch
            {
                return new MailConfig();
            }
        }
        /// <summary>
        /// 将配置写入指定文件。
        /// </summary>
        /// <param name="fileName">文件名。</param>
        /// <returns></returns>
        public void Write(string fileName)
        {
            using (var sw = new StreamWriter(fileName,false,Encoding.UTF8))
            {
                var config = JsonConvert.SerializeObject(this,Formatting.Indented);
                sw.WriteLine(config);
            }
        }
        ///<summary>
        ///邮件发送者。
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("邮件发送者")]
        public string MailSender { get; set; }

        ///<summary>
        ///密码。
        ///</summary>
        
        [JsonProperty]
        [Category("邮件参数"), Description("密码"), PasswordPropertyText(true)]
        public string MailSenderPassword { get; set; }

        ///<summary>
        ///邮件接收者列表。
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("邮件接收者列表")]
        public string[] MailReceivers { get; set; }

        ///<summary>
        ///SMTP服务器地址。
        ///</summary>
        [JsonProperty]
        [Category("服务器参数"), Description("SMTP服务器地址")]
        public string SmtpServerHost { get; set; }

        ///<summary>
        ///SMTP服务器端口。
        ///<para>默认使用587端口。</para>
        ///</summary>
        [JsonProperty]
        [Category("服务器参数"), Description("SMTP服务器端口\r\n默认:25")]
        public int SmtpServerPort { get; set; } = 25;

        ///<summary>
        ///是否指示使用SSL/TLS进行安全连接。
        ///<para>默认为True。</para>
        ///</summary>
        [JsonProperty]
        [Category("服务器参数"), Description("指示服务器是否使用SSL/TLS进行安全连接\r\n默认:False")]
        public bool SecurityConnection { get; set; } = false;

        ///<summary>
        ///发信人显示名称。
        ///<para>默认显示为:MailNotice。</para>
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("发信人显示名称\r\n默认:MailNotice")]
        public string DisplayName { get; set; } = "MailNotice";

        ///<summary>
        ///邮件正文的页头信息。
        ///<para>默认显示为:MailNotice。</para>
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("邮件正文的页头信息\r\n默认:MailNotice")]
        public string DisplayHeaderInfo { get; set; } = "MailNotice";

        ///<summary>
        ///邮件正文的页脚信息。
        ///<para>默认显示为:Powered by MailNotice。</para>
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("邮件正文的页脚信息\r\n默认:Powered by MailNotice")]
        public string DisplayFooterInfo { get; set; } = "Powered by MailNotice";

        /// <summary>
        ///邮件编码格式。
        ///<para>默认显示为:UTF8。</para>
        /// </summary>
        [JsonIgnore]
        [Category("邮件参数"), Description("邮件所有上下文的编码格式\r\n默认:UTF8")]
        public Encoding MailEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 邮件模板文件(如未指定,将使用内置模板)。
        /// </summary>
        [JsonIgnore]
        [Category("邮件参数"), Description("邮件模板文件(如未指定,将使用内置模板)。")]
        public string EmailTemplateFile { get; set; } = Resource.EmailTemplate;

        /// <summary>
        /// 标题背景颜色(16进制)。
        /// <para>默认为:4994CE。</para>
        /// </summary>
        [JsonProperty]
        [Category("邮件参数"), Description("标题背景颜色(16进制)。")]
        public string TitleColor { get; set; } = "4994CE";

        /// <summary>
        /// 附件文件列表。
        /// </summary>
        [JsonProperty]
        public string[] AttachmentFileList { get; set; }

        /// <summary>
        /// 是否验证凭据。
        /// <para>默认为:true。</para>
        /// </summary>
        [JsonProperty]
        public bool VerifyCredentials { get; set; } = true;

        /// <summary>
        /// 发送超时。
        /// <para>默认为:60000。</para>
        /// </summary>
        [JsonProperty]
        public int SendTimeout { get; set; } = 60000;

        /// <summary>
        /// 邮件优先级。
        /// <para>默认为:高。</para>
        /// </summary>
        [JsonProperty]
        public string Priority { get; set; } = "Normal";

        /// <summary>
        /// 是否随请求一起发送凭据。
        /// <para>默认为:false。</para>
        /// </summary>
        [JsonProperty]
        public bool IsUseDefaultCredentials { get; set; } = false;
    }
}