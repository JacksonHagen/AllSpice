import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class IngredientsService
{
	async getByRecipe(recipeId) {
		const res = await api.get('api/recipes/' + recipeId + '/ingredients');
		AppState.activeIngredients = res.data;
	}
	async createIngredient(ingredientData) {
		const res = await api.post('api/recipes/' + ingredientData.recipeId + '/ingredients', ingredientData);
		AppState.activeIngredients.push(res.data);
	}
	async delete(id, recipeId) {
		await api.delete('api/recipes/' + recipeId + '/ingredients/' + id);
		const index = AppState.activeIngredients.findIndex(i => i.id === id);
		AppState.activeIngredients.splice(index, 1);
	}
}

export const ingredientsService = new IngredientsService();