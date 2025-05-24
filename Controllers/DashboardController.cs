using KC.Application.Common.Controllers;
using KC.Application.Home.Queries;
using KC.KC.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KC.Controllers
{
	[Route("api/Dashboards")]
	public class DashboardController : BaseController
	{
		private readonly IMediator _mediator;
		public DashboardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Create Tool
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost("add")]
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
		[HttpPut("update")]
		public async Task<IActionResult> UpdatetToolAsync(int id, UpsertToolCommand command)
		{
			command.SetId(id);
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		[HttpGet("tools")]
		public async Task<IActionResult> GetToolsAsync()
		{
			var response = await _mediator.Send(new GetToolsQuery());
			return Ok(response);
		}
	}
}