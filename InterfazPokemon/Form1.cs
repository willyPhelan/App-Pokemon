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

        private void Form1_Load(object sender, EventArgs e)  { 
        
        cargar() ; 
        
        Text =  "Pokemons" ; 

        comboBoxCampo.Items.Add("Numero") ;

        comboBoxCampo.Items.Add("Nombre") ;

        comboBoxCampo.Items.Add("Descripcion" +
        "") ;
        
        
        
        } 




        private void cargar(){

         PokemonNegocio negocio = new PokemonNegocio() ;

            try {

            listaPokemon = negocio.listar() ; // guardo metodo en una variable (atributo)

            dgvPokemon.DataSource = listaPokemon ; 
         
            cargarImagen(listaPokemon[0].ImagenUrl);          
            
            ocultarColumnas() ;

           }

            catch (Exception ex){ MessageBox.Show(ex.ToString()) ; } ;


        }




        private void ocultarColumnas(){

        

         dgvPokemon.Columns["ImagenUrl"].Visible = false ; 

     
         dgvPokemon.Columns["Id"].Visible = false  ;
       
         } 



        private void pictureBoxPokemon_Click(object sender, EventArgs e)
        {

        }



        private void dgvPokemon_SelectionChanged(object sender, EventArgs e){ // evento 
       
        if(dgvPokemon.CurrentRow != null){
        
        Pokemon seleccionado =  (Pokemon)dgvPokemon.CurrentRow.DataBoundItem ;   // trato cada fila de la grilla como un      
   
        cargarImagen(seleccionado.ImagenUrl) ; }
        
        }
        
        
private void cargarImagen(string imagen) {

    try {
        // Validamos primero de forma estricta si es nulo, vacío o espacios en blanco
        if (string.IsNullOrWhiteSpace(imagen)) {

            pictureBoxPokemon.Load("https://media.istockphoto.com/id/2164022210/es/vector/tres-textos-de-estilo-no-c%C3%B3mics-mensaje-de-atenci%C3%B3n-vintage-p%C3%B3ster-retro-peligroso.jpg?s=612x612&w=0&k=20&c=tjHw_4fqBlJw1QyDmEeCohqrNQCfoblh23x_tPnE7d4=");
        
            } else {

            pictureBoxPokemon.Load(imagen) ;
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

        eliminar() ;

        }


        

        private void label1_Click(object sender, EventArgs e)
        {

        }

       private void btnBuscar_Click_1(object sender, EventArgs e) {
    
        PokemonNegocio negocio = new PokemonNegocio();

    try {

        // 1. Capturamos los valores seleccionados de manera segura dentro del try

        string campo = comboBoxCampo.SelectedItem.ToString() ;

        string criterio = comboBoxCriterio.SelectedItem.ToString() ;

        string filtro = textBoxFiltroAvanzado.Text ;  

        // 2. Ejecutamos el filtro y actualizamos la grilla dentro del mismo try

        dgvPokemon.DataSource = negocio.filtrar(campo, criterio, filtro) ;
        
        // 3. Ocultamos las columnas ID e ImagenUrl para que no se desordene la grilla

        ocultarColumnas() ;
    } 

        catch (Exception ex) { 

            MessageBox.Show(ex.ToString()) ;  
    }
}

        private void btnEliminadoLogico_Click(object sender, EventArgs e)

        {

        eliminar(true) ;

        }

        

       private void eliminar(bool logico = false) { // toma falso por defecto // fn reutilizable de eliminacion Logica y fisica
    
       PokemonNegocio negocio = new PokemonNegocio();
       Pokemon seleccionado;
              
            try {
            DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Yes) {
                seleccionado = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem;

                if (logico) {
                    negocio.eliminarLogico(seleccionado.Id);
                    MessageBox.Show("Eliminado lógico exitosamente.");
                } 
                else {
                    // Eliminación física (cuidado con las restricciones de clave foránea)
                    negocio.eliminar(seleccionado.Id);
                    MessageBox.Show("Eliminado físico exitosamente.");
                }

                cargar();
            }
        } 
        catch(Exception ex) {
            MessageBox.Show(ex.ToString());
        }
    }

        private void textBoxFiltro_TextChanged(object sender, EventArgs e) {

        List <Pokemon> listaFiltrada ; 

        string filtro =  textBoxFiltro.Text ; 

        if(filtro != "") { listaFiltrada = listaPokemon.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()) ) ; 

        } else {

         listaFiltrada = listaPokemon ; 

         }

        dgvPokemon.DataSource = null ; // limpio 

        dgvPokemon.DataSource = listaFiltrada ; // asigno 

        ocultarColumnas() ;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboBoxCampo.SelectedItem.ToString() ; 

            if(opcion == "Numero") {

            comboBoxCriterio.Items.Clear() ; 

            comboBoxCriterio.Items.Add("Mayor a") ; 

             comboBoxCriterio.Items.Add("Menor a") ; 

              comboBoxCriterio.Items.Add("Igual a") ; 


            } else {
            
            comboBoxCriterio.Items.Clear() ; 
            
            comboBoxCriterio.Items.Add("Comienza con:") ; 

             comboBoxCriterio.Items.Add("Termina con:") ; 

              comboBoxCriterio.Items.Add("Contiene:") ; 
            
            
            
            }
            ;       
            
            
            
            
            }
    }
    
       }
      
         
        
        
        
    
