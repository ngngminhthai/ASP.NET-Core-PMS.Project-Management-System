using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PMS.Application.CQRS.Products;
using PMS.Application.Products;
using PMS.Controllers;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Hubs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ManageAppDbContext _context;
        private readonly IHubContext<SignalSever> _signalrHub;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMemoryCache cache;

        public ProductsController(ManageAppDbContext context, IHubContext<SignalSever> signalrHub,
            IFileUploadService fileUploadService, IMemoryCache cache)
        {
            _context = context;
            _signalrHub = signalrHub;
            this._fileUploadService = fileUploadService;
            this.cache = cache;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method used to test result with CRQR Mediator
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> GetProducts2()
        {
            return await Mediator.Send(new ListProduct.Query());
        }

        [HttpGet]
        public IActionResult GetProducts(string searchTerm, int page, int pageSize)
        {
            var cacheKey = $"productsList_{searchTerm}_{page}_{pageSize}";
            var products = cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return Mediator.Send(new ListProduct.Query()
                {
                    SearchTerm = searchTerm,
                    PageIndex = page,
                    PageSize = pageSize
                });
            });
            Response.AddPaginationHeader(products.Result.MetaData);
            return Ok(products.Result);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await Mediator.Send(new GetProductDetail.Query { ProductId = id });
            if (product == null)
            {
                return NotFound();
            }
            var passedProduct = Mapper.Map<Product>(product);
            return View(passedProduct);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description")] Product product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var filePath = await _fileUploadService.UploadFile(file);
                product.Image = filePath;
                //await Mediator.Send(new CreateProduct.Command { Product = product });
                await _signalrHub.Clients.All.SendAsync("LoadProducts");
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await Mediator.Send(new GetProductDetail.Query { ProductId = id });
            await _signalrHub.Clients.All.SendAsync("LoadProducts");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(product);
                    //await Mediator.Send(new UpdateProduct.Command { Product = product });
                    await _signalrHub.Clients.All.SendAsync("LoadProducts");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await Mediator.Send(new GetProductDetail.Query { ProductId = id });
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new DeleteProduct.Command { ProductId = id });
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
