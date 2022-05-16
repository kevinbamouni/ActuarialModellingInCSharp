using Xunit;
using SimpleLife;

namespace SimpleLifeTestXUnit
{
    public class UnitTest1
    {
        [Fact]
        public void LifeTableClassTest_qx()
        {
            //4;116;F;1
            decimal intesrestRate = (decimal)0.0D;
            LifeTable lifeTable = new LifeTable(Input.PathMortRate, Input.SchemasMortRate,intesrestRate);
            decimal qx = lifeTable.qx(116, "F", 4);
            Assert.Equal(1,qx);
        }

        [Fact]
        public void LifeTableClassTest_qx1()
        {
            //1;0;M;0,00246
            decimal intesrestRate = 0m;
            LifeTable lifeTable = new LifeTable(Input.PathMortRate, Input.SchemasMortRate, intesrestRate);
            decimal qx = lifeTable.qx(0, "M", 1);
            decimal r = (decimal)0.00246;
            Assert.Equal(r,qx,5);
        }

        [Fact]
        public void GeneralStaticAssumptionsTest_BaseMort()
        {
            //TERM;1;1;3;MortAsmp2;LapseRate1;0,6;0,005;0,2;0,05;5;0,003;0,1;0;0;0,01;1000;0;0
            GeneralStaticAssumptions gsa = new GeneralStaticAssumptions(Input.PathParamAssumptions,
                Input.SchemasParamAssumptions,
                Input.PathAssumptions,
                Input.SchemasAssumptions);
            int baseMort = gsa.BaseMort("TERM", 1, 1);
            Assert.Equal(3, baseMort);
        }

        [Fact]
        public void GeneralStaticAssumptionsTest_SurrRate()
        {
            //TERM;1;1;3;MortAsmp2;LapseRate1;0,6;0,005;0,2;0,05;5;0,003;0,1;0;0;0,01;1000;0;0
            //1;0,50;0,70;0,40;0,40;0,40;0,40;0,70;0,08
            GeneralStaticAssumptions gsa = new GeneralStaticAssumptions(Input.PathParamAssumptions,
                Input.SchemasParamAssumptions,
                Input.PathAssumptions,
                Input.SchemasAssumptions);
            decimal surrRate = gsa.SurrRate("TERM", 1, 1, 1);
            Assert.Equal(0.08m, surrRate, 2);
        }

        [Fact]
        public void GeneralStaticAssumptionsTest_CnsmpTax()
        {
            //TERM;1;1;3;MortAsmp2;LapseRate1;0,6;0,005;0,2;0,05;5;0,003;0,1;0;0;0,01;1000;0;0
            //1;0,50;0,70;0,40;0,40;0,40;0,40;0,70;0,08
            GeneralStaticAssumptions gsa = new GeneralStaticAssumptions(Input.PathParamAssumptions,
                Input.SchemasParamAssumptions,
                Input.PathAssumptions,
                Input.SchemasAssumptions);
            decimal cnsmpTax = gsa.CnsmpTax("TERM", 1, 1);
            Assert.Equal(0m, cnsmpTax, 2);
        }

        [Fact]
        public void GeneralStaticAssumptionsTest_MortFactor()
        {
            //TERM;1;1;3;MortAsmp2;LapseRate1;0,6;0,005;0,2;0,05;5;0,003;0,1;0;0;0,01;1000;0;0
            //1;0,50;0,70;0,40;0,40;0,40;0,40;0,70;0,08
            GeneralStaticAssumptions gsa = new GeneralStaticAssumptions(Input.PathParamAssumptions,
                Input.SchemasParamAssumptions,
                Input.PathAssumptions,
                Input.SchemasAssumptions);
            decimal mortFactor = gsa.MortFactor("TERM", 1, 1, 1);
            Assert.Equal(0.7m, mortFactor,5);
        }

        [Fact]
        public void GeneralStaticAssumptionsTest_AssumtionParameterTableQuery1()
        {
            //TERM;1;1;3;MortAsmp2;LapseRate1;0,6;0,005;0,2;0,05;5;0,003;0,1;0;0;0,01;1000;0;0
            //1;0,50;0,70;0,40;0,40;0,40;0,40;0,70;0,08
            GeneralStaticAssumptions gsa = new GeneralStaticAssumptions(Input.PathParamAssumptions,
                Input.SchemasParamAssumptions,
                Input.PathAssumptions,
                Input.SchemasAssumptions);
            string? mortFactor = (string?)gsa.AssumtionParameterTableQuery("MortFactor", "TERM", 1, 1);
            Assert.Equal("MortAsmp2", mortFactor);
        }

