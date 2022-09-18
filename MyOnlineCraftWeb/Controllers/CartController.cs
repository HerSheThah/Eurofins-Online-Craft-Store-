using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOnlineCraftWeb;
using MyOnlineCraftWeb.Models;
using MyOnlineCraftWeb.Models.ViewModel;

namespace MyOnlineCraftWeb.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly OnlineCraftStoreDbContext _context;
        public ShoppingCartVM shoppingcartVM { get; set; }

        public CartController(OnlineCraftStoreDbContext context)
        {
            _context = context;
        }

        // GET: Shoppingcarts
        public async Task<IActionResult> Index()
        {

            var claimIndentity = (ClaimsIdentity)User.Identity;
            var claims = claimIndentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingcartVM = new ShoppingCartVM
            {
                shoppingcartList=_context.Shoppingcarts.Include(x=>x.Product).Where(x=>x.AppUserId==claims.Value).ToList(),
            };
            foreach(var item in shoppingcartVM.shoppingcartList)
            {
                shoppingcartVM.cartTotal += (item.Product.DiscountPrice * item.count);
            }
        
            return View(shoppingcartVM);
        }

        // GET: Shoppingcarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shoppingcarts == null)
            {
                return NotFound();
            }

            var shoppingcart = await _context.Shoppingcarts
                .Include(s => s.AppUser)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingcart == null)
            {
                return NotFound();
            }

            return View(shoppingcart);
        }

        public IActionResult Plus(int cartId)
        {
            var cartItem = _context.Shoppingcarts.FirstOrDefault(u => u.Id == cartId);
            if(cartItem.count <= 999)
            {
                cartItem.count += 1;
                _context.Update(cartItem);
                _context.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cartItem = _context.Shoppingcarts.FirstOrDefault(u => u.Id == cartId);
            
            cartItem.count -= 1;
            if(cartItem.count == 0)
            {
                
                return RedirectToAction("Delete", new { id = cartId });
            }
            else 
            {
                _context.Update(cartItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            

        }



        // GET: Shoppingcarts/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["ProductID"] = new SelectList(_context.Products, "productId", "productName");
            return View();
        }

        // POST: Shoppingcarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductID,count,AppUserId")] Shoppingcart shoppingcart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingcart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", shoppingcart.AppUserId);
            ViewData["ProductID"] = new SelectList(_context.Products, "productId", "productName", shoppingcart.ProductID);
            return View(shoppingcart);
        }

        // GET: Shoppingcarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shoppingcarts == null)
            {
                return NotFound();
            }

            var shoppingcart = await _context.Shoppingcarts.FindAsync(id);
            if (shoppingcart == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", shoppingcart.AppUserId);
            ViewData["ProductID"] = new SelectList(_context.Products, "productId", "productName", shoppingcart.ProductID);
            return View(shoppingcart);
        }

        // POST: Shoppingcarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductID,count,AppUserId")] Shoppingcart shoppingcart)
        {
            if (id != shoppingcart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingcart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingcartExists(shoppingcart.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", shoppingcart.AppUserId);
            ViewData["ProductID"] = new SelectList(_context.Products, "productId", "productName", shoppingcart.ProductID);
            return View(shoppingcart);
        }

        // GET: Shoppingcarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Shoppingcarts == null)
            {
                return Problem("Entity set 'OnlineCraftStoreDbContext.Shoppingcarts'  is null.");
            }
            var shoppingcart = await _context.Shoppingcarts.FindAsync(id);
            if (shoppingcart != null)
            {
                _context.Shoppingcarts.Remove(shoppingcart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ShoppingcartExists(int id)
        {
          return _context.Shoppingcarts.Any(e => e.Id == id);
        }
    }
}
