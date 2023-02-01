using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 邮件配置类。
    /// </summary>
    [JsonObject]
    public class MailConfig
    {
        /// <summary>
        /// 读取根目录下的配置文件,如果不存在,该方法将新建一个基础配置文件。
        /// </summary>
        /// <param name="fileName">欲写入的文件名。</param>
        /// <returns><see cref="MailConfig"/> 对象。</returns>
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
                Write(fileName);
                return new MailConfig();
            }
        }

        /// <summary>
        /// 将配置写入指定文件。
        /// </summary>
        /// <param name="fileName">欲写入的文件名。</param>
        /// <returns></returns>
        public void Write(string fileName)
        {
            using (var sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                var config = JsonConvert.SerializeObject(this, Formatting.Indented);
                sw.WriteLine(config);
            }
        }

        ///<summary>
        ///邮件发送者。
        ///<para>通常是邮件地址。</para>
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("邮件发送者用户名。(通常是邮件地址)")]
        public string MailSender { get; set; }

        ///<summary>
        ///邮件发送者登录密码。
        ///<para>目前各大主流邮箱几乎都变更为授权码或应用密码。</para>
        ///</summary>

        [JsonProperty]
        [Category("邮件参数"), Description("邮件发送者登录密码。"), PasswordPropertyText(true)]
        public string MailSenderPassword { get; set; }

        ///<summary>
        ///邮件接收者列表。
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("邮件接收者列表。")]
        public string[] MailReceivers { get; set; }

        ///<summary>
        ///SMTP服务器地址。
        ///</summary>
        [JsonProperty]
        [Category("服务器参数"), Description("SMTP服务器地址。")]
        public string SmtpServerHost { get; set; }

        ///<summary>
        ///SMTP服务器端口。
        ///<para>默认使用25端口。</para>
        ///</summary>
        [JsonProperty]
        [Category("服务器参数"), Description("SMTP服务器端口。\r\n默认:25")]
        public int SmtpServerPort { get; set; } = 25;

        ///<summary>
        ///是否指示使用SSL/TLS进行安全连接。
        ///<para>默认为false。</para>
        ///<para>如果开启，<see cref="SmtpServerPort"/> 参数可能需要更换。</para>
        ///</summary>
        [JsonProperty]
        [Category("服务器参数"), Description("指示服务器是否使用SSL/TLS进行安全连接\r\n默认:False")]
        public bool UseTls { get; set; } = false;

        ///<summary>
        ///发信人外显名称。
        ///<para>默认显示为:MailNotice。</para>
        ///</summary>
        [JsonProperty]
        [Category("邮件参数"), Description("发信人外显名称。\r\n默认:MailNotice")]
        public string DisplayName { get; set; } = "MailNotice";

        /// <summary>
        ///邮件编码格式。
        ///<para>默认显示为:UTF8。</para>
        /// </summary>
        [JsonIgnore]
        [Category("邮件参数"), Description("邮件编码格式。\r\n默认:UTF8")]
        public Encoding MailEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 邮件模板文件。
        /// <para>如未指定,将使用内置模板。</para>
        /// </summary>
        [JsonIgnore]
        [Category("邮件参数"), Description("邮件模板文件(如未指定,将使用内置模板)。")]
        public string EmailTemplateFile { get; set; } = Resource.EmailTemplate;

        /// <summary>
        /// 是否验证凭据。
        /// <para>默认为:true。</para>
        /// </summary>
        [JsonProperty]
        [Category("邮件参数"), Description("是否验证凭据。\r\n默认为:true。")]
        public bool VerifyCredentials { get; set; } = true;

        /// <summary>
        /// 发送超时。
        /// <para>默认为:60000(60秒)。</para>
        /// </summary>
        [JsonProperty]
        [Category("邮件参数"), Description("发送超时。\r\n默认为:60000(60秒)。")]
        public int SendTimeout { get; set; } = 60000;

        /// <summary>
        /// 邮件优先级。
        /// <para>默认为:Normal,可选:High,Low,Normal。</para>
        /// </summary>
        [JsonProperty]
        [Category("邮件参数"), Description("邮件优先级。\r\n默认为:Normal。\r\n可选:High,Low,Normal。")]
        public string Priority { get; set; } = "Normal";

        /// <summary>
        /// 是否随请求一起发送凭据。
        /// <para>默认为:false。</para>
        /// </summary>

        [JsonProperty]
        [Category("邮件参数"), Description("是否随请求一起发送凭据。\r\n默认为:false。")]
        public bool IsUseDefaultCredentials { get; set; } = false;

        /// <summary>
        /// 邮件模板参数。
        /// </summary>
        [Category("邮件参数"), Description("是否随请求一起发送凭据。\r\n默认为:false。")]
        public EmailTemplateConfig TemplateConfig { get; set; } = new EmailTemplateConfig();
    }
}