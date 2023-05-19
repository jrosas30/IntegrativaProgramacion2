using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2EVA1juanrosas
{
    internal class CLASEEVALUA2juanRosas
    {
        private string Rut;
        private DateTime InicioSesion;
        private DateTime FinSesion;
        private string Accion;
        private DateTime AccionF;

        public CLASEEVALUA2juanRosas()
        {

        }

        public CLASEEVALUA2juanRosas(string Rut, DateTime InicioSesion, DateTime FinSesion, string Accion, DateTime AccionF)
        {
            this.Rut = Rut;
            this.InicioSesion = InicioSesion;
            this.FinSesion = FinSesion;
            this.Accion = Accion;
            this.AccionF = AccionF;
        }

        public void setRut(string Rut)
        {
            this.Rut = Rut;
        }
        public string getRut()
        {
            return this.Rut;
        }

        public void setInicioSecion()
        {
            this.InicioSesion = DateTime.Now;
        }
        public string getInicioSesion()
        {
            return this.InicioSesion.ToString();
        }

        public void setFinSesion(DateTime FinSesion)
        {
            this.FinSesion = DateTime.Now;
        }
        public string getFinSesion()
        {
            return this.FinSesion.ToString();
        }

        public void setAccion(string Accion)
        {
            this.Accion = Accion;
        }
        public string getAccion()
        {
            return this.Accion;
        }

        public void setAccionF(DateTime AccionF)
        {
            this.AccionF = AccionF;
        }
        public string getAccionF()
        {
            return this.AccionF.ToString();
        }
    }
}
