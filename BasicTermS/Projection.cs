using System;
using System.Data;
using System.Linq;
using static System.Math;


namespace BasicTermS
{
    class Projection
    {
        public DataRow ModelPoint;
        public static DataTable MortTable = DataFromCsv.ReadDataTableFromCsv(Tables.PathMortRate, Tables.SchemasMortRate);
        public static DataTable DiscRateAnn = DataFromCsv.ReadDataTableFromCsv(Tables.PathDiscountRate, Tables.SchemasDiscountRate);
        public Cache cacheProjection = new Cache();
        public Projection(DataRow paramModelPoint){
            ModelPoint = paramModelPoint;
            //MortTable = paramMortTable; //.AsEnumerable().Where(x=> x.Field<float>( == 2);
        }

        // OK
        int age(int t)
        {
            return age_at_entry() + duration(t);
        }

        // OK
        char sex()
        {
            return ModelPoint.Field<char>("sex");
        }

        // OK
        double sum_assured()
        {
            return ModelPoint.Field<double>("sum_assured");
        }

        // OK
        int duration(int t)
        {
            return t%12;
        }

        // OK
        int age_at_entry()
        {
            return ModelPoint.Field<int>("age_at_entry");
        }

        // OK
        double claim_pp(int t)
        {
            return sum_assured();
        }

        // OK
        double commissions(int t)
        {
            if(duration(t) == 0){
                return premiums(t); } 
            else { return 0; }
        }

        // TODO
        List<double> disc_factors()
        {
            List<double> rates = new List<double>();
            for (int i = 0; i < proj_len(); i++)
            {
                rates.Add(1+Pow(disc_rate_mth()[i],-i));
            }
            return rates;
        }

        // TODO
        List<double> disc_rate_mth()
        {
            List<double> rates = new List<double>();
            for (int i = 0; i < proj_len(); i++) {
                var val = from row in DiscRateAnn.AsEnumerable()
                          where row.Field<int>("year") == i % 12
                          select (double)(Pow(1 + row.Field<double>("zero_spot"), 1 / 12) - 1);
                rates.Add(val.FirstOrDefault<double>());
            }
            //var val = from row in DiscRateAnn.AsEnumerable()
            //          where row.Field<int>("year") == t%12
            //          select (Double)Pow(1+row.Field<double>("zero_spot"),1/12)-1;
            return rates;
        }

        // OK
        double expense_acq()
        {
            return 300;
        }

        // OK
        double expense_maint()
        {
            return 60;
        }

        // OK
        double expenses(int t)
        {
            double maint = pols_if(t) * expense_maint() / 12 * inflation_factor(t);

            if (t == 0) {
                return expense_acq() + maint;} 
            else { return maint; }              
        }

        // OK
        double pols_if(int t)
        {
            if (cacheProjection.pols_if.ContainsKey(t)) { return cacheProjection.pols_if[t]; }
            else {
                if (t == 0) { cacheProjection.pols_if.Add(t, pols_if_init()); }
                else if (t > (policy_term() * 12)) { cacheProjection.pols_if.Add(t, 0); }
                else { cacheProjection.pols_if.Add(t, pols_if(t - 1) - pols_lapse(t - 1) - pols_death(t - 1) - pols_maturity(t)); }

                return cacheProjection.pols_if[t]; // return a partir du cache
            }
        }

        // OK
        double pols_if_init()
        {
            //return ModelPoint.Field<double>("policy_count");
            return 1;
        }

        //OK
        double inflation_factor(int t)
        {
            return Math.Pow((1 + inflation_rate()),(t/12));
        }

        //OK
        double inflation_rate()
        {
            return 0.01;
        }

        //OK
        double lapse_rate(int t)
        {
            return Max(0.1 - 0.02 * duration(t), 0.02);
        }

        //OK
        double loading_prem()
        {
            return 0.05;
        }

        // OK
        int model_point()
        {
            return ModelPoint.Field<int>("point_id");
        }

        // TODO
        double mort_rate(int t)
        {
            //var mortRate = MortTable.AsEnumerable().Where(x => x.Field<int>("Age")== (double)age(t))
            //    .Select(x => x.Field<Double>((Max(Min(5, duration(t)), 0)).ToString()));
            string col = (Max(Min(5, duration(t)), 0)).ToString();
            var mt = from to in MortTable.AsEnumerable() 
                     where to.Field<int>("Age") == age(t) 
                     select to.Field<Double>(col);
            //var res = from mot in mt select (double)(mot.Field<double>(col));
            var a = mt.ToList<Double>()[0];

            return a;
        }

