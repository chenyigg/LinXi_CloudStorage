using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace LinXi_Common
{
    public static class StaticHelper
    {
        private static readonly ILogger logger = new LoggerFactory().CreateLogger(nameof(StaticHelper));

        /// <summary>
        /// 获取文件加密MD5码
        /// </summary>
        /// <returns></returns>
        public static string MD5File(string filepath)
        {
            using (FileStream file = new FileStream(filepath, FileMode.Open))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// MD5加密 32位
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetMDFiveString(string pwd)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(pwd + "Auto");
                //开始加密
                byte[] newBuffer = md5.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();

                //16位密文是32位密文的9到24位字符
                //for (int i = 4; i < 12; i++)--16位
                //for (int i = 0; i < 16; i++)--32位
                //for (int i = 0; i < newBuffer.Length; i++)--默认32位

                for (int i = 0; i < newBuffer.Length; i++)
                {
                    //大写X:ToString("X2")即转化为大写的16进制。
                    // 小写x: ToString("x2")即转化为小写的16进制。
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 从文件流获取文件MD5
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetMD5HashFromStream(Stream stream)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] data = md5.ComputeHash(stream);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="FromMial">发件人邮箱</param>
        /// <param name="ToMial">收件人邮箱(多个收件人地址用";"号隔开)</param>
        /// <param name="AuthorizationCode">发件人授权码</param>
        /// <param name="ReplyTo">对方回复邮件时默认的接收地址（不设置也是可以的）</param>
        /// <param name="CCMial">//邮件的抄送者(多个抄送人用";"号隔开)</param>
        /// <param name="File_Path">附件的地址</param>
        public static bool SendEmail(string ToMail, out int ValidCode, string FromMial = "1812613402@qq.com")
        {
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();

                //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                mailMessage.Priority = MailPriority.Normal;

                //发件人邮箱地址。
                mailMessage.From = new MailAddress(FromMial);

                //收件人邮箱地址。需要群发就写多个，如    12@qq.com;23@qq.com;44@qq.com
                //拆分邮箱地址
                List<string> ToMaillist = ToMail.Split(';').ToList();
                for (int i = 0; i < ToMaillist.Count; i++)
                {
                    mailMessage.To.Add(new MailAddress(ToMaillist[i]));  //收件人邮箱地址。
                }

                //if (ReplyTo == "" || ReplyTo == null)
                //{
                //    ReplyTo = FromMial;
                //}

                //对方回复邮件时默认的接收地址(不设置也是可以的哟)
                //mailMessage.ReplyTo = new MailAddress(ReplyTo);

                //if (CCMial != "" && CCMial != null)
                //{
                //    List<string> CCMiallist = ToMail.Split(';').ToList();
                //    for (int i = 0; i < CCMiallist.Count; i++)
                //    {
                //        //邮件的抄送者，支持群发
                //        mailMessage.CC.Add(new MailAddress(CCMial));
                //    }
                //}

                //如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                mailMessage.SubjectEncoding = Encoding.UTF8;

                //邮件正文是否是HTML格式
                mailMessage.IsBodyHtml = false;

                //生成验证码
                ValidCode = new Random().Next(1000, 9999);

                //邮件标题。
                mailMessage.Subject = "灵犀云盘注册验证码";
                //邮件内容。
                mailMessage.Body = $"亲爱的云盘用户{ToMail}，您好！\r\n"
                                                + $"您的绑定验证码是：{ValidCode} \r\n"
                                                + "本邮件由系统自动发送，五分钟后将失去效果，请尽快使用，勿直接回复 \r\n"
                                                + "感谢您的访问，祝您使用愉快！\r\n"
                                                + "灵犀云盘";

                //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中
                //if (File_Path != "" && File_Path != null)
                //{
                //    //将附件添加到邮件
                //    mailMessage.Attachments.Add(new Attachment(File_Path));
                //    //获取或设置此电子邮件的发送通知。
                //    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                //}

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();

                #region 设置邮件服务器地址

                //在这里我使用的是163邮箱，所以是smtp.163.com，如果你使用的是qq邮箱，那么就是smtp.qq.com。
                // client.Host = "smtp.163.com";
                if (FromMial.Length != 0)
                {
                    //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
                    string[] addressor = FromMial.Trim().Split(new char[] { '@', '.' });
                    switch (addressor[1])
                    {
                        case "163":
                            client.Host = "smtp.163.com";
                            break;

                        case "126":
                            client.Host = "smtp.126.com";
                            break;

                        case "qq":
                            client.Host = "smtp.qq.com";
                            break;

                        case "gmail":
                            client.Host = "smtp.gmail.com";
                            break;

                        case "hotmail":
                            client.Host = "smtp.live.com";//outlook邮箱
                            //client.Port = 587;
                            break;

                        case "foxmail":
                            client.Host = "smtp.foxmail.com";
                            break;

                        case "sina":
                            client.Host = "smtp.sina.com.cn";
                            break;

                        default:
                            client.Host = "smtp.exmail.qq.com";//qq企业邮箱
                            break;
                    }
                }

                #endregion 设置邮件服务器地址

                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(FromMial, "imfxziqegdoededa");

                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //发送
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                var me = ex.Message.ToString();
                logger.LogError(me);
                ValidCode = -1;
                return false;
            }
        }

        /// <summary>
        /// 文件读
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string FileReader(string Path)
        {
            StringBuilder builder = new StringBuilder();
            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    builder.Append(sr.ReadToEnd());
                }
            }
            return builder.ToString();
        }


    }
}