        [Fact]
        public void GeneralStaticAssumptionsTest_AssumtionParameterTableQuery2()
        {
            //TERM;1;1;3;MortAsmp2;LapseRate1;0,6;0,005;0,2;0,05;5;0,003;0,1;0;0;0,01;1000;0;0
            //1;0,50;0,70;0,40;0,40;0,40;0,40;0,70;0,08
            GeneralStaticAssumptions gsa = new GeneralStaticAssumptions(Input.PathParamAssumptions,
                Input.SchemasParamAssumptions,
                Input.PathAssumptions,
                Input.SchemasAssumptions);
            string? surrender = (string?)gsa.AssumtionParameterTableQuery("Surrender", "TERM", 1, 1);
            Assert.Equal("LapseRate1", surrender);
        }

        [Fact]
        public void GeneralStaticAssumptionsTest_AssumtionParameterTableQuery3()
        {
            //TERM;1;1;3;MortAsmp2;LapseRate1;0,6;0,005;0,2;0,05;5;0,003;0,1;0;0;0,01;1000;0;0
            //1;0,50;0,70;0,40;0,40;0,40;0,40;0,70;0,08
            GeneralStaticAssumptions gsa = new GeneralStaticAssumptions(Input.PathParamAssumptions,
                Input.SchemasParamAssumptions,
                Input.PathAssumptions,
                Input.SchemasAssumptions);
            decimal? renewal = (decimal?)gsa.AssumtionParameterTableQuery("Renewal", "TERM", 1, 1);
            Assert.Equal(0.6m, renewal);
        }

        [Fact]
        public void EconomicAssumptioTest_DiscRate()
        {
            Economic gsa = new Economic(Input.PathScenarios, Input.SchemasScenarios);
            decimal? discRate = (decimal?)gsa.DiscRate(1,1);
            Assert.Equal(0.015m, discRate);
        }

        [Fact]
        public void EconomicAssumptioTest_InvstRetRate()
        {
            Economic gsa = new Economic(Input.PathScenarios, Input.SchemasScenarios);
            decimal? invstRetRate = (decimal?)gsa.InvstRetRate(1, 1);
            Assert.Equal(0.015m, invstRetRate);
        }
        [Fact]
        public void EconomicAssumptioTest_NumberOfEconomicScenarios()
        {
            Economic gsa = new Economic(Input.PathScenarios, Input.SchemasScenarios);
            int? nb = gsa.NumberOfEconomicScenarios();
            Assert.Equal(10, nb);
        }

        [Fact]
        public void ProductSpecifications_TableID1()
        {
            ProductSpecifications gsa = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
            int? nb = gsa.TableID("PREM","TERM",1,1);
            Assert.Equal(3, nb);
        }
        [Fact]
        public void ProductSpecifications_TableID2()
        {
            ProductSpecifications gsa = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
            int? nb = gsa.TableID("VAL", "TERM", 1, 1);
            Assert.Equal(1, nb);
        }

        [Fact]
        public void ProductSpecifications_LoadMaintSA2()
        {
            ProductSpecifications gsa = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
            decimal? nb = gsa.LoadMaintSA2("TERM", 1, 1);
            Assert.Equal(0, nb);
        }

        [Fact]
        public void ProductSpecifications_LoadMaintPremParam1()
        { 
            ProductSpecifications gsa = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
            decimal? nb = gsa.LoadMaintPrem("TERM", 1, 1);
            Assert.Equal(0.03m, nb);
        }
        [Fact]
        public void ProductSpecifications_IntRate()
        {
            ProductSpecifications gsa = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
            decimal? nb = gsa.IntRate("PREM", "TERM", 1, 1);
            Assert.Equal(0.015m, nb);
        }
        [Fact]
        public void ProductSpecifications_LoadAcqSA()
        {
            ProductSpecifications gsa = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
            decimal? nb = gsa.LoadAcqSA("WL", 1, 1,10);
            Assert.Equal(0.03m, nb);
        }
        [Fact]
        public void ProductSpecifications_InitSurrCharge()
        {
            ProductSpecifications gsa = new ProductSpecifications(Input.PathProductSpec, Input.SchemasProductSpec);
            decimal? nb = gsa.InitSurrCharge("WL", 1, 1, 10);
            Assert.Equal(0.015m, nb);
        }

    }
}