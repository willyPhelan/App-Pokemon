using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO ;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DominioPokemon ; 
using NegocioPokemon ; 
using System.Configuration ; 

namespace InterfazPokemon {
    public partial class frmAltaPokemon : Form {

    private Pokemon pokemon = null ;

    private OpenFileDialog archivo = null ; 

    
        public frmAltaPokemon() {

            InitializeComponent() ;
        }

          public frmAltaPokemon(Pokemon pokemon) {

            InitializeComponent() ;

            this.pokemon = pokemon ; 

            Text = "Modificar Pokemon" ;
        }

        private void btnCancelar_Click(object sender, EventArgs e) {

            this.Close() ;
        }

        private void btnAgregar_Click(object sender, EventArgs e){

          //  Pokemon pokemon1 = new Pokemon() ;

            PokemonNegocio negocio = new PokemonNegocio() ; // instancio obj de conexion

            try { 
            
            // capturo los datos y los transforomo en objeto de tipo Pokemon

            if (pokemon == null) pokemon = new Pokemon() ; 

            pokemon.Numero = int.Parse(textNumero.Text) ;

            pokemon.Nombre = textNombre.Text ;

            pokemon.Descripcion = textDescripcion.Text ;

            pokemon.ImagenUrl = textImagenUrl.Text ; 

            pokemon.Tipo = (Elemento)comboBoxTipo.SelectedItem ; 

            pokemon.Debilidad = (Elemento)comboBoxDebilidad.SelectedItem ; 

            pokemon.ImagenUrl = textImagenUrl.Text ;

            /// con el obj cargado lo mando a la BD
            /// 
            // si tiene id existe el pokemon 

            if(pokemon.Id != 0 ){

            negocio.modificar(pokemon) ;

            MessageBox.Show("Modificado Exitosamente") ; 

            } else {

            negocio.agregar(pokemon) ;
      
            MessageBox.Show("Agregado con exito") ; }

            // guardo imagen si la levanto localmente

            if(archivo != null && !(textImagenUrl.Text.ToUpper().Contains("HTTP"))) File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName, true) ; // safeFileName => obtengo nombre del archivo

            Close()  ; }
            
             catch (Exception ex) { 
            
             MessageBox.Show(ex.ToString() ) ; }

        }

        private void frmAltaPokemon_Load(object sender, EventArgs e) {

        ElementoNegocio elementoNegocio = new ElementoNegocio() ; 

        try {

        // asocio a la lista los desplegables
        
        comboBoxTipo.DataSource = elementoNegocio.listar() ;  

        comboBoxTipo.ValueMember = "Id" ;

        comboBoxTipo.DisplayMember = "Descripcion" ;
        
        comboBoxDebilidad.DataSource = elementoNegocio.listar() ; 

        comboBoxDebilidad.ValueMember = "Id" ;

        comboBoxDebilidad.DisplayMember = "Descripcion" ;

        if(pokemon != null ){

        textNumero.Text = pokemon.Numero.ToString() ;

        textNombre.Text = pokemon.Nombre ; 

        textDescripcion.Text = pokemon.Descripcion ;

        textImagenUrl.Text = pokemon.ImagenUrl ; 

        cargarImagen(pokemon.ImagenUrl) ;

        comboBoxTipo.SelectedValue = pokemon.Tipo.Id ;

        comboBoxDebilidad.SelectedValue = pokemon.Debilidad.Id ; 

        }

        } catch (Exception ex){ MessageBox.Show(ex.ToString())  ; }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textImagenUrl_Leave(object sender, EventArgs e)
        {

        cargarImagen(textImagenUrl.Text) ;

        }



          private void cargarImagen(string imagen){

        try {
        
            pictureBoxPokemon.Load(imagen) ; } catch {
            
            pictureBoxPokemon.Load("https://media.istockphoto.com/id/2164022210/es/vector/tres-textos-de-estilo-no-c%C3%B3mics-mensaje-de-atenci%C3%B3n-vintage-p%C3%B3ster-retro-peligroso.jpg?s=612x612&w=0&k=20&c=tjHw_4fqBlJw1QyDmEeCohqrNQCfoblh23x_tPnE7d4=") ;
            
            }
        
        
        }

        private void btnAgregarImg_Click(object sender, EventArgs e){ 

        archivo = new OpenFileDialog() ;

        archivo.Filter = "jpg|*.jpg; |png| *.png";

        if(archivo.ShowDialog() == DialogResult.OK){

        textImagenUrl.Text = archivo.FileName ; 

        cargarImagen(archivo.FileName) ;
        
        // guardo la img

   //    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName, true); // safeFileName => obtengo nombre del archivo


                  }
       

               }
  
            }
   
    
        }

        
    