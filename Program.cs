using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_sortNsearch
{
    class Program
    {
        static Random rand = new Random();

        static void sorting(List<String> arr, int first, int last)
        {
            String s = arr[(last - first) / 2 + first];
            string temp;
            int i = first, j = last;
            while (i <= j)
            {
                while (arr[i].CompareTo(s) < 0 && i <= last) ++i;
                while (arr[j].CompareTo(s) > 0 && j >= first) --j;
                if (i <= j)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    ++i; 
                    --j;
                }
            }
            if (j > first) sorting(arr, first, j);
            if (i < last) sorting(arr, i, last);
        }

        static bool check(List<String> arr, string s, int first, int last)
        {
            bool result;
            if (arr[first] == s || arr[last] == s) return true;
            if (last - first == 1) return false; 
            int i = (last - first) / 2 + first;
            String temp = arr[i];
            if (temp == s) return true;
            if (temp.CompareTo(s) < 0) result = check(arr, s, i, last); else result = check(arr, s, first, i);
            return result;

        }

        static String RandomString(int length)
        {
            String symbs = "abcdefghijklmnopqrstuvwxyz";
            String result="";
            for (int i=0; i<length; i++) result +=symbs[rand.Next(0,symbs.Length)]; 
            return result;
        }

        static void Main(string[] args)
        {
            //some consts
            int name_length = 7; 
            int name_count = 20000;
            int choosen_count = 1000;

            //array for test subdomain names
            String[] subdomain_ar = new String[20000];

            //array for domains - some domains will be in blacklist
            String[] domains = new String[] { ".push.lol.biz", ".wikipedia.com", ".test.au", ".ultimatecapper.com", ".u1j.in", ".afv.sepzfour.com", ".ripubcc.com" };

            List<String> blacklist = new List<String>();

            //create checking names 
            for (int i=0; i < name_count; i++)
                subdomain_ar[i] = RandomString(rand.Next(3,name_length))+domains[rand.Next(0,(domains.Length))];

            //get blacklist and sort it alphabetical
            try
            {
                using (StreamReader sr = new StreamReader("blacklist.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) blacklist.Add(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }
            sorting(blacklist, 0, blacklist.Count-1);

            //choose random names
            String[] check_names = new String[choosen_count];
            for (int i = 0; i < choosen_count; i++) check_names[i] = subdomain_ar[i * name_count/choosen_count + rand.Next(0, name_count/choosen_count)];

            //check choosen names and calc execution time
            Console.WriteLine("Names in blacklist:");
            DateTime start = DateTime.Now;
            String test;
            for (int i = 0; i < choosen_count; i++)
            {
                test = check_names[i];
                bool result = check(blacklist, test, 0, blacklist.Count - 1);
                int pos = 0;
                if (result == false)
                    do {
                        pos = test.IndexOf('.');
                        if (pos!=-1) 
                            test = test.Substring(pos+1, test.Length-pos-1);
                        result = check(blacklist, test, 0, blacklist.Count - 1);
                    } while (result == false && pos > 0);
                if (result) Console.WriteLine(check_names[i]);
            }
            TimeSpan sp = DateTime.Now - start;
            Console.WriteLine("Execution time: "+sp);
            Console.Read();
        }
    }
}
