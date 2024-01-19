using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {

        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        private readonly IWalkRepository _walkRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IOwnerRepository ownerRepository, IDogRepository dogRepository, IWalkerRepository walkerRepository, INeighborhoodRepository neighborhoodRepository, IWalkRepository walkRepository)
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
            _walkRepo = walkRepository;
        }

        // GET: WalkersController
       
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                int ownerId = GetCurrentUserId();

                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(ownerId);

                return View(walkers);
            }
            else
            {
                List<Walker> walkers = _walkerRepo.GetAllWalkers();

                return View(walkers);
            }
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {

            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);

            ProfileViewModel vm = new ProfileViewModel()
            {
                Walks = walks,
                Walker = walker,
            };

            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
