using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMonitor.Simulator
{
    internal class LocationProvider
    {
        // Hold boundary conditions for longitude and latitude
        // Don't need to calculate more than once
        private static int _integralMaxLat;
        private static int _fractionalMaxLat;
        private static int _integralMinLat;
        private static int _fractionalMinLat;
        private static int _integralMaxLong;
        private static int _fractionalMaxLong;
        private static int _integralMinLong;
        private static int _fractionalMinLong;

        private static readonly Random _latRandom = new Random(Guid.NewGuid().GetHashCode());
        private static readonly Random _longRandom = new Random(Guid.NewGuid().GetHashCode());
        private static Lazy<LocationProvider> _instance = new Lazy<LocationProvider>(() => new LocationProvider());

        public static LocationProvider Instance => _instance.Value;

        public LocationProvider()
        {
            // Longitude and Latitude boundaries within which to create event locations
            // Example rectangle that describes the bulk of England without hitting sea
            // Bottom left 51.010299, -3.114624 (Taunton)
            // Bottom right 51.083686, -0.145569 (Mid Sussex)
            // Top left 53.810382, -3.048706 (Blackpool)
            // Top right 53.745462, -0.346069 (Hull)
            // Use these as default if not supplied in args
            decimal maxLat = 53.810382m;
            decimal minLat = 51.010299m;
            decimal maxLong = -0.145569m;
            decimal minLong = -3.048706m;

            // Break the coordinates into integral and fractional components
            // So that each part can be randomly created within the right boundaries
            _integralMaxLat = (int)maxLat;
            decimal decFractionalMaxLat = maxLat - _integralMaxLat;
            _fractionalMaxLat = (int)(decFractionalMaxLat * GetMultiplyer(decFractionalMaxLat));

            _integralMinLat = (int)minLat;
            decimal decFractionalMinLat = minLat - _integralMinLat;
            _fractionalMinLat = (int)(decFractionalMinLat * GetMultiplyer(decFractionalMinLat));

            _integralMaxLong = (int)maxLong;
            decimal decFractionalMaxLong = maxLong - _integralMaxLong;
            _fractionalMaxLong = (int)(decFractionalMaxLong * GetMultiplyer(decFractionalMaxLong));

            _integralMinLong = (int)minLong;
            decimal decFractionalMinLong = minLong - _integralMinLong;
            _fractionalMinLong = (int)(decFractionalMinLong * GetMultiplyer(decFractionalMinLong));

            FlipIfNegative(ref _fractionalMaxLong, ref _fractionalMinLong);
            FlipIfNegative(ref _fractionalMaxLat, ref _fractionalMinLat);
        }

        public decimal GetLatitude()
        {
            int latIntegral = _latRandom.Next(_integralMinLat, _integralMaxLat + 1);
            int latFractional = _latRandom.Next(_fractionalMinLat, _fractionalMaxLat + 1);
            return latIntegral + (latFractional / 1000000m);
        }

        public decimal GetLongitude()
        {
            int longIntegral = _longRandom.Next(_integralMinLong, _integralMaxLong + 1);
            int longFractional = _longRandom.Next(_fractionalMinLong, _fractionalMaxLong + 1);
            return longIntegral + (longFractional / 1000000m);
        }

        private static int GetMultiplyer(decimal value)
        {
            int factor;

            switch (value.ToString().Length)
            {
                case 1:
                    factor = 10;
                    break;
                case 2:
                    factor = 100;
                    break;
                case 3:
                    factor = 1000;
                    break;
                case 4:
                    factor = 10000;
                    break;
                case 5:
                    factor = 100000;
                    break;
                default:
                    factor = 1000000;
                    break;
            }

            return factor;
        }

        private static void FlipIfNegative(ref int max, ref int min)
        {
            // Deal with negative Longitudes and Latitudes, 
            // so that when getting random number the min and max work correctly
            if (max < 0 && min < 0)
            {
                // Swap them
                int tmpMax = max;
                int tmpMin = min;

                max = tmpMin;
                min = tmpMax;
            }
        }
    }
}
