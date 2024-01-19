using DogGo.Models;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {

        List<Walk> GetAllWalks();
        Walk GetWalkById(int id);
        List<Walk> GetWalksByWalkerId(int walkerId);
        public void AddWalk(Walk walk);
        public void UpdateWalk(Walk walk);
        public void DeleteWalk(int id);


    }
}
