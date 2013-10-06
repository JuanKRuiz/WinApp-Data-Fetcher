WinApp Data Fetcher
===================

Is a C# library used to get information about **Windows Phone** or **Windows Store Apps**. 

App information is obtained fetching it through internet visiting each App Store web page, 
his allows you get updated infomation for every app you want and uses this information in your any way.

You only need to know two things about an App

* App Guid 
* What kind of app it's (Windows Phone or Windows Store) 

Usage Example
-------------

           var phoneGuid = "d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d";
           var parser = new StoreParser(new PhoneStoreBehavior());
                      
           IStoreApp appData = await parser.GetStoreAppDataAsync(phoneGuid);
           
           Console.WriteLine(appData);

###This will produce this output

>GUID:d2d17da2-e6ad-4b6b-bdcd-ffdea39ba78d
Name:Magic Ghost Hunter
Author:deltayeisson
StoreUri:http://www.windowsphone.com/es-co/store/app/dummy/d2d17da2-e6ad-4b6b-bd
cd-ffdea39ba78d
ThumbnailUri:http://cdn.marketplaceimages.windowsphone.com/v8/images/bbb6a2d3-7d
bd-4727-9aa7-8d006346e9ad?imageType=ws_icon_large
