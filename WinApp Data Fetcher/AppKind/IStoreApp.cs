/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */
using System;

namespace AppDataCreator.AppKind
{
    /// <summary>Define common information available for an application</summary>
    public interface IStoreApp
    {
        /// <summary>App unique ID, in windows systems is usually a GUID</summary>
        string GUID { get; set; }
        string Name { get; set; }
        string Author { get; set; }
        /// <summary>URI for App representative image</summary>
        Uri ThumbnailUri { get; set; }
        /// <summary>URI to access app web page in Store</summary>
        Uri StoreUri { get; set; }
    }
}
