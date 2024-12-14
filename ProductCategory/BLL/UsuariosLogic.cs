using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

using DAL;
using Entities;

namespace BLL
{
    public class UsuariosLogic
    {
       

      

        public static bool VerifyPassword(string password, string storedHash)
        {
            // Convertir el hash almacenado de Base64 a un byte array
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extraer el salt (los primeros 16 bytes)
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Generar el hash de la contraseña proporcionada usando el mismo salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                // Comparar los hashes (los últimos 32 bytes del hash almacenado)
                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false; // La contraseña no coincide
                    }
                }
            }

            return true; // La contraseña coincide
        }
    }
}
