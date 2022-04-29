using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLife
{
    //public class PresentValuesOfCF : ProjectionOfCF
    //{
    //    /// <summary>
    //    /// Interest accreted on pv of net cashflows
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal InterestNetCF(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else {return (PV_NetCashflow(t) - PremIncome(t) + ExpsTotal(t)) * DiscRate(t);}
    //    }

    //    /// <summary>
    //    /// Present value of death benefits
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_BenefitDeath(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else { return (-BenefitDeath(t) + PV_BenefitDeath(t + 1)) / (1 + DiscRate(t)); }
    //    }

    //    /// <summary>
    //    /// Present value of matuirty benefits
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_BenefitMat(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else { return (-BenefitMat(t) + PV_BenefitMat(t + 1)) / (1 + DiscRate(t)); }
    //    }

    //    /// <summary>
    //    /// Present value of surrender benefits
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_BenefitSurr(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else { return (-BenefitSurr(t) + PV_BenefitSurr(t + 1)) / (1 + DiscRate(t)); }
    //    }

    //    /// <summary>
    //    /// Present value of total benefits
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_BenefitTotal(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else { return (-BenefitTotal(t) + PV_BenefitTotal(t + 1)) / (1 + DiscRate(t)); }
    //    }

    //    /// <summary>
    //    /// Present value of acquisition expenses
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_Check(int t)
    //    {
    //        return PV_NetCashflow(t) - PV_NetCashflowForCheck(t);
    //    }

    //    public decimal PV_ExpsAcq(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else {return -ExpsAcq(t) + PV_ExpsAcq(t + 1) / (1 + DiscRate(t));}
    //    }

    //    /// <summary>
    //    /// Present value of commission expenses
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_ExpsCommTotal(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else { return -ExpsCommTotal(t) + PV_ExpsCommTotal(t + 1) / (1 + DiscRate(t));}
    //    }

    //    /// <summary>
    //    /// Present value of maintenance expenses
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_ExpsMaint(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else{return -ExpsMaint(t) + PV_ExpsMaint(t + 1) / (1 + DiscRate(t));}
    //    }

    //    /// <summary>
    //    /// Present value of total expenses
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_ExpsTotal(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else{return -ExpsTotal(t) + PV_ExpsTotal(t + 1) / (1 + DiscRate(t));}
    //    }

    //    /// <summary>
    //    /// Present value of net cashflow
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_NetCashflow(int t)
    //    {
    //        return (PV_PremIncome(t) + PV_ExpsTotal(t) + PV_BenefitTotal(t));
    //    }

    //    /// <summary>
    //    /// Present value of net cashflow
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_NetCashflowForCheck(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else{return (PremIncome(t) - ExpsTotal(t) - BenefitTotal(t) / (1 + DiscRate(t)) + PV_NetCashflow(t + 1) / (1 + DiscRate(t)));
    //        }
    //    }

    //    /// <summary>
    //    /// Present value of premium income
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_PremIncome(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else{
    //            return PremIncome(t) + PV_PremIncome(t + 1) / (1 + DiscRate(t));
    //        }
    //    }

    //    /// <summary>
    //    /// Present value of insurance in-force
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    public decimal PV_SumInsurIF(int t)
    //    {
    //        if (t > last_t)
    //        {
    //            return 0;
    //        }
    //        else { return InsurIF_Beg1(t) + PV_SumInsurIF(t + 1) / (1 + DiscRate(t));}
    //    }
    //}
}
