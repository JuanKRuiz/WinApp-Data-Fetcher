WinApp Data Fetcher
===================

Is a full asynchronous C# library used to get information about **Windows Phone** or **Windows Store Apps**. 

App information is obtained fetching it through internet visiting each App Store web page, 
his allows you get updated infomation for every app you want and uses this information in your any way.

You only need to know two things about an App

* App Guid 
* What kind of app it's (Windows Phone or Windows Store) 

Usage Examples
--------------

### Fetching One App Data

           var phoneGuid = "d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d";
           var parser = new StoreParser(new PhoneStoreBehavior());
                      
           IStoreApp appData = await parser.GetStoreAppDataAsync(phoneGuid);
           
           Console.WriteLine(appData);

This will produce the following output

           GUID:d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d
           Name:Magic Ghost Hunter
           Author:deltayeisson
           StoreUri:http://www.windowsphone.com/es-co/store/app/dummy/d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d
           ThumbnailUri:http://cdn.marketplaceimages.windowsphone.com/v8/images/bbb6a2d3-7dbd-4727-9aa7-8d006346e9ad?imageType=ws_icon_large

###Fecthing multiple Apps Data

           List<string> phoneGuids = new List<string>() 
                                            {"d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d",
                                             "f4232c0b-24e5-4f01-b8bb-69a892d06e28"};
           
           var parser = new StoreParser(new PhoneStoreBehavior());
           
           //this will open a Thread (Task) for each Guid in list
           IEnumerable<IStoreApp> appData = parser.GetStoreAppDataCollection(phoneGuids);
           
           foreach (var app in appData)
           {
               Console.WriteLine(app);
               Console.WriteLine("__________________________");               
           }

This will produce the following output

           GUID:f4232c0b-24e5-4f01-b8bb-69a892d06e28
           Name:Colsanitas
           Author:Organización Sanitas Internacional
           StoreUri:http://www.windowsphone.com/es-co/store/app/dummy/f4232c0b-24e5-4f01-b8bb-69a892d06e28
           ThumbnailUri:http://cdn.marketplaceimages.windowsphone.com/v8/images/653f8a8a-1e9f-4c13-b9dd-f9718879bfec?imageType=ws_icon_large
           
           __________________________
           GUID:d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d
           Name:Magic Ghost Hunter
           Author:deltayeisson
           StoreUri:http://www.windowsphone.com/es-co/store/app/dummy/d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d
           ThumbnailUri:http://cdn.marketplaceimages.windowsphone.com/v8/images/bbb6a2d3-7dbd-4727-9aa7-8d006346e9ad?imageType=ws_icon_large
           
           __________________________


Where to find App Guids?
------------------------

Guids are easily located in each App web page in the store. This is a link from a Windows Phone App, bold part is the Guid:

http://www.windowsphone.com/es-co/store/app/colsanitas/**f4232c0b-24e5-4f01-b8bb-69a892d06e28**

Same thing for this Windows Store App

http://apps.microsoft.com/windows/en-us/app/city-defend/**dda9a3b7-01cf-4ce7-a268-128712d7fcdd**


WinApp Data Fetcher Client && Exporting App information to JSon
---------------------------------------------------------------


Features
--------
* Async operations: each web call runs in a diferent thread
* Support Windows Phone and Windows Store Apps
* Created to be extensible
