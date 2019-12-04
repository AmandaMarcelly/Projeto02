using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ResultadoJson
    {
        public string url { get; set; }
        public string total { get; set; }
        public string status { get; set; }
        public string mensagem { get; set; }
        public string api_limite { get; set; }
        public string api_consultas { get; set; }
        public List<Item> item { get; set; }
    }
}
