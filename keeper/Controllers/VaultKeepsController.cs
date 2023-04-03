namespace keeper.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
      private readonly VaultKeepsService _vaultKeepsService;
      private readonly Auth0Provider _auth;

      public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth){
        _vaultKeepsService = vaultKeepsService; 
        _auth = auth;
      }

    

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultkeepData){
        try 
        {
          Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
          vaultkeepData.CreatorId = userInfo.Id;
          VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultkeepData);
          return Ok(vaultKeep);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }
 



    }
}