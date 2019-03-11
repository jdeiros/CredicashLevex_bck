using System;
using System.Collections.Generic;

namespace MvcPicashWeb.Models
{
    public class Alumno : ObjetoEscuelaBase
    {
        public List<Evaluación> Evaluaciones { get; set; } = new List<Evaluación>();
    }
}