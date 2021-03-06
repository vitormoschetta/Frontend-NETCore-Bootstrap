using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Projeto.Validators
{
    public class ValidarCnpj : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var success = Validar(value.ToString());

            if (success)
                return ValidationResult.Success;
            else
                return new ValidationResult("Favor digitar um Documento válido.");
        }

        public static bool Validar(string parametro)
        {
            var doc = parametro.Replace(".", "").Replace("-", "").Replace("/", "").Trim();

            if (doc.Length != 11 && doc.Length != 14)
                return false;

            if (ExisteCaractereAlfabetico(doc))
                return false;

            if (TodosCaractersIguais(doc))
                return false;

            bool valido = false;

            valido = ValidarDocumento(doc);

            return valido;
        }


        public static bool TodosCaractersIguais(string doc)
        {
            char[] arr = doc.ToCharArray();
            char caractere = 'x';
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == 0)
                    caractere = arr[i];
                else
                {
                    if (caractere == arr[i])
                        caractere = arr[i];
                    else
                        return false;
                }

            }
            return true;
        }

        public static bool ExisteCaractereAlfabetico(string doc)
        {
            char[] arr = doc.ToCharArray();
            return arr.Where(x => char.IsLetter(x)).Any();
        }


        public static bool ValidarDocumento(string doc)
        {
            // validar primeiro digito
            var dozeNumeros = doc.Substring(0, 12);
            var numerosInverso = InverteString(dozeNumeros);

            var acc = 0;
            var j = 2;
            for (var i = 0; i < numerosInverso.Length; i++)
            {
                var digito = Convert.ToInt32(numerosInverso.Substring(i, 1));
                acc += digito * j;
                j++;

                if (j > 9)
                    j = 2;
            }

            var resto = acc % 11;
            var verifica = 11 - resto;
            var verificador = 0;
            if (verifica < 10)
                verificador = verifica;


            if (Convert.ToInt32(doc.Substring(12, 1)) != verificador)
                return false;


            // validar segundo digito     
            var trezeNumeros = doc.Substring(0, 13);
            numerosInverso = InverteString(trezeNumeros);

            acc = 0;
            j = 2;
            for (var i = 0; i < numerosInverso.Length; i++)
            {
                var digito = Convert.ToInt32(numerosInverso.Substring(i, 1));
                acc += digito * j;
                j++;

                if (j > 9)
                    j = 2;
            }

            resto = acc % 11;
            verifica = 11 - resto;
            verificador = 0;
            if (verifica < 10)
                verificador = verifica;


            if (Convert.ToInt32(doc.Substring(13, 1)) != verificador)
                return false;

            return true;
        }

        public static string InverteString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}