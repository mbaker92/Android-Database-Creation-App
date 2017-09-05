using Android.App;
using Android.Widget;
using Android.OS;
using SQLite;
using System.IO;
using Android.Content;

namespace Android_Database_Creation_App
{
    [Activity(Label = "Android_Database_Creation_App", MainLauncher = true)]
    public class MainActivity : Activity
    {
        
        private EditText CategoryField;
        private EditText QuestionField;
        private EditText AnswerField;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            // Get the Create Button and Add Button on Main Screen
            Button AddButton = FindViewById<Button>(Resource.Id.addToDB);
            Button CreateButton = FindViewById<Button>(Resource.Id.createButton);


            // Get Fields that User will use
            CategoryField = FindViewById<EditText>(Resource.Id.categoryField);
            QuestionField = FindViewById<EditText>(Resource.Id.questionField);
            AnswerField = FindViewById<EditText>(Resource.Id.answerField);

            // Get the Database Path on the system
            string DBPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DatabaseCreated.db3");

            // If there is already a DatabaseCreated.db3 file, then delete it
            if (System.IO.File.Exists(DBPath))
            {
                System.IO.File.Delete(DBPath);
            }

            // Create the Connection
            var DB = new SQLiteConnection(DBPath);

            // Create a Table using the Question Class
            DB.CreateTable<Question>();


            AddButton.Click += delegate
            {
                // If all fields have text then add information to the database
                if((AnswerField.Text != "") && (QuestionField.Text != "") && (CategoryField.Text != ""))
                {
                    Question temp = new Question(CategoryField.Text, QuestionField.Text, AnswerField.Text);
                    DB.Insert(temp);
                }


                // Reset the fields to blank
                AnswerField.Text = "";
                QuestionField.Text = "";
                CategoryField.Text = "";

            };


            CreateButton.Click += delegate
            {

                // Close the database
                DB.Close();

                // Get path to the Android Download Directory
                var backup = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);

                // Get the fullpath with filename of Android Download Directory
                string DownloadPath = Path.Combine(backup.AbsolutePath, "DatabaseCreated.db3");

                // Copy Database to the Download Directory. If there is already a DatabaseCreated.db3 file, then overwrite it.
                System.IO.File.Copy(DBPath, DownloadPath, true);

            };


        }

    }
}

