/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */

using AppDataCreator.Resources;
using Extenders;
using Newtonsoft.Json;
using System;

namespace AppDataCreator.AppKind
{
    /// <summary>Windows Store app information</summary>
    public class WindowsStoreApp : IStoreApp
    {
        /// <summary>Windows Store App unique id</summary>
        [JsonIgnore]
        public string GUID { get; set; }

        [JsonProperty("appname")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author
        { get; set; }

        /// <summary>Package identifier for app and developer, this is available through windows store app web site</summary>
        /// <example>41516JosetoBenito.SHOOTERS_tt3vm1xt44ntj</example>
        [JsonProperty("pfn")]
        public string PackageFamilyName { get; set; }

        /// <summary>URI for App representative image</summary>
        [JsonProperty("thumbnail")]
        public Uri ThumbnailUri { get; set; }

        /// <summary>URI to access app web page in Store</summary>
        [JsonIgnore]
        public Uri StoreUri { get; set; }

        public WindowsStoreApp()
        {
            PackageFamilyName = Author = Name = GUID = string.Empty;
            StoreUri = ThumbnailUri = new Uri(GeneralResources.TempURI);
        }

        public override string ToString()
        {
            return this.PublicPopertiesToString();
        }
    }
}
