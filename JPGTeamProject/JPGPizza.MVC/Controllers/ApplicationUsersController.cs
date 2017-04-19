namespace JPGPizza.MVC.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using JPGPizza.Data;
    using JPGPizza.Models;
    using JPGPizza.MVC.ViewModels.ApplicationUser;
    using JPGPizza.MVC.Services;
    using JPGPizza.MVC.Utility;
    using System.Linq;

    public class ApplicationUsersController : Controller
    {
        private readonly JPGPizzaDbContext _context;
        private readonly ApplicationUsersRepository _applicationUserRepository;

        public ApplicationUsersController()
        {
            _context = new JPGPizzaDbContext();
            _applicationUserRepository = new ApplicationUsersRepository(_context);
        }

        // GET: ApplicationUsers
        public async Task<ActionResult> Index(string searchText, int? page)
        {
            var totalUsers = await _applicationUserRepository.GetTotalCountAsync();

            Pager pager = new Pager(totalUsers, page);

            var users = _applicationUserRepository.GetAll(searchText)
                .Skip((pager.CurrentPage - 1) * pager.PageSize)
                .Take(pager.PageSize);

            var viewModel = new ApplicationUserIndexViewModel()
            {
                Users = users,
                Pager = pager,
                SearchText = searchText
            };

            return View(viewModel);
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = _applicationUserRepository.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = _applicationUserRepository.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Age,Gender,Email,PhoneNumber,UserName")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                _applicationUserRepository.Update(user);

                if (!_applicationUserRepository.Save())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = _applicationUserRepository.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = _applicationUserRepository.GetById(id);
            _applicationUserRepository.Remove(user);

            if (!_applicationUserRepository.Save())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
