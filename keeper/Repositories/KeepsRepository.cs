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

    internal void DeleteKeep(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @id;";
      _db.Execute(sql, new{ id });
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

   internal List<Keep> GetByProfileId(string id)
    {
    string sql = @"
    SELECT *
    FROM keeps
    JOIN accounts creator ON keeps.CreatorId = creator.id
    WHERE keeps.CreatorId = @id;";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keeps, creator) => {
      keeps.Creator = creator;
      return keeps;
    }, new {id}).ToList();
    return keeps; 
    }

    internal int UpdateKeep(Keep updateData)
    {
      string sql = @"
UPDATE keeps
SET
Name = @Name,
Description = @Description,
Img = @Img,
Views = @Views
WHERE keeps.id = @id;";
      int rows = _db.Execute(sql, updateData);
      return rows;
    }
  }


}