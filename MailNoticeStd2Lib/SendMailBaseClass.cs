using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailNoticeStd2Lib
{
    /// <summary>
    /// 发送邮件的基类。
    /// </summary>
    public abstract class SendMailBaseClass
    {
   
        /// <summary>
        /// 发送邮件(HTML格式)。
        /// </summary>
        /// <param name="config">邮件配置。</param>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        protected void Send(MailConfig config, string title, string context)
        {
            string strbody = ReplaceText(config,config.TemplateConfig.MainTitle, config.TemplateConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), config.TemplateConfig.DisplayFooterInfo);

            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(config.MailSender, config.DisplayName, config.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = config.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = config.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = config.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    if (config.MailReceivers != null)
                    {
                        for (int i = 0; i < config.MailReceivers.Length; i++)
                        {
                            PrivateMailMessage.To.Add(config.MailReceivers[i]);
                        }
                    }
                    PrivateMailMessage.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), config.Priority);
                    PrivateMailMessage.Subject = title;
                    PrivateMailMessage.Body = $"{strbody}";
                    ServicePointManager.ServerCertificateValidationCallback = (j, i, a, n) => true;
                    using (var smtp = new SmtpClient()
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = 60000,
                        Host = config.SmtpServerHost,
                        Port = config.SmtpServerPort,
                        EnableSsl = config.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = config.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(config.MailSender, config.MailSenderPassword);
                        smtp.Send(PrivateMailMessage);

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 发送邮件(HTML格式)。
        /// </summary>
        /// <param name="config">邮件配置。</param>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        /// <param name="attachment">附件列表。</param>
        protected void Send(MailConfig config, string title, string context ,string[] attachment)
        {
            string strbody = ReplaceText(config, config.TemplateConfig.MainTitle, config.TemplateConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), config.TemplateConfig.DisplayFooterInfo);

            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(config.MailSender, config.DisplayName, config.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = config.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = config.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = config.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    if (config.MailReceivers != null)
                    {
                        for (int i = 0; i < config.MailReceivers.Length; i++)
                        {
                            PrivateMailMessage.To.Add(config.MailReceivers[i]);
                        }
                    }
                    PrivateMailMessage.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), config.Priority);
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
                        Host = config.SmtpServerHost,
                        Port = config.SmtpServerPort,
                        EnableSsl = config.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = config.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(config.MailSender, config.MailSenderPassword);
                        smtp.Send(PrivateMailMessage);

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 发送邮件(HTML格式)。
        /// </summary>
        /// <param name="config">邮件配置。</param>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        protected async Task SendAsync(MailConfig config, string title, string context)
        {
            string strbody = ReplaceText(config, config.TemplateConfig.MainTitle, config.TemplateConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), config.TemplateConfig.DisplayFooterInfo);

            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(config.MailSender, config.DisplayName, config.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = config.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = config.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = config.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    if (config.MailReceivers != null)
                    {
                        for (int i = 0; i < config.MailReceivers.Length; i++)
                        {
                            PrivateMailMessage.To.Add(config.MailReceivers[i]);
                        }
                    }
                    PrivateMailMessage.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), config.Priority);
                    PrivateMailMessage.Subject = title;
                    PrivateMailMessage.Body = $"{strbody}";
                    ServicePointManager.ServerCertificateValidationCallback = (j, i, a, n) => true;
                    using (var smtp = new SmtpClient()
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = 60000,
                        Host = config.SmtpServerHost,
                        Port = config.SmtpServerPort,
                        EnableSsl = config.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = config.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(config.MailSender, config.MailSenderPassword);
                        await smtp.SendMailAsync(PrivateMailMessage);

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 异步发送邮件(HTML格式)。
        /// </summary>
        /// <param name="config">邮件配置。</param>
        /// <param name="title">邮件标题。</param>
        /// <param name="context">邮件正文。</param>
        /// <param name="attachment">附件列表。</param>
        protected async Task SendAsync(MailConfig config, string title, string context, string[] attachment)
        {
            string strbody = ReplaceText(config, config.TemplateConfig.MainTitle, config.TemplateConfig.DisplayHeaderInfo, title, context.Replace("\r\n", "<br>"), config.TemplateConfig.DisplayFooterInfo);

            try
            {
                using (MailMessage PrivateMailMessage = new MailMessage
                {
                    From = new MailAddress(config.MailSender, config.DisplayName, config.MailEncoding)
                })
                {
                    PrivateMailMessage.IsBodyHtml = true;
                    PrivateMailMessage.BodyEncoding = config.MailEncoding;
                    PrivateMailMessage.HeadersEncoding = config.MailEncoding;
                    PrivateMailMessage.SubjectEncoding = config.MailEncoding;
                    PrivateMailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    if (config.MailReceivers != null)
                    {
                        for (int i = 0; i < config.MailReceivers.Length; i++)
                        {
                            PrivateMailMessage.To.Add(config.MailReceivers[i]);
                        }
                    }
                    PrivateMailMessage.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), config.Priority);
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
                        Host = config.SmtpServerHost,
                        Port = config.SmtpServerPort,
                        EnableSsl = config.UseTls,
                    })
                    {
                        smtp.UseDefaultCredentials = config.IsUseDefaultCredentials;
                        smtp.Credentials = new NetworkCredential(config.MailSender, config.MailSenderPassword);
                        await smtp.SendMailAsync(PrivateMailMessage);

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ReplaceText(MailConfig config, string htmltitle, string tag, string subject, string context, string footerinfo)
        {
            string str = config.EmailTemplateFile;
            str = str.Replace("$MAINTITLE", htmltitle);
            str = str.Replace("$DISPLAYHEADERINFO", tag);
            str = str.Replace("$DISPLAYSUBJECT", subject);
            str = str.Replace("$CONTEXT", context);
            str = str.Replace("$DISPLAYFOOTERINFO", footerinfo);
            str = str.Replace("$COLOR", config.TemplateConfig.TitleColor);
            return str;
        }
    }
}
