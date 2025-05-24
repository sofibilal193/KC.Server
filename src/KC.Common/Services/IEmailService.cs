using KC.Common.Config;
using KC.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KC.Services
{
	public interface IEmailService
	{
		Task SendEmailAsync(EmailTemplate emailTemplate);
	}
}