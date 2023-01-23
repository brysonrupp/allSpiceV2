namespace allSpiceV2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _RecipesService;

    private readonly Auth0Provider _auth0provider;

    public RecipesController(RecipesService RecipesService, Auth0Provider auth0Provider)
    {
        _RecipesService = RecipesService;
        _auth0provider = auth0Provider;
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> Get()
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Recipe> recipe = _RecipesService.Get(userInfo?.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _RecipesService.Create(recipeData);
            recipe.Creator = userInfo;
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetOne(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            Recipe recipe = _RecipesService.GetOne(id, userInfo?.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]

    public ActionResult<Recipe> Edit([FromBody] Recipe recipeEdit, int id)
    {
        try
        {
            Recipe recipe = _RecipesService.Edit(recipeEdit, id);
            return Ok(recipe);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            throw;
        }
    }

    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>> Delete(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _RecipesService.Delete(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

}
