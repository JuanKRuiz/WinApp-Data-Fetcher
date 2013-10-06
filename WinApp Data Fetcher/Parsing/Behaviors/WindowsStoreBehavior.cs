/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */

using AppDataCreator.AppKind;
using CsQuery;
using System;
using System.Text.RegularExpressions;

namespace AppDataCreator.Parsing.Behaviors
{
    /// <summary>Behavior used to get information to windows store apps</summary>
    public class WindowsStoreBehavior : StoreBehavior
    {
        #region Constants
        /// <summary>Regular expression used to get packageFamilyName code from web site</summary>
        const string PFN_REGEXP = @"packageFamilyName\s=\s'([\w\.]*)';";
        /// <summary>Default culture used when there isn't app.config informtation</summary>
        const string DEFAULT_STORE_CULTURE = "es-co";
        /// <summary>Default base uri used when there isn't app.config informtation</summary
        const string DEFAULT_STORE_BASE_URI = "http://apps.microsoft.com/windows/{0}/app/dummy/{1}";
        #endregion Constants
        
        /// <summary>Regex processor used to extract information from web site</summary>
        readonly Regex _infoExtractor = new Regex(PFN_REGEXP, RegexOptions.Compiled);

        /// <summary>Regex processor used to extract information from web site</summary>
        public Regex InfoExtractor
        {get{return _infoExtractor; }}

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
            storeApp.Author = dom["#AppDeveloper"].Text().Trim();
            storeApp.ThumbnailUri = new Uri(dom["img#ScreenshotImage"].Attr("src"));

            //from here working in windows store specific fields
            var winstoreApp = storeApp as WindowsStoreApp;
            if (winstoreApp != null)
            {
                //converting DOM object in string
                string strTargetString = dom.Text();
                //using regular expression to extract PFN
                winstoreApp.PackageFamilyName = InfoExtractor.Match(strTargetString).Groups[1].Value;
            }
        }

        /// <summary>Method working as object factory, used to create IStoreApp specialized instances</summary>
        /// <returns>WindowsStoreApp</returns>
        public override IStoreApp CreateStoreAppObject()
        {
            return new WindowsStoreApp();        
        }

        /// <summary>passing storeBaseUrlParam to base constructor, setting according of app.config contents</summary>
        public WindowsStoreBehavior()
            : base("WindowsStoreBaseUri")
        { }
    }
}
