using MailNoticeStd2Lib;
using System;
using System.IO;

namespace MailNoticeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new MailConfig().Read("mail.json");
            MailNotice notice = new MailNotice(config);
            
            notice.OnSucceed += Notice_OnSucceed;
            notice.OnFailured += Notice_OnFailured;
            notice.Send("测试邮件","测试邮件");
            Console.Read();
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
