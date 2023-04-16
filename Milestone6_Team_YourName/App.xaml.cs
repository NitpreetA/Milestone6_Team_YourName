using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Milestone6_Team_YourName
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string fileName = "HomeBudgetSettings.txt";

        public App()
        {
            this.Properties["SessionCount"] = 0;
        }

        private void App_Start(object sender, EventArgs e)
        {
            // Load the application-scope properties from isolated storage
            IsolatedStorageFile storage = IsolatedStorageFile.GetMachineStoreForDomain();
            if (storage.FileExists(fileName))
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Read each line and parse the key/value pair
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        this.Properties[parts[0]] = parts[1];
                    }
                }
            }
  
 
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetMachineStoreForDomain();
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Persist each application-scope property individually
                foreach (string key in this.Properties.Keys)
                {
                    writer.WriteLine("{0},{1}", key, this.Properties[key]);
                }
            }
        }

    }
}
