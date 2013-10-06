/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */

using System.Reflection;
using System.Text;

namespace Extenders
{
    /// <summary>Useful extension methods applicable to all objects</summary>
    public static class ObjectExtender
    {
        /// <summary>
        /// Get object properties and values in a multi line string  
        /// </summary>
        /// <param name="obj">object used to extract properties</param>
        /// <param name="format">string format for properties and values
        ///     <value>Default value is "{0}:{1}\n"</value>
        /// </param>
        /// <returns>A string listing public object properties and their values, each property in a line</returns>
        public static string PublicPopertiesToString(this object obj, string format = "{0}:{1}\n")
        {
            var type = obj.GetType();
            var output = new StringBuilder(100);
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance ))
            {
                output.AppendFormat(format, property.Name, property.GetValue(obj).ToString());
            }
            return output.ToString();
        }
    }
}
