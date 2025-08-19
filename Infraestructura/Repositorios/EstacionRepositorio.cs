using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaces;
using Infraestructura.Contexto;
using Infraestructura.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class EstacionRepositorio : IEstacionRepositorio
    {

        private readonly ReservaCitasDbContext context;


        public EstacionRepositorio(ReservaCitasDbContext context) 
        {

            this.context = context;
        
        }


        public string agregarEstacion(Estacione estacion)
        {
           
            context.Estaciones.Add(estacion);   
            context.SaveChanges();

            return "Estacion registrada correctamente";

        }

        public string ModificarEstacion(Estacione estacion)
        {
            
            context.Estaciones.Entry(estacion).State = EntityState.Modified;
            context.SaveChanges();

            return "Estacion modificada correctamente";

        }

        public List<Estacione> ObtenerEstaciones()
        {
            return context.Estaciones.ToList();
        }
    }
}
