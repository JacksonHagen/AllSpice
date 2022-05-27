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
	public class StepsController : ControllerBase
	{
		private readonly StepsService _ss;
		private readonly RecipesService _rs;

		public StepsController(StepsService ss, RecipesService rs)
		{
			_ss = ss;
			_rs = rs;
		}

		[HttpGet]
		public ActionResult<List<Step>> GetStepsByRecipe(int recipeId)
		{
			try
			{
				List<Step> steps = _ss.GetStepsByRecipe(recipeId);
				return Ok(steps);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<Step> Get(int id)
		{
			try
			{
				Step step = _ss.Get(id);
				return Ok(step);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Step>> Create([FromBody] Step stepData, int recipeId)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				Recipe recipe = _rs.Get(recipeId);
				stepData.CreatorId = userInfo.Id;
				stepData.RecipeId = recipeId;
				Step newStep = _ss.Create(stepData, recipe);
				return Ok(newStep);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult<Step>> Edit([FromBody] Step stepData, int id, int recipeId)
		{
			try
			{
				Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
				stepData.CreatorId = userInfo.Id;
				stepData.RecipeId = recipeId;
				stepData.Id = id;
				Step step = _ss.Edit(stepData);
				return Ok(step);
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
				_ss.Delete(id, userInfo.Id);
				return Ok("Deletion Successful");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}