namespace BudgetFirst.Infrastructure.Data
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// Contains the (raw) list of currencies
    /// </summary>
    public class Currencies
    {
        /// <summary>
        /// Cache of the document
        /// </summary>
        private XDocument cache;

        /// <summary>
        /// Initialises a new instance of the <see cref="Currencies"/> class.
        /// </summary>
        public Currencies()
        {
            var assembly = typeof(Currencies).GetTypeInfo().Assembly;
            var resourceName = assembly.GetManifestResourceNames().Single(x => x.EndsWith("currencies.xml"));
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                this.cache = XDocument.Load(stream);
            }
        }

        /// <summary>
        /// Gets the currency list 
        /// </summary>
        public XDocument CurrencyList => this.cache;
    }
}