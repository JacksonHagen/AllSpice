using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
	public class StepsRepository
	{
		private readonly IDbConnection _db;

		public StepsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal List<Step> GetStepsByRecipe(int id)
		{
			string sql = @"
			SELECT
				r.*,
				s.*
			FROM steps s
			JOIN recipes r ON s.recipeId = r.id
			WHERE s.recipeId = @id;
			";
			return _db.Query<Recipe, Step, Step>(sql, (recipe, step) =>
			{
				step.RecipeId = recipe.Id;
				return step;
			}, new { id }).ToList();
		}

		internal Step Get(int id)
		{
			string sql = @"
			SELECT
				r.*,
				s.*
			FROM steps s
			JOIN recipes r ON s.recipeId = r.id
			WHERE s.id = @id;
			";
			return _db.Query<Recipe, Step, Step>(sql, (recipe, step) =>
			{
				step.RecipeId = recipe.Id;
				return step;
			}, new { id }).FirstOrDefault();
		}

		internal Step Create(Step stepData)
		{
			string sql = @"
			INSERT INTO steps
			(position, body, recipeId, creatorId)
			VALUES
			(@Position, @Body, @RecipeId, @CreatorId);
			SELECT LAST_INSERT_ID();
			";
			stepData.Id = _db.ExecuteScalar<int>(sql, stepData);
			return stepData;
		}

		internal void Edit(Step original)
		{
			string sql = @"
			UPDATE steps
			SET
				position = @Position,
				body = @Body
			WHERE id = @Id;
			";
			_db.Execute(sql, original);
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM steps WHERE id = @id LIMIT 1";
			_db.Execute(sql, new { id });
		}
	}
}