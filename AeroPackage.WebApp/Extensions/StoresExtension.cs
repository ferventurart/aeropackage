using System;
using AeroPackage.WebApp.Model.Common;

namespace AeroPackage.WebApp.Extensions;

public static class StoresExtension
{
    public static List<CommonStore> GetCommonStores()
    {
        return new List<CommonStore>()
        {
            new CommonStore() { Name = "Amazon", UrlLogo = GetSquareLogoUrl("amazon.com") },
            new CommonStore() { Name = "eBay", UrlLogo = GetSquareLogoUrl("ebay.com") },
            new CommonStore() { Name = "Walmart", UrlLogo = GetSquareLogoUrl("walmart.com") },
            new CommonStore() { Name = "AliExpress", UrlLogo = GetSquareLogoUrl("aliexpress.com") },
            new CommonStore() { Name = "Best Buy", UrlLogo = GetSquareLogoUrl("bestbuy.com") },
            new CommonStore() { Name = "Target", UrlLogo = GetSquareLogoUrl("target.com") },
            new CommonStore() { Name = "Zappos", UrlLogo = GetSquareLogoUrl("zappos.com") },
            new CommonStore() { Name = "ASOS", UrlLogo = GetSquareLogoUrl("asos.com") },
            new CommonStore() { Name = "Zara", UrlLogo = GetSquareLogoUrl("zara.com") },
            new CommonStore() { Name = "Adidas", UrlLogo = GetSquareLogoUrl("adidas.com") },
            new CommonStore() { Name = "Nike", UrlLogo = GetSquareLogoUrl("nike.com") },
            new CommonStore() { Name = "Forever 21", UrlLogo = GetSquareLogoUrl("forever21.com") },
            new CommonStore() { Name = "Macy's", UrlLogo = GetSquareLogoUrl("macys.com") },
            new CommonStore() { Name = "Nordstrom", UrlLogo = GetSquareLogoUrl("nordstrom.com") },
            new CommonStore() { Name = "Gap", UrlLogo = GetSquareLogoUrl("gap.com") },
            new CommonStore() { Name = "H&M", UrlLogo = GetSquareLogoUrl("hm.com") },
            new CommonStore() { Name = "Uniqlo", UrlLogo = GetSquareLogoUrl("uniqlo.com") },
            new CommonStore() { Name = "Lululemon", UrlLogo = GetSquareLogoUrl("lululemon.com") },
            new CommonStore() { Name = "Sephora", UrlLogo = GetSquareLogoUrl("sephora.com") },
            new CommonStore() { Name = "Ulta Beauty", UrlLogo = GetSquareLogoUrl("ulta.com") },
            new CommonStore() { Name = "Victoria's Secret", UrlLogo = GetSquareLogoUrl("victoriassecret.com") },
            new CommonStore() { Name = "Gymshark", UrlLogo = GetSquareLogoUrl("gymshark.com") },
            new CommonStore() { Name = "Shein", UrlLogo = GetSquareLogoUrl("shein.com") },
        };
    }

    private static string GetSquareLogoUrl(string domain)
    {
        return $"https://logo.clearbit.com/{domain}?size=28";
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

