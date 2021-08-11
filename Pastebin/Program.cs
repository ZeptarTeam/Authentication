﻿using System;
using System.Net;
using System.Management;

namespace Pastebin
{
    class Program
    {
        static string authLink = "https://pastebin.com";

        static void Main()
        {
            Console.Title = "C# Authentication";

            WebClient webClient = new WebClient();
            try {

                webClient.DownloadString(authLink);

            }
            catch (Exception) {

                Console.WriteLine("Could not establish a stable connection to the authentication server, is it down?", Console.ForegroundColor = ConsoleColor.Red);
                Console.ReadKey();

            }
            bool authenticated = webClient.DownloadString(authLink).Contains(getHWID());
            if (authenticated)
            {
                Console.WriteLine("Authenticated successfully.", Console.ForegroundColor = ConsoleColor.Green);
                /** Do whatever you want here **/
                Console.Read();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Could not find HWID in auth server.");
            Console.WriteLine("Your HWID: " + getHWID());
            Console.ReadKey();
        }

        static string getHWID()
        {
            string id = "";

            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            return id;

        }
    }
}