using System;
using System.Collections.Generic;


namespace MvcPicashWeb.Models
{
    public class Curso:ObjetoEscuelaBase
    {
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