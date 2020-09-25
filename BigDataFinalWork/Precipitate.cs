using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDataFinalWork
{
    class Precipitate
    {
        public Precipitate(int year, double january, double february, double march, double april, double may, double june,double july, double august, double september, double october, double november,double december)
        {
            this.year = year;
            this.january = january;
            this.february = february;
            this.march = march;
            this.april = april;
            this.may = may;
            this.june = june;
            this.july = july;
            this.august = august;
            this.september = september;
            this.october = october;
            this.november = november;
            this.december = december;

        }


        public int year { get; set; }
        public double january { get; set; }
        public double february { get; set; }
        public double march { get; set; }
        public double april { get; set; }
        public double may { get; set; }
        public double june { get; set; }
        public double july { get; set; }
        public double august { get; set; }
        public double september { get; set; }
        public double october { get; set; }
        public double november { get; set; }
        public double december { get; set; }
    }
}
