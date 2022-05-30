import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class StepsService
{
	async getByRecipe(recipeId) {
		const res = await api.get('api/recipes/' + recipeId + '/steps');
		logger.log(res);
		AppState.activeSteps = res.data;
	}
	async createStep(stepData) {
		const res = await api.post('api/recipes/' + stepData.recipeId + '/steps', stepData);
		logger.log(res.data)
		AppState.activeSteps.push(res.data);
	}
	async delete(id, recipeId) {
		await api.delete('api/recipes/' + recipeId + '/steps/' + id);
		const index = AppState.activeSteps.findIndex(s => s.id == id);
		AppState.activeSteps.splice(index, 1);
		AppState.activeSteps.forEach((s, i) => s.position = i +1);
	}
}

export const stepsService = new StepsService();	