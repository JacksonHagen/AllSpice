<template>
  <div class="row">
    <div class="card bg-light p-0">
      <img
        src="https://images.unsplash.com/photo-1532092367580-3bd5bc78dd9d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
        alt=""
        class="topBar card-img"
      />
      <div class="card-img-overlay text-light text-center">
        <div class="row justify-content-between align-items-center">
          <div class="col-2"></div>
          <div
            class="
              col-md-3
              p-1
              rounded
              d-flex
              flex-column
              title-shadow
              rounded
              text-shadow
            "
          >
            <h1 class="dancing">All-Spice</h1>
            <h2 class="fw-light dancing">Keep your friends close</h2>
            <h2 class="fw-light dancing">And your recipes closer</h2>
          </div>
          <div class="col-md-2 rounded recipe-text p-2">
            <Login />
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="row justify-content-center">
    <div class="col-md-6">
      <div class="row justify-content-around">
        <div class="col-1"></div>
        <div
          class="
            col-3
            border border-secondary
            bg-info
            text-center
            rounded-bottom
            border-top-0
            selectable
          "
          @click="filter('all')"
        >
          <h3 class="py-2 dosis text-white text-shadow">Home</h3>
        </div>
        <div
          class="
            col-3
            border border-secondary
            bg-info
            text-center
            rounded-bottom
            border-top-0
            selectable
          "
          @click="filter('myRecipes')"
        >
          <h3 class="py-2 dosis text-white text-shadow">My Recipes</h3>
        </div>
        <div
          class="
            col-3
            border border-secondary
            bg-info
            text-center
            rounded-bottom
            border-top-0
            selectable
          "
          @click="filter('favorites')"
        >
          <h3 class="py-2 dosis text-white text-shadow">Favorites</h3>
        </div>
        <div class="col-1"></div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState.js'
export default {
  setup() {
    return {
      filter(str) {
        AppState.filterBy = str;
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.topBar {
  height: 25vh;
  object-fit: cover;
  object-position: 50% 80%;
}
.recipe-text {
  background-color: rgba(55, 70, 70, 0.56);
  color: rgb(255, 255, 255);
}
</style>