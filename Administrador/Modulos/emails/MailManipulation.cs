using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace Andon
{
    /// <summary>
    /// Mail Class. Manipulates Mail to send.
    /// </summary>
    public class MailManipulation
    {
        MailMessage Mail;
        string smtp_server = "SMTP.ALLEGION.COM";
        //string smtp_server = "hezemeetepeh.aliyon.kon";
        string response = "";
        private string p;
        private bool p_2;
        /// <summary>
        /// Class Constructor. Setups Mail to send, placing From and Mail Format.
        /// </summary>
        /// <param name="From">Sender Mail.</param>
        /// <param name="IsHTML">HTML Format.</param>
        public MailManipulation(string From, bool IsHTML)
        {
            Mail = new MailMessage();
            Mail.From = new MailAddress(From);
            Mail.IsBodyHtml = IsHTML;
        }

        /// <summary>
        /// Class Destructors. Dispose Class Objects.
        /// </summary>
        ~MailManipulation()
        {
            Mail.Dispose();
        }

        /// <summary>
        /// Sends Mail through SMTP Client. Setups To, Subject and Message to send.
        /// </summary>
        /// <param name="To">Mail To.</param>
        /// <param name="Subject">Mail Subject.</param>
        /// <param name="Body">Mail Message.</param>
        public string SendMail(string To, string Subject, string Body)
        {
            Mail.To.Clear();
            Mail.To.Add(To);
            Mail.Subject = Subject;
            Mail.Body = Body;

            try
            {
                SmtpClient MailClient = new SmtpClient(smtp_server);
                MailClient.Send(Mail);
            }
            catch
            {
                //try
                //{
                //    SmtpClient MailClient = new SmtpClient(smtp_server);
                //    MailClient.Send(Mail);
                //}
                //catch (Exception e)
                //{
                //    //System.Windows.Forms.MessageBox.Show("MailManipulation--" + e.Message + "Avisar a Sistemas", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    response = "MailManipulation--" + e.Message + "Avisar a Sistemas";
                //}
            }
            return response;
        }

        /// <summary>
        /// Sends Mail through SMTP Client. Setups To, CC, Subject and Message to send.
        /// </summary>
        /// <param name="To">Mail To.</param>
        /// <param name="CC">Mail CC.</param>
        /// <param name="Subject">Mail Subject.</param>
        /// <param name="Body">Mail Message.</param>
        public string SendMail(string To, string CC, string Subject, string Body)
        {
            Mail.To.Clear();
            Mail.To.Add(To);
            Mail.CC.Clear();
            Mail.CC.Add(CC);
            Mail.Subject = Subject;
            Mail.Body = Body;

            try
            {
                SmtpClient MailClient = new SmtpClient(smtp_server);
                MailClient.Send(Mail);
            }
            catch
            {
                //try
                //{
                //    SmtpClient MailClient = new SmtpClient(smtp_server);
                //    MailClient.Send(Mail);
                //}
                //catch (Exception e)
                //{
                //    //System.Windows.Forms.MessageBox.Show("MailManipulation--" + e.Message + "Avisar a Sistemas", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    response = "MailManipulation--" + e.Message + "Avisar a Sistemas";
                //}
            }
            return response;
        }

        /// <summary>
        /// Sends Mail through SMTP Client. Setups To, CC, BCC, Subject and Message to send.
        /// </summary>
        /// <param name="To">Mail To.</param>
        /// <param name="CC">Mail CC.</param>
        /// <param name="Bcc">Mail BCC.</param>
        /// <param name="Subject">Mail Subject.</param>
        /// <param name="Body">Mail Message.</param>
        public string SendMail(string To, string CC, string Bcc, string Subject, string Body)
        {
            Mail.To.Clear();
            Mail.To.Add(To);
            Mail.CC.Clear();
            Mail.CC.Add(CC);
            Mail.Bcc.Clear();
            Mail.Bcc.Add(Bcc);
            Mail.Subject = Subject;
            Mail.Body = Body;

            try
            {
                SmtpClient MailClient = new SmtpClient(smtp_server);
                MailClient.Send(Mail);
            }
            catch
            {
                //try
                //{
                //    SmtpClient MailClient = new SmtpClient(smtp_server);
                //    MailClient.Send(Mail);
                //}
                //catch (Exception e)
                //{
                //    //System.Windows.Forms.MessageBox.Show("MailManipulation--" + e.Message + "Avisar a Sistemas", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    response = "MailManipulation--" + e.Message + "Avisar a Sistemas";
                //}
            }
            return response;
        }
    }
}