// Controllers/NewsletterController.cs
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class NewsletterController : Controller
{
    private readonly ApplicationDbContext _context;

    public NewsletterController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignUp(NewsletterSignUp model)
    {
        if (ModelState.IsValid)
        {
            // Check if the email is not already signed up
            var existingSignUp = _context.NewsletterSignUps.FirstOrDefault(s => s.Email == model.Email);
            if (existingSignUp == null)
            {
                _context.NewsletterSignUps.Add(model);
                _context.SaveChanges();
                ViewData["Message"] = "You have been signed up to the newsletter!";
            }
            else
            {
                ViewData["ErrorMessage"] = "This email is already signed up.";
            }
        }
        return View("Index", model);
    }
}
