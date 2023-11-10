namespace TSLServer.Core.Helper
{
    /// <summary>
    /// RandomCurrencyGenerator
    /// </summary>
    public class RandomCurrencyGenerator
    {
        /// <summary>
        /// Generate Random data for currency
        /// </summary>
        /// <param name="baseValue">Base value to used for generating new value</param>
        /// <param name="limit">Number of generated output</param>
        /// <returns></returns>
        public static List<double> randomCurrencyGenerator(double baseValue, double limit = 4)
        {
            int count = 0;
            List<double> result = new List<double>();

            while (count < limit)
            {
                Random rnd = new Random();
                double randomVal = rnd.Next(0, 100) / 1000.0;

                result.Add(baseValue + randomVal);
                count++;
            }
            return result;
        }
    }
}
