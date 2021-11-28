using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BasicTermS
{
    class Projection
    {
        int age(int t)
        {
            return age_at_entry() + duration(t);
        }

        int duration(int t)
        {
            throw new NotImplementedException();
        }

        int age_at_entry()
        {
            throw new NotImplementedException();
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
                return expense_acq() + maint; } 
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
            throw new NotImplementedException();
        }

        double mort_rate(int t)
        {
            throw new NotImplementedException();
        }

        double mort_rate_mth(int t)
        {
            return Pow(1 - (1 - mort_rate(t)), (1 / 12));
        }

        double net_cf(int t)
        {
            return premiums(t) - claims(t) - expenses(t) - commissions(t);
        }

        private double claims(int t)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        double pols_death(int t)
        {
            return pols_if(t) * mort_rate_mth(t);
        }

        int pols_if_init(int t)
        {
            return 1;
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
        int sex()
        {
            throw new NotImplementedException();
        }

        int sum_assured()
        {
            throw new NotImplementedException();
        }
    }

}
