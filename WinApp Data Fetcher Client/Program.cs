/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-09-15
 * ------------------------------------
 */
using WinAppDataFetcher.Parsing;
using WinAppDataFetcher.Parsing.Behaviors;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WinAppDataFetcherClient
{
    /// <summary>This tool uses two files with app GUIDs and get information of this apps using WinApp Data Fetcher.
    /// Then exports all information to json files</summary>
    class Program
    {
        static void Main()
        {
            DrawWelcome();
            //load input files contents
            string[] phoneGuids = LoadGuids("phone.input");
            string[] winGuids = LoadGuids("win.input");

            //start task for each file process
            var t1 = Task.Factory.StartNew(() =>
            {
                GetInformationAndCreateOutput(winGuids, new WindowsStoreBehavior(), "win.json");
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                GetInformationAndCreateOutput(phoneGuids, new PhoneStoreBehavior(), "phone.json");
            });

            //wait for completion
            t1.Wait();
            t2.Wait();
            Console.WriteLine("\nPress ENTER to Close");
            Console.ReadLine();
        }


        /// <summary>
        /// Get information for each app GUId and then creates an output file formatted in json
        /// </summary>
        /// <param name="appGuids">Array of GUIDs</param>
        /// <param name="behavior">Behavior describing the Store App kind</param>
        /// <param name="outputName">Name for the output file</param>
        private static void GetInformationAndCreateOutput(string[] appGuids, StoreBehavior behavior, string outputName)
        {
            if (appGuids.Length > 0 && !string.IsNullOrWhiteSpace(outputName))
            {
                var wsaf = new StoreParser(behavior);

                Console.WriteLine("Getting information for {0} from internet...", outputName);

                try
                {
                    var listaStoreApps = wsaf.GetStoreAppDataCollection(appGuids);
                    Console.WriteLine("{0} Done", outputName);

                    Console.WriteLine("Generating output in {0}", outputName);
                    File.WriteAllText(outputName,
                        JsonConvert.SerializeObject(listaStoreApps, Formatting.Indented));

                    Console.WriteLine("{0} output file generated", outputName);
                }
                catch (WebException)
                {
                    Console.WriteLine("This program requires internet connection to work properly");
                }
            }
        }

        /// <summary>
        /// Load GUIDs from a text file and create an string[] with them
        /// </summary>
        /// <param name="fileName">File to be loaded</param>
        /// <returns></returns>
        private static string[] LoadGuids(string fileName)
        {
            string guids = string.Empty;
            try
            {
                using (var winFileStream = File.OpenText(fileName))
                {
                    guids = winFileStream.ReadToEnd();
                }
                Console.WriteLine("{0} Found", fileName);
            }
            catch { Console.WriteLine("{0} Not found", fileName); }

            var list = guids.Split(new string[] { Environment.NewLine, "\n", " ", "   ", ",", ";" },
                StringSplitOptions.RemoveEmptyEntries);

            return list;
        }

        /// <summary>Draw welcome screen</summary>
        private static void DrawWelcome()
        {
            Console.WriteLine(
@"************************************************
* WinApp Data Fetcher Tool
*________________________________________________
* This program runs reading this files
*  phone.input
*  win.input
* Then get information from internet 
* and produces this output files with json 
* formated data
*  phone.json
*  win.json
*************************************************");
        }
    }
}
