namespace keeper.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
       private readonly ProfilesService _profilesService;
       private readonly Auth0Provider _auth;

       public ProfilesController(ProfilesService profilesService, Auth0Provider auth){
        _profilesService = profilesService;
        _auth = auth;
       }
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>>GetProfileById(string id){
        try 
        {
           Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
           Profile profile = _profilesService.GetProfileById(id);
           return Ok(profile);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }
    }
}