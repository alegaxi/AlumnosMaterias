using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumnosMaterias
{
    public class DataList
    {
        public Int32 numControl { get; set; }
        public String nombre { get; set; }
    }

    public class DataAsistencia
    {
        public DateTime fecha { get; set; }
        //public Int32 grupo { get; set; }
        //public String materia { get; set; }
        public String nombreAlumno { get; set; }
        public Int32 numControl { get; set; }
        public Int32 asistencia { get; set; }

    }
}