        // OK
        double mort_rate_mth(int t)
        {
            // renvoie 0, à investiguer
            double b = (1 - mort_rate(t));
            double a = Pow(b,(1/12)); //renvoie 1 car la fonction puisssance arrondi automatiqument le resultat de Pow à 1, pour une raison à investiguer!!
            double c = 1 - 0.999488274708617;
            double d = mort_rate(t) / 12;
            //return 1 - Pow((1 - mort_rate(t)), (1 / 12));
            return d;
        }

        // OK
        double net_cf(int t)
        {
            return premiums(t) - claims(t) - expenses(t) - commissions(t);
        }

        // OK
        double claims(int t)
        {
            return claim_pp(t) * pols_death(t);
        }

        // OK
        double net_premium_pp()
        {
            return pv_claims() / pv_pols_if();
        }

        // OK
        int policy_term()
        {
            int a = ModelPoint.Field<int>("policy_term");
            return a;
        }

        // OK
        double pols_death(int t)
        {
            if (cacheProjection.pols_death.ContainsKey(t)) { return cacheProjection.pols_death[t]; }
            else
            {
                cacheProjection.pols_death.Add(t, pols_if(t) * mort_rate_mth(t));
                
                return cacheProjection.pols_death[t]; // return a partir du cache
            }
        }

        // OK
        double pols_lapse(int t)
        {
            if (cacheProjection.pols_lapse.ContainsKey(t)) { return cacheProjection.pols_lapse[t]; }
            else
            {
                cacheProjection.pols_lapse.Add(t, (pols_if(t) - pols_death(t)) * (1 - Pow((1 - lapse_rate(t)), (1 / 12))));
                
                return cacheProjection.pols_lapse[t]; // return a partir du cache
            }
        }

        // OK
        double pols_maturity(int t)
        {
            if (cacheProjection.pols_maturity.ContainsKey(t)) { return cacheProjection.pols_maturity[t]; }
            else
            {
                if (t == policy_term() * 12)
                {
                    cacheProjection.pols_maturity.Add(t, pols_if(t - 1) - pols_lapse(t - 1) - pols_death(t - 1));
                }
                else { cacheProjection.pols_maturity.Add(t, 0); }

                return cacheProjection.pols_maturity[t]; // return a partir du cache
            }
        }

        // OK
        double premium_pp()
        {
            return Round((1 + loading_prem()) * net_premium_pp(), 2);
        }

        // OK
        double premiums(int t)
        {
            return premium_pp() * pols_if(t);
        }

        // OK
        int proj_len()
        {
            int a = 12 * policy_term() + 1;
            return a;
        }

        // TODO
        public Dictionary<string, List<double>> result_cf()
        {
            List<double> vpolsid = new List<double>();
            List<double> vpremiums = new List<double>();
            List<double> vclaims = new List<double>();
            List<double> vexpenses = new List<double>();
            List<double> vcommissions = new List<double>();
            List<double> vNetCashflow = new List<double>();
            int len = proj_len();
            for (int i = 0; i < len; i++)
            {
                vpolsid.Add(model_point());
                vpremiums.Add(premiums(i));
                vclaims.Add(claims(i));
                vexpenses.Add(expenses(i));
                vcommissions.Add(commissions(i));
                vNetCashflow.Add(net_cf(i));
            }

            Dictionary<string, List<double>> result = new Dictionary<string, List<double>> {
                {"pols_id",vpolsid},
                {"premiums",vpremiums},
                {"claims",vclaims},
                {"expenses",vexpenses},
                {"commissions",vcommissions},
                {"Net_Cashflow",vNetCashflow}};

            return result;
        }

        // TODO
        int pv_pols_if()
        {
            return 1;
        }

        // TODO
        int pv_claims()
        {
            return 1;
        }

        // TODO
        double pv_commissions()
        {
            return 1;
        }
        // TODO
        double pv_expenses()
        {
            return 1;
        }

        // TODO
        double pv_net_cf()
        {
            return 1;
        }

        // TODO
        double pv_premiums()
        {
            return 1;
        }
        // TODO
        double result_pv()
        {
            return 1;
        }  
    }
}