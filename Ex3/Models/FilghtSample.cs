using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ex3.Models
{

    public class Location
    {
        public float longtitude { get; set; }
        public float latitude { get; set; }

        public Location(float lon, float lat)
        {
            this.longtitude = lon;
            this.latitude = lat;
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Location");
            writer.WriteElementString("lon", this.longtitude.ToString());
            writer.WriteElementString("lat", this.latitude.ToString());
            writer.WriteEndElement();
        }
    }

    public class FlightSample
    {
        public Location location { get; set; }
        public float throttle { get; set; }
        public float rudder { get; set; }

        public FlightSample(float lon, float lat, float throttle, float rudder)
        {
            this.location = new Location(lon, lat);
            this.throttle = throttle;
            this.rudder = rudder;
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("FlightSample");
            location.ToXml(writer);
            writer.WriteElementString("throttle", throttle.ToString());
            writer.WriteElementString("rudder", rudder.ToString());
            writer.WriteEndElement();
        }
    }
}