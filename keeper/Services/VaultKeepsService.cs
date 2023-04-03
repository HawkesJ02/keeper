namespace keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService){
        _repo = repo;
        _vaultsService = vaultsService;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep vaultkeepData)
    {
      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultkeepData);
      return vaultKeep;
    }

    internal List<VaultKeep> GetVaultKeepsById(int VaultId, string userId)
    {
      Vault vault = _vaultsService.GetVaultById(VaultId, userId);
      List<VaultKeep> vaultKeeps = _repo.GetVaultKeepsById(VaultId);
      if (vaultKeeps == null) throw new Exception($"No found vaultkeeps at ID location: {VaultId}");
      return vaultKeeps;
    }

    internal VaultKeep GetVaultKeepById(int id){
      VaultKeep vaultkeep = _repo.GetSingleVaultkeep(id);
      return vaultkeep;
    }

    internal string RemoveVaultKeep(int id, string userId)
    {
      VaultKeep vaultkeep = this.GetVaultKeepById(id);
      if (vaultkeep.CreatorId != userId) throw new Exception("Deleting vaultkeeps that are not your own is not allowed.");
      _repo.RemoveVaultKeep(id);
      return $"Vaultkeep was deleted";
    }
  }
}