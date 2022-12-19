using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FolkaShop.WebApi.Data.Repository
{
    public class BaseHttpRepository<TEntity>
    {
        public async Task<List<TEntity>> GetHttpRequest(string module, bool isId, int id)
        {
            var result = new List<TEntity>();
            var urlGetData = string.Concat("api/", module) + (isId == true ? "/" + id : "");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));

                HttpResponseMessage response = await client.GetAsync(urlGetData);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<List<TEntity>>();
                }
            }

            return result;
        }
    }
}
