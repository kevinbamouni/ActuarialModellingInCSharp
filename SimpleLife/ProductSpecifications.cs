using System.Data;

namespace SimpleLife
{
    internal class ProductSpecifications
    {
        /// <summary>
        /// Product specification dataTable
        /// </summary>
        public DataTable ProductSpecTable { get; set; }
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="PathProductSpec"></param>
        /// <param name="SchemasProductSpec"></param>
        public ProductSpecifications(string PathProductSpec, string SchemasProductSpec)
        {
            ProductSpecTable = DataFromCsv.ReadDataTableFromCsv(PathProductSpec, SchemasProductSpec);
        }
        /// <summary>
        /// Result of a query on product specification dataTable at the grain : PRODUCT x POLTYPE x GEN
        /// </summary>
        /// <param name="assumption"></param>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns>An assumption from the product specification dataTable at the grain : PRODUCT x POLTYPE x GEN</returns>
        public dynamic? ProductSpecTableQuery(string specification, string product, int policyType, int generation)
        {
            var r = from i in ProductSpecTable.AsEnumerable()
                    where i.Field<string>("Product") == product && i.Field<int>("PolType") == policyType && i.Field<int>("Gen") == generation
                    select i.Field<dynamic>(specification);
            return r.FirstOrDefault();
        }
        /// <summary>
        /// Initial Surrender Charge Rate from the product specification
        /// </summary>
        /// <returns>the init surrender charge from the product specification</returns>
        public decimal InitSurrCharge(string product, int policyType, int generation, int policyTerm)
        {
            decimal param1 = ProductSpecTableQuery("SurrChargeParam1", product, policyType, generation);
            decimal param2 = ProductSpecTableQuery("SurrChargeParam2", product, policyType, generation);
            return param1 + param2 * System.Math.Min(policyTerm / 10, 1);
        }
        /// <summary>
        /// Interest Rate from the product specification
        /// </summary>
        /// <param name="ratebasis"></param>
        /// <returns>Interest rate from the product specification</returns>
        /// <exception cref="Exception">when rate basis is not "PREM" or "VAL"</exception>
        public decimal IntRate(string ratebasis, string product, int policyType, int generation)
        {
            string basis;
            if (ratebasis == "PREM")
            { basis = "IntRatePrem"; }
            else if (ratebasis == "VAL")
            { basis = "IntRateVal"; }
            else
            { throw new Exception("Policy.IntRate(string ratebasis) : param ratebasis value error; must be 'PREM' or 'VAL' "); }
            int result = ProductSpecTableQuery(basis, product, policyType, generation);
            return result;
        }
        /// <summary>
        /// Acquisition Loading per Sum Assured from product specification
        /// </summary>
        /// <returns>Acquisition Loading per Sum Assured from product specification</returns>
        public decimal LoadAcqSA(string product, int policyType, int generation, int policyTerm)
        {
            decimal param1 = ProductSpecTableQuery("LoadAcqSAParam1", product, policyType, generation);
            decimal param2 = ProductSpecTableQuery("LoadAcqSAParam2", product, policyType, generation);
            return param1 + param2 * System.Math.Min(policyTerm / 10, 1);
        }
        /// <summary>
        /// Maintenance Loading per Gross Premium from product specification
        /// </summary>
        /// <returns>Maintenance Loading per Gross Premium from product specification</returns>
        public decimal LoadMaintPrem(string product, int policyType, int generation)
        {
            return ProductSpecTableQuery("LoadMaintPremParam1", product, policyType, generation);
        }
        /// <summary>
        /// Maintenance Loading per Sum Assured during Premium Payment from product specification
        /// </summary>
        /// <returns>Maintenance Loading per Sum Assured during Premium Payment from product specification</returns>
        public decimal LoadMaintSA(string product, int policyType, int generation)
        {
            decimal result = ProductSpecTableQuery("LoadMaintSA", product, policyType, generation);
            return result;
        }
        /// <summary>
        /// Maintenance Loading per Sum Assured after Premium Payment from product specification
        /// </summary>
        /// <returns>Maintenance Loading per Sum Assured after Premium Payment from product specification</returns>
        public decimal LoadMaintSA2(string product, int policyType, int generation)
        {
            decimal result = ProductSpecTableQuery("LoadMaintSA2", product, policyType, generation);
            return result;
        }
        /// <summary>
        /// Mortality Table ID to use for the current model point from product specification
        /// </summary>
        /// <param name="ratebasis"></param>
        /// <returns>Mortality Table ID to use for the current model point from product specification</returns>
        /// <exception cref="Exception"></exception>
        public int TableID(string ratebasis, string product, int policyType, int generation)
        {
            string basis;
            if (ratebasis == "PREM")
            { basis = "MortTablePrem"; }
            else if (ratebasis == "VAL")
            { basis = "MortTableVal"; }
            else
            { throw new Exception("Policy.TableID(string ratebasis) : param ratebasis value error; must be 'PREM' or 'VAL' "); }
            int result = ProductSpecTableQuery(basis, product, policyType, generation);
            return result;
        }
    }
}
