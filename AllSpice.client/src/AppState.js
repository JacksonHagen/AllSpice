import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
	filteredRecipes: null,
	allRecipes: null,
	activeSteps: null,
	activeIngredients: null,
	favorites: null,
	filterBy: "all"
})
