namespace keeper.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;
  private readonly KeepsRepository _krepo;
  private readonly VaultsRepository _vrepo;

  public AccountService(AccountsRepository repo, KeepsRepository krepo, VaultsRepository vrepo)
  {
    _repo = repo;
    _krepo = krepo;
    _vrepo = vrepo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  
  internal Account GetProfile(string profileId)
  {
    Account profile = _repo.GetById(profileId);
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    return _repo.Edit(original);
  }

  internal List<Keep> GetKeepsByProfileId(string id)
  {
    List<Keep> keeps = _krepo.GetByProfileId(id);
    return keeps;
  }

  internal List<Vault> GetVaultsByProfileId(string id)
  {
    List<Vault> vaults = _vrepo.GetVaultsByProfileId(id);
    return vaults;
  }

  internal List<Vault> GetVaultsByAccountId(string id)
  {
   List<Vault> vaults = _vrepo.GetVaultsByProfileId(id);
    return vaults;
  }
}
