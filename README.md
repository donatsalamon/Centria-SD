# Centria Software developer assistant position application Documentation

Overview
This C# console application allows users to search for current news and also news based on several parameters such as:
(all the input formats are written right before they need to be entered)
* Article Theme
* From Date: from what date should the program search the news
* To Date: until what date should the program search the news
* Language: Which languages`s news should be included
* SortBy: What should be the news sorted by
* Page: Use this to page through the results
* PageSize: The number of results to return per page
* Sources: You can add a web page's name in case you are interested in a specific source (like BBC)
* Domains: You can add a specific Domain also(bbc.com)
(For both language and Sort by there is another command such as options which provides the available possibilities)

The application utilizes a third-party API (https://newsapi.org/) to fetch news data and present it to the user in a console interface.

Features
Search for news articles based on user-defined search parameters (previously mentioned) or the most current news from every field.
Display news, articles with relevant information such as title, author, publication date, and Source.
Provide error handling when receiving input from the user.

# Requirements
.NET SDK installed on the system.
Internet connectivity to access the news API.
From NuGet Package Manager 2 packages are needed:
* NewsApi: This is to access the API in C#
* System.Configuration.ConfigurationManager: Gives access to the Configuration Manager class which provides the opportunity to read the API key from the App.config

# Installation
Clone or download the source code from the GitHub repository
Open the solution in Visual Studio or your preferred C# IDE
Restore the NuGet packages which were previously mentioned
Build the solution to ensure all dependencies are resolved.

# API KEY
There are 2 ways use the program with the API KEY

First is setting it up as an environmental variable. In order to do that have to run the a cmd as administrator.
Then paste the following command: setx NAME_OF_YOU_API KEY "your_actual_api_key" /m
/m Makes sure that the variable will be globally available.
Afterwards to check if it has been set up properly restart the cmd as an administrator then run the following command: set | findstr NAME_OF_YOU_API KEY
If we recieve the API KEY then it works perfectly.
Then just have to uncomment the line inside the Program.cs

Second option is to use the App.config where you have to replace the value`s property where the YOUR_API_KEY_HERE sign is with your API KEY.

In any case the program is set up with a Status check that will send a response in case if the API key would have any kind of problem.

# Credits
This application utilizes https://newsapi.org/ in order to fetch the data.

# License
This software is under the MIT License.
