using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immediate_Window
{
    public class MyClass
    {
        List<Application> AppList = new List<Application>()
            {
                new Application() {Name = "App1", Type = "Web"},
                new Application() {Name = "App2", Type = "Desktop"},
                new Application() {Name = "App3", Type = "Mobile"},
                new Application() {Name = "App4", Type = "Web"},
                new Application() {Name = "App5", Type = "IoT"},
                new Application() {Name = "App6", Type = "Mobile"},
                new Application() {Name = "App7", Type = "Web"},
                new Application() {Name = "App8", Type = "Desktop"},
                new Application() {Name = "App9", Type = "Web"},
                new Application() {Name = "App10", Type = "Desktop"}
            };

        public void Foo(string text)
        {
            foreach (Application application in AppList)
            {
                Console.Out.WriteLine(application.Name);
            }
        }

        public static string Bar(string text)
        {

            return text.ToLower();
        }
    }

    public class Application
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
