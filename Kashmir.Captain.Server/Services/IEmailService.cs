using Kashmir.Captain.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Services
{
    public interface IEmailService
{
    Task SendEmailAsync(EmailTemplate emailTemplate);
}

}