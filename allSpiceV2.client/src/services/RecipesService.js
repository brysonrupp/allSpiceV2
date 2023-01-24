import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {
    async getAll() {
        const res = await api.get('api/recipes')
        logger.log('[recipes get all]', res.data)
        AppState.recipes = res.data
    }

    async createRecipe(body) {
        const url = 'api/recipes'
        const res = await api.post(url, body)
        logger.log('[create recipe]', res.data)
        AppState.recipes.push(res.data)
        return res.data
    }

    async deleteRecipe(id) {
        const res = await api.delete('api/recipes/' + id)
        logger.log(res.data)
        let index = AppState.recipes.findIndex(r => r.id == id)
        if (index >= 0) {
            AppState.recipes.splice(index, 1)
        }
    }

}



export const recipesService = new RecipesService()