using Eje3Agenda.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eje3Agenda.ViewModels
{
    public class AmigosViewModels: INotifyPropertyChanged
    {
        public Agenda AgendaAmigos { get; set; } = new Agenda();


        private Amigo? amigo;
        public Amigo? Amigo {
            get { return amigo; }
            set
            {
                amigo = value;
                PropertyChange("Amigo");
            }
        }
        public string? error { get; set; }

        
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        //public ICommand RefrescarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public string Vista { get; set; } = "Ver";

        public AmigosViewModels()
        {
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            CancelarCommand = new RelayCommand(Cancelar);
            AgregarCommand = new RelayCommand(Agregar);
            GuardarCommand = new RelayCommand(Guardar);
            EliminarCommand = new RelayCommand<Amigo>(Eliminar);
        }

        private void Guardar()
        {
            //metodo para actualizar via
            if (Amigo != null)
            {
                AgendaAmigos.ActualizarAmigos(Amigo);
                Vista = "Ver";
            }
            PropertyChange();
            Refrescar();
        }

        int posicionAmigoEditar;
        public void CambiarVista(string obj)
        {
            // se pasa el dato del CommandParameter a la Vista
            Vista = obj;

            //Verificamos que sea correcto
            if (Vista == "Agregar")
            {
                Amigo = new Amigo();
            }

            if (Vista == "Editar")
            {
                if (amigo != null)
                {
                    Amigo clon = new Amigo()
                    {
                        Id = amigo.Id,
                        Nombre = amigo.Nombre,
                        FechaNacimiento = amigo.FechaNacimiento,
                        Telefono = amigo.Telefono,
                        Correo = amigo.Correo
                    };
                    posicionAmigoEditar = AgendaAmigos.DatosAmigos.IndexOf(amigo);

                    Amigo = clon;
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vista)));
        }

        public void Cancelar()
        {
            CambiarVista("Ver");
            Amigo = null;
        }

        public void Agregar()
        {
            if (Amigo != null)
            {
                error = "";
                if (string.IsNullOrWhiteSpace(Amigo.Nombre))
                {
                    error = "El Nombre debe de ser insertado";
                    PropertyChange("error");
                }
                if (string.IsNullOrWhiteSpace(Amigo.Telefono))
                {
                    error = "El telefono debe de ser insertado";
                    PropertyChange("error");
                }

                if (error == "")
                {
                    AgendaAmigos.AgregarAmigos(Amigo);
                    Vista = "Ver";
                }
                PropertyChange();
                Refrescar();
            }
        }

        public void Eliminar(Amigo amigo)
        {
            AgendaAmigos.EliminarAmigos(amigo);
            PropertyChange();
            Refrescar();
        }




        private void Refrescar()
        {
            AgendaAmigos.CargarDatos();
            PropertyChange();
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        void PropertyChange(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
