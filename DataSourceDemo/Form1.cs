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
    public partial class Form1 : Form
    {
        public Form1()
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

        // Eventos para guardar datos
        private void customersBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);
        }


        // error y se creo un evento duplicado igual al anterior 
        private void customersBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            // Carga los datos de la tabla 'Customers' en el DataSet 'northwindDataSet'
            // Esta línea de código se ejecuta cuando el formulario se carga
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
        }

        // Evento que se ejecuta cuando se hace clic en una celda del DataGridView
        private void customersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No se ha implementado ninguna funcionalidad en este evento
            // Este evento se puede utilizar para manejar acciones específicas cuando se hace clic en una celda
        }
    }
}
