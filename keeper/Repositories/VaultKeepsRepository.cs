namespace keeper.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db){
        _db = db;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep vaultkeepData)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (`CreatorId`, `VaultId`, `KeepId`)
      VALUES
      (@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultkeepData);
      vaultkeepData.Id = id;
      return vaultkeepData;
    }

    internal VaultKeep GetSingleVaultkeep(int id)
    {
     string sql = @"
     SELECT *
     FROM vaultkeeps
     WHERE vaultkeeps.id = @id;";
     VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new {id}).FirstOrDefault();
     return vaultKeep;
    }

internal List<VaultKeepViewModel> GetVaultKeepsById(int id)
    {
string sql = @"
SELECT 
vaultkeeps.*,
keeps.*,
accounts.*
FROM vaultkeeps
JOIN keeps ON vaultkeeps.KeepId = keeps.id
JOIN accounts ON keeps.CreatorId = accounts.id
WHERE vaultkeeps.VaultId = @id;";
List<VaultKeepViewModel> vaultKeeps = _db.Query<VaultKeep,VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, vkvm, prof) =>
{ 
vkvm.vaultKeepId = vk.Id;
vkvm.Creator = prof;
return vkvm;
}, new {id}).ToList();
 return vaultKeeps;
}


    internal int RemoveVaultKeep(int id)
    {
     string sql = @"
     DELETE FROM vaultkeeps
     WHERE id = @id;";
     int rows = _db.Execute(sql, new {id});
     return rows;
    }
  }
}