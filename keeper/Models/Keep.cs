namespace keeper.Models
{
  public class Keep
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int? Views { get; set; }
    public string CreatorId { get; set; }

    public int Kept { get; set; }
    // NOTE This may give me trouble later, check back when possible ^^^ 
    public Profile Creator { get; set; }
    }

public class VaultKeepViewModel : Keep
{
  public int vaultKeepId { get; set; }
}

}