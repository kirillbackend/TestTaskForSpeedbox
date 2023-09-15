using CdekSdk;
using CdekSdk.DataContracts;
using Microsoft.AspNetCore.Mvc;
using SpeedboxTestApi.Models;

namespace SpeedboxTestApi.Controllers
{
    /// <summary>
    /// Кодтроллер для расчета стоимости доставки
    /// </summary>
    public class SDECController : Controller
    {
        /// <summary>
        /// Получение формы View
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Order());
        }

        /// <summary>
        /// Подсчет и вывод стоимости доставки заказа по полученым данным
        /// </summary>
        /// <param name="order"> данные по заказу</param>
        [HttpPost]
        public IActionResult Index(Order order) 
        {
            var client = new CdekClient();
            var cityNotFound = "City not found";

            var clientDepartureCity = 
                client.GetCities().FirstOrDefault(x => x.FiasGuid == order.FiasDepartureCity);

            if (clientDepartureCity == null)
                return Content(cityNotFound);

            var departureCity = clientDepartureCity?.Code;

            var clientFiasReceivingCity = 
                client.GetCities().FirstOrDefault(x => x.FiasGuid == order.FiasReceivingCity);

            if (clientFiasReceivingCity == null)
                return Content(cityNotFound);

            var fiasReceivingCity = clientFiasReceivingCity?.Code;

            var tariff = client.CalculateTariff(new TariffRequest
            {
                TariffCode = 480,
                DeliveryType = DeliveryType.Delivery,
                FromLocation = new Location { CityCode = departureCity },
                ToLocation = new Location { CityCode = fiasReceivingCity },
                Packages =
                {
                    new PackageSize
                    {
                        Weight = order.Weight,
                        Length = order.Length / 10,
                        Width = order.Width / 10,
                        Height = order.Height / 10
                    }
                }
            });

            ViewData["totalSum"] = tariff;
            return View(new Order());
        }
    }
}
