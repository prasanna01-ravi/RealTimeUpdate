namespace TSLServer.Service.ViewModel
{
    public class CurrencyViewModel
    {
        public double InitVal
        {
            get
            {
                return Values?.FirstOrDefault() ?? 0;
            }
        }

        public double CurrentVal
        {
            get
            {
                return Values?.LastOrDefault() ?? 0;
            }
        }

        public double PercentageChange
        {
            get
            {
                if(InitVal > 0 && CurrentVal > 0)
                {
                    return Math.Round((CurrentVal - InitVal) * 100.00 / InitVal, 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool IsIncrease
        {
            get
            {
                return CurrentVal > InitVal;
            }
        }

        public string CurrencyName { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public List<double>? Values { get; set; }
    }
}
