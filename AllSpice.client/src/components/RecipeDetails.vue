<template>
  <div class="card">
    <div class="row">
      <div class="col-4">
        <img
          :src="recipe.picture"
          alt=""
          class="recipePic card-img rounded-start"
        />
      </div>
      <div class="col-8">
        <div class="row justify-content-between">
          <div class="col-11">
            <h3 class="text-success ps-1 mt-2 mb-0">
              <button
                v-if="
                  user.isAuthenticated &&
                  !favorites.find((f) => f.id === recipe.id)
                "
                class="btn btn-warning btn-sm m-0"
                title="Add to favorites"
                @click="addToFavorites()"
              >
                <i class="mdi mdi-star"></i>
              </button>
              <button
                class="btn bg-danger darken-10 btn-sm m-0"
                title="Remove from favorites"
                v-else-if="user.isAuthenticated"
                @click="removeFromFavorites()"
              >
                <i class="mdi mdi-delete"></i>
              </button>
              {{ recipe.title }}
            </h3>
            <h5 class="text-secondary ps-1 py-0">
              {{ recipe.subtitle }} |
              <i class="text-primary px-2 bg-grey p-1 rounded-pill fs-6">{{
                recipe.category
              }}</i>
            </h5>
          </div>
          <div class="col-1 ps-0 pt-2">
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <hr />
        </div>
        <div class="row">
          <div class="col-6">
            <div class="card me-2">
              <div class="card-header bg-success text-center">
                <h3 class="dosis">Recipe Steps</h3>
              </div>
              <div class="card-body scroll30 ps-0 scrollable-y">
                <ol>
                  <li
                    v-for="s in steps"
                    :key="s.id"
                    contenteditable="true"
                    class="m-0 mb-1 d-flex justify-content-between"
                  >
                    {{ s.position + ".\t" + s.body }}

                    <i
                      v-if="recipe.creatorId === account.id"
                      class="mdi mdi-delete-outline text-danger selectable"
                      @click="deleteStep(s.id)"
                    ></i>
                  </li>
                </ol>
              </div>
              <div
                class="card-body text-center"
                v-if="recipe.creatorId === account.id"
              >
                <form @submit.prevent="addStep()" id="stepForm">
                  <div class="mb-3">
                    <textarea
                      class="form-control bg-white"
                      name=""
                      id=""
                      rows="3"
                      placeholder="Step instructions..."
                      v-model="stepData.body"
                    ></textarea>
                  </div>
                  <button class="btn btn-success w-50">Add Step</button>
                </form>
              </div>
            </div>
          </div>
          <div class="col-6">
            <div class="card me-2">
              <div class="card-header bg-success text-center">
                <h3 class="dosis">Ingredients</h3>
              </div>
              <div class="card-body scroll30 ps-0">
                <ul>
                  <li
                    v-for="i in ingredients"
                    :key="i.id"
                    class="d-flex justify-content-between"
                  >
                    <p class="m-0 mb-1">
                      {{ i.quantity + " of " + i.name }}
                    </p>
                    <i
                      v-if="recipe.creatorId === account.id"
                      class="mdi mdi-delete-outline text-danger selectable"
                      @click="deleteIngredient(i.id)"
                    ></i>
                  </li>
                </ul>
              </div>
              <div
                class="card-body text-center"
                v-if="recipe.creatorId === account.id"
              >
                <form @submit.prevent="addIngredient()" id="ingredientForm">
                  <input
                    type="text"
                    class="form-control bg-white p-2 mb-2"
                    name=""
                    id=""
                    aria-describedby="helpId"
                    placeholder="Ingredient name..."
                    v-model="ingredientData.name"
                  />
                  <input
                    type="text"
                    class="form-control bg-white p-2 mb-2"
                    name=""
                    id=""
                    aria-describedby="helpId"
                    placeholder="Quantity and unit..."
                    v-model="ingredientData.quantity"
                  />
                  <button class="btn btn-success w-50">Add Ingredient</button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import Pop from '../utils/Pop.js'
import { stepsService } from '../services/StepsService.js'
import { ingredientsService } from '../services/IngredientsService.js'
import { favoritesService } from '../services/FavoritesService.js'
import { Modal } from 'bootstrap'
export default {
  props: {
    recipe: {
      type: Object,
      required: true
    },
  },
  setup(props) {
    const stepData = ref({});
    const ingredientData = ref({});
    return {
      stepData,
      ingredientData,
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      steps: computed(() => AppState.activeSteps?.sort((a, b) => (a.position - b.position))),
      ingredients: computed(() => AppState.activeIngredients),
      favorites: computed(() => AppState.favorites),
      async addStep() {
        stepData.value.position = this.steps.length + 1;
        stepData.value.recipeId = props.recipe.id;
        try {
          await stepsService.createStep(stepData.value);
          document.getElementById("stepForm").reset();
          stepData.value = {};
          Pop.toast('Step Added!', "success");
        }
        catch (error) {
          console.error("[COULD_NOT_ADD_STEP]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      async addIngredient() {
        ingredientData.value.recipeId = props.recipe.id;
        try {
          await ingredientsService.createIngredient(ingredientData.value);
          document.getElementById("ingredientForm").reset();
          ingredientData.value = {};
          Pop.toast('Ingredient Added!', 'success');
        }
        catch (error) {
          console.error("[COULD_NOT_ADD_INGREDIENT]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      async deleteStep(id) {
        try {
          if (await Pop.confirm()) {
            await stepsService.delete(id, props.recipe.id);
            Pop.toast('Step deleted!', 'success')
          }
        }
        catch (error) {
          console.error("[COULD_NOT_DELETE_STEP]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      async deleteIngredient(id) {
        try {
          if (await Pop.confirm()) {
            await ingredientsService.delete(id, props.recipe.id);
            Pop.toast('Ingredient deleted!', 'success');
          }
        }
        catch (error) {
          console.error("[COULD_NOT_DELETE_INGREDIENT]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      async addToFavorites() {
        try {
          await favoritesService.addFavorite(props.recipe.id);
          Pop.toast('Added to favorites!', 'success');
        }
        catch (error) {
          console.error("[COULD_NOT_ADD_TO_FAVORITES]", error.message);
          Pop.toast(error.message, "error");
        }
      },
      async removeFromFavorites() {
        try {
          if (await Pop.confirm()) {
            if (AppState.filterBy === 'favorites') {
              Modal.getOrCreateInstance(document.getElementById('rid-' + props.recipe.id)).toggle()
            }
            await favoritesService.deleteFavorite(props.recipe.id);
            Pop.toast('Removed from favorites', 'success')
          }
        }
        catch (error) {
          console.error("[COULD_NOT_REMOVE_FAVORITE]", error.message);
          Pop.toast(error.message, "error");
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.recipePic {
  object-fit: cover;
  height: 100%;
  width: 100%;
}
.scroll30 {
  height: 30vh;
  overflow-y: auto;
}
</style>