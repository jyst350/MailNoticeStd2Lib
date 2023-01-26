using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 邮件通知类。
    /// </summary>
    public class MailNotice : IDisposable
    {
        /// <summary>
        /// 当发送成功后引发的事件。
        /// </summary>
        public event EventHandler<MailNoticeEventArgs> SendSuccessed;

        /// <summary>
        /// 当发送失败后引发的事件。
        /// </summary>
        public event EventHandler<MailNoticeEventArgs> SendFailure;

        private MailMessage PrivateMailMessage;
        private SmtpClient PrivateSmtpClient;
        private readonly MailConfig PrivateMailConfig;

  
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
        /// <param name="title">邮件标题</param>
        /// <param name="context">邮件正文</param>
        public void Send(string title, string context)
        {
            string strbody = ReplaceText(PrivateMailConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), PrivateMailConfig.DisplayFooterInfo);
            try
            {
                PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(PrivateMailConfig.MailSender, PrivateMailConfig.DisplayName, PrivateMailConfig.MailEncoding)
                };

                PrivateMailMessage.IsBodyHtml = true;
                PrivateMailMessage.BodyEncoding = PrivateMailConfig.MailEncoding;
                PrivateMailMessage.HeadersEncoding = PrivateMailConfig.MailEncoding;
                PrivateMailMessage.SubjectEncoding = PrivateMailConfig.MailEncoding;
                if (PrivateMailConfig.AttachmentFileList != null)
                {
                    for (int i = 0; i < PrivateMailConfig.AttachmentFileList.Length; i++)
                    {
                        PrivateMailMessage.Attachments.Add(new Attachment(PrivateMailConfig.AttachmentFileList[i]));
                    }
                }
                if (PrivateMailConfig.MailReceivers != null)
                {
                    for (int i = 0; i < PrivateMailConfig.MailReceivers.Length; i++)
                    {
                        PrivateMailMessage.To.Add(PrivateMailConfig.MailReceivers[i]);
                    }
                }

                PrivateMailMessage.Subject = title;
                PrivateMailMessage.Body = $"{strbody}";
                ServicePointManager.ServerCertificateValidationCallback = delegate (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

                PrivateSmtpClient = new SmtpClient
                {
                    Host = PrivateMailConfig.SmtpServerHost,
                    Port = PrivateMailConfig.SmtpServerPort,
                    EnableSsl = PrivateMailConfig.SecurityConnection,
                    Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword)
                };
                PrivateSmtpClient.Send(PrivateMailMessage);
                SendSuccessed?.Invoke(this, new MailNoticeEventArgs { SendResult = "邮件发送成功。", Title = PrivateMailConfig.DisplayHeaderInfo, Context = PrivateMailMessage.Body, SendDate = DateTime.UtcNow.AddHours(8) });
            }
            catch (Exception ex)
            {
                SendFailure?.Invoke(this, new MailNoticeEventArgs { SendResult = ex.Message });
            }
        }

        /// <summary>
        /// 异步发送邮件。
        /// </summary>
        /// <param name="title">邮件标题</param>
        /// <param name="context">邮件正文</param>
        public async Task SendAsync(string title, string context)
        {
            string strbody = ReplaceText(PrivateMailConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), PrivateMailConfig.DisplayFooterInfo);
            try
            {
                PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(PrivateMailConfig.MailSender, PrivateMailConfig.DisplayName, PrivateMailConfig.MailEncoding)
                };

                PrivateMailMessage.IsBodyHtml = true;
                PrivateMailMessage.BodyEncoding = PrivateMailConfig.MailEncoding;
                PrivateMailMessage.HeadersEncoding = PrivateMailConfig.MailEncoding;
                PrivateMailMessage.SubjectEncoding = PrivateMailConfig.MailEncoding;
                if(PrivateMailConfig.AttachmentFileList !=null)
                {
                    for (int i = 0; i < PrivateMailConfig.AttachmentFileList.Length; i++)
                    {
                        PrivateMailMessage.Attachments.Add(new Attachment(PrivateMailConfig.AttachmentFileList[i]));
                    }
                }
                
                if (PrivateMailConfig.MailReceivers != null)
                {
                    for (int i = 0; i < PrivateMailConfig.MailReceivers.Length; i++)
                    {
                        PrivateMailMessage.To.Add(PrivateMailConfig.MailReceivers[i]);
                    }
                }

                PrivateMailMessage.Subject = title;
                PrivateMailMessage.Body = $"{strbody}";
                ServicePointManager.ServerCertificateValidationCallback = delegate (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

                PrivateSmtpClient = new SmtpClient
                {
                    Host = PrivateMailConfig.SmtpServerHost,
                    Port = PrivateMailConfig.SmtpServerPort,
                    EnableSsl = PrivateMailConfig.SecurityConnection,
                    Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword)
                };
                await PrivateSmtpClient.SendMailAsync(PrivateMailMessage);
                SendSuccessed?.Invoke(this, new MailNoticeEventArgs { SendResult = "邮件发送成功。", Title = PrivateMailConfig.DisplayHeaderInfo, Context = PrivateMailMessage.Body, SendDate = DateTime.UtcNow.AddHours(8) });
            }
            catch (Exception ex)
            {
                SendFailure?.Invoke(this, new MailNoticeEventArgs { SendResult = ex.Message });
            }
        }

        private string ReplaceText(string tag, string subject, string context, string footerinfo)
        {
            string str = PrivateMailConfig.EmailTemplateFile;
            str = str.Replace("$DISPLAYHEADERINFO", tag);
            str = str.Replace("$DISPLAYSUBJECT", subject);
            str = str.Replace("$CONTEXT", context);
            str = str.Replace("$DISPLAYFOOTERINFO", footerinfo);
            str = str.Replace("$COLOR", PrivateMailConfig.TitleColor);
            return str;
        }

        /// <summary>
        /// 释放 <see cref="MailNotice"/> 对象。
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)PrivateMailMessage).Dispose();
            ((IDisposable)PrivateSmtpClient).Dispose();
        }
    }
}