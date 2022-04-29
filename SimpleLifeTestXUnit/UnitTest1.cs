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
            decimal intesrestRate = (decimal)0.0D;
            LifeTable lifeTable = new LifeTable(Input.PathMortRate, Input.SchemasMortRate, intesrestRate);
            decimal qx = lifeTable.qx(0, "M", 1);
            decimal r = (decimal)0.00246;
            Assert.Equal(r,qx,5);
        }
    }
}