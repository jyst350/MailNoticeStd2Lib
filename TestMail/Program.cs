using MailNoticeStd2Lib;
using System;

namespace TestMail
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "TestMail C#/.NET控制台应用邮件测试工具";
            MailConfig config = new MailConfig().Read("mail.json");//如果文件不存在创建基本配置文件。
            config.TemplateConfig.DisplayHeaderInfo = "TestMail C#/.NET控制台应用邮件测试工具";
            config.TemplateConfig.MainTitle = "TestMail C#/.NET控制台应用邮件测试工具";
            config.TitleEncoding = "UTF-16";
            MailNotice notice = new MailNotice(config);//加载配置。
            notice.OnSucceed += Notice_OnSucceed;
            notice.OnFailured += Notice_OnFailured;
            notice.Send("邮件测试通过!", "来自TestMail C#/.NET控制台应用邮件测试工具");
            Console.ReadKey();
        }

        private static void Notice_OnFailured(object sender, MailNoticeEventArgs e)
        {
            Console.WriteLine(e.Result);
        }

        private static void Notice_OnSucceed(object sender, MailNoticeEventArgs e)
        {
            Console.WriteLine(e.Result);
        }
    }
}