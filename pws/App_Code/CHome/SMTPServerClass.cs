using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using Seika;
using Seika.COO.Util;
using Seika.COO.DBA.MA;
using System.IO;
using System.Data.SqlClient;
using Seika.Db;
using Seika.Util;
using Seika.CooException;
using Seika.COO.DBA.BS;
using wmail = System.Web.Mail;

namespace Seika.COO.Web.PG
{
    public class SMTPServerClass
    {
        private string mailTo;
        private string mailFrom;
        private string cc;
        private string Bcc;
        //private string password;
        private string attachments;
        private string subject;
        private string body;

        private Hashtable hashtable = new Hashtable();

        public Hashtable setHashtable 
        {
            set { this.hashtable = value; }
        }

        public bool SMTPSend()
        {
            try
            {
                wmail.MailMessage mail = new wmail.MailMessage();

                //收邮件地址
                mailTo = hashtable["mailTo"].ToString();
                mail.To = mailTo;

                //发邮件地址
                mailFrom = hashtable["mailFrom"].ToString();
                mail.From = mailFrom;

                //邮件附件
                if (hashtable["attachments"] != null)
                {
                    attachments = hashtable["attachments"].ToString();
                    if (attachments != null && attachments != "")
                    {
                        mail.Attachments.Add(new wmail.MailAttachment(attachments));
                    }
                }

                //邮件抄送
                if (hashtable["cc"] != null)
                {
                    cc = hashtable["cc"].ToString();
                    mail.Cc = cc;
                }

                //邮件暗送
                if (hashtable["Bcc"] != null)
                {
                    Bcc = hashtable["Bcc"].ToString();
                    mail.Bcc = Bcc;
                }

                //主题
                subject = hashtable["subject"].ToString();
                mail.Subject = subject;
                //优先级
                mail.Priority = wmail.MailPriority.Normal;
                //邮件格式
                mail.BodyFormat = wmail.MailFormat.Html;
                //内容
                body = hashtable["mailbody"].ToString();
                mail.Body = body;

                wmail.SmtpMail.Send(mail);

                //密码
                //password = hashtable["password"].ToString();

                ////设置server
                //int i = mailFrom.IndexOf('@');
                //string server = mailFrom.Substring(i + 1, (mailFrom.Length - i - 1));

                ////设置smtp服务器
                ////SmtpMail.SmtpServer.Insert(0, "smtp." + server);
                //SmtpMail.SmtpServer = "smtp." + server;              //少部分邮箱smtp不为这个规律
                ////SmtpMail.SmtpServer = "mail338.lolipop.jp";        //公司smtp服务器

                ////设置为需要用户验证 
                //mail.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");

                ////设置验证用户名
                //mail.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/sendusername", mailFrom);

                ////设置验证密码
                //mail.Fields.Add(@"http://schemas.microsoft.com/cdo/configuration/sendpassword", password);

                ////发送邮件
                //SmtpMail.Send(mail);

                return true;
            }
            catch (System.Exception e)
            {
                WriteLog.WriteDebug(e.ToString());
            }
            return false;
        }
    }
}
