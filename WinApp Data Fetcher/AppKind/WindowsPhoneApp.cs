/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */

using WinAppDataFetcher.Resources;
using Extenders;
using Newtonsoft.Json;
using System;

namespace WinAppDataFetcher.AppKind
{
    /// <summary>Windows Phone app information</summary>
    class WindowsPhoneApp:IStoreApp
    {
        /// <summary>Windows Phone App unique id</summary>
        [JsonIgnore]
        public string GUID
        { get;set;}

        [JsonProperty("appname")]
        public string Name
        { get; set; }
        
        [JsonProperty("author")]
        public string Author
        { get; set; }

        /// <summary>URI to access app web page in Store</summary>
        [JsonProperty("link")]
        public Uri StoreUri
        { get; set; }

        /// <summary>URI for App representative image</summary>
        [JsonProperty("thumbnail")]
        public Uri ThumbnailUri
        { get; set; }

        public WindowsPhoneApp()
        {
            Author = Name = GUID = string.Empty;
            StoreUri = ThumbnailUri = new Uri(GeneralResources.TempURI);
        }

        public override string ToString()
        {
            return this.PublicPopertiesToString();
        }
    }
}
