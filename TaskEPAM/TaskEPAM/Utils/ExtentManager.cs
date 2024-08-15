using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace TaskEPAM.Utils
{
    public static class ExtentManager
    {
        private static ExtentReports _extent;
        private static readonly object _lock = new object();

        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                lock (_lock)
                {
                    if (_extent == null)
                    {
                        string path = Directory.GetParent(@"../../../").FullName
                  + Path.DirectorySeparatorChar + "Reports"
                  + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyy HHmmss") + ".html";

                        ExtentSparkReporter reporter = new ExtentSparkReporter(path);
                        _extent = new ExtentReports();
                        _extent.AttachReporter(reporter);
                    }
                }
            }
            return _extent;
        }
    }
}
