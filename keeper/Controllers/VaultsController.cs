namespace keeper.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth){
            _vaultsService = vaultsService;
            _auth = auth;
        }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData){
        try 
        {
          Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
          vaultData.CreatorId = userInfo.Id;
          Vault vault = _vaultsService.CreateVault(vaultData);
          vault.Creator = userInfo;
          return Ok(vault);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>>GetVaultById(int id){
        try 
        {
         Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
         Vault vault = _vaultsService.GetVaultById(id, userInfo?.Id);
         return Ok(vault);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault updateData, int id){
        try 
        {
          Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
          updateData.CreatorId = userInfo.Id;
          updateData.Id = id;
          Vault vault = _vaultsService.UpdateVault(updateData);
          return Ok(vault);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }
    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>>
    DeleteVault(int id){
        try 
        {
          Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
          string message = _vaultsService.DeleteVault(id, userInfo.Id);
          return Ok(message);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }



    }
}