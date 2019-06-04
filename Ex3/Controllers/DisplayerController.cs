using System;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using Ex3.Models;

namespace Ex3.Controllers
{
    public class DisplayerController : Controller
    {

        public ActionResult Index()
        {
            return View("View");
        }

        public ActionResult Display(string first, int second, int rate = 0)
        {
            IPAddress address;
            if (IPAddress.TryParse(first, out address))
            {
                SimulatorClient.Instance.Connect(first, second);
                return DisplayOnline(first, second, rate);
            }
            else
            {
                return DisplaySavedMap(first, second);
            }
        }

        ActionResult DisplayOnline(string ip, int port, int rate)
        {
            ViewBag.rate = rate;
            return View("DisplayOnline");
        }

        ActionResult DisplaySavedMap(string fileName, int rate)
        {
            InfoModel.Instance.ReadData(fileName);
            Session["rate"] = rate;
            Session["SavedLocations"] = InfoModel.Instance.GetNumOfSamples();
            return View("DisplayOffline");
        }

        // get the data from the simulator
        [HttpPost]
        public string GetDataFromSimulator(string query)
        {
            float[] data = SimulatorClient.Instance.GetDataFromSimulator(query);
            return ToXml(new FlightSample(data[0], data[1], data[2], data[3]));
        }

        // get data from file
        [HttpPost]
        public string GetDataFromFile()
        {
            string[] info = InfoModel.Instance.GetSample().Split(',');
            return ToXml(new FlightSample(float.Parse(info[0]),
                                            float.Parse(info[1]),
                                            float.Parse(info[2]),
                                            float.Parse(info[3])));
        }

        //converting FlightSample to XML
        string ToXml(FlightSample sample)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            sample.ToXml(writer);
            writer.WriteEndDocument();
            writer.Flush();

            return sb.ToString();
        }
    }
}