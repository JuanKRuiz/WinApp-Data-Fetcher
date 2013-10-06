/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */
using AppDataCreator.AppKind;
using CsQuery;
using System;
using System.Configuration;
namespace AppDataCreator.Parsing.Behaviors
{
    /// <summary>Define base structure and methods for all behaviors</summary>
    /// <remarks>Each store needs their own behavior to setup configuration and 
    /// create App objects and extract information from app web page.
    /// </remarks>
    public abstract class StoreBehavior
    {
        /// <summary>URI Used to set store URL when no app.config file is available</summary>
        public abstract string DefaultStoreBaseUri { get; }
        
        /// <summary>Culture Used to set store URL when no app.config file is available</summary>
        public abstract string DefaultStoreCulture { get; }
        
        /// <summary>Templed URL to find app in their store</summary>
        /// <remarks>This URI is a template to create each app full uri in its store.
        /// formatted values are important and you could use these fragments when 
        /// overwrite <c>ObjectDOMMapper</c> method.</remarks>
        /// <example>http://apps.microsoft.com/windows/{0}/app/dummy/{1}</example>
        public string StoreBaseUri { get; set; }

        /// <summary>Culture Used to set store URL for an App</summary>
        /// <remarks>You could use these fragment when 
        /// overwrite <c>ObjectDOMMapper</c> method.</remarks>
        /// <example>es-co</example>
        public string StoreCulture { get; set; }

        /// <summary></summary>
        /// <remarks>If storeBaseUrlParam is not established, constructor uses app.config parameters.
        /// If app.config file not exists or storeBaseUrlParam is not defined then constructor uses default values.
        /// </remarks>
        /// <param name="storeBaseUrlParam">Name of app.config parameter containing store base url
        /// </param>
        public StoreBehavior(string storeBaseUrlParam = "")
        {
            StoreCulture = ConfigurationManager.AppSettings["StoreCulture"];

            if (string.IsNullOrWhiteSpace(StoreCulture))
                StoreCulture = DefaultStoreCulture;

            if (!string.IsNullOrWhiteSpace(storeBaseUrlParam))
                StoreBaseUri = ConfigurationManager.AppSettings[storeBaseUrlParam];

            if (string.IsNullOrWhiteSpace(StoreBaseUri))
                StoreBaseUri = DefaultStoreBaseUri;
        }

        /// <summary>Method working as object factory, used to create IStoreApp specialized instances</summary>
        /// <returns>IStoreApp compatible object</returns>
        public abstract IStoreApp CreateStoreAppObject(); 

        /// <summary>Get and process app information in public store web page</summary>
        /// <param name="storeApp">Object to complete fields with all information taken from app web page</param>
        /// <param name="dom">C# query object used to explore DOM from Store App web page</param>
        public abstract void ObjectDOMMapper(IStoreApp storeApp, CQ dom);
    }
}
