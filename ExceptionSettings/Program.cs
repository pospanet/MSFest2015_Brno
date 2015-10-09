using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionSettings
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Task<HttpWebResponse> ret = DoWorkAsync();
                Task.WaitAll(ret);
            }
            catch (Exception)
            {
                CleanUp();
            }
        }

        private static void CleanUp()
        {
            
        }

        private static async Task<HttpWebResponse> DoWorkAsync()
        {
            HttpWebRequest request =
                HttpWebRequest.CreateHttp("https://msfest2015.blob.core.windows.net/images/SAM_2336.JPG=0");
            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                return response;
            }
        }
    }
}
