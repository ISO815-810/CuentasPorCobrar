using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Utils
{
    public class Cedula: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var pCedula = value as string;
            
            // verificar que los caracteres de la cedula sean validos antes de empezar a calcular
            if (new Regex(@"\D+").IsMatch(pCedula))
                return new ValidationResult("La cedula solo puede contener numeros");

            int longitudCedula = pCedula.Trim().Length;
            if (longitudCedula < 11 || longitudCedula > 11 )
                return new ValidationResult("La cedula debe contener 11 digitos exactamente");

            if (pCedula.Equals("00000000000"))
                return new ValidationResult("La cedula no puede ser 00000000000");

            // pasar de cadena a lista de digitos
            var digitosCedula = pCedula.ToList().Select(digito => Int32.Parse(digito.ToString()));
            var digitoMult = (new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 }).ToList();

            var calculo = digitosCedula.Zip(digitoMult, (digito, multiplicador) => digito * multiplicador);
            // por si hay numeros con mayores a 10
            calculo = calculo.Select(sumarDigitos);

            var total = calculo.Aggregate(0, (acc, x) => acc + x);

            if (total % 10 == 0) return ValidationResult.Success;
            else return new ValidationResult("La cedula no es valida");
        }

        private static int sumarDigitos(int numero)
        {
            var digitos = numero.ToString().ToList().Select(digito => Int32.Parse(digito.ToString()));
            return digitos.Aggregate(0, (acc, x) => acc + x);
        }

        //public static bool validarCedula(string pCedula)
        //{
        //    // verificar que los caracteres de la cedula sean validos antes de empezar a calcular
        //    int pLongCed = pCedula.Trim().Length;
        //    if (pLongCed < 11 || pLongCed > 11 || pCedula.Equals("00000000000") || new Regex(@"\D+").IsMatch(pCedula))
        //        return false;

        //    // pasar de cadena a lista de digitos
        //    var digitosCedula = pCedula.ToList().Select(digito => Int32.Parse(digito.ToString()));


        //    // int vnTotal = 0;
        //    //string pCedula = pCedula.Replace("-", "");
        //    int pLongCed = pCedula.Trim().Length;
        //    int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

        //    if (pLongCed < 11 || pLongCed > 11 || pCedula.Equals("00000000000") || new Regex(@"\D+").IsMatch(pCedula))
        //        return false;

        //    for (int vDig = 1; vDig <= pLongCed; vDig++)
        //    {
        //        int vCalculo = Int32.Parse(pCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
        //        if (vCalculo < 10)
        //            vnTotal += vCalculo;
        //        else
        //            vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
        //    }

        //    return vnTotal % 10 == 0;
        //}
    }
}