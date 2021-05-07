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
    public partial class Admin : System.Web.UI.Page
    {
        static List<Universidades_Clase> array_universidades = new List<Universidades_Clase>();
        static List<Personal_Admin_Clase> array_admin = new List<Personal_Admin_Clase>();
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
        public void agregar_admin()
        {
            for (int i = 0; i < array_universidades.Count(); i++)
            {
                if (array_universidades[i].Nombre_universidad == TextBoxUniversidad.Text)
                {
                    Universidades_Clase busqueda_estudiante = array_universidades.Find(x => x.Nombre_universidad == TextBoxUniversidad.Text);
                    array_admin = busqueda_estudiante.Vector_admin;
                }
            }

            //agregar admin
            Personal_Admin_Clase nuevo_curso = new Personal_Admin_Clase();
            nuevo_curso.Numero_IGGS_Admin = TextBoxIGGS.Text;
            nuevo_curso.Profesion_Admin = TextBoxProfesion.Text;
            nuevo_curso.Nombre_general = TextBoxNombre.Text;
            nuevo_curso.Apellido_general = TextBoxApellido.Text;
            nuevo_curso.Direccion_general = TextBoxDireccion.Text;
            nuevo_curso.Fecha_nacimiento_general = CalendarFecha_Nac.SelectedDate;
            nuevo_curso.Fecha_inicio_admin = CalendarInicio.SelectedDate;
            nuevo_curso.Fecha_fin_admin = CalendarFIn.SelectedDate;
            array_admin.Add(nuevo_curso);
        }
        public void guardar_admin()
        {
            if (array_universidades.Count == 0)
            {
                Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                nuevo_estudiante.Nombre_universidad = TextBoxUniversidad.Text;
                nuevo_estudiante.Vector_admin = array_admin.ToList();

                array_universidades.Add(nuevo_estudiante);
            }

            for (int i = 0; i < array_universidades.Count(); i++)
            {
                if (array_universidades[i].Nombre_universidad == TextBoxUniversidad.Text)
                {
                    Universidades_Clase busqueda_estudiante = array_universidades.Find(x => x.Nombre_universidad == TextBoxUniversidad.Text);
                    busqueda_estudiante.Vector_admin = array_admin.ToList();

                    break;
                }
                else
                {
                    Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                    nuevo_estudiante.Nombre_universidad = TextBoxUniversidad.Text;
                    nuevo_estudiante.Vector_admin = array_admin.ToList();

                    array_universidades.Add(nuevo_estudiante);
                }
            }


            Json();
            array_admin.Clear();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            agregar_admin();
            guardar_admin();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seleccionada = GridView1.SelectedIndex;
            Label1.Text = "Datos de Univeresidad: \r\n" + array_universidades[seleccionada].Nombre_universidad;
            TextBoxUniversidad.Text = "" + array_universidades[seleccionada].Nombre_universidad;
            GridView2.DataSource = array_universidades[seleccionada].Vector_admin;
            GridView2.DataBind();
        }
    }
}