using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzallasManager
{
    static class ABKezelo
    {
        static SqlConnection connection;
        static SqlCommand command;



        static ABKezelo()
        {
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["szallasconstr4"].ConnectionString;
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("Sikertelen csatlakozás az adatbázishoz!", ex);
            }
        }

        public static void KapcsolatBontasa()
        {
            try
            {
                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A kapcsolat bontása sikertelen!", ex);
            }
        }

        public static void UjSzallashely(Szallashely uj)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.CommandText = "INSERT INTO [Szallashely] VALUES (@az, @ir, @var, @utc,   @haz, @faj)";
                command.Parameters.AddWithValue("@az", uj.Azonosito);
                command.Parameters.AddWithValue("@ir", uj.Cim.Irsz);
                command.Parameters.AddWithValue("@var", uj.Cim.Varos);
                command.Parameters.AddWithValue("@utc", uj.Cim.Utca);
                command.Parameters.AddWithValue("@haz", uj.Cim.Hsz);
                command.Parameters.AddWithValue("@faj", (byte)uj.Szallasfajta);
                command.ExecuteNonQuery();
                if (uj is Camping cam)
                {
                    command.CommandText = "INSERT INTO [Camping] VALUES (@az, @viz)";
                    command.Parameters.AddWithValue("@viz", cam.Vizparti);
                    command.ExecuteNonQuery();
                }

                else if (uj is EpitettSzallashely epit)
                {
                    command.CommandText = "INSERT INTO [Epitettszallashely] VALUES (@az, @csill, @szob)";
                    command.Parameters.AddWithValue("@csill", epit.Csillagokszama);
                    command.Parameters.AddWithValue("@szob", epit.Szobaar);
                    command.ExecuteNonQuery();
                    if (epit is Szalloda szall)
                    {
                        command.CommandText = "INSERT INTO [Szalloda] VALUES (@az, @well )";
                        command.Parameters.AddWithValue("@well", szall.Vanwellness);
                        //command.ExecuteNonQuery();
                    }
                    else if (epit is Panzio panz)
                    {
                        command.CommandText = "INSERT INTO [Panzio] VALUES (@az, @reg)";
                        command.Parameters.AddWithValue("@reg", panz.Vanreggeli);


                    }
                    command.ExecuteNonQuery();
                }

                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    command.Transaction.Rollback();
                }
                catch (Exception innerEx)
                {
                    throw new ABKivetel("Végzetes hiba történt!", innerEx);
                }
                throw new ABKivetel("Sikertelen kölcsönző rögzítés!", ex);
            }
        }



        /*public static void KolcsonzoModositas(Szallashely modosit)
         {
             try
             {
                 command.Parameters.Clear();
                 command.CommandText = "UPDATE [Kolcsonzo] SET [Megnevezes] = @megn WHERE [Id] = @id";
                 command.Parameters.AddWithValue("@megn", modosit.Megnevezes);
                 command.Parameters.AddWithValue("@id", modosit.Id);
                 command.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 throw new ABKivetel("Sikertelen kölcsönző módosítás!", ex);
             }
         }*/





        public static void SzallasTorles(Szallashely torol)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();


                command.Parameters.AddWithValue("@az", torol.Azonosito);
                if (torol is Camping cam)
                {
                    command.CommandText = "DELETE FROM [Camping] WHERE [Azonosito] = @az";
                    command.ExecuteNonQuery();
                }
                else if (torol is EpitettSzallashely)
                {
                    if (torol is Szalloda)
                    {
                        command.CommandText = "DELETE FROM [Szalloda] WHERE [Azonosito] = @az";
                    }
                    else if (torol is Panzio)
                    {
                        command.CommandText = "DELETE FROM [Panzio] WHERE [Azonosito] = @az";

                    }
                    command.ExecuteNonQuery();
                }

                command.CommandText = "DELETE FROM [Szallashely] WHERE [Azonosito] = @az";
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    command.Transaction.Rollback();
                }
                catch (Exception innerEx)
                {
                    throw new ABKivetel("Végzetes hiba történt!", innerEx);
                }
                throw new ABKivetel("Sikertelen jármű törlés!", ex);
            }
        }

        public static Szallashelylista TeljesFelolvasas()
        {
            try
            {
                Szallashelylista szallh = new Szallashelylista();
                command.CommandText = "SELECT * FROM Szallashely LEFT JOIN Camping ON Szallashely.Azonosito = Camping.Azonosito LEFT JOIN Epitettszallashely ON Szallashely.Azonosito = Epitettszallashely.Azonosito LEFT JOIN Szalloda ON Szallashely.Azonosito = Szalloda.Azonosito LEFT JOIN Panzio ON Szallashely.Azonosito = Panzio.Azonosito";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(6))
                        {
                            szallh.Add(new Camping(
                                     (string)reader["Azonosito"],
                                         new Cim(
                                    (short)(int)reader["Irsz"],
                                    (string)reader["Varos"],
                                   (string)reader["Utca"],
                                    (short)(int)reader["Hsz"]
                                   ),
                                    (Szallasfajta)(byte)reader["Fajta"],
                                   (bool)reader["Vizparti"]
                                    )
                                );
                        }

                        else if (!reader.IsDBNull(9))
                        {

                            if (!reader.IsDBNull(12))
                            {
                                szallh.Add(new Szalloda(
                                 (string)reader["Azonosito"],
                                    new Cim(
                                    (short)(int)reader["Irsz"],
                                    (string)reader["Varos"],
                                   (string)reader["Utca"],
                                    (short)(int)reader["Hsz"]
                                    ),

                                    (Szallasfajta)(byte)reader["Fajta"],
                                    (byte)reader["Csillagokszama"],
                                    (int)reader["Szobaar"],
                                     (bool)reader["Vanwellness"]
                                    )
                                );

                            }
                            else
                            {
                                szallh.Add(new Panzio(
                                     (string)reader["Azonosito"],
                                        new Cim(
                                        (short)(int)reader["Irsz"],
                                        (string)reader["Varos"],
                                       (string)reader["Utca"],
                                        (short)(int)reader["Hsz"]
                                        ),

                                        (Szallasfajta)(byte)reader["Fajta"],
                                        (byte)reader["Csillagokszama"],
                                        (int)reader["Szobaar"],
                                          (bool)reader["Vanreggel"]
                                        )
                                    );
                            }
                        }

                    }
                    reader.Close();
                }
                return szallh;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("Sikertelen felolvasás!", ex);
            }
        }
    }
}
