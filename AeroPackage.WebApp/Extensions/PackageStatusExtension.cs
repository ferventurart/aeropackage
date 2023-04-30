using System;
using AeroPackage.WebApp.Model.Common;

namespace AeroPackage.WebApp.Extensions;

public static class PackageStatusExtension
{
    public static List<CommonPackageStatus> GetCommonPackageStatus()
    {
        return new List<CommonPackageStatus>()
        {
            new CommonPackageStatus() { Value = "PreAlert", Name = "Registrado", Icon = "img/pre-register.png" },
            new CommonPackageStatus() { Value = "ReceivedInUsa", Name = "Recibido en USA", Icon = "img/usa.png" },
            new CommonPackageStatus() { Value = "PreparingForShipment", Name = "Preparando para Env\u00edo", Icon = "img/weight-scale.png" },
            new CommonPackageStatus() { Value = "InTransitToDestinationCountry", Name = "En Transito a Pa\u00eds Destino", Icon = "img/flight-box.png" },
            new CommonPackageStatus() { Value = "ReceivedAtDestinationCountry", Name = "Recibido en Pa\u00eds Destino", Icon = "img/el-salvador.png" },
            new CommonPackageStatus() { Value = "CustomsClearanceInProcess", Name = "Procesandose en Aduana", Icon = "img/custom.png" },
            new CommonPackageStatus() { Value = "ReleasedFromCustoms", Name = "Liberado en Aduanas", Icon = "img/custom-clear.png" },
            new CommonPackageStatus() { Value = "StoredInWarehouse", Name = "Almacenado en Bodega ESA", Icon = "img/warehouse.png" },
            new CommonPackageStatus() { Value = "DeliveredToLocalCourier", Name = "Entregado a Transportista Local", Icon = "img/delivery-in-progress.png" },
            new CommonPackageStatus() { Value = "Delivered", Name = "Entregado", Icon = "img/delivered.png" },
        };
    }
}

