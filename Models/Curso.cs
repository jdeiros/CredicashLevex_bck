using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcPicashWeb.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required]
        public override string Nombre { get => base.Nombre; set => base.Nombre = value; }

        public TiposJornada Jornada { get; set; }
        //navegacion hacia sus hijos
        //podemos hacer referencia a los hijos con listas
        public List<Asignatura> Asignaturas { get; set; }
        public List<Alumno> Alumnos { get; set; }

        public string Dirección { get; set; }

        //Navegacion hacia su padre
        //por convencion ya sabe que EscuelaId es el id del padre de curso (escuela)
        public string EscuelaId { get; set; }
        //tambien puedo hacer que me entregue el objeto escuela completo
        public Escuela Escuela { get; set; }
    }
}