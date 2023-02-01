using Newtonsoft.Json;
using System.ComponentModel;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 邮件模板参数配置类。
    /// </summary>
    [JsonObject]
    public class EmailTemplateConfig
    {
        /// <summary>
        /// 标题背景颜色(16进制)。
        /// <para>默认为:4994CE。</para>
        /// </summary>
        [JsonProperty]
        [Category("邮件参数"), Description("标题背景颜色(16进制)。")]
        public string TitleColor { get; set; } = "4994CE";

        /// <summary>
        /// HTML模板标题。
        /// <para>默认为:MailNotice。</para>
        /// </summary>
        [JsonProperty]
        [Category("邮件参数"), Description("HTML模板标题。")]
        public string MainTitle { get; set; } = "MailNotice";
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

     
    }
}