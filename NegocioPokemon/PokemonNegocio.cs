using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient ;
using DominioPokemon ;
using System.Security.AccessControl;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SqlServer.Server;

namespace NegocioPokemon {
    public class PokemonNegocio {

      public void agregar(Pokemon po) {
    AccesoDatos datos = new AccesoDatos();
    try { 
        // Corregimos la cantidad de columnas, el orden y los parámetros
        datos.setearConsulta("INSERT INTO Pokemons (Numero, Nombre, Descripcion, ImagenUrl, IdTipo, IdDebilidad, Activo) VALUES (" + po.Numero + ", @Nombre, @Descripcion, @ImagenUrl, @IdTipo, @IdDebilidad)");
     
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

            public void eliminar(int id){

            try {
            
            
                AccesoDatos datos = new AccesoDatos() ;

                datos.setearConsulta("DELETE from pokemons where id = @Id") ;
                
                datos.setearParametro("@id", id) ;

                datos.ejecutarAccion() ;

                
            
            
            } catch (Exception e) { throw e ; } } 



            public void eliminarLogico(int id){
            
            try {
            
            AccesoDatos datos = new AccesoDatos() ;

            datos.setearConsulta("update Pokemons set Activo = 0 Where Id = @id ; ") ;
            
            datos.setearParametro("@id", id) ;

            datos.ejecutarAccion() ;
            
            } catch (Exception ex){ throw ex ;  } }
            
    



        public void modificar(Pokemon poke){
        
            AccesoDatos datos = new AccesoDatos() ; // conexion 
            
            try {
            
            datos.setearConsulta("Update Pokemons set Numero = @numero, Nombre = @nombre , Descripcion = @descripcion , ImagenUrl = @img , IdTipo = @IdTipo, IdDebilidad = @idDebilidad Where Id = @id  ;") ; 
            
            datos.setearParametro("@numero", poke.Numero) ;
            datos.setearParametro("@nombre", poke.Nombre) ;
            datos.setearParametro("@descripcion", poke.Descripcion) ;
            datos.setearParametro("@img", poke.ImagenUrl) ;
            datos.setearParametro("@idTipo", poke.Tipo.Id) ;
            datos.setearParametro("@idDebilidad", poke.Debilidad.Id) ;
            datos.setearParametro("@id", poke.Id) ;

            datos.ejecutarAccion() ;
         




            } catch (Exception ex) { throw ex ; } 
        
            finally { datos.cerrarConexion() ;}
        
        } 

        public List <Pokemon> listar(){

            List<Pokemon> lista = new List<Pokemon>() ;

            SqlConnection conexion = new SqlConnection() ; 

            SqlCommand comando = new SqlCommand() ;

            SqlDataReader lector ; 

            try {
            
                conexion.ConnectionString = "server=localhost; database=PokemonDB; User Id=sa; Password=@Willystu10; TrustServerCertificate=True;";  ; 

                comando.CommandType = System.Data.CommandType.Text ; 

                comando.CommandText = "SELECT p.Numero, p.Nombre, p.Descripcion, p.ImagenUrl, t.NombreTipo AS Elemento, d.NombreTipo AS Debilidad, p.IdTIpo, p.IdDebilidad, p.Id FROM Pokemons p INNER JOIN Tipos t ON p.IdTipo = t.IdTipo INNER JOIN Tipos d ON p.IdDebilidad = d.IdTipo and p.Activo = 1 ;" ;

                comando.Connection = conexion ;

                conexion.Open() ;

                lector = comando.ExecuteReader() ;

               
                while(lector.Read()){

                 Pokemon aux = new Pokemon() ;

                 aux.Id = (int)lector["Id"] ;

                 aux.Numero = lector.GetInt32(0) ;
                 
                 aux.Nombre = (string)lector["Nombre"] ; 

                 aux.Descripcion = (string)lector["Descripcion"] ; 

                 if (!(lector.IsDBNull(lector.GetOrdinal("ImagenUrl")))) { 
 
                 aux.ImagenUrl = (string)lector["ImagenUrl"] ; 

                } else {  aux.ImagenUrl = "" ; // 👈 Si en la base de datos es NULL, le asignamos un string vacío para que no rompa
                
                }

                /* if(!(lector.IsDBNull(lector.GetOrdinal("ImagenUrl")))){ // si no es null la columna en la bd...

               //  if(!(lector["ImagenUrl"] is DBNull)) aux.ImagenUrl = (string)lector["ImagenUrl"] ; OTRA FORMA DE HACERLO


                
                 aux.ImagenUrl = (string)lector ["ImagenUrl"] ; }*/

                 aux.Tipo = new Elemento() ; 

                 aux.Tipo.Id = (int)lector["IdTipo"] ;

                 aux.Tipo.Descripcion = (string)lector["Elemento"]  ;

                 aux.Debilidad = new Elemento() ; 

                 aux.Debilidad.Descripcion = (string)lector["Debilidad"] ;

                 aux.Debilidad.Id = (int)lector["IdDebilidad"] ;

                 
                 lista.Add(aux) ;

                } ;



            conexion.Close() ;

            return lista ; } catch(Exception ex) {
            
            throw ex ; }
                      
        }
        

        public List<Pokemon> filtrar(string campo, string criterio, string filtro) {
    
        List<Pokemon> lista = new List<Pokemon>() ;
    
        AccesoDatos datos = new AccesoDatos() ;
    
    try {
        // 1. Quitamos la comilla doble sobrante del principio y dejamos los espacios listos para el WHERE

        string consulta = "SELECT p.Numero, p.Nombre, p.Descripcion, p.ImagenUrl, t.NombreTipo AS Elemento, d.NombreTipo AS Debilidad, p.IdTIpo, p.IdDebilidad, p.Id FROM Pokemons p INNER JOIN Tipos t ON p.IdTipo = t.IdTipo INNER JOIN Tipos d ON p.IdDebilidad = d.IdTipo WHERE p.Activo = 1 AND ";
        
        if (campo == "Numero") {

            switch (criterio) {

                case "Mayor a":

                    consulta += "p.Numero > " + filtro;

                    break;

                case "Menor a":

                    consulta += "p.Numero < " + filtro;

                    break;

                default:

                    consulta += "p.Numero = " + filtro;

                    break;
            }
        } 

        else if (campo == "Nombre") {

            switch (criterio) {

                case "Comienza con":

                    consulta += "p.Nombre LIKE '" + filtro + "%'" ;

                    break; 

                case "Termina con":

                    consulta += "p.Nombre LIKE '%" + filtro + "'";

                    break;

                default:

                    consulta += "p.Nombre LIKE '%" + filtro + "%'";

                    break;
            }
        } 

        else { 

            switch (criterio) {

                case "Comienza con":

                    consulta += "p.Descripcion LIKE '" + filtro + "%'";

                    break;

                case "Termina con":

                    consulta += "p.Descripcion LIKE '%" + filtro + "'";

                    break;

                default:

                    consulta += "p.Descripcion LIKE '%" + filtro + "%'";

                    break;
            }
        }

        datos.setearConsulta(consulta);

        datos.ejecutarLectura();
               
        while (datos.Lector.Read()) {

            Pokemon aux = new Pokemon();

            aux.Id = (int)datos.Lector["Id"];

            aux.Numero = datos.Lector.GetInt32(0);

            aux.Nombre = (string)datos.Lector["Nombre"];

            aux.Descripcion = (string)datos.Lector["Descripcion"];
            
            if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl")))) {

                aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

            } else {

                aux.ImagenUrl = "";
            }

            aux.Tipo = new Elemento();

            aux.Tipo.Id = (int)datos.Lector["IdTipo"];

            aux.Tipo.Descripcion = (string)datos.Lector["Elemento"];

            aux.Debilidad = new Elemento();

            aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

            aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];

            lista.Add(aux);
        }

        return lista;
    } 

    catch (Exception ex) {

        throw ex;
    }

    finally {

        datos.cerrarConexion();
    }
}
 
}

}