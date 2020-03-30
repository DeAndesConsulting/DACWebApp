using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACWebApp.Models
{
	public class CuestionarioModel
	{
		public int cue_id { get; set; }
		public int pac_id { get; set; }
		public DateTime cue_fecha { get; set; }
		public bool cue_diagnostico { get; set; }
		public string cue_latitud { get; set; }
		public string cue_longitud { get; set; }
	}
}