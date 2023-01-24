<template>
    <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">Create recipe</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <form @submit.prevent="createRecipe()">
        <div class="modal-body">
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.title" type="text" required class="form-control" id="title"
                    placeholder="Recipe Title">
                <label for="title">Recipe Title</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.description" type="text" required class="form-control" id="description"
                    placeholder="Recipe description">
                <label for="title">Recipe description</label>
            </div>
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.Category" type="text" required class="form-control" id="Category"
                    placeholder="Recipe Category">
                <label for="title">Recipe Category</label>
            </div>
            <!-- <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.startDate" type="date" required class="form-control" id="startDate"
                    placeholder="Recipe Date">
                <label for="title">Recipe Date</label>
            </div> -->
            <div class="form-floating mb-3 elevation-5">
                <input v-model="editable.Img" type="url" required class="form-control" id="Img" placeholder="Image">
                <label for="Img">Image</label>
            </div>
            <!-- <div class="form-floating mb-3 elevation-5">
                <select v-model="editable.type" class="form-select" id="floatingSelect"
                    aria-label="Floating label select example">
                    <option value="convention">convention</option>
                    <option value="sport">Sport</option>
                    <option value="digital">Digital</option>
                    <option value="concert">Concert</option>
                </select>
                <label for="floatingSelect">Category</label>
            </div> -->
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Create Recipe</button>
        </div>
    </form>
</template>


<script>
import { ref } from 'vue';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { router } from '../router.js';
export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async createRecipe() {
                try {
                    const recipe = await recipesService.createRecipe(editable.value)
                    editable.value = {}
                    Modal.getOrCreateInstance('#recipeModal').hide()
                    router.push({ title: 'Recipe', params: { recipeId: recipe.id } })
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>

</style>