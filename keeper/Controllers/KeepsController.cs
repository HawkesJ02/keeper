namespace keeper.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
       private readonly KeepsService _keepsService;
       private readonly Auth0Provider _auth;

       public KeepsController(KeepsService keepsService, Auth0Provider auth)
       {
        _keepsService = keepsService;
        _auth = auth;
       }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData){
        try 
        {
          Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
          keepData.CreatorId = userInfo.Id;
          Keep keep = _keepsService.CreateKeep(keepData);
          keep.Creator = userInfo;
          return Ok(keep);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>>GetKeeps(){
        try 
        {
            List<Keep> keeps = _keepsService.GetKeeps();
            return Ok(keeps);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }





    }
}