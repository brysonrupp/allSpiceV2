namespace allSpiceV2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0provider;

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0provider)
    {
        _favoritesService = favoritesService;
        _auth0provider = auth0provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Favorite>> Create([FromBody] Favorite newFavorite)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            newFavorite.AccountId = userInfo.Id;
            return _favoritesService.Create(newFavorite);
            // userInfo.LikerId = id;
            // return Ok(userInfo);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _favoritesService.Delete(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }




}
