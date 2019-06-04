using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class InfoModel
    {
        private static InfoModel s_instace = null;
        private Queue<string> samples;

        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                    s_instace.samples = new Queue<string>();
                }
                return s_instace;
            }
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";

        // inserting all samples to queue
        public void ReadData(string fileName)
        {
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            if (File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);        // reading all the lines of the file

                for (int i = 0; i < lines.Length; i++)
                {
                    samples.Enqueue(lines[i]);
                }
            }
        }

        //save the sample in the file
        public void SaveSample(string sample, string fileName, bool toCreateFile)
        {
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            if (toCreateFile)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            //write sample to file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(sample);
            }
        }
        // dequeue from sample if we need the oldest sample
        public string GetSample()
        {
            return samples.Dequeue();
        }
        // get number of samples
        public int GetNumOfSamples()
        {
            return samples.Count();
        }
    }
}