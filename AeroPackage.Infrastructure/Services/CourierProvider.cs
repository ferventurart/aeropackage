using System;
using System.Text.RegularExpressions;
using AeroPackage.Application.Common.Interfaces.Services;
using AeroPackage.Domain.PackageAggregate.ValueObjects;
using AeroPackage.Domain.PackageAggregate.Constants;

namespace AeroPackage.Infrastructure.Services;

public class CourierProvider : ICourierProvider
{
    public Courier? ValidateTrackingNumber(string trackingNumber)
    {
        try
        {
            // Lista de expresiones regulares de servicios de mensajería
            var regexes = new (string, string)[]
            {
            (CourierTrackingNumberRegex.AmazonTrackingNumber, "Amazon"),
            (CourierTrackingNumberRegex.UpsTrackingNumber, "UPS"),
            (CourierTrackingNumberRegex.FedexTrackingNumber, "FedEx"),
            (CourierTrackingNumberRegex.DhlExpressTrackingNumber, "DHL Express"),
            (CourierTrackingNumberRegex.UspsTrackingNumber, "USPS"),
            (CourierTrackingNumberRegex.RoyalMailTrackingNumber, "Royal Mail"),
            (CourierTrackingNumberRegex.CanadaPostTrackingNumber, "Canada Post"),
            (CourierTrackingNumberRegex.AustraliaPostTrackingNumber, "Australia Post"),
            (CourierTrackingNumberRegex.DeutschePostTrackingNumber, "Deutsche Post"),
            (CourierTrackingNumberRegex.JapanPostTrackingNumber, "Japan Post"),
            (CourierTrackingNumberRegex.ChinaPostTrackingNumber, "China Post"),
            (CourierTrackingNumberRegex.TntExpressTrackingNumber, "TNT Express"),
            (CourierTrackingNumberRegex.AramexTrackingNumber, "Aramex")
            };

            // Itera a través de las regexes para encontrar la que haga match con el número de rastreo
            foreach (var regex in regexes)
            {
                if (Regex.IsMatch(trackingNumber, regex.Item1))
                {
                    string courierName = regex.Item2;
                    string logo = $"https://logo.clearbit.com/{regex.Item2.Replace(" ", "").ToLower()}.com";
                    if (regex.Item2 == "TNT Express")
                    {
                        logo = "https://www.tnt.com/express/en_us/site/home.html";
                    }
                    if (regex.Item2 == "Japan Post")
                    {
                        logo = "https://www.post.japanpost.jp/index_en.html";
                    }
                    if (regex.Item2 == "DHL Express")
                    {
                        logo = "https://logo.clearbit.com/www.dhl.com";
                    }
                    if(regex.Item2 == "China Post")
                    {
                        logo = "https://dvow0vltefbxy.cloudfront.net/assets/landing/carriers/china-post-2c839e6966a8fc1a5efa387397e910918ae859d980e42b0763a17b7a8c0b005f.svg";
                    }
                    // Si encuentra una match, devuelve un objeto CourierInfo con el nombre del courier y el URL del logo
                    return Courier.Create(courierName, logo);
                }
            }
            return Courier.Create(string.Empty, string.Empty);
        }
        catch (Exception ex)
        {
            return Courier.Create(string.Empty, string.Empty);
        }
        
    }
}

