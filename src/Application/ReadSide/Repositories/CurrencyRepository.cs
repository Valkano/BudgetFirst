namespace BudgetFirst.ReadSide.Repositories
{
    using System;
    using System.Collections.Generic;

    using BudgetFirst.Common.Infrastructure.Data;
    using BudgetFirst.ReadSide.ReadModel;
    using BudgetFirst.SharedInterfaces.Annotations;

    /// <summary>
    /// Repository for currencies
    /// </summary>
    public class CurrencyRepository
    {
        /// <summary>
        /// Currencies are compiled into the application, therefore this repository does not use the read store 
        /// </summary>
        private static readonly CurrencyList Cache;

        /// <summary>
        /// Initialises static members of the <see cref="CurrencyRepository"/> class.
        /// </summary>
        static CurrencyRepository()
        {
            Cache = new CurrencyList();

            var alreadyLoadedCurrencies = new HashSet<string>();

            // Load currencies 
            var data = new Currencies();

            var currencies = data.CurrencyList.Root?.Element("CcyTbl")?.Elements("CcyNtry");
            if (currencies == null)
            {
                // Should not happen...
                return;
            }

            foreach (var entry in currencies)
            {
                var currency = new Currency()
                                   {
                                       Name = entry.Element("CcyNm")?.Value,
                                       Code = entry.Element("Ccy")?.Value,
                                   };

                if (alreadyLoadedCurrencies.Contains(currency.Code))
                {
                    continue;
                }
                
                Cache.Add(currency);
                alreadyLoadedCurrencies.Add(currency.Code);
            }
        }

        /// <summary>
        /// Get all currencies
        /// </summary>
        /// <returns>List of currencies</returns>
        public CurrencyList GetAll()
        {
            return Cache;
        }
    }
}