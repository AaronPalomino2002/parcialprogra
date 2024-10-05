using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TransaccionesApp.Services
{
    public class CurrencyConverterService
    {
        private readonly HttpClient _httpClient;

        public CurrencyConverterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para convertir entre dos monedas
        public async Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount)
        {
            // Formar la URL de la API
            var url = $"https://api.coingecko.com/api/v3/simple/price?ids={fromCurrency.ToLower()}&vs_currencies={toCurrency.ToLower()}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var rates = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(json);

                // Verificar que la tasa de cambio existe
                if (rates != null && rates.ContainsKey(fromCurrency.ToLower()) && rates[fromCurrency.ToLower()].ContainsKey(toCurrency.ToLower()))
                {
                    return rates[fromCurrency.ToLower()][toCurrency.ToLower()] * amount;
                }
                else
                {
                    throw new Exception($"No se pudo encontrar la tasa de cambio para {fromCurrency} a {toCurrency}.");
                }
            }

            throw new Exception("Error al obtener la tasa de conversión de moneda.");
        }
    }
}
