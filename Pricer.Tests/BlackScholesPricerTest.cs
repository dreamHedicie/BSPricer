using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Pricer.Tests
{
    [TestFixture]
    public class BlackScholesPricerTest
    {
        [Test]
        public void TestGetPrice()
        {
            var strikePrice = 100;
            var underlyingPrice = 90;
            var daysUntilExpiration = 230 / 365.0;
            var volatility = 0.5;
            var interestRate = 0.1;
            var d1 = 0.09176057;
            var d2 = -0.30514527;

            var cdfComputer = Substitute.For<INormalCdfComputer>();
            cdfComputer.Compute(Arg.Is<double>(c => Math.Abs(Math.Round(c, 8) - d1) < double.Epsilon)).Returns(0.5365558638);
            cdfComputer.Compute(Arg.Is<double>(c => Math.Abs(Math.Round(c, 8) + d1) < double.Epsilon)).Returns(1 - 0.5365558638);
            cdfComputer.Compute(Arg.Is<double>(c => Math.Abs(Math.Round(c, 8) - d2) < double.Epsilon)).Returns(0.3801277569);
            cdfComputer.Compute(Arg.Is<double>(c => Math.Abs(Math.Round(c, 8) + d2) < double.Epsilon)).Returns(1 - 0.3801277569);

            var callParameter = new PricerParameter(OptionType.Call, underlyingPrice, strikePrice, daysUntilExpiration, interestRate, volatility);
            var putParameter = new PricerParameter(OptionType.Put, underlyingPrice, strikePrice, daysUntilExpiration, interestRate, volatility);
            var pricer = new Pricer(cdfComputer);

            var callPrice = Math.Round(pricer.GetPrice(callParameter), 4);
            var putPrice = Math.Round(pricer.GetPrice(putParameter), 4);

            Assert.That(callPrice, Is.EqualTo(12.5987));
            Assert.That(putPrice, Is.EqualTo(16.4917));
        }

        public static IEnumerable<TestCaseData> TestGetPriceCaseData => new[]
        {
            new TestCaseData(OptionType.Call, 100, 100, 30, 0.05, 0.25).Returns(3.0626m),
            new TestCaseData(OptionType.Put, 100, 100, 30, 0.05, 0.25).Returns(2.6525m),
            new TestCaseData(OptionType.Call, 90, 100, 230, 0.1, 0.5).Returns(17.574m), 
            new TestCaseData(OptionType.Put, 90, 100, 365, 0.1, 0.5).Returns(18.0578m) 
        };
    }
}
