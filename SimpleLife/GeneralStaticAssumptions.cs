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
        public dynamic AssumtionParameterTableQuery(string assumption, string product, string productType, int generation)
        {
            var r = from i in AssumtionParameterTable.AsEnumerable()
                    where i.Field<string>("Product")==product && i.Field<string>("PolType")==productType && i.Field<int>("Gen")==generation
                    select i.Field<dynamic>(assumption);
            return r.FirstOrDefault();
        }
        /// <summary>
        /// Get the ID of the mortality table to use
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public int MortalityTableID(string product, string productType, int generation)
        {
            return (int)AssumtionParameterTableQuery("BaseMort", product, productType, generation);
        }
        /// <summary>
        /// Consumption tax rate
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CnsmpTax(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CnsmpTax", product, productType, generation);
        }
        /// <summary>
        /// Initial commission per premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CommInitPrem(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CommInitPrem", product, productType, generation);
        }
        /// <summary>
        /// Renewal commission per premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CommRenPrem(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CommRenPrem", product, productType, generation);
        }
        /// <summary>
        /// Renewal commission term
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal CommRenTerm(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("CommRenTerm", product, productType, generation);
        }
        /// <summary>
        /// Acquisition expense per annualized premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsAcqAnnPrem(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsAcqAnnPrem", product, productType, generation);
        }
        /// <summary>
        /// Acquisition expense per policy
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsAcqPol(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsAcqPol", product, productType, generation);
        }
        /// <summary>
        /// Acquisition expense per sum assured
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsAcqSA(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsAcqSA", product, productType, generation);
        }
        /// <summary>
        /// Maintenance expense per annualized premium
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsMaintAnnPrem(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsMaintAnnPrem", product, productType, generation);
        }
        /// <summary>
        /// Maintenance expense per policy
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsMaintPol(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsMaintPol", product, productType, generation);
        }
        /// <summary>
        /// Maintenance expense per sum assured
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal ExpsMaintSA(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("ExpsMaintSA", product, productType, generation);
        }
        /// <summary>
        /// Inflation rate
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal InflRate(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("InflRate", product, productType, generation);
        }
        /// <summary>
        /// Age at which mortality becomes 1
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal LastAge(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("LastAge", product, productType, generation);
        }
        /// <summary>
        /// Mortality factor : 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal MortFactor(string product, string productType, int generation, int year)
        {
            string column = (string)AssumtionParameterTableQuery("MortFactor", product, productType, generation);
            var r = from i in AssumtionTable.AsEnumerable()
                    where i.Field<int>("Year")== year
                    select i.Field<decimal>(column);
            return (decimal)column.FirstOrDefault();
        }
        /// <summary>
        /// Mortality Table
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal MortTable(string product, string productType, int generation)
        {
            return (decimal)AssumtionParameterTableQuery("MortTable", product, productType, generation);
        }
        /// <summary>
        /// Surrender Rate
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productType"></param>
        /// <param name="generation"></param>
        /// <returns></returns>
        public decimal SurrRate(string product, string productType, int generation, int year)
        {
            string column = (string)AssumtionParameterTableQuery("Surrender", product, productType, generation);
            var r = from i in AssumtionTable.AsEnumerable()
                    where i.Field<int>("Year") == year
                    select i.Field<decimal>(column);
            return (decimal)column.FirstOrDefault();
        }

        public int BaseMort(string product, string productType, int generation)
        {
            return (int)AssumtionParameterTableQuery("BaseMort", product, productType, generation);
        }
    }
}
