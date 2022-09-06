using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjControlVentaBoletos
{
    public partial class frmCine : Form
    {
        double precio = 0;
        string categoria = "";

        public frmCine()
        {
            InitializeComponent();
        }

        private void cboEdad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Capturando la edad seleccionada
            int edad = cboEdad.SelectedIndex;

            // Asignando el precio y categoría según la edad seleccionada
            switch(edad)
            {
                case 0: precio = 10; categoria = "Niño"; break; 
                case 1: precio = 25; categoria = "Adulto"; break; 
                case 2: precio = 15; categoria = "Tercera Edad"; break; 
            }

            // Mostrando el precio y la categoría
            lblPrecio.Text = precio.ToString("C");
            lblCategoria.Text = categoria;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Capturando los datos necesarios
            categoria = lblCategoria.Text;
            int cantidad = int.Parse(txtCantidad.Text);

            // Realizando los cálculos
            double subtotal = precio * cantidad;
            double descuento = 0;

            switch(categoria)
            {
                case "Niño": descuento = 20.0 / 100 * subtotal; break;
                case "Adulto": descuento = 5.0 / 100 * subtotal; break;
                case "Tercera Edad": descuento = 10.0 / 100 * subtotal; break;
            }
            double importe = subtotal - descuento;

            // Imprimir en la lista
            ListViewItem fila = new ListViewItem(categoria);
            fila.SubItems.Add(precio.ToString("0.00"));
            fila.SubItems.Add(cantidad.ToString());
            fila.SubItems.Add(subtotal.ToString("0.00"));
            fila.SubItems.Add(descuento.ToString("0.00"));
            fila.SubItems.Add(importe.ToString("0.00"));
            lvRegistro.Items.Add(fila);

            lvEstadisticas.Items.Clear();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            lvEstadisticas.Items.Clear();

            // Hallar el monto total sin descuento
            double tSubtotal = 0;
            int i;
            for(i = 0; i < lvRegistro.Items.Count; i++)
            {
                tSubtotal += double.Parse(lvRegistro.Items[i].SubItems[3].Text);
            }

            // Hallar el monto total que la empresa no percibe
            // los decuentos realizados
            double tDescuento = 0;
            i = 0;
            while(i < lvRegistro.Items.Count)
            {
                tDescuento += double.Parse(lvRegistro.Items[i].SubItems[4].Text);
                i++;
            }

            // Hallar el monto total acumulado por categoría
            double aNiño = 0, aAdulto = 0, aTerceraEdad = 0;
            i = 0;
            do
            {
                string categoria = lvRegistro.Items[i].SubItems[0].Text;
                switch (categoria)
                {
                    case "Niño":
                        aNiño += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                    case "Adulto":
                        aAdulto += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                    case "Tercera Edad":
                        aTerceraEdad += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                }
                i++;
            } while(i < lvRegistro.Items.Count);

            // Imprimiendo estadísticas
            lvEstadisticas.Items.Clear();
            string[] elementosFila = new string[2];
            ListViewItem row;

            elementosFila[0] = "Monto total sin descuento";
            elementosFila[1] = tSubtotal.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto total que la empresa no percibe";
            elementosFila[1] = tDescuento.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto acumulado por categoría Niño";
            elementosFila[1] = aNiño.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto acumulado por categoría Adulto";
            elementosFila[1] = aAdulto.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto acumulado por categoría Tercera Edad";
            elementosFila[1] = aTerceraEdad.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Está seguro de salir?",
                                             "Cine",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
            if(r == DialogResult.Yes)
                this.Close();
        }
    }
}
