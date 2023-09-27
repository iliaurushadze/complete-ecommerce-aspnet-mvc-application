﻿using System.ComponentModel.DataAnnotations;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && 
                n.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null) 
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id &&
                n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                    shoppingCartItem.Amount--;
                else
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
            _context.SaveChanges();
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public List<ShoppingCartItem> GetShoppingCartItems() 
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Include(n => n.Movie)
                .ToList());
        }

        public double GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amount).Sum();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .ToList();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
