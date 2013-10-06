/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */
using WinAppDataFetcher.AppKind;
using CsQuery;
using System;

namespace WinAppDataFetcher.Parsing.Behaviors
{
    /// <summary>Behavior used to get information to windows store apps</summary>
    public class PhoneStoreBehavior : StoreBehavior
    {
        #region Constants
        const string DEFAULT_STORE_BASE_URI = "http://www.windowsphone.com/{0}/store/app/dummy/{1}";
        const string DEFAULT_STORE_CULTURE = "es-co"; 
        #endregion Constants

        /// <summary>Default app uri template to locate app in store</summary>
        public override string DefaultStoreBaseUri
        {get { return DEFAULT_STORE_BASE_URI;}}
        
        /// <summary>Default app culture to locate app in store</summary>        
        public override string DefaultStoreCulture
        {get { return DEFAULT_STORE_CULTURE; }}

        /// <summary>Get and process app information in public store web page</summary>
        /// <param name="storeApp">Object to complete fields with all information taken from app web page</param>
        /// <param name="dom">C# query object used to explore DOM from Store App web page</param>
        public override void ObjectDOMMapper(IStoreApp storeApp, CQ dom)
        {
            storeApp.Name = dom["h1[itemprop='name']"].Text();
            storeApp.Author = dom["#publisher a, #publisher span[itemprop='publisher']"].Text();
            storeApp.ThumbnailUri = new Uri(dom["img.appImage"].Attr("src"));
        }

        /// <summary>Method working as object factory, used to create IStoreApp specialized instances</summary>
        /// <returns>WindowsPhoneApp</returns>
        public override IStoreApp CreateStoreAppObject()
        {
            return new WindowsPhoneApp();
        }
        
        /// <summary>passing storeBaseUrlParam to base constructor, setting according of app.config contents</summary>
        public PhoneStoreBehavior()
            : base("PhoneStoreBaseUri")
        { }
    }
}
