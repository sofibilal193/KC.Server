using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kashmir.Captain.Server.Application.Workers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kashmir.Captain.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly IMediator _mediator;
		public HomeController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Create Tool
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("tool")]
		public async Task<IActionResult> AddToolAsync(UpsertToolCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		/// <summary>
		/// Upsert Worker
		/// </summary>
		/// <param name="id"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPut("tool")]
		public async Task<IActionResult> UpdatetToolAsync(int id, UpsertToolCommand command)
		{
			command.SetId(id);
			var response = await _mediator.Send(command);
			return Ok(response);
		}
	}
}