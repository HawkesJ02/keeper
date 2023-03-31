namespace keeper.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db){
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
     string sql = @"
    INSERT INTO keeps
    (`Name`, `Description`, `Img`, `CreatorId` )
    VALUES
    (@Name, @Description, @Img, @CreatorId); 
    SELECT LAST_INSERT_ID();";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData; 
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"
    SELECT *
    FROM keeps
    JOIN accounts creator ON keeps.CreatorId = creator.id
    WHERE keeps.id = @id;";
    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, creator)
    => {
        keep.Creator = creator;
        return keep;
    }, new {id}).FirstOrDefault();
    return keep;
    }

    internal List<Keep> GetKeeps()
    {
      string sql = @"
SELECT *
FROM keeps
JOIN accounts creator ON keeps.CreatorId = creator.id GROUP BY keeps.id;";
List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keeps, creator) => {
    keeps.Creator = creator;
    return keeps;
}).ToList();
return keeps;
    }
  }
}