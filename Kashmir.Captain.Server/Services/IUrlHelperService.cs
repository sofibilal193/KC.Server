using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashmir.Captain.Server.Services
{
	public interface IUrlHelperService
{
    string GenerateUrl(string action, string controller, object? values = null);
}
}