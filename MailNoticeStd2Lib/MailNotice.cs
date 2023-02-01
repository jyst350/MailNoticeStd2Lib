using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 邮件通知类。
    /// </summary>
    public class MailNotice
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
        /// 发送HTML格式邮件。
        /// </summary>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        public void Send(string title, string context)
        {
            string strbody = ReplaceText(title, context.Replace("\r\n", "<br>"));
            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(PrivateMailConfig.MailSender, PrivateMailConfig.DisplayName, PrivateMailConfig.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
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
                    using (var smtp = new SmtpClient()
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = 60000,
                        Host = PrivateMailConfig.SmtpServerHost,
                        Port = PrivateMailConfig.SmtpServerPort,
                        EnableSsl = PrivateMailConfig.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = PrivateMailConfig.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword);
                        smtp.Send(PrivateMailMessage);
                        OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
                    }
                }
            }
            catch (Exception ex)
            {
                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }
        }

        /// <summary>
        /// 发送带附件的HTML格式邮件。
        /// </summary>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        /// <param name="attachment">附件列表。</param>
        public void Send(string title, string context, string[] attachment)
        {
            string strbody = ReplaceText(title, context.Replace("\r\n", "<br>"));

            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(PrivateMailConfig.MailSender, PrivateMailConfig.DisplayName, PrivateMailConfig.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
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
                    if (attachment != null)
                    {
                        for (int i = 0; i < attachment.Length; i++)
                        {
                            PrivateMailMessage.Attachments.Add(new Attachment(attachment[i]));
                        }
                    }
                    ServicePointManager.ServerCertificateValidationCallback = (j, i, a, n) => true;
                    using (var smtp = new SmtpClient()
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = 60000,
                        Host = PrivateMailConfig.SmtpServerHost,
                        Port = PrivateMailConfig.SmtpServerPort,
                        EnableSsl = PrivateMailConfig.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = PrivateMailConfig.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword);
                        smtp.Send(PrivateMailMessage);
                        OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
                    }
                }
            }
            catch (Exception ex)
            {
                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }
        }

        /// <summary>
        /// 异步发送HTML格式邮件。
        /// </summary>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        public async Task SendAsync(string title, string context)
        {
            string strbody = ReplaceText(title, context.Replace("\r\n", "<br>"));
            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(PrivateMailConfig.MailSender, PrivateMailConfig.DisplayName, PrivateMailConfig.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
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
                    using (var smtp = new SmtpClient()
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = 60000,
                        Host = PrivateMailConfig.SmtpServerHost,
                        Port = PrivateMailConfig.SmtpServerPort,
                        EnableSsl = PrivateMailConfig.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = PrivateMailConfig.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword);
                        await smtp.SendMailAsync(PrivateMailMessage);
                        OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
                    }
                }
            }
            catch (Exception ex)
            {
                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }
        }

        /// <summary>
        /// 异步发送带附件的HTML格式邮件。
        /// </summary>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        /// <param name="attachment">附件列表。</param>
        public async Task SendAsync(string title, string context, string[] attachment)
        {
            string strbody = ReplaceText(title, context.Replace("\r\n", "<br>"));

            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(PrivateMailConfig.MailSender, PrivateMailConfig.DisplayName, PrivateMailConfig.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = PrivateMailConfig.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
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
                    if (attachment != null)
                    {
                        for (int i = 0; i < attachment.Length; i++)
                        {
                            PrivateMailMessage.Attachments.Add(new Attachment(attachment[i]));
                        }
                    }
                    ServicePointManager.ServerCertificateValidationCallback = (j, i, a, n) => true;
                    using (var smtp = new SmtpClient()
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = 60000,
                        Host = PrivateMailConfig.SmtpServerHost,
                        Port = PrivateMailConfig.SmtpServerPort,
                        EnableSsl = PrivateMailConfig.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = PrivateMailConfig.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(PrivateMailConfig.MailSender, PrivateMailConfig.MailSenderPassword);
                        await smtp.SendMailAsync(PrivateMailMessage);
                        OnSucceed?.Invoke(this, new MailNoticeEventArgs() { Result = "发送成功" });
                    }
                }
            }
            catch (Exception ex)
            {
                OnFailured?.Invoke(this, new MailNoticeEventArgs() { Result = ex.Message });
            }
        }

        private string ReplaceText(string title, string content)
        {
            string str = PrivateMailConfig.EmailTemplateFile;
            str = str.Replace("$MAINTITLE", PrivateMailConfig.TemplateConfig.MainTitle);
            str = str.Replace("$DISPLAYHEADERINFO", PrivateMailConfig.TemplateConfig.DisplayHeaderInfo);
            str = str.Replace("$DISPLAYSUBJECT", title);
            str = str.Replace("$CONTEXT", content);
            str = str.Replace("$DISPLAYFOOTERINFO", PrivateMailConfig.TemplateConfig.DisplayFooterInfo);
            str = str.Replace("$COLOR", PrivateMailConfig.TemplateConfig.TitleColor);
            return str;
        }
    }
}