using Microsoft.AspNetCore.Mvc;
using ZaliczeniowyProjekt.Models;
using System.Threading.Tasks;

namespace ZaliczeniowyProjekt.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Contact
        public IActionResult Index()
        {
            // Strona z formularzem kontaktowym
            return View();
        }

        // POST: /Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactMessage model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Zapisujemy wiadomość do bazy
            _context.ContactMessages.Add(model);
            await _context.SaveChangesAsync();

            // Przekierowanie na stronę potwierdzenia
            return RedirectToAction(nameof(ThankYou));
        }

        // GET: /Contact/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }

        // (Opcjonalnie) Administrator może przeglądać wszystkie wiadomości, 
        // ale to wymaga roli Admin i atrybutu [Authorize(Roles="Admin")]
        //public async Task<IActionResult> Messages()
        //{
        //    var allMessages = await _context.ContactMessages.ToListAsync();
        //    return View(allMessages);
        //}
    }
}
