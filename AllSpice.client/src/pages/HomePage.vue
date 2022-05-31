<template>
  <div class="row justify-content-around mt-5">
    <div class="text-end position-fixed col-12">
      <button
        class="btn btn-info round"
        data-bs-toggle="modal"
        data-bs-target="#createRecipe"
        type="button"
        @click="resetForm()"
        v-if="user.isAuthenticated"
      >
        <i class="mdi mdi-plus"></i>
      </button>
    </div>
    <RecipeCard v-for="r in recipes" :key="r.id" :recipe="r" />
  </div>
  <Modal id="createRecipe">
    <template #title>
      <h3>Create a recipe</h3>
    </template>
    <template #body>
      <RecipeForm />
    </template>
  </Modal>
</template>

<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { onMounted, watchEffect } from '@vue/runtime-core'
import { recipesService } from "../services/RecipesService.js";
import { favoritesService } from "../services/FavoritesService.js";
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
export default {
  name: 'Home',
  setup() {
    const recipes = ref([])
    onMounted(async () => {
      try {
        await recipesService.getAll();
      }
      catch (error) {
        console.error("[COULD_NOT_GET_RECIPES]", error.message);
        Pop.toast(error.message, "error");
      }
    })
    watchEffect(async () => {
      let user = AppState.user
      if (user.isAuthenticated) {
        try {
          await favoritesService.getFavorites();
        }
        catch (error) {
          console.error("[COULD_NOT_GET_FAVORITES]", error.message);
          Pop.toast(error.message, "error");
        }
      }
    })
    watchEffect(() => {
      let filterBy = AppState.filterBy
      let user = AppState.user
      let account = AppState.account
      if (filterBy === 'all') {
        recipes.value = AppState.allRecipes
      } else if (filterBy === 'myRecipes') {
        recipes.value = AppState.allRecipes.filter(r => r.creatorId === account?.id)
      } else if (filterBy === 'favorites') {
        recipes.value = AppState.favorites
      }
    })

    return {
      recipes,
      user: computed(() => AppState.user),
      resetForm() {
        document.getElementById('recipeForm').reset()
      }
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
  .round {
    border-radius: 100% !important;
  }
}
</style>
