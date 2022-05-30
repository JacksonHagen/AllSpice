import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class FavoritesService
{
	async getFavorites() {
		const res = await api.get('account/favorites');
		logger.log(res.data)
		AppState.favorites = res.data;
	}
	async addFavorite(recipeId) {
		const res = await api.post('api/recipes/' + recipeId + '/favorite', { recipeId });
		logger.log(res.data)
		AppState.favorites.unshift(res.data)
	}
	async deleteFavorite(recipeId) {
		await api.delete('api/recipes/' + recipeId + '/favorite');
		const index = AppState.favorites.findIndex(f => f.recipeId = recipeId);
		AppState.favorites.splice(index, 1);
	}
}

export const favoritesService = new FavoritesService();