using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class SendEmail
    {
        public SendEmail() { }
        public SendEmail(string host, int port, string sender, string password)
        {
            Host = host;
            Port = port;
            From =sender;
            Password = password;
        }
        #region //Public properties
        public string From { get; set; }
        public string[] To { get; set; }
        public string[] BCC { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Attachment { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        #endregion
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="recepient">Recepient address</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        public static void SendMailMessage(string @from, string recepient, string bcc_recepient, string subject, string body)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(@from);
            // Set the recepient address of the mail message
            mMailMessage.To.Add(new MailAddress(recepient));
            //string[] arrTo = recepient.Split(';');
            //foreach (string strTo in arrTo)
            //{
            //    mMailMessage.To.Add(new MailAddress(strTo));
            //}
            //'multiple attachment
            //For j As Integer = 0 To ary.Count - 1
            //    mMailMessage.Attachments.Add(ary(j))
            //Next j


            // Check if the bcc value is nothing or an empty string
            if ((bcc_recepient != null) & bcc_recepient != string.Empty)
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc_recepient));
            }

            //' Check if the cc value is nothing or an empty value
            //If Not cc Is Nothing And cc <> String.Empty Then
            //    ' Set the CC address of the mail message
            //    mMailMessage.CC.Add(New MailAddress(cc))
            //End If

            // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;

            mMailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/plain");
            plainView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/html");
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

            mMailMessage.AlternateViews.Add(plainView);
            mMailMessage.AlternateViews.Add(htmlView);


            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();
            // Send the mail message
            mSmtpClient.Send(mMailMessage);
        }


        public static void SendMailMessage(string @from, string[] recepient, string[] bcc_recepient, string subject, string body, string attach)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(@from);
            // Set the recepient address of the mail message
            foreach(string t in recepient)
            {
                mMailMessage.To.Add(new MailAddress(t));
            }
           

            if (bcc_recepient.Length > 0)
            {
                foreach (string bcc in bcc_recepient)
                {
                    mMailMessage.Bcc.Add(bcc);
                }
            }

            //Add attachment
            if (attach.Trim() != "")
            {
                mMailMessage.Attachments.Add(new Attachment(attach));
            }

            // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;

            mMailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/plain");
            plainView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/html");
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

            mMailMessage.AlternateViews.Add(plainView);
            mMailMessage.AlternateViews.Add(htmlView);


            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();
            // Send the mail message
            mSmtpClient.Send(mMailMessage);
        }

        public bool SendMailMessage(SendEmail mail_obj)
        {
            bool result = true;
            try
            {
               
                MailMessage mMailMessage = new MailMessage();
                mMailMessage.From = new MailAddress(mail_obj.From);

                foreach (string strTo in mail_obj.To)
                {
                    mMailMessage.To.Add(strTo);
                }


                //Check BCC
                if (mail_obj.BCC != null && mail_obj.BCC.Length>0)
                {
                    foreach (string strBccTo in mail_obj.BCC)
                    {
                        mMailMessage.Bcc.Add(strBccTo);
                    }

                }

                //Check attachment
                if (mail_obj.Attachment != null && mail_obj.Attachment != "")
                {
                    mMailMessage.Attachments.Add(new Attachment(mail_obj.Attachment));
                }

                // Set the subject of the mail message
                mMailMessage.Subject = mail_obj.Subject;
                // Set the body of the mail message
                mMailMessage.Body = mail_obj.Message;

                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                mMailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/plain");
                plainView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/html");
                htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

                mMailMessage.AlternateViews.Add(plainView);
                mMailMessage.AlternateViews.Add(htmlView);


                // Instantiate a new instance of SmtpClient
                SmtpClient mSmtpClient = new SmtpClient(mail_obj.Host, mail_obj.Port);
                mSmtpClient.EnableSsl = true;

                mSmtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = mail_obj.From,
                    Password = mail_obj.Password
                };

                // Send the mail message
                mSmtpClient.Send(mMailMessage);
            }
            catch (Exception ex)
            {
                result = false;
                Log.AddExceptionToLog("Error function [SendMailMessage(SendEmail mail_obj)] in class [SendEmail], detail:" + ex.Message , "");
            }
            return result;
        }


        public bool Send()
        {
            bool result = true;
            try
            {

                MailMessage mMailMessage = new MailMessage();
                mMailMessage.From = new MailAddress(From);

                foreach (string strTo in To)
                {
                    mMailMessage.To.Add(strTo);
                }


                //Check BCC
                if (BCC != null && BCC.Length > 0)
                {
                    foreach (string strBccTo in BCC)
                    {
                        mMailMessage.Bcc.Add(strBccTo);
                    }

                }

                //Check attachment
                if (Attachment != null && Attachment != "")
                {
                    mMailMessage.Attachments.Add(new Attachment(Attachment));
                }

                // Set the subject of the mail message
                mMailMessage.Subject = Subject;
                // Set the body of the mail message
                mMailMessage.Body = Message;

                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                mMailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/plain");
                plainView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mMailMessage.Body, null, "text/html");
                htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;

                mMailMessage.AlternateViews.Add(plainView);
                mMailMessage.AlternateViews.Add(htmlView);


                // Instantiate a new instance of SmtpClient
                SmtpClient mSmtpClient = new SmtpClient(Host, Port);
                mSmtpClient.EnableSsl = true;

                mSmtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = From,
                    Password = Password
                };

                // Send the mail message
                mSmtpClient.Send(mMailMessage);
            }
            catch (Exception ex)
            {
                result = false;
                Log.AddExceptionToLog("Error function [Send()] in class [SendEmail], detail:" + ex.Message, "");
            }
            return result;
        }
    }


}