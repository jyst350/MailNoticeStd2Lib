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
        public bool Result { get; set; }

        /// <summary>
        /// 信息。
        /// </summary>
        public string Message { get; set; }
    }
}