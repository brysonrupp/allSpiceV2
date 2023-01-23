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
    INSERT INTO likers
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
        DELETE FROM recipes
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }

    internal List<Favorite> GetFavorites(int recipeId)
    {
        string sql = @"
        SELECT
        ac.*,
        l.id AS likerId
        FROM likers l
        JOIN accounts ac ON ac.id = l.accountId
        WHERE l.recipeId = @recipeId;
        ";
        return _db.Query<Favorite>(sql, new { recipeId }).ToList();
    }

    internal Recipe GetOne(int id)
    {
        string sql = @"
        SELECT
        re.*,
        ac.*
        FROM recipes re
        JOIN accounts ac ON ac.id = re.creatorId
        WHERE re.id = @id;
        ";
        return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { id }).FirstOrDefault();
    }
}
