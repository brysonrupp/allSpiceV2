namespace allSpiceV2.Services;

public class FavoritesService
{
    private readonly FavoriteRepository _repo;
    private readonly RecipesService _recipesService;

    public FavoritesService(FavoriteRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal ActionResult<Favorite> Create(Favorite newFavorite)
    {

        Recipe recipe = _recipesService.GetOne(newFavorite.RecipeId, newFavorite.AccountId);

        Favorite favorite = _repo.Create(newFavorite);
        return favorite;
        // int id = _repo.Create(likerData);
        // return id;
    }

    internal string Delete(int id, string userId)
    {
        Recipe original = _repo.GetOne(id);
        if (original == null)
        {
            throw new Exception("no recipe to unlike");
        }
        _repo.Delete(id);
        return $"Recipe at {id} was removed";
    }

    internal List<Favorite> GetFavorites(int recipeId, string userId)
    {
        Recipe recipe = _recipesService.GetOne(recipeId, userId);
        List<Favorite> favorites = _repo.GetFavorites(recipeId);
        return favorites;
    }


}
