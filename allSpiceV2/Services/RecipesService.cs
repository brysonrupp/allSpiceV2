namespace allSpiceV2.Services;

public class RecipesService
{

    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }

    internal Recipe Create(Recipe recipeData)
    {
        Recipe recipe = _repo.Create(recipeData);
        return recipe;
    }


    internal List<Recipe> Get(string userId)
    {
        List<Recipe> recipes = _repo.Get();
        return recipes;
    }

    internal Recipe GetOne(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null)
        {
            throw new Exception("no recipe at that id");
        }
        return recipe;
    }
    internal Recipe Edit(Recipe recipeEdit, int id)
    {

    }
}
