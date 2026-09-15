using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient ; 

namespace NegocioPokemon {
    internal class AccesoDatos {

    // que modelo aca? obejtos para estableces conexion

    private SqlConnection conexion ; 
    private SqlCommand comando ;
    private SqlDataReader lector ;
    public SqlDataReader Lector { get { return lector ; } }
    public AccesoDatos(){

    conexion = new SqlConnection("server=localhost; database=PokemonDB; User Id=sa; Password=@Willystu10; TrustServerCertificate=True;") ;
    
    comando = new SqlCommand() ;
    
    }

    public void setearConsulta(string consulta){

    comando.CommandType = System.Data.CommandType.Text ;
    comando.CommandText = consulta ; // encapsulo la accion de darle tipo y consulta


    }

    public void ejecutarLectura(){
    
    comando.Connection = conexion ; 

    try {
    
    conexion.Open() ;

    lector = comando.ExecuteReader() ;  } catch (Exception ex) {

    
    
    } }

    public void ejecutarAccion(){

    comando.Connection = conexion ;
    
    try{
    
        conexion.Open() ;

        comando.BeginExecuteNonQuery() ; 
    
    
    } catch(Exception ex){

       throw ex ;
        
       }  
        
        } 

    public void cerrarConexion(){
    
    if(lector != null){
        
            conexion.Close() ; } }} }  

    

