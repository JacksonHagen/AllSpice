using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly AccountService _accountService;
		private readonly FavoritesService _fs;

		public AccountController(AccountService accountService, FavoritesService fs)
		{
			_accountService = accountService;
			_fs = fs;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<Account>> Get()
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				return Ok(_accountService.GetOrCreateProfile(userInfo));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet("favorites")]
		[Authorize]
		public async Task<ActionResult<List<Favorite>>> GetFavorites()
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				List<Favorite> favorites = _fs.GetByAccount(userInfo.Id);
				return Ok(favorites);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}


}