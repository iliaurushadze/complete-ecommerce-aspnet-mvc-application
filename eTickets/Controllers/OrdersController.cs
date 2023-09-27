using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await _ordersService.GetOrdersByUderIdAsync(userId);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCardTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie != null)
            {
                _shoppingCart.AddItemToCart(movie);
                
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveFromShoppingCart(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie != null)
                _shoppingCart.RemoveItemFromCart(movie);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<ActionResult> CompleteOrder() 
        { 
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";
            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
