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

    internal Recipe Get(int id)
    {
        Recipe recipe = _repo.Get(id);
        if (recipe == null)
        {
            throw new Exception("no recipe at that id");
        }
        return recipe;
    }


    internal Recipe GetOne(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null)
        {
            throw new Exception("no recipe at that id");
        }
        if (recipe.CreatorId != userId)
        {
            throw new Exception("you dont own that recipe");
        }
        return recipe;
    }
    internal Recipe Edit(Recipe recipeEdit, int id)
    {
        Recipe original = Get(id);
        original.Title = recipeEdit.Title ?? original.Title;
        original.Instructions = recipeEdit.Instructions ?? original.Instructions;
        original.Img = recipeEdit.Img ?? original.Img;
        original.Category = recipeEdit.Category ?? original.Category;

        bool edited = _repo.Edit(original);
        if (edited == false)
        {
            throw new Exception("Recipe was not edited");
        }
        return original;
    }

    internal string Delete(int id, string userId)
    {
        Recipe original = GetOne(id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("Not your recipe");
        }
        _repo.Delete(id);
        return $"{original.Title} has been deleted";
    }
}
