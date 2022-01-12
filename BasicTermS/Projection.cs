using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using static System.Math;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace BasicTermS
{
    class Projection
    {
        public DataRow ModelPoint;
        public static DataTable MortTable = DataFromCsv.ReadDataTableFromCsv(Tables.PathMortRate, Tables.SchemasMortRate);
        public static DataTable DiscRateAnn = DataFromCsv.ReadDataTableFromCsv(Tables.PathDiscountRate, Tables.SchemasDiscountRate);

        public Projection(DataRow paramModelPoint) {
            ModelPoint = paramModelPoint;
            //MortTable = paramMortTable; //.AsEnumerable().Where(x=> x.Field<float>( == 2);
        }

        int age(int t)
        {
            return age_at_entry() + duration(t);
        }

        int duration(int t)
        {
            return t/12;
        }

        int age_at_entry()
        {
            return ModelPoint.Field<int>("age_at_entry");
        }

        double claim_pp(int t)
        {
            return sum_assured();
        }

        double commissions(int t)
        {
            if(duration(t) == 0){
                return premiums(t); } 
            else { return 0; }
        }

        double disc_factors()
        {
            return 0;
        }

        double disc_rate_mth()
        {
            return 0;
        }

        double expense_acq()
        {
            return 300;
        }

        double expense_maint()
        {
            return 60;
        }

        double expenses(int t)
        {
            double maint = pols_if(t) * expense_maint() / 12 * inflation_factor(t);

            if (t == 0) {
                return expense_acq() + maint;} 
            else { return expense_maint(); }              
        }

        double pols_if(int t)
        {
            if(t == 0){return pols_if_init();}
            else if(t > policy_term() * 12) { return 0; }
            else { return pols_if(t - 1) - pols_lapse(t - 1) - pols_death(t - 1) - pols_maturity(t); }       
        }

        double pols_if_init()
        {
            //return ModelPoint.Field<double>("policy_count");
            return 1;
        }


        double inflation_factor(int t)
        {
            return (1 + Pow(inflation_rate(),(t/12)));
        }

        double inflation_rate()
        {
            return 0.01;
        }

        double lapse_rate(int t)
        {
            return Max(0.1 - 0.02 * duration(t), 0.02);
        }

        double loading_prem()
        {
            return 0.05;
        }

        int model_point()
        {
            return ModelPoint.Field<int>("point_id");
        }

        double mort_rate(int t)
        {
            var mortRate = MortTable.AsEnumerable().Where(x => x.Field<double>("Age")== age(t))
                .Select(x => x.Field<double>((Max(Min(5, duration(t)), 0))));

            return (Double)mortRate.FirstOrDefault();
        }

        double mort_rate_mth(int t)
        {
            return Pow(1 - (1 - mort_rate(t)), (1 / 12));
        }

        double net_cf(int t)
        {
            return premiums(t) - claims(t) - expenses(t) - commissions(t);
        }

        double claims(int t)
        {
            return claim_pp(t)*pols_death(t);
        }

        double net_premium_pp()
        {
            return pv_claims() / pv_pols_if();
        }

        int pv_pols_if()
        {
            throw new NotImplementedException();
        }

        int pv_claims()
        {
            throw new NotImplementedException();
        }

        int policy_term()
        {
            return ModelPoint.Field<int>("policy_term");
        }

        double pols_death(int t)
        {
            return pols_if(t) * mort_rate_mth(t);
        }

        double pols_if_init(int t)
        {
            return ModelPoint.Field<double>("policy_count");
        }

        double pols_lapse(int t)
        {
            return (pols_if(t) - pols_death(t)) * Pow(1 - (1 - lapse_rate(t)),(1 / 12));
        }

        double pols_maturity(int t)
        {
            if (t == policy_term() * 12)
            {
                return pols_if(t - 1) - pols_lapse(t - 1) - pols_death(t - 1);
            }
            else { return 0; }
        }

        double premium_pp()
        {
            return Round((1 + loading_prem()) * net_premium_pp(), 2);
        }

        double premiums(int t)
        {
            return premium_pp() * pols_if(t);
        }

        int proj_len(int t)
        {
            return 12 * policy_term() + 1;
        }
        char sex()
        {
            return ModelPoint.Field<char>("sex");
        }

        double sum_assured()
        {
            return ModelPoint.Field<double>("sum_assured");
        }
    }
}