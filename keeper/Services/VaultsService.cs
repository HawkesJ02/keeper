namespace keeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo){
        _repo = repo;
    }
    internal Vault CreateVault(Vault vaultData)
    {
      Vault vault = _repo.CreateVault(vaultData);
      return vault;
    }

    internal Vault GetVaultById(int id)
    {
     Vault vault = _repo.GetVaultById(id);
     if (vault == null) throw new Exception($"No found vault at ID location: {id}");
     return vault;
    }
  }
}