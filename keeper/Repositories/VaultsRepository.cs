namespace keeper.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db){
        _db = db;
    }
    internal Vault CreateVault(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (`CreatorId`,`Name`, `Description`)
      VALUES
      (@CreatorId, @Name, @Description);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal Vault GetVaultById(int id)
    {
        string sql = @"
        SELECT *
        FROM vaults
        JOIN accounts creator ON vaults.CreatorId = creator.id
        WHERE vaults.Id = @id;";
        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, creator) => {
            vault.Creator = creator;
            return vault;
        }, new {id}).FirstOrDefault();
        return vault;
    }
  }
}