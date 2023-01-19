using System;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 为 <see cref=" MailNotice"/> 提供事件数据的方法。
    /// </summary>
    public class MailNoticeEventArgs : EventArgs
    {
        /// <summary>
        /// 发送结果。
        /// </summary>
        public object SendResult { get; set; }

        /// <summary>
        /// 邮件标题。
        /// </summary>
        public object Title { get; set; }

        /// <summary>
        /// 邮件正文。
        /// </summary>
        public object Context { get; set; }

        /// <summary>
        /// 发送日期/时间。
        /// </summary>
        public object SendDate { get; set; }
    }
}