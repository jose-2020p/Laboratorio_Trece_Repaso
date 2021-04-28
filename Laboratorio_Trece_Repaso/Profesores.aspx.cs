using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorio_Trece_Repaso
{
    public partial class Profesores : System.Web.UI.Page
    {
        static List<Universidades_Clase> array_universidades = new List<Universidades_Clase>();
        static List<Profesores_Clase> array_profesor = new List<Profesores_Clase>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string archivo = Server.MapPath("Datos_Universidad.json");
                StreamReader jsonStream = File.OpenText(archivo);
                string json = jsonStream.ReadToEnd();
                jsonStream.Close();

                if (json.Length > 0)
                {
                    array_universidades = JsonConvert.DeserializeObject<List<Universidades_Clase>>(json);
                    GridView1.DataSource = array_universidades;
                    GridView1.DataBind();
                }
            }
        }
        public void agregar_profesor()
        {
            for (int i = 0; i < array_universidades.Count(); i++)
            {
                if (array_universidades[i].Nombre_universidad == TextBoxUniversidad_P.Text)
                {
                    Universidades_Clase busqueda_estudiante = array_universidades.Find(x => x.Nombre_universidad == TextBoxUniversidad_P.Text);
                    array_profesor = busqueda_estudiante.Vector_profesor;
                }
            }

            //agregar alumno
            Profesores_Clase nuevo_curso = new Profesores_Clase();
            nuevo_curso.Id_profesor = TextBoxIDprof.Text;
            nuevo_curso.Titulo_universitario_profesor = TextBoxTituloUni.Text;
            nuevo_curso.Nombre_general = TextBoxNombre_profe.Text;
            nuevo_curso.Apellido_general = TextBoxApellido_profe.Text;
            nuevo_curso.Direccion_general = TextBoxDireccion_profe.Text;
            nuevo_curso.Fecha_nacimiento_general = CalendarProf.SelectedDate;
            array_profesor.Add(nuevo_curso);
        }
        public void guardar_profesor()
        {
            if (array_universidades.Count == 0)
            {
                Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                nuevo_estudiante.Nombre_universidad = TextBoxUniversidad_P.Text;
                nuevo_estudiante.Vector_profesor = array_profesor.ToList();

                array_universidades.Add(nuevo_estudiante);
            }

            for (int i = 0; i < array_universidades.Count(); i++)
            {
                if (array_universidades[i].Nombre_universidad == TextBoxUniversidad_P.Text)
                {
                    Universidades_Clase busqueda_estudiante = array_universidades.Find(x => x.Nombre_universidad == TextBoxUniversidad_P.Text);
                    busqueda_estudiante.Vector_profesor = array_profesor.ToList();

                    break;
                }
                else
                {
                    Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                    nuevo_estudiante.Nombre_universidad = TextBoxUniversidad_P.Text;
                    nuevo_estudiante.Vector_profesor= array_profesor.ToList();

                    array_universidades.Add(nuevo_estudiante);
                }
            }


            Json();
            array_profesor.Clear();
        }
        public void Json()
        {
            string json = JsonConvert.SerializeObject(array_universidades);
            string archivo = Server.MapPath("Datos_Universidad.json");
            System.IO.File.WriteAllText(archivo, json);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            agregar_profesor();
            guardar_profesor();
            TextBoxUniversidad_P.Text = "";
            TextBoxIDprof.Text = "";
            TextBoxTituloUni.Text = "";
            TextBoxNombre_profe.Text = "";
            TextBoxApellido_profe.Text = "";
            TextBoxDireccion_profe.Text = "";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seleccionada = GridView1.SelectedIndex;
            Label1.Text = "Datos de Univeresidad: \r\n" + array_universidades[seleccionada].Nombre_universidad;
            TextBoxUniversidad_P.Text = "" + array_universidades[seleccionada].Nombre_universidad;
            GridView2.DataSource = array_universidades[seleccionada].Vector_profesor;
            GridView2.DataBind();
        }
    }
}