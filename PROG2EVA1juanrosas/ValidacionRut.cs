using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PROG2EVA1juanrosas
{
    internal class ValidacionRut
    {
        private string rut;

        public string Rut
        {
            get
            {
                return rut;
            }
            set
            {

                rut = value;

                Regex regex = new Regex(@"^[0-9\-kK]+$");
                bool caracteresDistintos = regex.IsMatch(rut);

                if (!caracteresDistintos)
                {
                    rut = "contiene un caracter invalido";
                }
                else
                {
                    //rellenar digitos con ceros
                    while (rut.Length <= 9)
                    {
                        rut = "0" + rut;
                    }


                    char[] arrestring = new char[10];
                    for (int i = 0; i < 10; i++)
                    {
                        arrestring[i] = rut[i];

                    }

                    if (arrestring[arrestring.Length - 2] == '-')
                    {
                        int[] arreint = new int[8];
                        for (int i = 0; i < arreint.Length; i++)
                        {
                            arreint[i] = Int32.Parse(arrestring[i].ToString());
                        }

                        //arreglo constantes
                        int[] constantes = { 3, 2, 7, 6, 5, 4, 3, 2 };

                        //calculo de suma constantes + char del rut
                        double producto;
                        double divisionDecimal;
                        double suma = 0;
                        int divisionEntero = 0;

                        for (int i = 0; i < constantes.Length; i++)
                        {
                            producto = constantes[i] * arreint[i];
                            suma += producto;
                        }


                        //algoritmo calculo rut
                        divisionDecimal = suma / 11;
                        divisionEntero = (int)divisionDecimal;
                        double valorDecimal;
                        valorDecimal = divisionDecimal - divisionEntero;
                        double resta11;
                        resta11 = (11 - (11 * (valorDecimal)));
                        int digito;
                        digito = (int)Math.Round(resta11);
                        if (digito == 11)
                        {
                            digito = 0;
                        }

                        string digitotxt = digito.ToString();
                        if (digitotxt == "10")
                        {
                            digitotxt = "k";
                        }


                        string respuesta;
                        if (digitotxt == rut[9].ToString())
                        {
                            respuesta = "correcto";
                        }
                        else
                        {
                            respuesta = "Rut incorrecto, digito verificador es: " + digitotxt;
                        }
                        rut = respuesta;
                    }
                    else
                    {
                        rut = "Rut incorrecto falta guión y digito verificador";
                    }
                }

            }
        }
    }
}
