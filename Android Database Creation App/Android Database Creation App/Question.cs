using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Android_Database_Creation_App
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string Category { get; set; }
        public string Questions { get; set; }
        public string Answer { get; set; }



        public Question(string cat, string question, string answer)
        {

            Category = cat;
            Questions = question;
            Answer = answer;
        }

        public Question()
        {

        }
    }

  
}