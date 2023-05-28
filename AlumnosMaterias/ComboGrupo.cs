using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumnosMaterias
{
   
    public class ComboGrupo
    {
        public ObservableCollection<Grupo> DatosGrupo { get; set; }
        public ComboGrupo()
        {
            this.DatosGrupo = new ObservableCollection<Grupo>();
            this.DatosGrupo.Add(new Grupo() { Name = "101", ID = 0 });
            this.DatosGrupo.Add(new Grupo() { Name = "201", ID = 1 });
            this.DatosGrupo.Add(new Grupo() { Name = "301", ID = 2 });
            this.DatosGrupo.Add(new Grupo() { Name = "401", ID = 3 });
            this.DatosGrupo.Add(new Grupo() { Name = "501", ID = 4 });
            this.DatosGrupo.Add(new Grupo() { Name = "601", ID = 5 });
            this.DatosGrupo.Add(new Grupo() { Name = "701", ID = 6 });
            this.DatosGrupo.Add(new Grupo() { Name = "801", ID = 7 });
        }
    }
    public class Grupo
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
