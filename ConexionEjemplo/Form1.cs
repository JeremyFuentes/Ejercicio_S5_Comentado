using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatosLayer;
using System.Net;
using System.Reflection;


namespace ConexionEjemplo
{
    public partial class Form1 : Form
    {
        // Instancia del repositorio de clientes para acceder a los datos
        CustomerRepository customerRepository = new CustomerRepository();

        public Form1()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta al hacer clic en el botón para cargar todos los clientes en el DataGridView
        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Obtiene todos los clientes y los asigna como fuente de datos del DataGridView
            var Customers = customerRepository.ObtenerTodos();
            dataGrid.DataSource = Customers;
        }

        // Evento que se ejecuta cuando se cambia el texto en el TextBox de filtro (en el futuro puede usarse para filtrar clientes)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Aquí puedes implementar un filtro basado en el texto del TextBox, si es necesario
            // var filtro = Customers.FindAll(X => X.CompanyName.StartsWith(tbFiltro.Text));
            // dataGrid.DataSource = filtro;
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            /* Configuración de la base de datos (actualmente comentado)
            DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            DatosLayer.DataBase.ConnectionTimeout = 30;
            string cadenaConexion = DatosLayer.DataBase.ConnectionString;
            var conxion = DatosLayer.DataBase.GetSqlConnection();
            */
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtiene el cliente por ID y llena los campos de texto con la información del cliente encontrado
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text);
            tboxCustomerID.Text = cliente.CustomerID;
            tboxCompanyName.Text = cliente.CompanyName;
            tboxContacName.Text = cliente.ContactName;
            tboxContactTitle.Text = cliente.ContactTitle;
            tboxAddress.Text = cliente.Address;
            tboxCity.Text = cliente.City;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var resultado = 0;
      ;

            var nuevoCliente = ObtenerNuevoCliente();


            // hayNull= validarCampoNull(nuevoCliente) ? true : false ;

            /*  if (tboxCustomerID.Text != "" || 
                  tboxCompanyName.Text !="" ||
                  tboxContacName.Text != "" ||
                  tboxContacName.Text != "" ||
                  tboxAddress.Text != ""    ||
                  tboxCity.Text != "")
              {
                  resultado = customerRepository.InsertarCliente(nuevoCliente);
                  MessageBox.Show("Guardado" + "Filas modificadas = " + resultado);
              }
              else {
                  MessageBox.Show("Debe completar los campos por favor");
              }

              */

            /*
            if (nuevoCliente.CustomerID == "") {
                MessageBox.Show("El Id en el usuario debe de completarse");
               return;    
            }

            if (nuevoCliente.ContactName == "")
            {
                MessageBox.Show("El nombre de usuario debe de completarse");
                return;
            }
            
            if (nuevoCliente.ContactTitle == "")
            {
                MessageBox.Show("El contacto de usuario debe de completarse");
                return;
            }
            if (nuevoCliente.Address == "")
            {
                MessageBox.Show("la direccion de usuario debe de completarse");
                return;
            }
            if (nuevoCliente.City == "")
            {
                MessageBox.Show("La ciudad de usuario debe de completarse");
                return;
            }

            */

            // Valida que ningún campo sea nulo o vacío antes de insertar
            if (validarCampoNull(nuevoCliente) == false)
            {
                // Inserta el nuevo cliente en la base de datos y muestra el resultado
                resultado = customerRepository.InsertarCliente(nuevoCliente);
                MessageBox.Show("Guardado" + " Filas modificadas = " + resultado);
            }
            else
            {
                // Muestra un mensaje de error si algún campo está vacío
                MessageBox.Show("Debe completar los campos por favor");
            }
        }
        // si encautnra un null enviara true de lo caontrario false
        public Boolean validarCampoNull(Object objeto) {

            // Recorre todas las propiedades del objeto
            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);
                if ((string)value == "")
                {
                    return true; // Retorna true si encuentra algún campo vacío
                }
            }
            return false; // Retorna false si todos los campos están completos
        }
      
        private void label5_Click(object sender, EventArgs e)
        {

        }
        // Evento que se ejecuta al hacer clic en el botón de modificar, actualiza la información del cliente en la base de datos
        private void btModificar_Click(object sender, EventArgs e)
        {
            // Obtiene un cliente actualizado a partir de los campos de texto
            var actualizarCliente = ObtenerNuevoCliente();
            // Actualiza el cliente en la base de datos y muestra el resultado
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente);
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
        }

        // Método que obtiene un nuevo cliente a partir de los valores en los campos de texto
        private Customers ObtenerNuevoCliente() {

            var nuevoCliente = new Customers
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContacName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };

            return nuevoCliente;
        }

        // Evento que se ejecuta al hacer clic en el botón de eliminar, elimina un cliente de la base de datos
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Elimina el cliente usando el ID y muestra el resultado
            int elimindas = customerRepository.EliminarCliente(tboxCustomerID.Text);
            MessageBox.Show("Filas eliminadas = " + elimindas);
        }
    }
}
