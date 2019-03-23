using System;

namespace Pricer
{
    public class Pricer
    {
        private readonly INormalCdfComputer _cdfComputer;

        public Pricer(INormalCdfComputer cdfComputer)
        {
            _cdfComputer = cdfComputer;
        }

        public double GetPrice(PricerParameter pricingPricerParameter)
        {
            var s = pricingPricerParameter.S;
            var k = pricingPricerParameter.K;
            var r = pricingPricerParameter.R;
            var t = pricingPricerParameter.T;
            var sigma = pricingPricerParameter.Sigma;

            var d1 = (Math.Log(s / k) + (r + Math.Pow(sigma, 2) / 2) * t) / (sigma * Math.Sqrt(t));
            var d2 = d1 - sigma * Math.Pow(t, 0.5);

            switch (pricingPricerParameter.OptionType)
            {
                case OptionType.Call:
                    return s * N(d1) - k * Math.Exp(-r * t) * N(d2);
                case OptionType.Put:
                    return k * Math.Exp(-r * t) * N(-d2) - s * N(-d1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(pricingPricerParameter.OptionType), pricingPricerParameter.OptionType, $"{pricingPricerParameter.OptionType} is not managed.");
            }
            
        }

        private double N(double d1)
        {
            return _cdfComputer.Compute(d1);
        }
    }
}
