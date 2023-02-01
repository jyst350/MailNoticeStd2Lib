using System;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 为 <see cref=" MailNotice"/> 提供事件数据的方法。
    /// </summary>
    public class MailNoticeEventArgs : EventArgs
    {
        /// <summary>
        /// 结果。
        /// </summary>
        public string Result { get; set; }
    }
}