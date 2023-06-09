namespace keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;

    private readonly KeepsService _keepsService;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService){
        _repo = repo;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep vaultkeepData, string userId)
    {
      Vault vault = _vaultsService.GetVaultById(vaultkeepData.VaultId, userId);
      if(vault == null || vault.CreatorId != userId) throw new Exception("Bad Request, dummy");
      Keep keep = _keepsService.GetKeepById(vaultkeepData.KeepId, userId);
      if(keep == null) throw new Exception("Your silly, billy");
      keep.Kept = keep.Kept + 1; 
      if(keep != null)_keepsService.UpdateKept(keep);
      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultkeepData);
      return vaultKeep;
    }

    internal List<VaultKeepViewModel> GetVaultKeepsById(int VaultId, string userId)
    {
      Vault vault = _vaultsService.GetVaultById(VaultId, userId);
      List<VaultKeepViewModel> vaultKeeps = _repo.GetVaultKeepsById(VaultId);
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