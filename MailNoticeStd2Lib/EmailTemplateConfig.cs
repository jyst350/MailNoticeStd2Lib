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
        /// 标题背景颜色(16进制,前面不需要加上＃)。
        /// <para>默认为:4994CE。</para>
        /// </summary>
        [JsonProperty]
        public string TitleColor { get; set; } = "4994CE";

        /// <summary>
        /// HTML模板标题。
        /// <para>默认为:MailNotice。</para>
        /// </summary>
        [JsonProperty]
        public string MainTitle { get; set; } = "MailNotice";

        ///<summary>
        ///邮件正文的页头信息。
        ///<para>默认显示为:MailNotice。</para>
        ///</summary>
        [JsonProperty]
        public string DisplayHeaderInfo { get; set; } = "MailNotice";

        ///<summary>
        ///邮件正文的页脚信息。
        ///<para>默认显示为:Powered by MailNotice。</para>
        ///</summary>
        [JsonProperty]
        public string DisplayFooterInfo { get; set; } = "Powered by MailNotice";
    }
}