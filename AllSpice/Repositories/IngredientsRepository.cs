using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
	public class IngredientsRepository
	{
		private readonly IDbConnection _db;

		public IngredientsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal List<Ingredient> GetIngredientsByRecipe(int id)
		{
			string sql = @"
			SELECT
				r.*,
				i.*
			FROM ingredients i
			JOIN recipes r ON i.recipeId = r.id
			WHERE i.recipeId = @id;
			";
			return _db.Query<Recipe, Ingredient, Ingredient>(sql, (recipe, ingredient) =>
			{
				ingredient.RecipeId = recipe.Id;
				return ingredient;
			}, new { id }).ToList();
		}
		internal Ingredient Get(int id)
		{
			string sql = @"
			SELECT
				r.*,
				i.*
			FROM ingredients i
			JOIN recipes r ON i.recipeId = r.id
			WHERE i.id = @id;
			";
			return _db.Query<Recipe, Ingredient, Ingredient>(sql, (recipe, ingredient) =>
			{
				ingredient.RecipeId = recipe.Id;
				return ingredient;
			}, new { id }).FirstOrDefault();
		}
		internal Ingredient Create(Ingredient ingData)
		{
			string sql = @"
			INSERT INTO ingredients
			(name, quantity, recipeId, creatorId)
			VALUES
			(@Name, @Quantity, @RecipeId, @CreatorId);
			SELECT LAST_INSERT_ID();
			";
			ingData.Id = _db.ExecuteScalar<int>(sql, ingData);
			return ingData;
		}

		internal void Edit(Ingredient original)
		{
			string sql = @"
			UPDATE ingredients
			SET 
				name = @Name,
				quantity = @Quantity
			WHERE id = @Id;
			";
			_db.Execute(sql, original);
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM ingredients WHERE id = @id LIMIT 1";
			_db.Execute(sql, new { id });
		}
	}
}