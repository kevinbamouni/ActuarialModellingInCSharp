using System;
using System.Data;

namespace PropertyAndCasualtyLossReserving
{
	public class Triangle : TriangleBase
	{
		private DataTable Data { get;}
		private List<string> Index { get; }
		private List<string> Columns { get; }
		private List<string> Origin { get; }
		private List<string> Development { get; }
		private string Name { get; }
		private DateOnly ReportDate { get; }

		public Triangle(DataTable data, List<string> index, List<string> columns, List<string> origin, List<string> development, string name, DateOnly reportDate)
		{
			Data = data;
			Index = index;
			Columns = columns;
			Origin = origin;
			Development = development;
			Name = name;
			ReportDate = reportDate;
		}

		public string PrintTriangle()
        {
			return "00";
        }

		public void IncrementalToCumulative()
        {

        }

		public void CumulativeToIncremental()
        {

        }

		public void CorrelationEvaluation()
        {

        }

		public string PrintTriangleDetails()
        {
			return "0";
        }

		public void LatestValuationDate()
        {
			// Latest development period

        }

		public void ComputeLinkRation()
        {
			//Compute and return LinkRatios
        }

		public void LatestDiagonal()
        {
			//Compute lastest diagonal of the triangle.
        }

	}
}

