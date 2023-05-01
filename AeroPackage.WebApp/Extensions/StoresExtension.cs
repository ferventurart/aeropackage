using System;
using AeroPackage.WebApp.Model.Common;

namespace AeroPackage.WebApp.Extensions;

public static class StoresExtension
{
    public static List<CommonStore> GetCommonStores()
    {
        return new List<CommonStore>()
        {
            new CommonStore() { Name = "Apple", UrlLogo = @"img/apple.svg" },
            new CommonStore() { Name = "Amazon", UrlLogo = @"img/amazon.svg" },
            new CommonStore() { Name = "eBay", UrlLogo =  @"img/ebay.svg" },
            new CommonStore() { Name = "Walmart", UrlLogo = @"img/walmart.svg" },
            new CommonStore() { Name = "AliExpress", UrlLogo =  @"img/aliexpress.svg" },
            new CommonStore() { Name = "Best Buy", UrlLogo = @"img/best-buy.svg" },
            new CommonStore() { Name = "Target", UrlLogo =  @"img/target.svg" },
            new CommonStore() { Name = "Zara", UrlLogo = @"img/zara.svg"  },
            new CommonStore() { Name = "Adidas", UrlLogo = @"img/adidas.svg" },
            new CommonStore() { Name = "Nike", UrlLogo = @"img/nike.svg"  },
            new CommonStore() { Name = "Forever 21", UrlLogo = @"img/forever-21.svg" },
            new CommonStore() { Name = "Macy's", UrlLogo =  @"img/macys.svg" },
            new CommonStore() { Name = "Nordstrom", UrlLogo = @"img/nordstrom.svg" },
            new CommonStore() { Name = "Gap", UrlLogo = @"img/gap.svg"},
            new CommonStore() { Name = "H&M", UrlLogo =  @"img/h&m.svg" },
            new CommonStore() { Name = "Sephora", UrlLogo = @"img/sephora.svg" },
            new CommonStore() { Name = "Victoria's Secret", UrlLogo = @"img/victoria-secret.svg" },
            new CommonStore() { Name = "Gymshark", UrlLogo =  @"img/gymshark.svg" },
            new CommonStore() { Name = "Shein", UrlLogo =  @"img/shein.svg" },
        };
    }

    public static string GetLogoOfCommonStoreByName(string name)
    {
        return GetCommonStores().Single(s => s.Name == name).UrlLogo;
    }

    public static CommonStore GetStoreByName(string name)
    {
        return GetCommonStores().Single(s => s.Name == name);
    }
}

