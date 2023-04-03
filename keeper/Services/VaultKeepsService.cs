namespace keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo){
        _repo = repo;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep vaultkeepData)
    {
      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultkeepData);
      return vaultKeep;
    }

    internal VaultKeep GetVaultKeepsById(int VaultId, string userId)
    {
      VaultKeep vaultKeep = _repo.GetVaultKeepsById(VaultId);
      if (vaultKeep == null) throw new Exception($"No found vaultkeeps at ID location: {VaultId}");
      return vaultKeep;
    }
  }
}