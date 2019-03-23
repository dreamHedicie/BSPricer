using System;
using System.ComponentModel;
using Prism.Commands;
using Prism.Common;

namespace Pricer.ViewModels
{
    public class MainViewModel : IDataErrorInfo
    {
        public OptionType OptionType { get; set; }
        public double UnderlyingPrice { get; set; }
        public double Strike { get; set; }
        public double VolatilityInPercent { get; set; }
        public int DaysUntilExpiration { get; set; }
        public double RiskFreeInterestRateInPercent { get; set; }
        public DelegateCommand GetQuoteCommand { get; }
        public ObservableObject<string> Result { get; }
        private readonly Pricer _pricer;

        public MainViewModel()
        {
            Result = new ObservableObject<string>();
            GetQuoteCommand = new DelegateCommand(GetQuote, CanExecuteGetQuote);
            _pricer = new Pricer(new NormalCdfComputer());
        }

        private bool CanExecuteGetQuote()
        {
            return VolatilityInPercent > 0 && DaysUntilExpiration > 0 && UnderlyingPrice > 0 && Strike > 0 &&
                   RiskFreeInterestRateInPercent >= 0;
        }

        private void GetQuote()
        {
            try
            {
                var price = _pricer.GetPrice(new PricerParameter(OptionType, UnderlyingPrice, Strike, DaysUntilExpiration / 365.0,
                    RiskFreeInterestRateInPercent / 100, VolatilityInPercent / 100));
                Result.Value = price.ToString("0.0000");
            }
            catch (Exception)
            {
                Result.Value = "Internal Error";
            }
            
            
        }

        

        public string this[string columnName]
        {
            get
            {
                var message = string.Empty;
                switch (columnName)
                {
                    case nameof(VolatilityInPercent):
                        if (VolatilityInPercent <= 0)
                        {
                            message = "Volatility must be strictly positive";
                        }

                        break;
                    case nameof(UnderlyingPrice):
                        if (UnderlyingPrice <= 0)
                        {
                            message = "Price must be strictly positive";
                        }

                        break;
                    case nameof(Strike):
                        if (Strike <= 0)
                        {
                            message = "Price must be strictly positive";
                        }

                        break;
                    case nameof(DaysUntilExpiration):
                        if (DaysUntilExpiration <= 0)
                        {
                            message = "Time must be strictly positive";
                        }

                        break;
                    case nameof(RiskFreeInterestRateInPercent):
                        if (RiskFreeInterestRateInPercent < 0)
                        {
                            message = "Interest Rate must be positive";
                        }

                        break;
                }
                
                GetQuoteCommand.RaiseCanExecuteChanged();
                return message;
            }
        }


        public string Error => string.Empty;
        
    }
}
