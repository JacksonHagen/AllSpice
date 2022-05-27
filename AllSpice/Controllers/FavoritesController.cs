using System;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
	[ApiController]
	[Route("api/recipes/{recipeId}/favorite")]
	public class FavoritesController : ControllerBase
	{
		private readonly FavoritesService _fs;

		public FavoritesController(FavoritesService fs)
		{
			_fs = fs;
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<String>> MakeFavorite(int recipeId)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				_fs.Create(recipeId, userInfo.Id);
				return Ok("Favorite Added!");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete]
		[Authorize]
		public async Task<ActionResult<String>> RemoveFavorite(int recipeId) {
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				_fs.Delete(recipeId, userInfo.Id);
				return Ok("Favorite Removed!");
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}