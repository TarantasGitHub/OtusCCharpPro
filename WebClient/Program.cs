using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        static HttpClient httpClient = new HttpClient();
        static async Task Main(string[] args)
        {            
            ConsoleKeyInfo action;
            do
            {
                Console.WriteLine("Введите действие:");
                Console.WriteLine("1. Запросить пользователя по Id;");
                Console.WriteLine("2. Добавить пользователя;");
                Console.WriteLine("3. Выйти из приложения;");
                action = Console.ReadKey();
                switch (action.Key.ToString())
                {
                    case "D1":
                        await GetCustomer();
                        break;
                    case "D2":
                        await RandomCustomer();
                        break;
                    default:
                        Console.WriteLine(action.Key.ToString());
                        break;
                }
            } while (action.Key.ToString() != "D3");
        }

        private static async Task RandomCustomer()
        {
            Console.WriteLine("Введите CustomerId для новой записи:");
            var stringId = Console.ReadLine();
            if (Int64.TryParse(stringId, out Int64 id))
            {
                var customerCreateRequest = new CustomerCreateRequest();
                string json = JsonConvert.SerializeObject(
                    new Customer
                    {
                        Id = id,
                        Firstname = customerCreateRequest.Firstname,
                        Lastname = customerCreateRequest.Lastname
                    });

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/customers/", httpContent))
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine(response.ReasonPhrase);
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        Console.WriteLine(response.ReasonPhrase);
                    }
                    else if (response.StatusCode == HttpStatusCode.OK)
                    {
                        GetCustomerById(await response.Content.ReadFromJsonAsync<Int64>());
                    }
                }
            }
        }

        private static async Task GetCustomer()
        {
            Console.WriteLine("Введите CustomerId(q for exit):");
            var stringId = Console.ReadLine();

            if (Int64.TryParse(stringId, out Int64 id))
            {
                await GetCustomerById(id);
            }
            else
            {
                Console.WriteLine("\"{0}\" не является числом.", stringId);
            }
        }

        private static async Task GetCustomerById(long id)
        {
            using (var response = await httpClient.GetAsync($"https://localhost:5001/customers/{id}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    //Error? error = await response.Content.ReadFromJsonAsync<Error>();
                    //Console.WriteLine(error?.Message);
                    Console.WriteLine(response.ReasonPhrase);
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    // считываем ответ
                    Customer? customer = await response.Content.ReadFromJsonAsync<Customer>();
                    Console.WriteLine("Id: {0}, Firstname: {1}, Lastname: {2}\n", customer?.Id, customer?.Firstname, customer?.Lastname);
                }
            }
        }
    }
}