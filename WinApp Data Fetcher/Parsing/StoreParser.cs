/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */

using WinAppDataFetcher.AppKind;
using WinAppDataFetcher.Parsing.Behaviors;
using WinAppDataFetcher.Resources;
using Helpers;
using CsQuery;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace WinAppDataFetcher.Parsing
{
    /// <summary>Is the parsing engine offering base functionality.
    /// this component needs behaviors to execute particular steps on each app kind</summary>
    public sealed class StoreParser : IDisposable
    {
        /// <summary>Specific store app logic</summary>
        StoreBehavior StoreBehavior{get; set;}
        /// <summary>Used to get app information from store web page</summary>
        HttpClient HttpClient { get; set; }

        /// <summary></summary>
        /// <param name="behavior">logic container to get information for specific App Kind</param>
        public StoreParser(StoreBehavior behavior)
        {
            StoreBehavior = behavior;
            HttpClient = new HttpClient();
        }

        static StoreParser()
        {
        }

        /// <summary>
        /// Get related app information from internet
        /// </summary>
        /// <param name="appGuid">App unique Id, doesn't matter the app kind. Method resolves app kind itself using
        /// StoreBehavior property</param>
        /// <returns>A complete IStoreApp compatible object</returns>
        public async Task<IStoreApp> GetStoreAppDataAsync(string appGuid)
        {
            string htmlContents = string.Empty;
            //using behavior factory this return the right object type for each app kind
            var storeApp = StoreBehavior.CreateStoreAppObject();
            
            //setup 'local' fields
            storeApp.GUID = appGuid;

            storeApp.StoreUri = new Uri(string.Format(StoreBehavior.StoreBaseUri,
                                                      StoreBehavior.StoreCulture, appGuid));

            if (NetWorkHelper.IsInternetAvailable)
            {
                //setup field getting information from web
                try
                {
                    //creating C# query object

                    CQ dom = await HttpClient.GetStringAsync(storeApp.StoreUri);
                    //fill all IStoreApp fields using web information according to behavior
                    StoreBehavior.ObjectDOMMapper(storeApp, dom);
                }
                catch
                {
                    //easing error reporting for user point of view
                    storeApp.Name = string.Format(GeneralResources.AppUrlNotFound, storeApp.StoreUri, appGuid);
                }
            }
            else 
            {
                throw new WebException(GeneralResources.InternetRequiredForAppsDataException);
            }

            return storeApp;
        }

        /// <summary>
        /// Get data for each App GUID in a collection
        /// </summary>
        /// <param name="appGuidCollection">GUIDs Collection of same IStoreApp derived Type</param>
        /// <returns>A list os IStoreApp objects with complete information</returns>
        public IEnumerable<IStoreApp> GetStoreAppDataCollection(IEnumerable<string> appGuidCollection)
        {
            var storeAppCollection = new List<IStoreApp>();
            
            Parallel.ForEach(appGuidCollection, (appGuid) =>
            {
                storeAppCollection.Add(GetStoreAppDataAsync(appGuid).Result);
            });

            return storeAppCollection;
        }

        public void Dispose()
        {
            if (HttpClient != null)
                HttpClient.Dispose();
        }
    }
}
