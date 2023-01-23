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
    (@name, @quantity, @creatorId, @recipeId)
    ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        string sql = @"
        SELECT
        I.*,
        a.*
        From ingredients I
        JOIN accounts a ON p.creatorId = a.id
        WHERE recipeId = @recipeId;
        ";
        List<Ingredient> ingredients = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, new { recipeId }).ToList();
        return ingredients;
    }
}
