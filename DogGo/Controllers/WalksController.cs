using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepo;


        // ASP.NET will give us an instance of our Walk Repository. This is called "Dependency Injection"
        public WalksController(IWalkRepository walkRepository)
        {
            _walkRepo = walkRepository;
        }


        // GET: WalksController
        public ActionResult Index()
        {

            List<Walk> walks = _walkRepo.GetAllWalks();

            return View(walks);


        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            Walk walk = _walkRepo.GetWalkById(id);

            if (walk == null)
            {
                return NotFound();
            }

            return View(walk);
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walk walk)
        {
            try
            {
                _walkRepo.AddWalk(walk);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            Walk walk = _walkRepo.GetWalkById(id);

            if (walk == null)
            {
                return NotFound();
            }

            return View(walk);
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Walk walk)
        {
            try
            {
                _walkRepo.UpdateWalk(walk);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walk);
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {

            Walk walk = _walkRepo.GetWalkById(id);

            return View(walk);
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walk walk)
        {
            try
            {
                _walkRepo.DeleteWalk(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
