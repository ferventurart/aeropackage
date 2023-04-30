using System;
namespace AeroPackage.Domain.PackageAggregate.Constants;

public static class CourierTrackingNumberRegex
{
    public const string AmazonTrackingNumber = @"^TBA\d{9}(.\d{1,3})?$";
    public const string UpsTrackingNumber = @"^1Z[0-9A-Z]{16}$";
    public const string FedexTrackingNumber = @"^(\d{15}|\d{12})$";
    public const string DhlExpressTrackingNumber = @"^([0-9]{10}|[0-9]{12})$";
    public const string UspsTrackingNumber = @"^(91|92|\w{2})\d{20}$|^(7\d|03|23|13|43)\d{18}$|^(82|00)\d{20}$|^(70|14|23|03)\d{14}$|^(M0|M1)\d{16}$";
    public const string RoyalMailTrackingNumber = @"^[A-Za-z]{2}[0-9]{9}GB$|^[A-Za-z]{2}[0-9]{9}$";
    public const string CanadaPostTrackingNumber = @"^[A-Za-z]{2}\d{9}CA$";
    public const string AustraliaPostTrackingNumber = @"^[A-Za-z]{2}\d{9}AU$";
    public const string DeutschePostTrackingNumber = @"^\d{12}$";
    public const string JapanPostTrackingNumber = @"^\d{13}$";
    public const string ChinaPostTrackingNumber = @"^[A-Za-z]{2}\d{9}CN$";
    public const string TntExpressTrackingNumber = @"^([0-9]{8}|[0-9]{9}|GD[0-9]{10}|GE[0-9]{10})$";
    public const string AramexTrackingNumber = @"^([0-9]{9}|[0-9]{10})$";
    public const string OnTracTrackingNumber = @"^\d{15}$|^\d{12}$|^\d{14}[A-Z]$";
    public const string PurolatorTrackingNumber = @"^[A-Za-z]{2}\d{9}CA$";
    public const string GlsTrackingNumber = @"^\d{11}[A-Z]{2}$";
    public const string LaserShipTrackingNumber = @"^L[A-Za-z]\d{8}$";
    public const string NewgisticsTrackingNumber = @"^9[0-9]{27}$";
    public const string OldDominionTrackingNumber = @"^\d{10}$|^\d{12}$|^\d{9}[A-Z]$|^[A-Z]\d{8}$";
    public const string PittOhioTrackingNumber = @"^\d{9}$";
    public const string SaiaTrackingNumber = @"^\d{12}$";
    public const string SoutheasternTrackingNumber = @"^\d{9}$";
    public const string EstesTrackingNumber = @"^[A-Z]{3}\d{7}$";
    public const string RlCarriersTrackingNumber = @"^\d{9}$|^\d{10}$";
}