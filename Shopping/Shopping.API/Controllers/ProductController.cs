using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Shopping.API.Data;
using Shopping.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductContext context, ILogger<ProductController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override bool Equals(object? obj)
        {
            return obj is ProductController controller &&
                   EqualityComparer<ProductContext>.Default.Equals(_context, controller._context) &&
                   EqualityComparer<ILogger<ProductController>>.Default.Equals(_logger, controller._logger);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_context, _logger);
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }
    }
}
