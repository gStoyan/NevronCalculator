namespace Nevron.Calculator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Nevron.Calculator.Common.Extensions;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string SessionKeyNumber = "_Numbers";
        private const string SessionKeySum = "_Sum";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sessionNumbers = HttpContext.Session.Get<List<int>>(SessionKeyNumber);
            var sessionSum = HttpContext.Session.Get<int>(SessionKeySum);

            ViewData["Numbers"] = sessionNumbers;
            ViewData["Count"] = sessionNumbers?.Count;
            ViewData["Sum"] = sessionSum != 0 ? sessionSum : null;
            return View();
        }

        public IActionResult SumNumbers(List<int> numbers)
        {
            var sum = numbers.Sum();
            HttpContext.Session.Set<int>(SessionKeySum, sum);
            return Json(new { sum = sum });
        }

        public IActionResult AddNumber(int randomNumber)
        {
            var sessionNumbers = HttpContext.Session.Get<List<int>>(SessionKeyNumber);
            if (sessionNumbers == null)
            {
                sessionNumbers = new List<int>() { randomNumber };
                HttpContext.Session.Set<List<int>>(SessionKeyNumber, sessionNumbers);
            }
            else
            {
                sessionNumbers.Add(randomNumber);
                HttpContext.Session.Set<List<int>>(SessionKeyNumber, sessionNumbers);
            }

            var count = sessionNumbers.Count;
            return Json(new { count = count });
        }
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();

            return Json(new { count = 0 });
        }
    }
}
