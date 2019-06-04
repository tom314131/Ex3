using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ex3.Models;

namespace Ex3.Controllers
{
    public class SaverController : Controller
    {

        public ActionResult Save(string ip, int port, int rate, int time, string name)
        {
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
            {
                SimulatorClient.Instance.Connect(ip, port);
            }
            ViewBag.rate = rate;
            Session["rate"] = rate;
            Session["iterations"] = rate * time;
            ViewBag.fileName = name;
            return View("SaveOnline");
        }
        // Saving the data sample in the file
        public void SaveDataSample(string data, string fileName, bool toCreateFile)
        {
            InfoModel.Instance.SaveSample(data, fileName, toCreateFile);
        }
    }
}