using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using EAGetMail;
using EmailTest2.Shared;
using System.Xml.Serialization;


namespace EmailTest2.Server
{
    public class PesquisaRecursiva
    {

        /// <summary>
        /// A lista<>directories vai buscar o caminho dos emls recursivamente à diretoria desejada. Adiciona os caminhos
        /// dos eml à lista<> files. A lista<> emails vai buscar os eml(guardados em files) e dá parse de acordo com a funcao ParseEmail,
        /// retirando apenas os campos que achamos necessários.
        /// </summary>
        /// <returns> Retorna uma lista do tipo Email{Path, Assunto, De:, Data </returns>
        public static List<Email> GetEmailsFromFolders()
        {
            var emails = new List<Email>();
            var files = new List<string>();
            List<string> directories = System.IO.Directory.GetDirectories(@"C:\Users\alexc\Desktop\Teste", "*", System.IO.SearchOption.AllDirectories).ToList();
            foreach (var path in directories)
            {
                files.AddRange(System.IO.Directory.GetFiles(path, "*.eml"));
            }

            foreach (var file in files)
            {
                emails.Add(ParseEmail(file));
            }
            return emails;
        }

        static Email ParseEmail(string emlFile)
        {
            Mail oMail = new Mail("TryIt");

            oMail.Load(emlFile, false);

            return new Email
            {
                Mail = Path.GetFileNameWithoutExtension(emlFile),
                Assunto = oMail.Subject,
                De = oMail.From.Address,
                Data = oMail.ReceivedDate,
            };
        }

    }

}