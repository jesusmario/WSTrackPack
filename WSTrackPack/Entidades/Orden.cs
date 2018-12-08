using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSTrackPack.Entidades
{
    public class Orden
    {
        public Destinatario Destinatario { get; set; }
        public String Fecha { get; set; }
        public Paquete Paquete { get; set; }
        public List<Historial> Historiales { get; set; }
        public string NumeroRastreo { get; set; }
    }
}