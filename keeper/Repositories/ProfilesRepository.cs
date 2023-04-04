namespace keeper.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;
    public ProfilesRepository(IDbConnection db){
        _db = db;
    }
    internal Profile GetProfileById(string id)
    {
      string sql = @"
      SELECT
      profiles.*
      FROM profiles
      JOIN accounts ON profiles.accountId = accounts.id
      WHERE accountId = @id;";
      Profile profile = _db.Query<Profile>(sql).FirstOrDefault();
      return profile;
    }
  }
}