using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient ;
using DominioPokemon ;
using System.Security.AccessControl;

namespace NegocioPokemon {
    public class PokemonNegocio {

      public void agregar(Pokemon po) {
    AccesoDatos datos = new AccesoDatos();
    try { 
        // Corregimos la cantidad de columnas, el orden y los parámetros
        datos.setearConsulta("INSERT INTO Pokemons (Numero, Nombre, Descripcion, ImagenUrl, IdTipo, IdDebilidad, Activo) VALUES (" + po.Numero + ", @Nombre, @Descripcion, @ImagenUrl, @IdTipo, @IdDebilidad, 1)");
      
        datos.setearParametro("@Nombre", po.Nombre);
        datos.setearParametro("@Descripcion", po.Descripcion);
        datos.setearParametro("@ImagenUrl", po.ImagenUrl);
        datos.setearParametro("@IdTipo", po.Tipo.Id);
        datos.setearParametro("@IdDebilidad", po.Debilidad.Id);

        datos.ejecutarAccion(); 
    } 
    catch (Exception ex) { 
        throw ex; 
    } 
    finally { 
        datos.cerrarConexion(); 
    } 
}
        public void modificar(){} 

        public List <Pokemon> listar(){

            List<Pokemon> lista = new List<Pokemon>() ;

            SqlConnection conexion = new SqlConnection() ; 

            SqlCommand comando = new SqlCommand() ;

            SqlDataReader lector ; 

            try {
            
                conexion.ConnectionString = "server=localhost; database=PokemonDB; User Id=sa; Password=@Willystu10; TrustServerCertificate=True;";  ; 

                comando.CommandType = System.Data.CommandType.Text ; 

                comando.CommandText = "SELECT p.Numero, p.Nombre, p.Descripcion, p.ImagenUrl, t.NombreTipo AS Elemento, d.NombreTipo AS Debilidad, p.IdTIpo, p.IdDebilidad FROM Pokemons p INNER JOIN Tipos t ON p.IdTipo = t.IdTipo INNER JOIN Tipos d ON p.IdDebilidad = d.IdTipo ;" ;

                comando.Connection = conexion ;

                conexion.Open() ;

                lector = comando.ExecuteReader() ;

               
                while(lector.Read()){

                 Pokemon aux = new Pokemon() ;

                 aux.Numero = lector.GetInt32(0) ;
                 
                 aux.Nombre = (string)lector["Nombre"] ; 

                 aux.Descripcion = (string)lector["Descripcion"] ; 

                 if(!(lector.IsDBNull(lector.GetOrdinal("ImagenUrl")))){ // si no es null la columna en la bd...

               //  if(!(lector["ImagenUrl"] is DBNull)) aux.ImagenUrl = (string)lector["ImagenUrl"] ; OTRA FORMA DE HACERLO

                 aux.ImagenUrl = (string)lector ["ImagenUrl"] ; }

                 aux.Tipo = new Elemento() ; 

                 aux.Tipo.Id = (int)lector["IdTipo"] ;

                 aux.Tipo.Descripcion = (string)lector["Elemento"]  ;

                 aux.Debilidad = new Elemento() ; 

                 aux.Debilidad.Descripcion = (string)lector["Debilidad"] ;

                 
                 lista.Add(aux) ;

                } ;



            conexion.Close() ;

            return lista ; } catch(Exception ex) {
            
            throw ex ; }
                      
        }  

    }
}
