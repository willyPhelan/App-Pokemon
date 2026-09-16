using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominioPokemon ; 
using NegocioPokemon ; 

namespace InterfazPokemon
{
    public partial class frmAltaPokemon : Form
    {
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close() ;
        }

        private void btnAgregar_Click(object sender, EventArgs e){

            Pokemon pokemon1 = new Pokemon() ;

            PokemonNegocio negocio = new PokemonNegocio() ; // instancio obj de conexion

            try { 
            
            // capturo los datos y los transforomo en objeto de tipo Pokemon

            pokemon1.Numero = int.Parse(textNumero.Text) ;

            pokemon1.Nombre = textNombre.Text ;

            pokemon1.Descripcion = textDescripcion.Text ;

            pokemon1.Tipo = (Elemento)comboBoxTipo.SelectedItem ; 

            pokemon1.Debilidad = (Elemento)comboBoxDebilidad.SelectedItem ; 

            pokemon1.ImagenUrl = textImagenUrl.Text ;

            /// con el obj cargado lo mando a la BD
            /// 
            negocio.agregar(pokemon1) ;

            MessageBox.Show("Agregaro Exitosamente") ; 

            Close() ;
            
            } catch (Exception ex) { 
            
            MessageBox.Show(ex.ToString() ) ; }

        }

        private void frmAltaPokemon_Load(object sender, EventArgs e) {

        ElementoNegocio elementoNegocio = new ElementoNegocio() ; 

        try {

        // asocio a la lista los desplegables
        
        comboBoxTipo.DataSource = elementoNegocio.listar() ;  
        
        comboBoxDebilidad.DataSource = elementoNegocio.listar() ; 

        } catch (Exception ex){ MessageBox.Show(ex.ToString())  ; }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
        
        }

        
    