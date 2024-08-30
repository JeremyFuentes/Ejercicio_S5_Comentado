using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSourceDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // Evento que se ejecuta al hacer clic en el botón de guardar en el BindingNavigator
        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            // Valida los datos y guarda los cambios en el DataSet
            this.Validate(); // Valida todos los controles en el formulario
            this.customersBindingSource.EndEdit(); // Finaliza la edición de los datos en el BindingSource
            this.tableAdapterManager.UpdateAll(this.northwindDataSet); // Actualiza todos los cambios en el DataSet a la base de datos
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void Form2_Load(object sender, EventArgs e)
        {
            // Carga los datos de la tabla 'Customers' en el DataSet 'northwindDataSet'
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
        }

        // Evento vacío asociado al clic en la caja de texto (puede no estar en uso)
        private void cajaTextoID_Click(object sender, EventArgs e)
        {
            // No se ha implementado ninguna funcionalidad en este evento
        }

        // Evento que maneja la entrada de texto en la caja de texto para buscar por ID de cliente
        private void CajaTextoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada es "Enter" (código ASCII 13)
            if (e.KeyChar == (char)13)
            {
                // Busca la posición del registro con el CustomerID especificado en la caja de texto
                var index = customersBindingSource.Find("CustomerID", cajaTextoID.Text);

                // Si encuentra un resultado
                if (index > -1)
                {
                    // Establece la posición actual del BindingSource al índice encontrado
                    customersBindingSource.Position = index;
                    return; // Sale del método
                }
                else
                {
                    // Muestra un mensaje si el CustomerID no se encuentra
                    MessageBox.Show("Elemento no encontrado");
                }
            }
        }
    }
}
