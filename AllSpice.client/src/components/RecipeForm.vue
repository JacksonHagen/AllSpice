<template>
  <form @submit.prevent="createRecipe()" id="recipeForm">
    <div class="mb-3">
      <label for="title" class="form-label"></label>
      <input
        type="text"
        name="title"
        id="title"
        class="form-control"
        required
        placeholder="Recipe title..."
        v-model="editable.title"
      />
    </div>
    <div class="mb-3">
      <label for="subtitle" class="form-label"></label>
      <input
        type="text"
        name="subtitle"
        id="subtitle"
        class="form-control"
        required
        placeholder="Recipe subtitle..."
        v-model="editable.subtitle"
      />
    </div>
    <div class="mb-3">
      <label for="picture" class="form-label"></label>
      <input
        type="text"
        name="picture"
        id="picture"
        class="form-control"
        required
        placeholder="Recipe image url..."
        v-model="editable.picture"
      />
    </div>
    <div class="mb-3">
      <label for="category" class="form-label"></label>
      <input
        type="text"
        name="category"
        id="category"
        class="form-control"
        required
        placeholder="Recipe category..."
        v-model="editable.category"
      />
    </div>
    <div class="d-flex pt-4">
      <button
        class="btn btn-danger w-50 me-3"
        type="button"
        @click="resetForm()"
      >
        Reset
      </button>
      <button class="btn btn-success w-50 ms-3">Submit</button>
    </div>
  </form>
</template>


<script>
import { ref } from '@vue/reactivity'
import { recipesService } from '../services/RecipesService.js'
import Pop from '../utils/Pop.js'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      resetForm() {
        document.getElementById('recipeForm').reset()
      },
      async createRecipe() {
        try {
          const id = await recipesService.create(editable.value);
          Pop.toast('Recipe created!', 'success');
          this.resetForm();
          Modal.getOrCreateInstance(document.getElementById('createRecipe')).toggle()
          setTimeout(() => {
            Modal.getOrCreateInstance(document.getElementById('rid-' + id)).toggle()
          }, 750)
        }
        catch (error) {
          console.error("[COULD_NOT_CREATE_RECIPE]", error.message);
          Pop.toast(error.message, "error");
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>