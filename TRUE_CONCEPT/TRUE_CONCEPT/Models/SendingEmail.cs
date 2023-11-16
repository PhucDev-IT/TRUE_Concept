using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TRUE_CONCEPT.Models
{
    public class SendingEmail
    {
        private static readonly string from = "nguyenvanphuc3603@gmail.com";
        private static readonly string pass = "mepz qhxp ovbq bjgz";

        public static bool SendingVerificationEmail(string receiver,string fullName,string code)
        {
            try
            {
                string content = $"<h3 style=\"font - weight: bold; \">Xin chào, <span>{fullName}</span></h3> " +
                "<p>Chúng tôi rất vui vì bạn đã đăng ký True Concept. Để tận hưởng những trải nghiệm tốt nhất và bắt đầu khám phá " +
                "ứng dụng của chúng tôi, vui lòng xác minh địa chỉ Email của bạn.</p>" +
                $"<p>Mã xác minh: <span style=\"color: violet; font - weight: 700; \">{code}</span></p>" +
                "<div>" +
                " <h4>Welcome to True Concept!</h4>" +
                "<p>True Concept Team</p>" +
                "</div>";

                setUpEmail("Xác minh địa chỉ Email cho ứng dụng True Concept", receiver, content);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }


        private static bool setUpEmail(string subject, string receiver, string content)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(from);
                mail.To.Add(receiver);
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    try
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(from, pass);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return false;
                    }

                }
            }

        }

      
    }
}