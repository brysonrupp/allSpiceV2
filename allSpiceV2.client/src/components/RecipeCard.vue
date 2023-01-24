<template>
    <!-- //NOTE - add @click for active modal after making active recipe function -->
    <div @click="setActiveRecipe(recipe.id)" class="elevation-6 rounded bg-success">
        <img :src="recipe.img" alt="" class="img-fluid card-img rounded-top">
        <div class="text-center fw-bold p-1">
            <b>{{ recipe.title }}</b>
            <p>{{ recipe.instructions }}</p>
            <div> {{ recipe.category }}</div>
        </div>
    </div>

</template>


<script>
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';


export default {
    props: {
        recipe: { type: Object, required: true }
    },
    setup(props) {
        return {
            setActiveRecipe(recipeId) {
                AppState.activeRecipe = props.recipe
                // ingredientsService.getIngredients(recipeId)
                logger.log(AppState.activeRecipe)
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.card-img {
    height: 30vh;
    width: 100%;
    object-fit: cover;
}

.elevation-6 {
    box-shadow: 4pt 4pt whitesmoke;
}
</style>