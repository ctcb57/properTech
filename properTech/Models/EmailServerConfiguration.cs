using properTech.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class EmailServerConfiguration
    {
        public EmailServerConfiguration(int _smtpPort = 587)
        {
            SmtpPort = _smtpPort;
        }
        public string SmtpServer = "smtp.gmail.com";
        public int SmtpPort { get; }
        public string SmtpUsername = "devcodecampsweepstakes@gmail.com";
        public string SmtpPassword = Password.EmailServerPassword;

    }
}
