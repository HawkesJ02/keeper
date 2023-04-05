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

    internal string DeleteVault(int id, string userId)
    {
      Vault vault = _repo.GetVaultById(id);
      if (vault.CreatorId != userId) throw new Exception($"Cannot delete Vault if you are not the creator of vault at ID location: {id}");
      if (vault == null) throw new Exception ("This vault isn't real, go home, what is home?");
      _repo.DeleteVault(id);
      return $"Removed vault!";
    //   TODO Make nicer delete messages
    }

    internal Vault GetVaultById(int id, string userId)
    {
     Vault vault = _repo.GetVaultById(id);
     if (vault == null) throw new Exception($"No found vault at ID location: {id}");
     if(vault.CreatorId != userId && vault.IsPrivate == true) throw new Exception("Private vault, just stop please");
     return vault;
    }

    internal Vault UpdateVault(Vault updateData)
    {
      Vault originvault = this.GetVaultById(updateData.Id, updateData.CreatorId);
      originvault.Name = updateData.Name != null ? updateData.Name : originvault.Name;
      originvault.Description = updateData.Description != null ? updateData.Description : originvault.Description;
      _repo.UpdateVault(originvault);
      return originvault;
    }
  }
}