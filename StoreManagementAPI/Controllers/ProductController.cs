using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2023PRN221.Models;

namespace StoreManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PRN221PROJECTContext _context;

        public ProductController()
        {
            _context = new PRN221PROJECTContext(); 
        }

        [HttpGet]
        public IActionResult ListProduct()
        {
            List<TblMatHang> product = _context.TblMatHangs.Where(a => a.Active == true).ToList();
            return Ok(product);
        }
    }
}
