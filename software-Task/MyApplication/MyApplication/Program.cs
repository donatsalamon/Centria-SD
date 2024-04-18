
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;
using System.Configuration;
using System.ComponentModel.Design;
using System.Data;
using Newtonsoft.Json.Linq;

using System.Reflection;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var today = DateTime.Today;
            DateTime yesterdayDate = today.AddDays(-1);
            
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            //IN CASE YOU WOULD LIKE TO USE ENVIRONMENTAL VARIABLES
            //HAVE TO SET THE VARIABLE THROUGH THE CONSOLE -> THE COMMAND IS IN THE DOCUMENTATION
            //string apiKey = Environment.GetEnvironmentVariable("YOUR_API_KEY_NAME");

            var newsApiClient = new NewsApiClient(apiKey);

            string controlInput = "";
            
            //USER INPUTS

            BasicFunction(ref controlInput);

            string articleTheme = "";
            string fromDate = "";
            string toDate = "";

            int pageInt;
            int pageSizeInt;

            string languageInput = "";
            bool languageEnumExist = false;

            string sortByInput = "";
            bool sortByEnumExist = false;

            string page = "";
            string pageSize = "";

            Languages languageCheck = new Languages();
            SortBys sortBysCheck = new SortBys();
            Countries countriesCheck = new Countries();

            List<string> sourcesList = new List<string>();
            string sourceInput = "";

            List<string> domainsList = new List<string>();
            string domainInput = "";

            while (controlInput != "4")
            {

                if (controlInput == "1")
                {
                    foreach (Categories category in Enum.GetValues(typeof(Categories)))
                    {
                        TopHeadlinesRequest topHeadlinesRequest = (new TopHeadlinesRequest
                        {
                            Language = Languages.EN,
                            Category = category,
                        });
                        
                        var articlesResponse = newsApiClient.GetTopHeadlines(topHeadlinesRequest);

                        //CHECKING THE RESPONSE STATUS IN CASE OF AN ERROR
                        if (articlesResponse.Status == Statuses.Ok)
                        {

                            foreach (var article in articlesResponse.Articles)
                            {
                                //some of the articles are not correct which it sends back so this is a correction here
                                // title
                                if (!article.Title.Contains("Remove"))
                                {
                                    Console.WriteLine(article.Title);
                                    // author
                                    Console.WriteLine(article.Author);

                                    // url
                                    Console.WriteLine(article.Url);

                                    // published at
                                    Console.WriteLine(article.PublishedAt);

                                    Console.WriteLine();
                                    Console.WriteLine();
                                    
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Status: Error" + '\n' + "Code: API Key is missing" + '\n'+ "Message: Please check if the API key is properly given" + '\n');
                            break;
                        }
                    }

                }
                else if (controlInput == "2")
                {
                    //Keyword

                    Console.Write("Please choose a search word:");
                    articleTheme = Console.ReadLine();
                    Console.WriteLine();

                    //From Date
                    Console.Write("Please choose a Date FROM what time (Example: 2024.4.5) if you dont write anything Yesterday`s date will be applied:" + '\n');
                    fromDate = Console.ReadLine();
                    if (fromDate == "")
                    {
                        fromDate = yesterdayDate.ToString();
                    }
                    Console.WriteLine();

                    //To Date
                    Console.Write("Please choose a Date UNTIL what time (Example: 2024.4.5) if you dont write anything Today`s date will be applied" + '\n');
                    toDate = Console.ReadLine();
                    if (toDate == "")
                    {
                        toDate = today.ToString();
                    }
                    Console.WriteLine();

                    //Language Parameter

                    while (languageEnumExist == false)
                    {
                        Console.Write('\n' + "Write a Language`s MONOGRAM (with Capital letters)/(Type options if you would want to see the possible entries)");
                        languageInput = Console.ReadLine();

                        if (languageInput == "options")
                        {
                            int count = 1;
                            Languages[] enumlanguages = (Languages[])Enum.GetValues(typeof(Languages));

                            foreach (Languages userType in enumlanguages)
                            {

                                if (enumlanguages.Length != count)
                                {
                                    Console.Write(userType + ", ");
                                }
                                else
                                {
                                    Console.Write(userType);
                                }
                                count++;
                            }
                        }

                        if (Enum.TryParse(languageInput, true, out languageCheck))
                        {
                            Console.WriteLine();
                            languageEnumExist = true;
                            break;
                        }
                    }

                    //SortBy Parameter

                    while (sortByEnumExist == false)
                    {
                        Console.Write('\n' + "Write BASED on what to Sort By (Type options if you would want to see the possible entries)");
                        sortByInput = Console.ReadLine();

                        if (sortByInput == "options")
                        {
                            int count = 1;
                            SortBys[] enumSortBys = (SortBys[])Enum.GetValues(typeof(SortBys));

                            foreach (SortBys userType in enumSortBys)
                            {

                                if (enumSortBys.Length != count)
                                {
                                    Console.Write(userType + ", ");
                                }
                                else
                                {
                                    Console.Write(userType);
                                }
                                count++;
                            }

                        }

                        if (Enum.TryParse(sortByInput, true, out sortBysCheck))
                        {
                            Console.WriteLine();
                            sortByEnumExist = true;
                            break;
                        }
                    }


                    //Page
                    Console.Write("Write a Page amount: ");
                    page = Console.ReadLine();
                    while(!int.TryParse(page, out pageInt))
                    {
                        Console.Write("Write a Page amount: ");
                        page = Console.ReadLine();
                    }

                    Console.WriteLine();

                    //Page size
                    Console.Write("Write a Page size: ");
                    pageSize = Console.ReadLine();
                    while (!int.TryParse(pageSize, out pageSizeInt))
                    {
                        Console.Write("Write a Page amount: ");
                        pageSize = Console.ReadLine();
                    }
                  
                    Console.WriteLine();

                    //Source(s)
                    Console.Write("Write the Sources that you would like to read (bbc-news,the-verge) splitting with ,: ");
                    sourceInput = Console.ReadLine();
                    sourcesList = sourceInput.Split(",").ToList();
                    Console.WriteLine();

                    //Domain(s)
                    Console.Write("Write the Domains that you would like to read (bbc.co.uk,techcrunch.com)  splitting with , : ");
                    domainInput = Console.ReadLine();
                    domainsList = domainInput.Split(",").ToList();
                    Console.WriteLine();


                    EverythingRequest userInformation = (new EverythingRequest
                    {
                        Q = articleTheme,
                        SortBy = sortBysCheck,
                        Page = pageInt,
                        Language = languageCheck,
                        From = Convert.ToDateTime(fromDate),
                        To = Convert.ToDateTime(toDate),
                        PageSize = pageSizeInt,
                        Sources = domainsList,
                        Domains = sourcesList
                    }); ;

                    var articlesResponse = newsApiClient.GetEverything(userInformation);

                    //CHECKING THE RESPONSE STATUS IN CASE OF AN ERROR
                    if (articlesResponse.Status == Statuses.Ok)
                    {
                        foreach (var article in articlesResponse.Articles)
                        {

                            if (!article.Title.Contains("Remove"))
                            {
                                // title
                                Console.WriteLine(article.Title);

                                // author
                                Console.WriteLine(article.Author);

                                // url
                                Console.WriteLine(article.Url);

                                // published at
                                Console.WriteLine(article.PublishedAt);

                                Console.WriteLine();
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Status: Error" + '\n' + "Code: API Key is missing" + '\n' + "Message: Please check if the API key is properly given" + '\n');
                    }
                }
                else if (controlInput == "3")
                {
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("That value does not exist!");
                }

                BasicFunction(ref controlInput);
            }

        }
        public static void BasicFunction(ref string controlInput)
        {
            Console.WriteLine("Select the menu choice with the help of the numbers");
            Console.WriteLine("Get the newest news 1");
            Console.WriteLine("Custom Input 2");
            Console.WriteLine("Exit 3");
           
            controlInput = Console.ReadLine();
        }
    }
}