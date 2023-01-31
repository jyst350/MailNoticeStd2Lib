using System;
using System.Net;
using System.Net.Mail;
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
        public event EventHandler<MailNoticeEventArgs> OnSucceed;

        /// <summary>
        /// 当发送出现故障后引发的事件。
        /// </summary>
        public event EventHandler<MailNoticeEventArgs> OnFailured;

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
            string strbody = ReplaceText(PrivateMailConfig.MainTitle,PrivateMailConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), PrivateMailConfig.DisplayFooterInfo);

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
                PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
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
                PrivateMailMessage.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), PrivateMailConfig.Priority);
                PrivateMailMessage.Subject = title;
                PrivateMailMessage.Body = $"{strbody}";
                ServicePointManager.ServerCertificateValidationCallback = (j, i, a, n) => true;

                PrivateSmtpClient = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 60000,
                    Host = PrivateMailConfig.SmtpServerHost,
                    Port = PrivateMailConfig.SmtpServerPort,
                    EnableSsl = PrivateMailConfig.SecurityConnection,
                   
                };
                PrivateSmtpClient.UseDefaultCredentials = PrivateMailConfig.IsUseDefaultCredentials;
                PrivateSmtpClient.Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword);
                PrivateSmtpClient.Send(PrivateMailMessage);
                OnSucceed?.Invoke(this, new MailNoticeEventArgs { Result = "发送成功" });
            }
            catch (SmtpException ex)
            {
                OnFailured?.Invoke(this, new MailNoticeEventArgs { Result = ex.Message });
            }
        }

        /// <summary>
        /// 异步发送邮件。
        /// </summary>
        /// <param name="title">邮件标题</param>
        /// <param name="context">邮件正文</param>
        public async Task SendAsync(string title, string context)
        {
            string strbody = ReplaceText(PrivateMailConfig.MainTitle, PrivateMailConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), PrivateMailConfig.DisplayFooterInfo);
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
                PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
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
                PrivateMailMessage.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), PrivateMailConfig.Priority);
                PrivateMailMessage.Subject = title;
                PrivateMailMessage.Body = $"{strbody}";
                ServicePointManager.ServerCertificateValidationCallback = (j, i, a, n) => true;
                PrivateSmtpClient = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 60000,
                    Host = PrivateMailConfig.SmtpServerHost,
                    Port = PrivateMailConfig.SmtpServerPort,
                    EnableSsl = PrivateMailConfig.SecurityConnection,
                    UseDefaultCredentials = PrivateMailConfig.IsUseDefaultCredentials,
                    Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword)
                };

                if (PrivateMailConfig.VerifyCredentials)
                {
                    
                   

                    await PrivateSmtpClient.SendMailAsync(PrivateMailMessage);
                    OnSucceed?.Invoke(this, new MailNoticeEventArgs { Result = "通知邮件已发送" });
                }
                else
                {
                    OnFailured?.Invoke(this, new MailNoticeEventArgs { Result = "验证失败" });
                }
            }
            catch (SmtpException ex)
            {
                OnFailured?.Invoke(this, new MailNoticeEventArgs { Result = ex.Message });
            }
        }

        private string ReplaceText(string htmltitle,string tag, string subject, string context, string footerinfo)
        {
            string str = PrivateMailConfig.EmailTemplateFile;
            str = str.Replace("$MAINTITLE", htmltitle);
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