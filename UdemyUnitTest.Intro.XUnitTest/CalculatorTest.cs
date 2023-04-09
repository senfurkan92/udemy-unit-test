using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyUnitTest.Intro.XUnitTest
{
	public class CalculatorTest
	{
		private readonly Calculator _calculator;

		public CalculatorTest()
		{
			_calculator= new Calculator();
		}

		[Fact]
		public void SumTest()
		{
			// arrange
			var a = 5;
			var b = 15;

			// act
			var result = _calculator.Sum(a, b);

			//assert
			Assert.Contains("2",result.ToString());
			Assert.DoesNotContain("1", result.ToString());
			Assert.True(result == a + b);
			Assert.False(result == a * b);
			Assert.Matches("^\\d+$", result.ToString());
			Assert.DoesNotMatch("[a-zA-Z_]\\w*", result.ToString());
			Assert.NotEmpty(result.ToString());
			Assert.NotNull(result.ToString());
			Assert.InRange(result,5,25);
			Assert.Single(new List<int> { a+b }, result);
			Assert.IsType<int>(result);
			Assert.IsAssignableFrom<IEnumerable<int>>(new List<int> { result });
			Assert.IsAssignableFrom<IList<int>>(new List<int> { result });
			Assert.IsAssignableFrom<ICollection<int>>(new List<int> { result });
			Assert.IsAssignableFrom<object>(new List<int> { result });
			Assert.IsAssignableFrom<dynamic>(new List<int> { result });
			Assert.Equal(a + b, result);
			Assert.NotEqual(a * b, result);
		}

		[Theory]
		[InlineData(2,3,5)]
		[InlineData(-1,-7,-8)]
		public void SumTestWithParameters(int a, int b, int total)
		{ 
			// arrange

			// act
			var result = _calculator.Sum(a, b);

			// assert
			Assert.Equal(total, result);
		}
	}
}
