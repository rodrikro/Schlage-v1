using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administrador.Modulos.emails {
    public class EmailServer {
        private string toEmail = ""; 

        private Andon.MailManipulation mailServer;

        public void configure( string from, string to ) {
            this.toEmail = to;
            this.mailServer = new Andon.MailManipulation(from, true);
        }

        public void SendEmail(EmailInterface email){
            var subject = email.getSubject();
            var body = email.getBody();
            
            //Enviar Email
            this.mailServer.SendMail(this.toEmail, subject, body);
        }
    }
}
