namespace allSpiceV2.Repositories;

public class RecipesRepository
{

    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe Create(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes
        (title, category, img, instructions, creatorId)
        VALUES
        (@title, @category, @img, @instructions, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, recipeData);
        recipeData.Id = id;
        return recipeData;

    }


    internal List<Recipe> Get()
    {
        string sql = @"
        SELECT
        re.*,
        ac.*
        from recipes re
        JOIN accounts ac ON ac.id = re.creatorId;
        ";
        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }).ToList();
        return recipes;

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
    internal bool Edit(Recipe original)
    {
        string sql = @"
        EDIT recipes
        SET
        title = @title,
        instructions = @instructions,
        img = @img,
        category = @category
        WHERE id = @id;
        ";
        int rows = _db.Execute(sql, original);
        return rows > 0;
    }

    internal void Delete(int id)
    {
        string sql = @"
        DELETE FROM recipes
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }
}
