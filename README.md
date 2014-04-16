SortAndSearch
=============

C# test task

Here is a problem description to solve:
We would like to build domain name based blacklist/filter. Our database contains around 20000 domain names, including Top Level Domains, Second-level and Lower level domains, for example:

.biz 
stackoverflow.com 
ru.wikipedia.com

We would like to perform lookups to see if input URL domain name is listed in the blacklist. 

Simply looping over the list and doing string based comparison is also not an option. If I have to do 1000 lookups it's simply too slow. 

Lookup must return positive match if TLD is blacklisted, for example, if .biz TLD is blacklisted, mycompany.biz should return positive match. On the other hand, fr.wikipedia.com should not match, because sub-domain is different.

Your task is to write sample .NET console application that builds up in-memory collection of 20000 domain names and perform lookups of 1000 random domain names against the blacklist. I attached list of random domain names you can use as sample source.
