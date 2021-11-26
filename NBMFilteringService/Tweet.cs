/*
 * AUTHOR: Suparna Deb
 * DATE LAST MODIFIED: 15/11/2021
 * FILE NAME: Tweet.cs
 * PURPOSE: Tweet object class
 * LAYER: Business
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    public class Tweet : Message
    {
        public Tweet(string id, string sender, string text) : base(id, sender, text)
        {

        }

        public Tweet()
        {

        }
    }
}
