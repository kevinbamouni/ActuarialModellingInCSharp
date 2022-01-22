using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTermS
{
    internal class Cache
    {
        //public Dictionary<int,int> age;
        //public Dictionary<int, double> sum_assured;
        //public Dictionary<int, int> duration;
        //public Dictionary<int, double> aclaim_ppge;
        //public Dictionary<int, double> commissions;
        //public Dictionary<int, double> expenses;
        //public Dictionary<int, double> inflation_factor;
        //public Dictionary<int, double> lapse_rate { get; set; }
        //public Dictionary<int, double> net_cf { get; set; }
        //public Dictionary<int, double> claims { get; set; }
        //public Dictionary<int, double> premiums { get; set; }
        //
        // Test : Les Variables stockées dans le cache sont uniquement les variables dont on a besoin d'accéder, dans la projection,
        // à la valeur à un instant t-n antérieur
        // t étant l'instant présent, et n le niveau de l'historique
        //
        public Dictionary<int, double> pols_death { get; set; }
        public Dictionary<int, double> pols_lapse { get; set; }
        public Dictionary<int, double> pols_if { get; set; } 
        public Dictionary<int, double> pols_maturity { get; set; }
        public Cache() {
            pols_maturity = new Dictionary<int, double>();
            pols_lapse = new Dictionary<int, double>();
            pols_if = new Dictionary<int, double>();
            pols_death = new Dictionary<int, double>();
        }
    }
}
