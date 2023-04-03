namespace keeper.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;
    public ProfilesService(ProfilesRepository repo){
        _repo = repo;
    }
    internal Profile GetProfileById(string id)
    {
      Profile profile = _repo.GetProfileById(id);
      return profile;
    }
  }
}