import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RecipesService
{
	async getAll() {
		const res = await api.get('api/recipes');
		AppState.allRecipes = res.data;
	}
	async create(recipeData) {
		const res = await api.post('api/recipes', recipeData);
		AppState.allRecipes.unshift(res.data);
		return res.data.id
	}
}

export const recipesService = new RecipesService();