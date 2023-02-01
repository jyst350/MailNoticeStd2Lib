using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 邮件通知类。
    /// </summary>
    public class MailNotice : SendMailBaseClass
    {
        /// <summary>
        /// 当发送成功后引发的事件。
        /// </summary>
        public event EventHandler<MailNoticeEventArgs> OnSucceed;

        /// <summary>
        /// 当发送出现故障后引发的事件。
        /// </summary>
        public event EventHandler<MailNoticeEventArgs> OnFailured;
        private static MailConfig PrivateMailConfig;
        /// <summary>
        /// 初始化一个 <see cref="MailNotice"/> 邮件通知新实例。
        /// </summary>
        /// <param name="mailConfig">邮件参数配置文件。</param>
        public MailNotice(MailConfig mailConfig)
        {
            PrivateMailConfig = mailConfig;
        }
        /// <summary>
        /// 发送邮件。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="context">正文。</param>
        public void Send(string title,string context)
        {
            try
            {
                Send(PrivateMailConfig, title, context);
                OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
            }
            catch (Exception ex)
            {

                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }
            
        }
        /// <summary>
        /// 发送邮件。其中包含附件。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="context">正文。</param>
        /// <param name="attachmentList">附件列表。</param>
        public void Send(string title, string context,string[] attachmentList)
        {
            try
            {
                Send(PrivateMailConfig, title, context,attachmentList);
                OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
            }
            catch (Exception ex)
            {

                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }

        }
        /// <summary>
        /// 异步发送邮件。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="context">正文。</param>
        public async Task SendAsync(string title, string context)
        {

            try
            {
                await SendAsync(PrivateMailConfig, title, context);
                OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
            }
            catch (Exception ex)
            {

                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }
           
        }
        /// <summary>
        /// 异步发送邮件。其中包含附件。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="context">正文。</param>
        /// <param name="attachmentList">附件列表。</param>
        public async Task SendAsync(string title, string context, string[] attachmentList)
        {

            try
            {
                await SendAsync(PrivateMailConfig, title, context,attachmentList);
                OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
            }
            catch (Exception ex)
            {

                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }

        }



    }
}