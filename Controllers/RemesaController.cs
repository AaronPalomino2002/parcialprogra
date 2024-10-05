using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransaccionesApp.Data;
using TransaccionesApp.Models;
using TransaccionesApp.Services;
using System.Threading.Tasks;
using System.Linq;

namespace TransaccionesApp.Controllers
{
    [Route("[controller]")]
    public class RemesaController : Controller
    {
        private readonly ILogger<RemesaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly CurrencyConverterService _currencyConverterService;

        public RemesaController(ILogger<RemesaController> logger, ApplicationDbContext context, CurrencyConverterService currencyConverterService)
        {
            _logger = logger;
            _context = context;
            _currencyConverterService = currencyConverterService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Remesa remesa)
        {
            if (ModelState.IsValid)
            {
                // Realiza la conversión de moneda usando el servicio
                decimal convertedAmount;
                try
                {
                    convertedAmount = await _currencyConverterService.ConvertCurrency(remesa.TipoMoneda, "usd", remesa.MontoEnviado);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al convertir la moneda: {ex.Message}");
                    return View("Index", remesa);
                }

                // Asignar el monto final
                remesa.MontoFinal = convertedAmount;

                _context.DataRemesa.Add(remesa);
                await _context.SaveChangesAsync(); // Usa SaveChangesAsync para operaciones asíncronas

                return RedirectToAction("Details", new { id = remesa.Id }); // Redirigir a la vista de detalles
            }

            return View("Index", remesa);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            var remesa = await _context.DataRemesa.FindAsync(id);
            if (remesa == null)
            {
                return NotFound();
            }
            return View(remesa); // Asegúrate de que tienes una vista llamada Details.cshtml
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var remesas = _context.DataRemesa.ToList(); // Obtener todas las remesas
            return View(remesas); // Asegúrate de que tienes una vista llamada List.cshtml
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!"); // Asegúrate de que tienes una vista llamada Error.cshtml
        }
    }
}
