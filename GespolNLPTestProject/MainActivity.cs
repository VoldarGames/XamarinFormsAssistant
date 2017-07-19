using System.IO;
using System.Reflection;
using System.Text;
using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using XamarinFormsAssistant.Mocks;

namespace XamarinFormsAssistantTest
{
    [Activity(Label = "XamarinFormsAssistantTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestSuiteActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            //MOCK PRECEPTOS
            using (StreamReader streamReader = new StreamReader(Assets.Open("Preceptos.txt"), Encoding.GetEncoding("iso-8859-1")))
            {
                while (streamReader.Peek() >= 0)
                {
                    MockPreceptos.AddPrecepto(streamReader.ReadLine());
                }
            }
            //////
         
            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}

