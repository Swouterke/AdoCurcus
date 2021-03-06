﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoLibrary
{
    public class Brouwer
    {
        private Int32 brouwersNrValue;
        private String brNaamValue;
        private String adresValue;
        private Int16 postcodeValue;
        private String gemeenteValue;
        private Int32? omzetValue;

        public Int32 BrouwerNr
        {
            get { return brouwersNrValue; }
        }
        public String BrNaam
        {
            get { return brNaamValue; }
            set { brNaamValue = value; }
        }
        public String Adres
        {
            get { return adresValue; }
            set { adresValue = value; }
        }
        public Int16 Postcode
        {
            get { return postcodeValue; }
            set
            {
                    postcodeValue = value;
            }
        }
        public String Gemeente
        {
            get { return gemeenteValue; }
            set { gemeenteValue = value; }
        }
        public Int32? Omzet
        {
            get { return omzetValue; }
            set
            {
                if (value.HasValue && Convert.ToInt32(value) < 0)
                    throw new Exception("Omzet moet groter dan 0 zijn!");
                else
                    omzetValue = value;
            }
        }

        public Brouwer(Int32 brNr, String brnaam, String adres, Int16 postcode, String gemeente, Int32? omzet)
        {
            brouwersNrValue = brNr;
            this.BrNaam = brnaam;
            this.Adres = adres;
            this.Postcode = postcode;
            this.Gemeente = gemeente;
            this.Omzet = omzet;
        }
    }
}
