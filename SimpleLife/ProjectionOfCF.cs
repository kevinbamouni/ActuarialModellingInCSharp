
namespace SimpleLife
{
    internal class ProjectionOfCF
    {
        //Policy;Product;PolicyType;Gen;Channel;Duration;Sex;IssueAge;PaymentMode;PremFreq;PolicyTerm;MaxPolicyTerm;PolicyCount;SumAssured
        /// <summary>
        /// Model point Data Object
        /// </summary>
        public Policy ModelPoint { get; set; }
        /// <summary>
        /// Economic Scenario Data Object
        /// </summary>
        public static Economic EconomicScenarios = new Economic(Input.PathScenarios, Input.SchemasScenarios);
        /// <summary>
        /// Economic Scenatio ID
        /// </summary>
        public int EconomicScenarioID = 0;
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="modelPointRow">ModelPoint to project</param>
        /// <param name="economicScenarioID">Economic scenario of the projection</param>
        public ProjectionOfCF(Policy modelPointRow, int economicScenarioID)
        {
            ModelPoint = modelPointRow;
            EconomicScenarioID = economicScenarioID;
        }
        /// <summary>
        /// Accumulated cachflows
        /// </summary>
        /// <see cref="AccumCF"/>
        /// <see cref="IntAccumCF"/>
        /// <see cref="NetInsurCF"/>
        /// <param name="t"></param>
        /// <returns>Accumulated cachflows</returns>
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
        /// <see cref="Policy.IssueAge"/>
        /// <param name="t"></param>
        /// <returns></returns>
        public int AttAge(int t)
        {
            return ModelPoint.IssueAge() + t;
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
        /// <returns>Accidental hospitalisation benefits</returns>
        public decimal BenefitAccHosp(int t)
        {
            return SizeBenefitAccDth(t) * PolsAccDeath(t);
        }
        /// <summary>
        /// Annuity benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Annuity benefits</returns>
        public decimal BenefitAnn(int t)
        {
            return SizeBenefitAnn(t) * PolsAnnuity(t);
        }
        /// <summary>
        /// Death benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Death benefits</returns>
        public decimal BenefitDeath(int t)
        {
            return SizeBenefitDeath(t) * PolsDeath(t);
        }
        /// <summary>
        /// Living benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Living benefits</returns>
        public decimal BenefitLiving(int t)
        {
            return SizeBenefitLiving(t) * PolsLiving(t);
        }
        /// <summary>
        /// Maturity benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Maturity benefits</returns>
        public decimal BenefitMat(int t)
        {
            return SizeBenefitMat(t) * PolsMaturity(t);
        }
        /// <summary>
        /// Other benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Other benefits</returns>
        public decimal BenefitOther(int t)
        {
            return SizeBenefitOther(t) * PolsOther(t);
        }
        /// <summary>
        /// Sickness hospitalization benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Sickness hospitalization benefits</returns>
        public decimal BenefitSickHosp(int t)
        {
            return SizeBenefitSickHosp(t) * PolsSickHosp(t);
        }
        /// <summary>
        /// Surgery benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Surgery benefits</returns>
        public decimal BenefitSurg(int t)
        {
            return SizeBenefitSurg(t) * PolsSurg(t);
        }
        /// <summary>
        /// Surrender benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Surrender benefits</returns>
        public decimal BenefitSurr(int t)
        {
            return SizeBenefitSurr(t) * PolsSurr(t);
        }
        /// <summary>
        /// Benefit Total
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Benefit Total</returns>
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
        /// <returns>Change in reserve</returns>
        public decimal ChangeRsrv(int t)
        {
            return ReserveTotal_End(t + 1) - ReserveTotal_End(t);
        }
        /// <summary>
        /// Acquisition expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Acquisition expenses</returns>
        public decimal ExpsAcq(int t)
        {
            return SizeExpsAcq(t) * (PolsNewBiz(t) + PolsRenewal(t));
        }
        /// <summary>
        /// Commissions and acquisition expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Commissions and acquisition expenses</returns>
        public decimal ExpsAcqTotal(int t)
        {
            return ExpsCommTotal(t) + ExpsAcq(t);
        }
        /// <summary>
        /// Initial expenses commissions
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Initial expenses commissions</returns>
        public decimal ExpsCommInit(int t)
        {
            return SizeExpsCommInit(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Renewal commissions
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Renewal commissions</returns>
        public decimal ExpsCommRen(int t)
        {
            return SizeExpsCommRen(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Commissions Total
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Commissions Total</returns>
        public decimal ExpsCommTotal(int t)
        {
            return ExpsCommInit(t) + ExpsCommRen(t);
        }
        /// <summary>
        /// Maintenance expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Maintenance expenses</returns>
        public decimal ExpsMaint(int t)
        {
            return SizeExpsMaint(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Total maintenance expenses including other expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Total maintenance expenses including other expenses</returns>
        public decimal ExpsMaintTotal(int t)
        {
            return ExpsMaint(t) + ExpsOther(t);
        }
        /// <summary>
        /// Other expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Other expenses</returns>
        public decimal ExpsOther(int t)
        {
            return 0;
        }
        /// <summary>
        /// Total expenses
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Total expenses</returns>
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
        /// <returns>Income Total</returns>
        public decimal IncomeTotal(int t)
        {
            return PremIncome(t) + InvstIncome(t);
        }
        /// <summary>
        /// Insurance in-force: Beginning of period 1
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Insurance in-force: Beginning of period 1</returns>
        public decimal InsurIF_Beg1(int t)
        {
            return PolsIF_Beg1(t) * SizeSumAssured(t);
        }
        /// <summary>
        /// Insurance in-force: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Insurance in-force: End of period</returns>
        public decimal InsurIF_End(int t)
        {
            return PolsIF_End(t) * SizeSumAssured(t);
        }
        /// <summary>
        /// Intrest on accumulated cashflows
        /// Depend on the economic scenarios
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Intrest on accumulated cashflows</returns>
        public decimal IntAccumCF(int t)
        {
            return (AccumCF(t)
            + PremIncome(t)
            - ExpsTotal(t)) * EconomicScenarios.DiscRate(EconomicScenarioID, t);
        }
        /// <summary>
        /// Investment income
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Investment income</returns>
        public decimal InvstIncome(int t)
        {
            return SizeInvstIncome(t) * PolsIF_Beg1(t);
        }
        /// <summary>
        /// Net liability cashflow
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Net liability cashflow</returns>
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
        /// <returns>Number of policies: Accidental death</returns>
        public decimal PolsAccDeath(int t)
        {
            return 0;
        }
        /// <summary>
        /// Number of policies: Accidental Hospitalization
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Accidental Hospitalization</returns>
        public decimal PolsAccHosp(int t) { return 0; }
        /// <summary>
        /// Number of policies: Annuity
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Annuity</returns>
        public decimal PolsAnnuity(int t) { return 0; }
        /// <summary>
        /// Number of policies: Death
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Death</returns>
        public decimal PolsDeath(int t)
        {
            decimal mortFactor = Policy.generalStaticAssumptions.MortFactor(ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen(), t);
            decimal baseMortRate = ModelPoint.BaseMortRate(AttAge(t));
            return PolsIF_Beg1(t) * baseMortRate * mortFactor;
        }
        /// <summary>
        /// Number of policies: Maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Maturity</returns>
        public decimal PolsIF_AftMat(int t)
        {
            return PolsIF_End(t) - PolsMaturity(t);
        }
        /// <summary>
        /// Number of policies: Beginning of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Beginning of period</returns>
        public decimal PolsIF_Beg(int t)
        {
            return PolsIF_AftMat(t);
        }
        /// <summary>
        /// Number of policies: Beginning of period 1
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Beginning of period 1</returns>
        public decimal PolsIF_Beg1(int t) { return PolsIF_Beg(t) + PolsRenewal(t) + PolsNewBiz(t); }
        /// <summary>
        /// Number of policies: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: End of period</returns>
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
        /// <returns>Number of policies: Living benefits</returns>
        public decimal PolsLiving(int t) { return 0; }
        /// <summary>
        /// Number of policies: Maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Maturity</returns>
        public decimal PolsMaturity(int t)
        {
            if (t == ModelPoint.PolicyTerm())
            { return PolsIF_End(t); }
            else
            { return 0; }
        }
        /// <summary>
        /// Number of policies: New business
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: New business</returns>
        public decimal PolsNewBiz(int t)
        {
            if (t == 0)
            {
                return ModelPoint.PolicyCount();
            }
            else { return 0; }
        }
        /// <summary>
        /// Number of policies: Other benefits
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Other benefits wich is 0</returns>
        public decimal PolsOther(int t) { return 0; }
        /// <summary>
        /// Number of policies: Renewal policies
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Number of policies: Renewal policies wich is 0</returns>
        public decimal PolsRenewal(int t) { return 0; }
        /// <summary>
        /// Number of policies: Sickness Hospitalization
        /// </summary>
        /// <param name="t"></param>
        /// <returns>umber of policies: Sickness Hospitalization with is 0</returns>
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
        /// <returns>Number of policies: Surrender</returns>
        public decimal PolsSurr(int t)
        {
            return PolsIF_Beg1(t) * Policy.generalStaticAssumptions.SurrRate(ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen(), t);
        }
        /// <summary>
        /// Premium income
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Premium income</returns>
        public decimal PremIncome(int t) { return SizePremium(t) * PolsIF_Beg1(t); }
        /// <summary>
        /// Profit before Tax
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Profit before Tax</returns>
        public decimal ProfitBefTax(int t)
        {
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
        /// <returns>Hospitalization reserve: End of period</returns>
        public decimal ReserveHospRsrvEnd(int t)
        {
            return 0;
        }
        /// <summary>
        /// Premium reserve: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Premium reserve: End of period</returns>
        public decimal ReservePremRsrvEnd(int t) { return SizeReservePremRsrvEnd(t) * PolsIF_End(t); }
        /// <summary>
        /// Total reserve: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Total reserve: End of period</returns>
        public decimal ReserveTotal_End(int t)
        {
            return (ReservePremRsrvEnd(t)
                    + ReserveUernPremEnd(t)
                    + ReserveHospRsrvEnd(t));
        }
        /// <summary>
        /// Unearned Premium: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Unearned Premium: End of period</returns>
        public decimal ReserveUernPremEnd(int t)
        {
            return 0;
        }
        /// <summary>
        /// Annualized premium per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Annualized premium per policy at time t</returns>
        public decimal SizeAnnPrem(int t)
        {
            return SizeSumAssured(t) * ModelPoint.AnnPremRate();
        }
        /// <summary>
        /// Accidental death benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Accidental death benefit per policy</returns>
        public decimal SizeBenefitAccDth(int t) { return 0; }
        /// <summary>
        /// Accidental hospitalization benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Accidental hospitalization benefit per policy</returns>
        public decimal SizeBenefitAccHosp(int t) { return 0; }
        /// <summary>
        /// Annuity benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Annuity benefit per policy</returns>
        public decimal SizeBenefitAnn(int t) { return 0; }
        /// <summary>
        /// Death benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Death benefit per policy</returns>
        public decimal SizeBenefitDeath(int t) { return SizeSumAssured(t); }
        /// <summary>
        /// Living benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Living benefit per policy</returns>
        public decimal SizeBenefitLiving(int t) { return 0; }
        /// <summary>
        /// Maturity benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Maturity benefit per policy</returns>
        public decimal SizeBenefitMat(int t) { return 0; }
        /// <summary>
        /// Other benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Other benefit per policy</returns>
        public decimal SizeBenefitOther(int t) { return 0; }
        /// <summary>
        /// Sickness hospitalization benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Sickness hospitalization benefit per policy</returns>
        public decimal SizeBenefitSickHosp(int t) { return 0; }
        /// <summary>
        /// Surgery benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Surgery benefit per policy</returns>
        public decimal SizeBenefitSurg(int t) { return 0; }
        /// <summary>
        /// Surrender benefit per policy
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Surrender benefit per policy</returns>
        public decimal SizeBenefitSurr(int t)
        {
            return SizeSumAssured(t) * (ModelPoint.CashValueRate(t)
                    + ModelPoint.CashValueRate(t + 1)) / 2;
        }
        /// <summary>
        /// Acquisition expense per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Acquisition expense per policy at time t</returns>
        public decimal SizeExpsAcq(int t)
        {
            decimal inflFactorT = Policy.generalStaticAssumptions.InflFactor(t, ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            decimal inflFactor0 = Policy.generalStaticAssumptions.InflFactor(0, ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            decimal expsAcqAnnPrem = Policy.generalStaticAssumptions.ExpsAcqAnnPrem(ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            decimal expsAcqSA = Policy.generalStaticAssumptions.ExpsAcqSA(ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            decimal expsAcqPol = Policy.generalStaticAssumptions.ExpsAcqPol(ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            if (t == 0)
            {
                return (SizeAnnPrem(t) * expsAcqAnnPrem + (SizeSumAssured(t) * expsAcqSA + expsAcqPol) * inflFactorT / inflFactor0);
            }
            else { return 0; }
        }
        /// <summary>
        /// Initial commission per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Initial commission per policy at time t</returns>
        public decimal SizeExpsCommInit(int t)
        {
            decimal commInitPrem = Policy.generalStaticAssumptions.CommInitPrem(ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen());
            decimal cnspTax = Policy.generalStaticAssumptions.CnsmpTax(ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen());
            if (t == 0)
            { return SizePremium(t) * commInitPrem * (1 + cnspTax); }
            else { return 0; }
        }
        /// <summary>
        /// Renewal commission per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Renewal commission per policy at time t</returns>
        public decimal SizeExpsCommRen(int t)
        {
            decimal CommRenTerm = Policy.generalStaticAssumptions.CommRenTerm(ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            decimal cnsmpTax = Policy.generalStaticAssumptions.CnsmpTax(ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            if (t == 0)
            { return 0; }
            else if (t < CommRenTerm)
            { return SizePremium(t) * CommRenTerm * (1 + cnsmpTax); }
            else { return 0; }
        }
        /// <summary>
        /// Maintenance expense per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Maintenance expense per policy at time t</returns>
        public decimal SizeExpsMaint(int t)
        {
            decimal expsMaintAnnPrem = Policy.generalStaticAssumptions.ExpsMaintAnnPrem(ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen());
            decimal expsMaintSA = Policy.generalStaticAssumptions.ExpsMaintSA(ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen());
            decimal expsMaintPol = Policy.generalStaticAssumptions.ExpsMaintPol(ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen());
            decimal inflFactor = Policy.generalStaticAssumptions.InflFactor(t, ModelPoint.Product(),
                ModelPoint.PolicyType(),
                ModelPoint.Gen());
            return (SizeAnnPrem(t) * expsMaintAnnPrem + (SizeSumAssured(t) * expsMaintSA + expsMaintPol) * inflFactor);
        }
        /// <summary>
        /// Other expenses per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Other expenses per policy at time t</returns>
        public decimal SizeExpsOther(int t) { return 0; }
        /// <summary>
        /// Investment Income per policy from t to t+1.
        /// Depend on the economic scenarios
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Investment Income per policy from t to t+1.</returns>
        public decimal SizeInvstIncome(int t)
        {
            return (SizeReserveTotalAftMat(t) + SizePremium(t)) * EconomicScenarios.InvstRetRate(EconomicScenarioID, t);
        }
        /// <summary>
        /// Premium income per policy from t to t+1
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Premium income per policy from t to t+1</returns>
        public decimal SizePremium(int t)
        { return SizeSumAssured(t) * ModelPoint.GrossPremRate() * ModelPoint.PremFreq(); }
        /// <summary>
        /// Premium reserve per policy: After maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Premium reserve per policy: After maturity</returns>
        public decimal SizeReservePremRsrvAftMat(int t)
        {
            return SizeSumAssured(t) * ModelPoint.ReserveNLP_Rate("VAL", t);
        }
        /// <summary>
        /// Premium reserve per policy: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Premium reserve per policy: End of period</returns>
        public decimal SizeReservePremRsrvEnd(int t)
        {
            return SizeSumAssured(t) * ModelPoint.ReserveNLP_Rate("VAL", t);
        }
        /// <summary>
        /// Total reserve per policy: After maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Total reserve per policy: After maturity</returns>
        public decimal SizeReserveTotalAftMat(int t)
        {
            return (SizeReservePremRsrvAftMat(t) + SizeReserveUernPremAftMat(t));
        }
        /// <summary>
        /// Unearned premium: After maturity
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Unearned premium: After maturity</returns>
        public decimal SizeReserveUernPremAftMat(int t)
        {
            return 0;//SizeSumAssured(t) * polset.UnernPremRate(polset, tt, True)
        }
        /// <summary>
        /// Unearned reserve per policy: End of period
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Unearned reserve per policy: End of period</returns>
        public decimal SizeReserveUernPremEnd(int t)
        {
            return 0;//SizeSumAssured(t) * ModelPoint.UnernPremRate(polset, tt)
        }
        /// <summary>
        /// Sum assured per policy at time t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Sum assured per policy at time t</returns>
        public decimal SizeSumAssured(int t)
        {
            return (decimal)ModelPoint.SumAssured();
        }
        /// <summary>
        /// ultimate projection time.
        /// </summary>
        /// <returns>ultimate projection time.</returns>
        public decimal last_t()
        {
            int tid = Policy.generalStaticAssumptions.BaseMort(ModelPoint.Product(), ModelPoint.PolicyType(), ModelPoint.Gen());
            int lastage = Policy.mortalityTable.LastAge(ModelPoint.Sex(), tid);
            return Math.Min(lastage - ModelPoint.IssueAge(), ModelPoint.PolicyTerm());
        }
    }
}
