using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace Eje3Agenda.Models
{
    public class Agenda
    {
        public ObservableCollection<Amigo> DatosAmigos { get; set; } = new ObservableCollection<Amigo>();

        MySqlConnection conexion = new MySqlConnection();
        MySqlCommand comandosql;
        MySqlDataReader lector;

        public Agenda()
        {
            conexion.ConnectionString= "server=localhost;user=root;database=agenda;password=adlogcat45";
            Conectar();
            comandosql = new MySqlCommand("select * from amigos order by Nombre", conexion);
            lector = comandosql.ExecuteReader();
            while (lector.Read())
            {
                Amigo a = new Amigo()
                {
                    Id = (int)lector["Id"],
                    Nombre = (string)lector["Nombre"],
                    FechaNacimiento = (DateTime)lector["FechaNacimiento"],
                    Telefono = (string)lector["Telefono"],
                    Correo = Convert.IsDBNull(lector["Correo"]) ? "No Tiene Correo" : (string)lector["Correo"]

                };
                DatosAmigos.Add(a);
            }
            lector.Close();
        }

        public void AgregarAmigos(Amigo a)
        {
            var dateandtime = a.FechaNacimiento;
            string date = dateandtime.ToString("yyyy/MM/dd");

            comandosql = new MySqlCommand($"insert into amigos (Nombre, FechaNacimiento, Telefono, Correo) values ('{a.Nombre}', '{date}', '{a.Telefono}', '{a.Correo}')", conexion);
            comandosql.ExecuteNonQuery();

        }

        public void CargarDatos()
        {
            //conexion.ConnectionString = "server=localhost;user=root;database=agenda;password=adlogcat45";
            Conectar();
            comandosql = new MySqlCommand("select * from amigos order by Nombre", conexion);
            lector = comandosql.ExecuteReader();
            DatosAmigos = new();
            while (lector.Read())
            {
                Amigo a = new Amigo()
                {
                    Id = (int)lector["Id"],
                    Nombre = (string)lector["Nombre"],
                    FechaNacimiento = (DateTime)lector["FechaNacimiento"],
                    Telefono = (string)lector["Telefono"],
                    Correo = Convert.IsDBNull(lector["Correo"]) ? "No Tiene Correo" : (string)lector["Correo"]

                };
                DatosAmigos.Add(a);

            }
            lector.Close();
        }

        public void ActualizarAmigos(Amigo a)
        {
            //update amigos set Nombre = 'Juanito', FechaNacimiento = '2005-09-10', Telefono = '8611124587', Correo = 'juan@gmail.com'  where Id = 6
            var dateandtime = a.FechaNacimiento;
            string date = dateandtime.ToString("yyyy/MM/dd");

            comandosql = new MySqlCommand($"update amigos set Nombre = '{a.Nombre}', FechaNacimiento = '{date}', Telefono = '{a.Telefono}', Correo = '{a.Correo}'  where Id = {a.Id}", conexion);
            comandosql.ExecuteNonQuery();
        }



        public void EliminarAmigos(Amigo a)
        {
            /*
             comandosql = new MySqlCommand($"insert into amigos (Nombre, FechaNacimiento, Telefono, Correo) values ('{a.Nombre}', '{date}', '{a.Telefono}', '{a.Correo}')", conexion);
             comandosql.ExecuteNonQuery();
             */
            comandosql = new MySqlCommand($"delete from amigos where Id = {a.Id}", conexion);
            comandosql.ExecuteNonQuery();
        }

       

        private void Conectar()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }
        
        ~Agenda()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
