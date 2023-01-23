namespace allSpiceV2.Repositories;

public class FavoriteRepository
{
    private readonly IDbConnection _db;

    public FavoriteRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite Create(Favorite newFavorite)
    {
        string sql = @"
    INSERT INTO favorites
    (recipeId, accountId)
    VALUES
    (@recipeId, @accountId);
    SELECT LAST_INSERT_Id();
    ";
        int id = _db.ExecuteScalar<int>(sql, newFavorite);
        newFavorite.Id = id;

        return newFavorite;
    }

    internal void Delete(int id)
    {
        string sql = @"
        DELETE FROM favorites
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }

    // internal List<Favorite> GetFavorites(int recipeId)
    // {
    //     string sql = @"
    //     SELECT
    //     ac.*,
    //     l.id AS likerId
    //     FROM likers l
    //     JOIN accounts ac ON ac.id = l.accountId
    //     WHERE l.recipeId = @recipeId;
    //     ";
    //     return _db.Query<Favorite>(sql, new { recipeId }).ToList();
    // }

    internal List<FavoritedRecipe> GetFavorites(string accountId)
    {
        string sql = @"
    SELECT
    re.*,
    f.*,
    cr.*
    FROM favorites f
    JOIN recipes re ON re.id = f.recipeId
    JOIN accounts cr ON re.creatorId = cr.id
    WHERE f.accountId = @accountId;
    ";
        List<FavoritedRecipe> favoritedRecipes = _db.Query<FavoritedRecipe, Favorite, Account, FavoritedRecipe>(sql, (re, f, cr) =>
        {
            re.FavoriteId = f.Id;
            re.Creator = cr;
            return re;
        }, new { accountId }).ToList();
        return favoritedRecipes;
    }


    internal Favorite GetOne(int id)
    {
        string sql = @"
        SELECT
      *
        FROM favorites
        WHERE id = @id;
        ";
        return _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
    }
}
