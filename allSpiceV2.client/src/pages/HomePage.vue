<template>
  <div class="row text-end">
    <col-2>
      <button data-bs-toggle="modal" data-bs-target="#recipeModal"> CREATE RECIPE </button>
    </col-2>
  </div>
  <div class="row">
    <div v-for="r in recipes" class="col-12 col-md-3 mb-3 p-4">
      <RecipeCard :recipe="r" />
    </div>
  </div>
</template>

<script>

import { onMounted, computed } from 'vue';
import { AppState } from '../AppState.js';
import { recipesService } from '../services/RecipesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import RecipeCard from '../components/RecipeCard.vue';

export default {
  setup() {

    async function getRecipes() {
      try {
        await recipesService.getAll();
      } catch (error) {
        Pop.error(error.message);
        logger.error(error)
      }
    }

    onMounted(() => {
      getRecipes();
    });
    return {
      recipes: computed(() => AppState.recipes),

    }
  },
  components: { RecipeCard }
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

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
