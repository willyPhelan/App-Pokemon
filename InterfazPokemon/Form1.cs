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

        private void Form1_Load(object sender, EventArgs e)  { cargar() ; 
        
        Text =  "Pokemons" ; } 

        private void cargar(){

         PokemonNegocio negocio = new PokemonNegocio() ;

            try {

            listaPokemon = negocio.listar() ; // guardo metodo en una variable (atributo)

            dgvPokemon.DataSource = listaPokemon ; 

            dgvPokemon.Columns["ImagenUrl"].Visible = false ; 
         
            cargarImagen(listaPokemon[0].ImagenUrl);

            
            dgvPokemon.Columns["Id"].Visible = false ; }

            catch (Exception ex){ MessageBox.Show(ex.ToString()) ; } ;


        }

        private void pictureBoxPokemon_Click(object sender, EventArgs e)
        {

        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e){ // evento 
       
        
        Pokemon seleccionado =  (Pokemon)dgvPokemon.CurrentRow.DataBoundItem ;   // trato cada fila de la grilla como un      
   
        cargarImagen(seleccionado.ImagenUrl) ;
        
        }
        
        
private void cargarImagen(string imagen) {
    try {
        // Validamos primero de forma estricta si es nulo, vacío o espacios en blanco
        if (string.IsNullOrWhiteSpace(imagen)) {
            pictureBoxPokemon.Load("https://media.istockphoto.com/id/2164022210/es/vector/tres-textos-de-estilo-no-c%C3%B3mics-mensaje-de-atenci%C3%B3n-vintage-p%C3%B3ster-retro-peligroso.jpg?s=612x612&w=0&k=20&c=tjHw_4fqBlJw1QyDmEeCohqrNQCfoblh23x_tPnE7d4=");
        } else {
            pictureBoxPokemon.Load(imagen);
        }
    } 
    catch {
        // Si la URL falla (ej. no tiene internet o la ruta de la web está rota), carga la de respaldo
        pictureBoxPokemon.Load("https://media.istockphoto.com/id/2164022210/es/vector/tres-textos-de-estilo-no-c%C3%B3mics-mensaje-de-atenci%C3%B3n-vintage-p%C3%B3ster-retro-peligroso.jpg?s=612x612&w=0&k=20&c=tjHw_4fqBlJw1QyDmEeCohqrNQCfoblh23x_tPnE7d4=");
    }}


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

        private void btnEliminar_Click(object sender, EventArgs e){

        PokemonNegocio negocio = new PokemonNegocio() ;

        Pokemon seleccionado =  new Pokemon() ;
            
        // eliminado fisico

        try {

      
        
        DialogResult respuesta =  MessageBox.Show("De verdad queres eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ;

        if(respuesta == DialogResult.Yes){

        seleccionado = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem ;

        negocio.eliminar(seleccionado.Id) ;

        cargar() ;

        }


        } catch(Exception ex) {

        MessageBox.Show(ex.ToString()) ;
        }




        }
    } 
        
        
        
}
