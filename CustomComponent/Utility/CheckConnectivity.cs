using System;
using Plugin.Connectivity;
using System.Collections.Generic;
using Plugin.Connectivity.Abstractions;
using System.Diagnostics;

namespace UtilityLibrary.Utility
{
    public class CheckConnectivity
    {

        /// <summary>
        /// Check Internet Status
        /// </summary>
        /// <returns><c>true</c>, if internet was checked, <c>false</c> otherwise.</returns>
        public bool CheckInternet()
        {
            if (!CrossConnectivity.IsSupported)
                return true;
            return CrossConnectivity.Current.IsConnected;
        }

        /// <summary>
        /// Gets the list of all active connection types.
        /// </summary>
        public ConnectionType CheckConnectionType()
        {
            ConnectionType connType = ConnectionType.Other;
            try
            {
                IEnumerable<ConnectionType> connectionType = CrossConnectivity.Current.ConnectionTypes;
                foreach (var item in connectionType)
                {
                    if (item == ConnectionType.WiFi)
                    {
                        connType = item;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return connType;
        }

        /// <summary>
        /// Retrieves a list of available bandwidths for the platform in bits per second.
        /// Only active connections.
        /// 
        /// Apple Platforms: Bandwidths are not supported and will always return an empty list.
        /// Android: In releases earlier than 3.0.2 this was returned as Mbps.
        /// Android: Only returns bandwidth of WiFi connections.For all others you can check the
        /// </summary>
        public UInt64 CheckBandWidths()
        {
            UInt64 value = 0;
            try
            {
                IEnumerable<UInt64> bandWidths = CrossConnectivity.Current.Bandwidths;
                foreach (var item in bandWidths)
                {
                    if (item > 160000)
                    {
                        value = item;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return value;
        }

        #region ConnectivityChanged
        //CrossConnectivity.Current.ConnectivityTypeChanged += async(sender, args) =>
        //{
        //    Debug.WriteLine($"Connectivity changed to {args.IsConnected}");
        //    foreach(var t in args.ConnectionTypes)
        //      Debug.WriteLine($"Connection Type {t}");
        //};



        //CrossConnectivity.Current.ConnectivityChanged += async(sender, args) =>
        //{
        //    Debug.WriteLine($"Connectivity changed to {args.IsConnected}");
        //};
        #endregion
    }
}
