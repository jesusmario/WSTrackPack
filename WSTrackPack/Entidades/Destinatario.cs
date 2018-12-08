using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSTrackPack.Entidades
{
    public class Destinatario
    {
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Avenida { get; set; }
        public string Colonia { get; set; }
        public string Cp { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }
    }
}