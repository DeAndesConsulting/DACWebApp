using DACWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DACWebApp.Controllers
{
	public class HomeController : Controller
	{
		public async Task<ActionResult> Index()
		{
			string urlRequest = "http://dacservicesdiagnosticapp.azurewebsites.net/api/ServiceFormulario";

			try
			{
				HttpClient httpClient = new HttpClient();

				//Seteo time out al httpCient porque itris responde muy lento
				httpClient.Timeout = TimeSpan.FromMinutes(30);

				HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(new Uri(urlRequest));
				string response = await httpResponseMessage.Content.ReadAsStringAsync();

				List<CuestionarioModel> jsonResponse = JsonConvert.DeserializeObject<List<CuestionarioModel>>(response);

				//Filtro los elementos que tienen coordenadas nulas
				List<CuestionarioModel> salida = FiltrarNulos(jsonResponse);

				return View(salida);
			}
			catch (HttpRequestException reqx)
			{
				throw reqx;
			}
		}

		/// <summary>
		/// Filtro las coordenadas nulas y las que no esten dentro de la poryección utilizada
		/// </summary>
		/// <param name="jsonResponse">Json con todos los registros que me devuelve el servicio</param>
		/// <returns>lista de puntos que estan dentro de la proyección utilizada</returns>
		private List<CuestionarioModel> FiltrarNulos(List<CuestionarioModel> jsonResponse)
		{
			Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
			List<CuestionarioModel> salida = new List<CuestionarioModel>();
			foreach (var obj in jsonResponse)
			{
				if(obj.cue_latitud != "0" && obj.cue_longitud != "0")
				{
					//Seteo cultura invariante porque me arroja diferencias en el servidor azure
					string stringLatitud = obj.cue_latitud.ToString(System.Globalization.CultureInfo.InvariantCulture);
					string stringLongitud = obj.cue_longitud.ToString(System.Globalization.CultureInfo.InvariantCulture);

					//Voy a recibir las coordenadas separadas por , para los decimales
					double latitud = Convert.ToDouble(stringLatitud.Replace(',', '.'));
					double longitud = Convert.ToDouble(stringLongitud.Replace(',', '.'));

					//Valido que la latitud y longitud esten dentro de la proyección EPSG:4326
					if (latitud >= -47 && latitud <= 47) 
					{
						if (longitud >= -180 && longitud <= 180)
						{
							salida.Add(obj);
						}
					}
				}
			}
			return salida;
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}