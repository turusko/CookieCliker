namespace CookieClicks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var saveFile = "C:\\Users\\chris.fisher\\Downloads\\ChocolatePortalBakery.txt";
            var loadFromFile = true;
            var cookieSite = new CookieSite();

            bool counter = true;

            if (loadFromFile && File.Exists(saveFile))
            {
                cookieSite.LoadFromFile(saveFile);
            }

            MainLoop(cookieSite, counter, saveFile);
        }

        private static void MainLoop(CookieSite cookieSite, bool counter, string saveFile)
        {
            var time = DateTime.Now;

            while (counter)
            {
                var ClickCycle = 5;

                if (cookieSite.IsUpgradeAvaliable())
                {
                    cookieSite.GetUpgrades();
                }
                if (cookieSite.IsProductAvaliable())
                {
                    cookieSite.GetProducts();
                }

                while (ClickCycle > 0)
                {
                    cookieSite.ClickCookie();
                    ClickCycle--;
                }

                if ((DateTime.Now - time).TotalSeconds > 60)
                {
                    if (File.Exists(saveFile))
                    {
                        File.Delete(saveFile);
                    }
                    cookieSite.SaveToFile();
                    time = DateTime.Now;
                }
            }
        }
    }
}