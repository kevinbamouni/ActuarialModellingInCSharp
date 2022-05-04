using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLife
{
    public static class Input
    {   /////////////////////////////////////////////////////////////ok
        public static string PathAssumptions = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\mort_lapse_assumption.csv";
        public static string SchemasAssumptions = @"{""Year"":""int"",
                                                        ""MortAsmp1"":""decimal"",
                                                        ""MortAsmp2"":""decimal"",
                                                        ""Morb1"":""decimal"",
                                                        ""Morb2"":""decimal"",
                                                        ""Morb3"":""decimal"",
                                                        ""Morb4"":""decimal"",
                                                        ""Morb5"":""decimal"",
                                                        ""LapseRate1"":""decimal""}";
        /////////////////////////////////////////////////////////////ok
        public static string PathParamAssumptions = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\assumption.csv";
        public static string SchemasParamAssumptions = @"{""Product"": ""string"",
                                                            ""PolType"": ""string"",
                                                            ""Gen"": ""int"",
                                                            ""BaseMort"": ""int"",
                                                            ""MortFactor"": ""string"",
                                                            ""Surrender"": ""string"",
                                                            ""Renewal"": ""decimal"",
                                                            ""InvstRet"": ""decimal"",
                                                            ""CommInitPrem"": ""decimal"",
                                                            ""CommRenPrem"": ""decimal"",
                                                            ""CommRenTerm"": ""decimal"",
                                                            ""ExpsAcqSA"": ""decimal"",
                                                            ""ExpsAcqAnnPrem"": ""decimal"",
                                                            ""ExpsAcqPol"": ""decimal"",
                                                            ""ExpsMaintSA"": ""decimal"",
                                                            ""ExpsMaintPrem"": ""decimal"",
                                                            ""ExpsMaintPol"": ""decimal"",
                                                            ""InflRate"": ""decimal"",
                                                            ""CnsmpTax"": ""decimal""}";
        /////////////////////////////////////////////////////////////ok
        public static string PathMortRate = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\mortality_table.csv";
        public static string SchemasMortRate = @"{""MortTableID"":""int"",
                                                    ""Age"":""int"",
                                                    ""Sex"":""string"",
                                                    ""Qx"":""decimal""}";
        /////////////////////////////////////////////////////////////ok
        public static string PathProductSpec = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\productspec.csv";
        public static string SchemasProductSpec = @"{""Product"": ""string"",
                                                    ""PolType"": ""string"",
                                                    ""Gen"": ""string"",
                                                    ""LoadAcqSAParam1"": ""decimal"",
                                                    ""LoadAcqSAParam2"": ""decimal"",
                                                    ""LoadMaintSA"": ""decimal"",
                                                    ""LoadMaintSA2"": ""decimal"",
                                                    ""LoadMaintPremParam1"": ""decimal"",
                                                    ""LoadMaintPremParam2"": ""decimal"",
                                                    ""SurrChargeParam1"": ""decimal"",
                                                    ""SurrChargeParam2"": ""decimal"",
                                                    ""MortTablePrem"": ""decimal"",
                                                    ""MortTableVal"": ""decimal"",
                                                    ""IntRatePrem"": ""decimal"",
                                                    ""IntRateVal"": ""decimal"",
                                                    ""LapseRatePrem"": ""decimal"",
                                                    ""LapseRateVal"": ""decimal"",
                                                    ""Hsx"": ""decimal"",
                                                    ""Tsx"": ""decimal"",
                                                    ""Hax"": ""decimal"",
                                                    ""Tax"": ""decimal""}";
        /////////////////////////////////////////////////////////////ok
        public static string PathScenarios = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\scenarios.csv";
        public static string SchemasScenarios = @"{""ScenID"":""int"",
                                                    ""Year"":""int"",
                                                    ""IntRate"":""decimal""}";
        /////////////////////////////////////////////////////////////ok
        public static string PathModelPoint = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\policydata.csv";
        public static string SchemasModelPoint = @"{""Policy"":""int"",
                                                    ""Product"":""string"",
                                                    ""PolicyType"":""int"",
                                                    ""Gen"":""int"",
                                                    ""Channel"":""int"",
                                                    ""Duration"":""int"",
                                                    ""Sex"":""string"",
                                                    ""IssueAge"":""int"",
                                                    ""PaymentMode"":""int"",
                                                    ""PremFreq"":""int"",
                                                    ""PolicyTerm"":""int"",
                                                    ""MaxPolicyTerm"":""int"",
                                                    ""PolicyCount"":""decimal"",
                                                    ""SumAssured"":""decimal""}";
        /// //////////////////////////////////////////////////////////ok
        public static string PathModelPointTest = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\policydata_test.csv";

        public static string results = @"C:\Users\work\source\repos\ActuarialModellingInCSharp\SimpleLife\Data\results.json";
    }
}
