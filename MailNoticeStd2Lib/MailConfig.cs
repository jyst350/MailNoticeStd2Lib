using System.ComponentModel;
using System.Text;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 邮件参数配置类。
    /// </summary>
    public class MailConfig
    {
        ///<summary>
        ///邮件发送者。
        ///</summary>
        [Category("邮件参数"), Description("邮件发送者")]
        public string MailSender { get; set; }

        ///<summary>
        ///密码。
        ///</summary>
        [Category("邮件参数"), Description("密码"), PasswordPropertyText(true)]
        public string MailSenderPassword { get; set; }

        ///<summary>
        ///邮件接收者列表。
        ///</summary>
        [Category("邮件参数"), Description("邮件接收者列表")]
        public string[] MailReceivers { get; set; }

        ///<summary>
        ///SMTP服务器地址。
        ///</summary>
        [Category("服务器参数"), Description("SMTP服务器地址")]
        public string SmtpServerHost { get; set; }

        ///<summary>
        ///SMTP服务器端口。
        ///<para>默认使用587端口。</para>
        ///</summary>
        [Category("服务器参数"), Description("SMTP服务器端口\r\n默认:25")]
        public int SmtpServerPort { get; set; } = 25;

        ///<summary>
        ///是否指示使用SSL/TLS进行安全连接。
        ///<para>默认为True。</para>
        ///</summary>
        [Category("服务器参数"), Description("指示服务器是否使用SSL/TLS进行安全连接\r\n默认:False")]
        public bool SecurityConnection { get; set; } = false;

        ///<summary>
        ///发信人显示名称。
        ///<para>默认显示为:MailNotice。</para>
        ///</summary>
        [Category("邮件参数"), Description("发信人显示名称\r\n默认:MailNotice")]
        public string DisplayName { get; set; } = "MailNotice";

        ///<summary>
        ///邮件正文的页头信息。
        ///<para>默认显示为:MailNotice。</para>
        ///</summary>
        [Category("邮件参数"), Description("邮件正文的页头信息\r\n默认:MailNotice")]
        public string DisplayHeaderInfo { get; set; } = "MailNotice";

        ///<summary>
        ///邮件正文的页脚信息。
        ///<para>默认显示为:Powered by MailNotice。</para>
        ///</summary>
        [Category("邮件参数"), Description("邮件正文的页脚信息\r\n默认:Powered by MailNotice")]
        public string DisplayFooterInfo { get; set; } = "Powered by MailNotice";

        /// <summary>
        ///邮件所有上下文的编码格式。
        ///<para>默认显示为:UTF8。</para>
        /// </summary>
        [Category("邮件参数"), Description("邮件所有上下文的编码格式\r\n默认:UTF8")]
        public Encoding MailEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 邮件模板文件(如未指定,将使用内置模板)。
        /// </summary>
        [Category("邮件参数"), Description("邮件模板文件(如未指定,将使用内置模板)。")]
        public string EmailTemplateFile { get; set; } = Resource.EmailTemplate;

        /// <summary>
        /// 标题背景颜色(16进制)。
        /// <para>默认为:4994CE。</para>
        /// </summary>
        public string TitleColor { get; set; } = "4994CE";

        /// <summary>
        /// 附件文件列表。
        /// </summary>
        public string[] AttachmentFileList { get; set; }
    }
}