using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace SimpleLife
{
    public class GeneralStaticAssumptions
    {
        public DataTable AssumtionParameterTable { get; set; }
        public DataTable AssumtionTable { get; set; }
        public GeneralStaticAssumptions(string paramAssumpPath, string paramAssumpSchema, string assumpPath, string assumpSchema)
        {
            AssumtionParameterTable = DataFromCsv.ReadDataTableFromCsv(paramAssumpPath, paramAssumpSchema);
            AssumtionTable = DataFromCsv.ReadDataTableFromCsv(assumpPath, assumpSchema);
        }
        /// <summary>
        /// Permet de lancer une requete sur la table Assumption
        /// </summary>
        /// <param name="assumption"></param>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public dynamic? AssumtionParameterTableQuery(string assumption, string product, int policyType, int generation)
        {
            var r = from i in AssumtionParameterTable.AsEnumerable()
                    where i.Field<string>("Product")==product && i.Field<int>("PolType")== policyType && i.Field<int>("Gen")==generation
                    select i.Field<dynamic>(assumption);
            return r.FirstOrDefault();
        }
        ///// <summary>
        ///// Get the ID of the mortality table to use
        ///// </summary>
        ///// <param name="product"></param>
        ///// <param name="productType"></param>
        ///// <param name="generation"></param>
        ///// <returns></returns>
        //public int MortalityTableID(string product, int policyType, int generation)
        //{
        //    return (int)AssumtionParameterTableQuery("BaseMort", product, policyType, generation);
        //}
        /// <summary>
        /// Consumption tax rate
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CnsmpTax(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CnsmpTax", product, policyType, generation);
        }
        /// <summary>
        /// Initial commission per premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CommInitPrem(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CommInitPrem", product, policyType, generation);
        }
        /// <summary>
        /// Renewal commission per premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CommRenPrem(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CommRenPrem", product, policyType, generation);
        }
        /// <summary>
        /// Renewal commission term
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CommRenTerm(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CommRenTerm", product, policyType, generation);
        }
        /// <summary>
        /// Acquisition expense per annualized premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsAcqAnnPrem(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsAcqAnnPrem", product, policyType, generation);
        }
        /// <summary>
        /// Acquisition expense per policy
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsAcqPol(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsAcqPol", product, policyType, generation);
        }
        /// <summary>
        /// Acquisition expense per sum assured
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsAcqSA(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsAcqSA", product, policyType, generation);
        }
        /// <summary>
        /// Maintenance expense per annualized premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsMaintAnnPrem(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsMaintAnnPrem", product, policyType, generation);
        }
        /// <summary>
        /// Maintenance expense per policy
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsMaintPol(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsMaintPol", product, policyType, generation);
        }
        /// <summary>
        /// Maintenance expense per sum assured
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsMaintSA(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsMaintSA", product, policyType, generation);
        }
        /// <summary>
        /// Inflation rate
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal InflRate(string product, int policyType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("InflRate", product, policyType, generation);
        }
        ///// <summary>
        ///// Age at which mortality becomes 1
        ///// </summary>
        ///// <param name="product"></param>
        ///// <param name="productType"></param>
        ///// <param name="generation"></param>
        ///// <returns></returns>
        //public decimal LastAge(string product, int policyType, int generation)
        //{
        //    return (decimal)AssumtionParameterTableQuery("LastAge", product, policyType, generation);
        //}
        /// <summary>
        /// Mortality factor : 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal MortFactor(string product, int policyType, int generation, int year)
        {
            string column = (string)AssumtionParameterTableQuery("MortFactor", product, policyType, generation);
            var r = from i in AssumtionTable.AsEnumerable()
                    where i.Field<int>("Year")== year
                    select i.Field<decimal>(column);
            return (decimal)column.FirstOrDefault();
        }
        ///// <summary>
        ///// Mortality Table
        ///// </summary>
        ///// <param name="product"></param>
        ///// <param name="productType"></param>
        ///// <param name="generation"></param>
        ///// <returns></returns>
        //public decimal MortTable(string product, int policyType, int generation)
        //{
        //    return (decimal)AssumtionParameterTableQuery("BaseMort", product, policyType, generation);
        //}
        /// <summary>
        /// Surrender Rate
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="productType">product type</param>
        /// <param name="generation">generation</param>
        /// /// <param name="year">past duration</param>
        /// <returns>return the name of the surrender table to use</returns>
        public decimal SurrRate(string product, int policyType, int generation, int year)
        {
            
            string? column = AssumtionParameterTableQuery("Surrender", product, policyType, generation);
            if (column is not null)
            {
                var r = from i in AssumtionTable.AsEnumerable()
                        where i.Field<int>("Year") == year
                        select i.Field<decimal>(column);
                return (decimal)r.FirstOrDefault();
            }
            else
            { throw new Exception("Missing Value in Table AssumtionParameterTable : Column Surrender!"); }
        }
        /// <summary>
        /// Get the Mortality table ID to use for a policy
        /// </summary>
        /// <param name="product"></param>
        /// <param name="policyType"></param>
        /// <param name="generation"></param>
        /// <returns>return the Mortality table id to use.</returns>
        public int BaseMort(string product, int policyType, int generation)
        {
            return (int)AssumtionParameterTableQuery("BaseMort", product, policyType, generation);
        }
        /// <summary>
        /// Get the inflation factor
        /// </summary>
        /// <param name="t"></param>
        /// <param name="product"></param>
        /// <param name="policyType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal InflFactor(int t, string product, int policyType, int generation)
        {
            if (t == 0)
            {
                return 1;
            }
            else
            {
                return InflFactor(t - 1, product, policyType, generation) / (1 + InflRate(product, policyType, generation));
                //return InflFactor(t - 1) / (1 + 0);
            }
        }
    }
}
