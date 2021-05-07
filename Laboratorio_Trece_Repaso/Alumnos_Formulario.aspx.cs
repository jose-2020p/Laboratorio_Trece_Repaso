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
    public partial class Alumnos_Formulario : System.Web.UI.Page
    {

        static List<Universidades_Clase> array_universidades = new List<Universidades_Clase>();
        static List<Alumnos_Clase> array_alumnos = new List<Alumnos_Clase>();
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
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
        public void Json()
        {
            string json = JsonConvert.SerializeObject(array_universidades);
            string archivo = Server.MapPath("Datos_Universidad.json");
            System.IO.File.WriteAllText(archivo, json);
        }
        public void agregar_estudiante()
        {
            for (int i = 0; i < array_universidades.Count(); i++)
            {
                if (array_universidades[i].Nombre_universidad == TextBoxUniversidad.Text)
                {
                    Universidades_Clase busqueda_estudiante = array_universidades.Find(x => x.Nombre_universidad == TextBoxUniversidad.Text);
                    array_alumnos = busqueda_estudiante.Vector_alumnos;
                }
            }

            //agregar alumno
            Alumnos_Clase nuevo_curso = new Alumnos_Clase();
            nuevo_curso.Carnet_alumno = TextBoxCarnet.Text;
            nuevo_curso.Nombre_general = TextBoxNombre.Text;
            nuevo_curso.Apellido_general = TextBoxApellido.Text;
            nuevo_curso.Direccion_general = TextBoxDireccion.Text;
            nuevo_curso.Fecha_nacimiento_general = Calendar1.SelectedDate;
            array_alumnos.Add(nuevo_curso);
        }
        public void guardar_estudiante()
        {
            if (array_universidades.Count == 0)
            {
                Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                nuevo_estudiante.Nombre_universidad = TextBoxUniversidad.Text;
                nuevo_estudiante.Vector_alumnos = array_alumnos.ToList();

                array_universidades.Add(nuevo_estudiante);
            }

            for (int i = 0; i < array_universidades.Count(); i++)
            {
                if (array_universidades[i].Nombre_universidad == TextBoxUniversidad.Text)
                {
                    Universidades_Clase busqueda_estudiante = array_universidades.Find(x => x.Nombre_universidad == TextBoxUniversidad.Text);
                    busqueda_estudiante.Vector_alumnos = array_alumnos.ToList();

                    break;
                }
                else
                {
                    Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                    nuevo_estudiante.Nombre_universidad = TextBoxUniversidad.Text;
                    nuevo_estudiante.Vector_alumnos = array_alumnos.ToList();

                    array_universidades.Add(nuevo_estudiante);
                }
            }


            Json();
            array_alumnos.Clear();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            agregar_estudiante();
            guardar_estudiante();
            TextBoxUniversidad.Text = "";
            TextBoxCarnet.Text = "";
            TextBoxNombre.Text = "";
            TextBoxApellido.Text = "";
            TextBoxDireccion.Text = "";

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seleccionada = GridView1.SelectedIndex;
            Label1.Text = "Datos de Univeresidad: \r\n" + array_universidades[seleccionada].Nombre_universidad;
            TextBoxUniversidad.Text = "" + array_universidades[seleccionada].Nombre_universidad;
            GridView2.DataSource = array_universidades[seleccionada].Vector_alumnos;
            GridView2.DataBind();
        }

        protected void ButtonModificar_Click(object sender, EventArgs e)
        {

            //editar
            int idUniversidad = GridView1.SelectedIndex;
            int idAlumno = GridView2.SelectedIndex;

            TextBoxCarnet.Text = array_universidades[idUniversidad].Vector_alumnos[idAlumno].Carnet_alumno;
            TextBoxNombre.Text = array_universidades[idUniversidad].Vector_alumnos[idAlumno].Nombre_general;
            TextBoxApellido.Text = array_universidades[idUniversidad].Vector_alumnos[idAlumno].Apellido_general;
            TextBoxDireccion.Text = array_universidades[idUniversidad].Vector_alumnos[idAlumno].Direccion_general;

            Label1.Text = "Realice los cambios y luego oprima el boton GUARDAR CAMBIOS";

        }

        protected void ButtonGuardarCambios_Click(object sender, EventArgs e)
        {
            //guardar cambios
            int idUniversidad = GridView1.SelectedIndex;
            int idAlumno = GridView2.SelectedIndex;

            array_universidades[idUniversidad].Vector_alumnos[idAlumno].Carnet_alumno = TextBoxCarnet.Text;
            array_universidades[idUniversidad].Vector_alumnos[idAlumno].Nombre_general = TextBoxNombre.Text;
            array_universidades[idUniversidad].Vector_alumnos[idAlumno].Apellido_general = TextBoxApellido.Text;
            array_universidades[idUniversidad].Vector_alumnos[idAlumno].Direccion_general = TextBoxDireccion.Text;

            Json();

            GridView2.DataSource = array_universidades[idUniversidad].Vector_alumnos;
            GridView2.DataBind();
            TextBoxCarnet.Text = "";
            TextBoxNombre.Text = "";
            TextBoxApellido.Text = "";
            TextBoxDireccion.Text = "";

        }
    }
}