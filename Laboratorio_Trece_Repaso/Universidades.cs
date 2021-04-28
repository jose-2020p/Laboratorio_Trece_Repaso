using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_Trece_Repaso
{
    public class Universidades_Clase
    {
        string nombre_universidad;
        List<Alumnos_Clase> vector_alumnos = new List<Alumnos_Clase>();
        List<Profesores_Clase> vector_profesor = new List<Profesores_Clase>();
        public string Nombre_universidad { get => nombre_universidad; set => nombre_universidad = value; }
        public List<Alumnos_Clase> Vector_alumnos { get => vector_alumnos; set => vector_alumnos = value; }
        public List<Profesores_Clase> Vector_profesor { get => vector_profesor; set => vector_profesor = value; }

        public Universidades_Clase()
        {
            vector_alumnos = new List<Alumnos_Clase>();
            vector_profesor= new List<Profesores_Clase>();
        }
    }
    public class Alumnos_Clase:Datos_Generales_Clase
    {
        string carnet_alumno;

        public string Carnet_alumno { get => carnet_alumno; set => carnet_alumno = value; }
    }
    public class Profesores_Clase : Datos_Generales_Clase 
    {
        string id_profesor;
        string titulo_universitario_profesor;

        public string Id_profesor { get => id_profesor; set => id_profesor = value; }
        public string Titulo_universitario_profesor { get => titulo_universitario_profesor; set => titulo_universitario_profesor = value; }
    }
    public class Personal_Admin_Clase : Datos_Generales_Clase 
    {
        string numero_IGGS_Admin;
        string profesion_Admin;
        DateTime fecha_inicio_admin;
        DateTime fecha_fin_admin;

        public string Numero_IGGS_Admin { get => numero_IGGS_Admin; set => numero_IGGS_Admin = value; }
        public string Profesion_Admin { get => profesion_Admin; set => profesion_Admin = value; }
        public DateTime Fecha_inicio_admin { get => fecha_inicio_admin; set => fecha_inicio_admin = value; }
        public DateTime Fecha_fin_admin { get => fecha_fin_admin; set => fecha_fin_admin = value; }
    }
    public class Datos_Generales_Clase
    {
        string nombre_general;
        string apellido_general;
        string direccion_general;
        DateTime fecha_nacimiento_general;
        int edad;
    
        public string Nombre_general { get => nombre_general; set => nombre_general = value; }
        public string Apellido_general { get => apellido_general; set => apellido_general = value; }
        public string Direccion_general { get => direccion_general; set => direccion_general = value; }
        public DateTime Fecha_nacimiento_general { get => fecha_nacimiento_general; set => fecha_nacimiento_general = value; }
        public int Edad { get { return edad_calcular(); } }

        public int edad_calcular()
        {
            DateTime fechaActual = DateTime.Now;

            int edad = fechaActual.Year - Fecha_nacimiento_general.Year;

            if (Fecha_nacimiento_general.Month < fechaActual.Month)

            {
                edad--;
            }

            return edad;
        }
    }
}