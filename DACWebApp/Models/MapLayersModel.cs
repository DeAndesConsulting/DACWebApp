using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACWebApp.Models
{
	public class MapLayersModel
	{
		List<LayerModel> ListaLayerModel { get; set; }
	}

	public class LayerModel
	{
		public string NombreLayer { get; set; }
		public string ColorLayer { get; set; }
		public List<CustomPoint> ListaPuntos { get; set; }
	}

	public class CustomPoint
	{
		public string LabelPoint { get; set; }
		public string Long { get; set; }
		public string Lat { get; set; }
	}
}