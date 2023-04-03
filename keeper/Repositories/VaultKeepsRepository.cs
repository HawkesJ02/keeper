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

    internal VaultKeep GetVaultKeepsById(int id)
    {
      string sql = @"
      SELECT *
      FROM vaultkeeps
      JOIN accounts creator ON vaultkeeps.CreatorId = creator.id
      JOIN vaults ON vaultkeeps.VaultId = vaults.id
      JOIN keeps ON vaultkeeps.KeepId = keeps.id
      WHERE vaultkeeps.id = @id;";
      VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, creator) => {
        vaultKeep.Creator = creator;
        return vaultKeep;
      }, new{id}).FirstOrDefault();
      return vaultKeep;
    }
  }
}