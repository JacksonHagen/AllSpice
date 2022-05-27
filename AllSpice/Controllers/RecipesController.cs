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
	[Route("api/[controller]")]
	public class RecipesController : ControllerBase
	{
		private readonly RecipesService _rs;

		public RecipesController(RecipesService rs)
		{
			_rs = rs;
		}

		[HttpGet]
		public ActionResult<List<Recipe>> Get()
		{
			try
			{
				List<Recipe> recipes = _rs.Get();
				return Ok(recipes);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<Recipe> Get(int id)
		{
			try
			{
				Recipe recipe = _rs.Get(id);
				return Ok(recipe);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				recipeData.CreatorId = userInfo.Id;
				Recipe recipe = _rs.Create(recipeData);
				recipe.Creator = userInfo;
				return Ok(recipe);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult<Recipe>> Edit([FromBody] Recipe recipeData, int id)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				recipeData.CreatorId = userInfo.Id;
				recipeData.Id = id;
				Recipe updatedRecipe = _rs.Edit(recipeData);
				return Ok(updatedRecipe);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<String>> Delete(int id)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				_rs.Delete(id, userInfo.Id);
				return Ok("Deletion success!");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		
	}
}