namespace allSpiceV2.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        string sql = @"
    INSERT INTO ingredients
    (name, quantity, creatorId, recipeId)
    VALUES
    (@name, @quantity, @creatorId, @recipeId);
    SELECT LAST_INSERT_ID();
    ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }

    internal void Delete(int id)
    {
        string sql = @"
        DELETE FROM ingredients
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        string sql = @"
        SELECT
        i.*,
        a.*
        From ingredients i
        JOIN accounts a ON i.creatorId = a.id
        WHERE recipeId = @recipeId;
        ";
        List<Ingredient> ingredients = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, new { recipeId }).ToList();
        return ingredients;
    }

    internal Ingredient GetOne(int id)
    {
        string sql = @"
        SELECT
        *
        FROM ingredients
        WHERE id = @id;
        ";
        return _db.Query<Ingredient>(sql, new { id }).FirstOrDefault();
    }
}
