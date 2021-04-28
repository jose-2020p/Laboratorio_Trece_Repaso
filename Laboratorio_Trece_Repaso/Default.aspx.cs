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
    public partial class _Default : Page
    {
        static List<Universidades_Clase> array_universidad = new List<Universidades_Clase>();
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
                    array_universidad = JsonConvert.DeserializeObject<List<Universidades_Clase>>(json);
                    GridView1.DataSource = array_universidad;
                    GridView1.DataBind();
                }
            }
        }
        public void Guardar_Universidad() 
        {
            if (array_universidad.Count >= 1)
            {
                bool existe = true;

                for (int i = 0; i < array_universidad.Count(); i++)
                {
                    if (array_universidad[i].Nombre_universidad == TextBox1.Text)
                    {
                        existe = true;
                        break;
                    }
                    else
                    {
                        existe = false;
                    }

                }
                if (existe == true)
                {
                    Response.Write("<script>alert('La Universidad ya existe en el sistema')</script>");

                }
                if (existe == false)
                {
                    Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                    nuevo_estudiante.Nombre_universidad = TextBox1.Text;
                    array_universidad.Add(nuevo_estudiante);
                }
            }


            if (array_universidad.Count == 0)
            {
                Universidades_Clase nuevo_estudiante = new Universidades_Clase();
                nuevo_estudiante.Nombre_universidad = TextBox1.Text;

                array_universidad.Add(nuevo_estudiante);
            }
        }
        public void Mostrar_Datos() 
        {
            GridView1.DataSource = array_universidad;
            GridView1.DataBind();
        }

        public void Json()
        {
            string json = JsonConvert.SerializeObject(array_universidad);
            string archivo = Server.MapPath("Datos_Universidad.json");
            System.IO.File.WriteAllText(archivo, json);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Guardar_Universidad();
            Json();
            Mostrar_Datos();
            TextBox1.Text = "";
        }
    }
}