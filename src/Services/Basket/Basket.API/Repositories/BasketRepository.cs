using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDistributedCache _redisCash;

        public BasketRepository(IDistributedCache redisCash)
        {
            _redisCash = redisCash;
        }

        public async Task<ShoppingCart> GetBasket(string UserName)
        {
            var basket = await _redisCash.GetStringAsync(UserName);

            if(String.IsNullOrEmpty(basket)) return null;

            var result = JsonConvert.DeserializeObject<ShoppingCart>(basket);

            return result;
        }

        
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {

            await _redisCash.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);

        }

        
        public async Task DeleteBasket(string UserName)
        {
            await _redisCash.RemoveAsync(UserName);
        }
    }
}