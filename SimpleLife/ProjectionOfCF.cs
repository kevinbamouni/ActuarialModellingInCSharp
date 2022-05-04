using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SimpleLife
{
    internal class ProjectionOfCF
    {
        //Policy;Product;PolicyType;Gen;Channel;Duration;Sex;IssueAge;PaymentMode;PremFreq;PolicyTerm;MaxPolicyTerm;PolicyCount;SumAssured
        public DataRow ModelPoint { get; set; }
        public static GeneralStaticAssumptions generalStaticAssumptions = new GeneralStaticAssumptions(Input.PathAssumptions,
            Input.SchemasAssumptions,
            Input.PathParamAssumptions,
            Input.SchemasParamAssumptions);

        public ProjectionOfCF(DataRow modelPointRow)
        {
            ModelPoint = modelPointRow;
        }

        /// <summary>
        /// Accumulated cachflows
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal AccumCF(int t)
        {
            if (t == 0)
            {
                return 0;
            }
            else { return AccumCF(t - 1) + IntAccumCF(t - 1) + NetInsurCF(t - 1); }
        }
        /// <summary>
        /// Attainded age at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal AttAge(int t)
        {
            return (decimal)ModelPoint["IssueAge"] + t;
        }
        /// <summary>
        /// Accidental death benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitAccDth(int t)
        {
            return SizeBenefitAccDth(t) * PolsAccDeath(t);
        }
        /// <summary>
        /// Accidental hospitalisation benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitAccHosp(int t)
        {
            return SizeBenefitAccDth(t) * PolsAccDeath(t);
        }
        /// <summary>
        /// Annuity benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitAnn(int t)
        {
            return SizeBenefitAnn(t) * PolsAnnuity(t);
        }
        /// <summary>
        /// Death benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitDeath(int t)
        {
            return SizeBenefitDeath(t) * PolsDeath(t);
        }
        /// <summary>
        /// Living benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitLiving(int t)
        {
            return SizeBenefitLiving(t) * PolsLiving(t);
        }
        /// <summary>
        /// Maturity benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitMat(int t)
        {
            return SizeBenefitMat(t) * PolsMaturity(t);
        }
        /// <summary>
        /// Other benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitOther(int t)
        {
            return SizeBenefitOther(t) * PolsOther(t);
        }
        /// <summary>
        /// Sickness hospitalization benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitSickHosp(int t)
        {
            return SizeBenefitSickHosp(t) * PolsSickHosp(t);
        }
        /// <summary>
        /// Surgery benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitSurg(int t)
        {
            return SizeBenefitSurg(t) * PolsSurg(t);
        }
        /// <summary>
        /// Surrender benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitSurr(int t)
        {
            return SizeBenefitSurr(t) * PolsSurr(t);
        }
        /// <summary>
        /// Benefit Total
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal BenefitTotal(int t)
        {
            return (BenefitMat(t)
            + BenefitDeath(t)
            + BenefitAccDth(t)
            + BenefitSurr(t)
            + BenefitAnn(t)
            + BenefitAccHosp(t)
            + BenefitSickHosp(t)
            + BenefitSurg(t)
            + BenefitLiving(t)
            + BenefitOther(t));
        }
        /// <summary>
        /// Change in reserve
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ChangeRsrv(int t)
        {
            return ReserveTotal_End(t + 1) - ReserveTotal_End(t);
        }
        /// <summary>
        /// Acquisition expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsAcq(int t)
        {
            return SizeExpsAcq(t) * (PolsNewBiz(t) + PolsRenewal(t));
        }
        /// <summary>
        /// Commissions and acquisition expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsAcqTotal(int t)
        {
            return ExpsCommTotal(t) + ExpsAcq(t);
        }
        /// <summary>
        /// Initial expenses commissions
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsCommInit(int t)
        {
            return SizeExpsCommInit(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Renewal commissions
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsCommRen(int t)
        {
            return SizeExpsCommRen(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Commissions Total
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsCommTotal(int t)
        {
            return ExpsCommInit(t) + ExpsCommRen(t);
        }
        /// <summary>
        /// Maintenance expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsMaint(int t)
        {
            return SizeExpsMaint(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Total maintenance expenses including other expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsMaintTotal(int t)
        {
            return ExpsMaint(t) + ExpsOther(t);
        }
        /// <summary>
        /// Other expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsOther(int t)
        {
            return 0;
        }
        /// <summary>
        /// Total expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ExpsTotal(int t)
        {
            return (ExpsCommInit(t)
            + ExpsCommRen(t)
            + ExpsAcq(t)
            + ExpsMaint(t)
            + ExpsOther(t));
        }
        /// <summary>
        /// Income Total
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal IncomeTotal(int t)
        {
            return PremIncome(t) + InvstIncome(t);
        }
        /// <summary>
        /// Insurance in-force: Beginning of period 1
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal InsurIF_Beg1(int t)
        {
            return PolsIF_Beg1(t) * SizeSumAssured(t);
        }
        /// <summary>
        /// Insurance in-force: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal InsurIF_End(int t)
        {
            return PolsIF_End(t) * SizeSumAssured(t);
        }
        /// <summary>
        /// Intrest on accumulated cashflows
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal IntAccumCF(int t)
        {
            return (AccumCF(t)
            + PremIncome(t)
            - ExpsTotal(t)) * DiscRate(t);
        }
        /// <summary>
        /// Investment income
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal InvstIncome(int t)
        {
            return SizeInvstIncome(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Net liability cashflow
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal NetInsurCF(int t)
        {
            return (PremIncome(t)
            - BenefitTotal(t)
            - ExpsTotal(t));
        }
        /// <summary>
        /// Number of policies: Accidental death
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsAccDeath(int t)
        {
            return 0;
        }
        /// <summary>
        /// Number of policies: Accidental Hospitalization
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsAccHosp(int t) { return 0; }
        /// <summary>
        /// Number of policies: Annuity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsAnnuity(int t) { return 0; }
        /// <summary>
        /// Number of policies: Death
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsDeath(int t) { return PolsIF_Beg1(t) * asmp.BaseMortRate(AttAge(t)) * asmp.MortFactor(t); }
        /// <summary>
        /// Number of policies: Maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsIF_AftMat(int t) { return PolsIF_End(t) - PolsMaturity(t); }
        /// <summary>
        /// Number of policies: Beginning of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsIF_Beg(int t) { return PolsIF_AftMat(t); }
        /// <summary>
        /// Number of policies: Beginning of period 1
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsIF_Beg1(int t) { return PolsIF_Beg(t) + PolsRenewal(t) + PolsNewBiz(t); }
        /// <summary>
        /// Number of policies: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsIF_End(int t)
        {
            if (t == 0)
            {
                return 0;
            } // pol.PolicyCount
            else
            {
                return PolsIF_Beg1(t - 1) - PolsDeath(t - 1) - PolsSurr(t - 1);
            }
        }
        /// <summary>
        /// Number of policies: Living benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsLiving(int t) { return 0; }
        /// <summary>
        /// Number of policies: Maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsMaturity(int t)
        {
            if (t == (int)ModelPoint["PolicyTerm"])
            { return PolsIF_End(t); }
            else
            { return 0; }
        }
        /// <summary>
        /// Number of policies: New business
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int PolsNewBiz(int t)
        { 
            if (t == 0) 
            { 
                return (int)ModelPoint["PolicyCount"]; 
            } else { return 0; } 
        }
        /// <summary>
        /// Number of policies: Other benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsOther(int t) { return 0; }
        /// <summary>
        /// Number of policies: Renewal policies
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsRenewal(int t) { return 0; }
        /// <summary>
        /// Number of policies: Sickness Hospitalization
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsSickHosp(int t) { return 0; }
        /// <summary>
        /// Number of policies: Surgery
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsSurg(int t) { return 0; }
        /// <summary>
        /// Number of policies: Surrender
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PolsSurr(int t) { return PolsIF_Beg1(t) * asmp.SurrRate(t); }
        /// <summary>
        /// Premium income
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal PremIncome(int t) { return SizePremium(t) * PolsIF_Beg1(t); }
        /// <summary>
        /// Profit before Tax
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ProfitBefTax(int t) {
            return (PremIncome(t)
                    + InvstIncome(t)
                    - BenefitTotal(t)
                    - ExpsTotal(t)
                    - ChangeRsrv(t));
        }
        /// <summary>
        /// Hospitalization reserve: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ReserveHospRsrvEnd(int t)
        {
            return 0;
        }
        /// <summary>
        /// Premium reserve: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ReservePremRsrvEnd(int t) { return SizeReservePremRsrvEnd(t) * PolsIF_End(t); }
        /// <summary>
        /// Total reserve: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ReserveTotal_End(int t) {
            return (ReservePremRsrvEnd(t)
                    + ReserveUernPremEnd(t)
                    + ReserveHospRsrvEnd(t));
        }
        /// <summary>
        /// Unearned Premium: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal ReserveUernPremEnd(int t)
        {
            return 0;
        }
        /// <summary>
        /// Annualized premium per policy at time ``t``
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeAnnPrem(int t) {
            return SizeSumAssured(t) * pol.AnnPremRate;
        }
        /// <summary>
        /// Accidental death benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitAccDth(int t) { return 0; }
        /// <summary>
        /// Accidental hospitalization benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitAccHosp(int t) { return 0; }
        /// <summary>
        /// Annuity benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitAnn(int t) { return 0; }
        /// <summary>
        /// Death benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitDeath(int t) { return SizeSumAssured(t); }
        /// <summary>
        /// Living benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitLiving(int t) { return 0; }
        /// <summary>
        /// Maturity benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitMat(int t) { return 0; }
        /// <summary>
        /// Other benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitOther(int t) { return 0; }
        /// <summary>
        /// Sickness hospitalization benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitSickHosp(int t) { return 0; }
        /// <summary>
        /// Surgery benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitSurg(int t) { return 0; }
        /// <summary>
        /// Surrender benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeBenefitSurr(int t) {
            return SizeSumAssured(t) * (pol.CashValueRate(t)
                    + pol.CashValueRate(t + 1)) / 2;
        }
        /// <summary>
        /// Acquisition expense per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeExpsAcq(int t)
        {
            if (t == 0)
            {
                return (SizeAnnPrem(t) * GeneralStaticAssumptions.ExpsAcqAnnPrem
                  + (SizeSumAssured(t) * GeneralStaticAssumptions.ExpsAcqSA + GeneralStaticAssumptions.ExpsAcqPol)
                  * InflFactor(t) / InflFactor(0));
            }
            else { return 0; }
        }
        /// <summary>
        /// Initial commission per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeExpsCommInit(int t)
        {
            if (t == 0)
            { return SizePremium(t) * generalStaticAssumptions.CommInitPrem() * (1 + generalStaticAssumptions.CnsmpTax(); }
            else { return 0; }
        }
        /// <summary>
        /// Renewal commission per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeExpsCommRen(int t)
        {
            if (t == 0)
            { return 0; }
            else if (t < generalStaticAssumptions.CommRenTerm()
            { return SizePremium(t) * generalStaticAssumptions.CommRenPrem() * (1 + generalStaticAssumptions.CnsmpTax(); }
            else { return 0; }
        }
        /// <summary>
        /// Maintenance expense per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeExpsMaint(int t)
        {
            return (SizeAnnPrem(t) 
                * generalStaticAssumptions.ExpsMaintAnnPrem() 
                + (SizeSumAssured(t) 
                * generalStaticAssumptions.ExpsMaintSA() 
                + generalStaticAssumptions.ExpsMaintPol()) 
                * InflFactor(t));
        }
        /// <summary>
        /// Other expenses per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeExpsOther(int t) { return 0; }
        /// <summary>
        /// Investment Income per policy from t to t+1
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeInvstIncome(int t)
        {
            return (SizeReserveTotalAftMat(t) + SizePremium(t)) * InvstRetRate(t);
        }
        /// <summary>
        /// Premium income per policy from t to t+1
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizePremium(int t)
        { return SizeSumAssured(t) * ModelPoint["GrossPremRate"] * ModelPoint["PremFreq"]; }
        /// <summary>
        /// Premium reserve per policy: After maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeReservePremRsrvAftMat(int t)
        {
            return SizeSumAssured(t) * pol.ReserveNLP_Rate('VAL', t);
        }
        /// <summary>
        /// Premium reserve per policy: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeReservePremRsrvEnd(int t)
        { 
            return SizeSumAssured(t) * pol.ReserveNLP_Rate('VAL', t); 
        }
        /// <summary>
        /// Total reserve per policy: After maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeReserveTotalAftMat(int t)
        {
            return (SizeReservePremRsrvAftMat(t) + SizeReserveUernPremAftMat(t));
        }
        /// <summary>
        /// Unearned premium: After maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeReserveUernPremAftMat(int t)
        {
            return 0; // SizeSumAssured(t) * polset.UnernPremRate(polset, tt, True)
        }
        /// <summary>
        /// Unearned reserve per policy: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeReserveUernPremEnd(int t)
        {
            return 0; // SizeSumAssured(t) * pol.UnernPremRate(polset, tt)
        }
        /// <summary>
        /// Sum assured per policy at time ``t``
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal SizeSumAssured(int t)
        {
            return (decimal)ModelPoint["SumAssured"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal last_t()
        {
            return Math.Min(generalStaticAssumptions.LastAge() - ModelPoint["IssueAge"], ModelPoint["PolicyTerm"]);
        }
    }
}
