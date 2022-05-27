using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
	public class StepsService
	{
		private readonly StepsRepository _repo;

		public StepsService(StepsRepository repo)
		{
			_repo = repo;
		}

		internal List<Step> GetStepsByRecipe(int recipeId)
		{
			return _repo.GetStepsByRecipe(recipeId);
		}
		internal Step Get(int id)
		{
			Step step = _repo.Get(id);
			if (step == null)
			{
				throw new Exception("Invalid Step Id.");
			}
			return step;
		}

		internal Step Create(Step stepData, Recipe recipe)
		{
			if (stepData.CreatorId != recipe.CreatorId)
			{
				throw new Exception("You cannot add steps to a recipe that is not yours.");
			}
			return _repo.Create(stepData);
		}

		internal Step Edit(Step stepData)
		{
			Step original = Get(stepData.Id);
			if (original.CreatorId != stepData.CreatorId)
			{
				throw new Exception("You cannot edit steps on a recipe that is not yours.");
			}
			original.Position = stepData.Position;
			original.Body = stepData.Body ?? original.Body;
			_repo.Edit(original);
			return Get(original.Id);
		}

		internal void Delete(int id, string userId)
		{
			Step target = Get(id);
			if (target.CreatorId != userId)
			{
				throw new Exception("You cannot delete ingredients on a recipe that is not yours.");
			}
			_repo.Delete(id);
		}
	}
}