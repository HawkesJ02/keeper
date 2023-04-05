namespace keeper.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
       private readonly ProfilesService _profilesService;
       private readonly AccountService _accountsService;
       private readonly Auth0Provider _auth;

       public ProfilesController(ProfilesService profilesService, Auth0Provider auth, AccountService accountService){
        _profilesService = profilesService;
        _auth = auth;
        _accountsService = accountService;
       }
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>>GetProfileById(string id){
        try 
        {
           Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
           Profile profile = _accountsService.GetProfile(id);
           return Ok(profile);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }
   [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<Keep>>>GetKeepsByProfileId(string id){
        try 
        {
           Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
           List<Keep> keeps = _accountsService.GetKeepsByProfileId(id);
           return Ok(keeps);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }

[HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>>GetVaultsByProfileId(string id){
        try 
        {
           Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
           List<Vault> vaults = _accountsService.GetVaultsByProfileId(id, userInfo?.Id);
           return Ok(vaults);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }




    }
}