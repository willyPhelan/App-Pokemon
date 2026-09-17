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


namespace InterfazPokemon {
    public partial class Form1 : Form {

        private List<Pokemon> listaPokemon ; // armo lista 
        public Form1() {

            InitializeComponent() ;
        }

        private void Form1_Load(object sender, EventArgs e)  { cargar() ; } 

        private void cargar(){

         PokemonNegocio negocio = new PokemonNegocio() ;

            try {

            listaPokemon = negocio.listar() ; // guardo metodo en una variable (atributo)

            dgvPokemon.DataSource = listaPokemon ; 

            dgvPokemon.Columns["ImagenUrl"].Visible = false ; 
         
            pictureBoxPokemon.Load(listaPokemon[0].ImagenUrl) ; 

            dgvPokemon.Columns["ImagenUrl"].Visible = false; }

            catch (Exception ex){ MessageBox.Show(ex.ToString()) ; } ;


        }

        private void pictureBoxPokemon_Click(object sender, EventArgs e)
        {

        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e){ // evento 
       
        
        Pokemon seleccionado =  (Pokemon)dgvPokemon.CurrentRow.DataBoundItem ;   // trato cada fila de la grilla como un      }
   
        cargarImagen(seleccionado.ImagenUrl) ;
        
        }
        
        
        private void cargarImagen(string imagen){

        try {
        
            pictureBoxPokemon.Load(imagen) ; } catch {
            
            pictureBoxPokemon.Load("https://media.istockphoto.com/id/2164022210/es/vector/tres-textos-de-estilo-no-c%C3%B3mics-mensaje-de-atenci%C3%B3n-vintage-p%C3%B3ster-retro-peligroso.jpg?s=612x612&w=0&k=20&c=tjHw_4fqBlJw1QyDmEeCohqrNQCfoblh23x_tPnE7d4=") ;
            
            }
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon() ;

            alta.ShowDialog() ;

            cargar() ; // cargo pokemon nuevo a la lista


        }



        private void buttonModificar_Click_1(object sender, EventArgs e)
        {

         Pokemon seleccionado ; 

                seleccionado = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem ; 

                frmAltaPokemon modificar = new frmAltaPokemon(seleccionado) ; 

                modificar.ShowDialog();

                cargar() ;

        }
    } 
        
        
        
}
