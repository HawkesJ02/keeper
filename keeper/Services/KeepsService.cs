namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo){
        _repo = repo;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      Keep keep = _repo.CreateKeep(keepData);
      return keep;
    }

    internal Keep GetKeepById(int id, string userId)
    {
      Keep keep = _repo.GetKeepById(id);
      if (keep == null) throw new Exception($"No found Keep at ID location {id}");
      if (keep.CreatorId != userId){
            keep.Views++;
            _repo.UpdateKeep(keep);
      } 
      return keep;
    }

    internal List<Keep> GetKeeps()
    {
      List<Keep> keeps = _repo.GetKeeps();
      return keeps;
    }

    internal Keep UpdateKeep(Keep updateData)
    {
      Keep originkeep = this.GetKeepById(updateData.Id, updateData.CreatorId);
      originkeep.Name = updateData.Name != null ? updateData.Name : originkeep.Name;
      originkeep.Description = updateData.Description != null ? updateData.Description : originkeep.Description;
      originkeep.Img = updateData.Img != null ? updateData.Img: originkeep.Img;
      _repo.UpdateKeep(originkeep);
      return originkeep;
    }
  }
}