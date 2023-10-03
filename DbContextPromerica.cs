using System.Linq;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity; //Para esta clase utilizamos la libreria de entity framewok

public class DbContextPromerica : DbContext
{
    // Deeclaramos las entidades a utilizar dentro del contexto
    public DbSet<PromericaCliente> PromericaCliente { get; set; }

    // Constructor en el cual debemos especificar la cadena de conexion
    public DbContextPromerica(string connectionString) : base(connectionString)
    {
    }

}

public class PromericaCliente
{
    public int Id { get; set; }
    public string NombreCliente { get; set; }
}

public class AccesoDatos
{
    private readonly DbContextPromerica contexto;

    public AccesoDatos(string connectionString)
    {
        contexto = new DbContextPromerica(connectionString);
    }

    // En este metodo ejecutamos el select y devolvemos un datatable-dataset
    public DataTable EjecutarSelect(string consulta)
    {
        try
        {
            DataTable resultado = new DataTable();
            using (var conexion = contexto.Database.Connection)
            {
                conexion.Open();
                using (var cmd = conexion.CreateCommand())
                {
                    cmd.CommandText = consulta;
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        resultado.Load(dataReader);
                    }
                }
            }
            return resultado;
        }
        catch (Exception ex)
        {
            throw new Exception("Ha ocurrido un error al ejecutar el select el cual indica: " + ex.Message);
        }
    }

    public int EjecutarNonQuery(string consulta)
    {
        try
        {
            using (var conexion = contexto.Database.Connection)
            {
                conexion.Open();
                using (var cmd = conexion.CreateCommand())
                {
                    cmd.CommandText = consulta;
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {

            throw new Exception("Ha ocurrido un error al ejecutar INSERT/UPDATE/DELETE indica: " + ex.Message);
        }
    }

    public int GetNumberLineSelect(string consulta)
    {
        try
        {
            using (var conexion = contexto.Database.Connection)
            {
                conexion.Open();
                using (var cmd = conexion.CreateCommand())
                {
                    cmd.CommandText = consulta;
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        int numeroFilas = 0;
                        while (dataReader.Read())
                        {
                            numeroFilas++;
                        }
                        return numeroFilas;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ha ocurrido un error al obtener numero de filas indica: " + ex.Message);
        }
    }
}