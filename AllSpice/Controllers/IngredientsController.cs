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
	[Route("api/recipes/{recipeId}/[controller]")]
	public class IngredientsController : ControllerBase
	{
		private readonly IngredientsService _is;
		private readonly RecipesService _rs;

		public IngredientsController(IngredientsService @is, RecipesService rs)
		{
			_is = @is;
			_rs = rs;
		}

		[HttpGet]
		public ActionResult<List<Ingredient>> Get(int recipeId)
		{
			try
			{
				List<Ingredient> ingredients = _is.GetIngredientsByRecipe(recipeId);
				return Ok(ingredients);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingData, int recipeId)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				Recipe recipe = _rs.Get(recipeId);
				ingData.CreatorId = userInfo.Id;
				ingData.RecipeId = recipeId;
				Ingredient ingredient = _is.Create(ingData, recipe);
				return Ok(ingredient);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult<Ingredient>> Edit([FromBody] Ingredient ingData, int id, int recipeId)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				ingData.CreatorId = userInfo.Id;
				ingData.RecipeId = recipeId;
				ingData.Id = id;
				Ingredient ingredient = _is.Edit(ingData);
				return Ok(ingredient);
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
				_is.Delete(id, userInfo.Id);
				return Ok("Deletion Successful.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}