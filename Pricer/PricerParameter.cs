namespace Pricer
{
    public class PricerParameter
    {
        public PricerParameter(OptionType optionType, double s, double k,
            double time, double r, double sigma)
        {
            S = s;
            K = k;
            T = time;
            R = r;
            Sigma = sigma;
            OptionType = optionType;
        }

        public OptionType OptionType { get; }

        public double S { get; }

        public double K { get; }

        public double T { get; }

        public double R { get; }

        public double Sigma { get; }

    }
}