<template>
  <div class="col-md-4 mb-5">
    <div
      class="card selectable"
      :title="recipe.title"
      @click="openDetailsModal"
    >
      <img :src="recipe.picture" alt="" class="card-img recipePic" />
      <div
        class="card-img-overlay d-flex flex-column justify-content-between p-0"
      >
        <div class="recipe-text dosis p-2 rounded-top">
          <h5 class="card-title">
            {{ recipe.category }}
          </h5>
        </div>
        <div class="recipe-text p-2 rounded-bottom m-0 dosis">
          <h4>
            {{ recipe.title }}
          </h4>
          <p class="fs-6 mb-0">
            {{ recipe.subtitle }}
          </p>
        </div>
      </div>
    </div>
  </div>
  <LargeModal :id="'rid-' + recipe.id">
    <template #title>
      <h3>
        {{ recipe.title }}
      </h3>
    </template>
    <template #body>
      <RecipeDetails :recipe="recipe" />
    </template>
  </LargeModal>
</template>


<script>
import { Modal } from 'bootstrap'
import { stepsService } from "../services/StepsService.js";
import { ingredientsService } from "../services/IngredientsService.js";
export default {
  props: {
    recipe: {
      type: Object,
      required: true
    },
  },
  setup(props) {
    return {
      async openDetailsModal() {
        Modal.getOrCreateInstance(document.getElementById('rid-' + props.recipe.id)).toggle()
        await stepsService.getByRecipe(props.recipe.id);
        await ingredientsService.getByRecipe(props.recipe.id);
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.recipePic {
  object-fit: cover;
  width: 100%;
  height: 25vw;
}
.recipe-text {
  background-color: rgba(55, 70, 70, 0.56);
  color: rgb(255, 255, 255);
}
</style>