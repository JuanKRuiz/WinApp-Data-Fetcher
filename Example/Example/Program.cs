/* Original Author: Juan Carlos Ruiz
 * Twitter        : @JuanKRuiz
 * Date           : 2013-10-05
 * ------------------------------------
 */

using AppDataCreator.AppKind;
using AppDataCreator.Parsing;
using AppDataCreator.Parsing.Behaviors;
using System;
using System.Collections.Generic;


namespace Example
{
    class Program
    {
        static void Main()
        {
            var phoneGuid = "d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d";
            var parser = new StoreParser(new PhoneStoreBehavior());

            IStoreApp appData = parser.GetStoreAppDataAsync(phoneGuid).Result;

            Console.WriteLine(appData);
            Console.ReadLine();
            
            List<string> phoneGuids = new List<string>() 
                        {"d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d",
                            "f4232c0b-24e5-4f01-b8bb-69a892d06e28"};


            //this will open a Thread (Task) for each Guid in list
            IEnumerable<IStoreApp> appDataList = parser.GetStoreAppDataCollection(phoneGuids);

            foreach (var app in appDataList)
            {
                Console.WriteLine(app);
                Console.WriteLine("__________________________");
            }

            Console.ReadLine();
        }
    }
}
