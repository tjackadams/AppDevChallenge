﻿using SecurityMonitor.Core.Domain;
using System;

namespace SecurityMonitor.Core.Models
{
    public class Device : IEntity
    {
        private Device() { }

        public static Device New(int id, string name, double lat, double lng)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name), "Device name must be supplied.");

            return new Device
            {
                Id = id,
                Name = name,
                Latitude = lat,
                Longitude = lng

            };
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string ImageUrl { get; private set; }
    }
}