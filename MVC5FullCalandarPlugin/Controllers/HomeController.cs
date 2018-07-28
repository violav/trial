using MVC5FullCalandarPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace MVC5FullCalandarPlugin.Controllers
{
    public class HomeController : Controller
    {
        #region Index method

        private DateTime _dt;
        private string _userId;

   
        string userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        DateTime dt
        {
            get { return _dt; }
            set { _dt = value; }
        }

        private string _path = @"C:\Users\Viola\Downloads\calendar\testsees\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\PublicHoliday.json";
        /// <summary>
        /// GET: Home/Index method.
        /// </summary>
        /// <returns>Returns - index view page</returns> 
        public ActionResult Index()
        {
            List<Risorsa> risorse = new List<Risorsa>();

            //Models.Risorsa ris = new Risorsa();
            //ris.id = "1";
            //ris.eventColor = "Green";
            //ris.title = "Mario Rossi";

            //Models.Risorsa ris1 = new Risorsa();
            //ris1.id = "1";
            //ris1.eventColor = "Blue";
            //ris1.title = "Mario Bianchi";

            //risorse.Add(ris);
            //risorse.Add(ris1);

            // string t = JsonConvert.SerializeObject(risorse);


            /* [
               {
                 "id": "1",
                 "eventColor": "Green",
                 "title": "Mario Rossi"
               },
               {
                 "id": "1",
                 "eventColor": "Blue",
                 "title": "Mario Bianchi"
               }
             ],*/

            ViewBag.Risorse = risorse;
            // Info.
            return this.View();
        }

        #endregion

        #region Get Calendar data method.

        /// <summary>
        /// GET: /Home/GetCalendarData
        /// </summary>
        /// <returns>Return data</returns>
        public ActionResult GetCalendarData()
        {
            // Initialization.
            JsonResult result = new JsonResult();

            try
            {
                // Loading.
                List<PublicHoliday> data = this.LoadData(null);
                // Processing.
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            return result;
        }




        
        public JsonResult GetRsouerceData()
        {
            // Initialization.
            JsonResult result = new JsonResult();

            try
            {
                StreamReader file = System.IO.File.OpenText(@"C:\Users\Viola\Downloads\calendar\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\Risorse.json");
          
                JsonSerializer serializer = new JsonSerializer();
                List<Risorsa> risorse = (List<Risorsa>)serializer.Deserialize(file, typeof(List<Risorsa>));

                // Processing.
                result = this.Json(risorse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            return result;
        }


        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        //private List<PublicHoliday> LoadData(string evtId)
        //{
        //    // Initialization.
        //    List<PublicHoliday> lst = new List<PublicHoliday>();

        //    try
        //    {
        //        // Initialization.
        //        string line = string.Empty;
        //        //string srcFilePath = "Content/files/PublicHoliday.txt";
        //        //var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        //        //var fullPath = Path.Combine(rootPath, srcFilePath);
        //        //string filePath = new Uri(fullPath).LocalPath;
        //        //StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

        //        string srcFilePath = "Content/files/PublicHoliday.json";
        //        var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        //        var fullPath = Path.Combine(rootPath, srcFilePath);
        //        string filePath = new Uri(fullPath).LocalPath;
        //        StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

        //        JsonSerializer serializer = new JsonSerializer();
        //        List<PublicHoliday> risorse = (List<PublicHoliday>)serializer.Deserialize(srcFilePath, typeof(List<PublicHoliday>));


        //        // Read file.
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            // Initialization.
        //            PublicHoliday infoObj = new PublicHoliday();
        //            string[] info = line.Split(',');

        //            // Setting.
        //            infoObj.Sr = Convert.ToInt32(info[0].ToString());
        //            infoObj.Title = info[4].ToString();
        //            infoObj.Desc = info[4].ToString();
        //            infoObj.Start_Date = Convert.ToDateTime(info[2].ToString());
        //            infoObj.End_Date = Convert.ToDateTime(info[3].ToString());
        //            // infoObj.Colore = info[5].ToString();
        //            infoObj.Id = Convert.ToInt32(info[0].ToString());
        //            infoObj.ResourceId = info[1].ToString();

        //            // Adding.
        //            lst.Add(infoObj);
        //        }

        //        // Closing.
        //        sr.Dispose();
        //        sr.Close();

        //        if (!string.IsNullOrEmpty(evtId))
        //        {
        //            return lst.Where(t => t.Id.ToString() == evtId).ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        // info.
        //        Console.Write(ex);
        //    }

        //    // info.
        //    return lst;
        //}



        private List<PublicHoliday> LoadData(string evtId)
        {
            // Initialization.
            List<PublicHoliday> lst = new List<PublicHoliday>();

            try
            {
                // Initialization.
                string line = string.Empty;
                //string srcFilePath = "Content/files/PublicHoliday.txt";
                //var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                //var fullPath = Path.Combine(rootPath, srcFilePath);
                //string filePath = new Uri(fullPath).LocalPath;
                //StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

               // string srcFilePath = @"C:\Users\Viola\Downloads\calendar\testsees\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\PublicHoliday.json";
                //var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                //var fullPath = Path.Combine(rootPath, srcFilePath);
                //string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(_path, FileMode.Open, FileAccess.Read));

                JsonSerializer serializer = new JsonSerializer();
                lst = (List<PublicHoliday>)serializer.Deserialize(sr, typeof(List<PublicHoliday>));


                // Read file.
                //while ((line = sr.ReadLine()) != null)
                //{
                //    // Initialization.
                //    PublicHoliday infoObj = new PublicHoliday();
                //    string[] info = line.Split(',');

                //    // Setting.
                //    infoObj.Sr = Convert.ToInt32(info[0].ToString());
                //    infoObj.Title = info[4].ToString();
                //    infoObj.Desc = info[4].ToString();
                //    infoObj.Start_Date = Convert.ToDateTime(info[2].ToString());
                //    infoObj.End_Date = Convert.ToDateTime(info[3].ToString());
                //    // infoObj.Colore = info[5].ToString();
                //    infoObj.Id = Convert.ToInt32(info[0].ToString());
                //    infoObj.ResourceId = info[1].ToString();

                //    // Adding.
                //    lst.Add(infoObj);
                //}

                // Closing.
                sr.Dispose();
                sr.Close();

                if (!string.IsNullOrEmpty(evtId))
                {
                    return lst.Where(t => t.Id.ToString() == evtId).ToList();
                }
                else return lst;
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion

        //public ActionResult InsertSchedulation(string userId, DateTime dt)
        //{
        //    try
        //    {
        //        Schedulazione sche = new Schedulazione();
        //        Risorsa ri = new Risorsa();
        //        ri.id = userId;
        //        ri.title = "test";

        //        sche.risorse = new List<Risorsa>();
        //        StreamReader file = System.IO.File.OpenText(@"C:\Users\Viola\Downloads\calendar\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\Risorse.json");

        //        JsonSerializer serializer = new JsonSerializer();
        //        sche.risorse = (List<Risorsa>)serializer.Deserialize(file, typeof(List<Risorsa>));

        //        ViewBag.RisorsaSel = ri;

        //        //return View("AddDate", sche.risorse);
        //        return RedirectToAction("AddDate");
        //    }
        //    catch (Exception exc)
        //    {
        //        return null;
        //    }
        //}


        public ActionResult ModifyEvent(string evtId)
        {
            try
            {             
                StreamReader file = System.IO.File.OpenText(@"C:\Users\Viola\Downloads\calendar\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\Risorse.json");

                List<PublicHoliday> dati = LoadData(evtId);

                PublicHoliday ph = dati == null ? null : dati[0];


                JsonSerializer serializer = new JsonSerializer();
                List<Risorsa> risorse = (List<Risorsa>)serializer.Deserialize(file, typeof(List<Risorsa>));

                Schedulazione sche = new Schedulazione();
                Risorsa ri = new Risorsa();

                sche.publicHoliday = ph;
                //sche.publicHoliday.End_Date = sche.publicHoliday.End_Date == null ? new DateTime(9999, 12, 31) : new DateTime(sche.publicHoliday.End_Date.Year, sche.publicHoliday.End_Date.Month, sche.publicHoliday.End_Date.Day);
                //sche.publicHoliday.Start_Date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                ri = risorse.Where(t => t.id == ph.ResourceId).FirstOrDefault();

                sche.Risorsa = risorse;
                ViewBag.RisorsaSel = ri.id;
                ViewBag.Risorse = risorse;

                return View("AddDate", sche);
            }
            catch (Exception exc)
            {
                return null;
            }
        }


        private PublicHoliday GetDato(FormCollection collection)
        {
            try
            {                                
                PublicHoliday ph = new PublicHoliday();

               // if (eventi != null) ph = eventi.Where(t => t.Id == Int32.Parse(collection["publicHoliday.id"].ToString())).LastOrDefault();

                ph.Desc = collection["publicHoliday.Desc"].ToString();
                string inizio = collection["publicHoliday.Start_Date"] + " " + collection["oraInizio"].ToString();
                string fine = collection["publicHoliday.End_Date"] + " " + collection["oraFine"].ToString();
                ph.Start_Date = Convert.ToDateTime(inizio);
                ph.End_Date = Convert.ToDateTime(fine);
                ph.Id = Int32.Parse(collection["publicHoliday.id"].ToString());
                ph.Title = collection["publicHoliday.Title"].ToString();

                List<PublicHoliday> lista = LoadData(string.Empty);
                int nMax = lista.Max(t => t.Id) + 1;
                ph.Id = nMax;
                ph.ResourceId = collection["Risorsa"];

                return ph;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [HttpPost]
        public ActionResult ModifyEvent(FormCollection collection)
        {
            try
            {
               
                Schedulazione sche = new Schedulazione();
                Risorsa ri = new Risorsa();

                if (ModelState.IsValid)
                {
                    List<PublicHoliday> phl = LoadData(string.Empty);
                    
                    PublicHoliday ph = phl.Where( t => t.Id == Int32.Parse(collection["publicHoliday.id"].ToString())).Last();
                    //ph.Desc = collection["publicHoliday.Desc"].ToString();
                    //ph.Start_Date = Convert.ToDateTime(collection["publicHoliday.Start_Date"]);
                    //ph.End_Date = Convert.ToDateTime(collection["publicHoliday.End_Date"]);
                    //ph.Id = int.Parse(collection["publicHoliday.id"]);
                    //ph.ResourceId = collection["Risorsa"];
                    //ph.Title = collection["publicHoliday.Title"].ToString();

                    PublicHoliday ph_got = GetDato(collection);
                    ph.Desc = ph_got.Desc;
                    ph.Start_Date = ph_got.Start_Date;
                    ph.End_Date = ph_got.End_Date;
                    ph.Id = ph_got.Id;
                    ph.ResourceId = ph_got.ResourceId;
                    ph.Title = ph_got.Title;

                    //string startToWrite = ph.Start_Date.Year + "-" + ph.Start_Date.Month.ToString().PadLeft(2, '0') + "-" + ph.Start_Date.Day.ToString().PadLeft(2, '0') + "T" + ph.Start_Date.Hour + ":" + ph.Start_Date.Minute + ":" + ph.Start_Date.Second;
                    //string endToWrite = ph.End_Date.Year + "-" + ph.End_Date.Month.ToString().PadLeft(2, '0') + "-" + ph.End_Date.Day.ToString().PadLeft(2, '0') + "T" + ph.End_Date.Hour + ":" + ph.End_Date.Minute + ":" + ph.End_Date.Second;
                    //string jsString = JsonConvert.SerializeObject(phl);
                    //StreamWriter wr = new StreamWriter(_path);
                    //wr.Write(jsString);                  
                    //wr.Close();
                    //wr.Dispose();

                    FillList(null, phl);                    
                    LoadData(string.Empty);

                }
                return View("Index");
            }
            catch (Exception exc)
            {
                return null;
            }
        }


        public ActionResult AddDate(string userId, DateTime dt)
        {
            try
            {
                _dt = dt;
                _userId = userId;

                StreamReader file = System.IO.File.OpenText(@"C:\Users\Viola\Downloads\calendar\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\Risorse.json");

                JsonSerializer serializer = new JsonSerializer();
                List<Risorsa> risorse = (List<Risorsa>)serializer.Deserialize(file, typeof(List<Risorsa>));

                Schedulazione sche = new Schedulazione();
                Risorsa ri = new Risorsa();
                sche.publicHoliday.End_Date = sche.publicHoliday.End_Date == null ? new DateTime(9999, 12, 31) : new DateTime( sche.publicHoliday.End_Date.Year, sche.publicHoliday.End_Date.Month, sche.publicHoliday.End_Date.Day);
                sche.publicHoliday.Start_Date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                ri = risorse.Where(t => t.id == userId).FirstOrDefault();
                sche.publicHoliday.Id = 0;

                sche.Risorsa = risorse;
                ViewBag.RisorsaSel = ri== null? "a": ri.id;
                ViewBag.Risorse = risorse;
                
                return View(sche);
            }
            catch (Exception exc)
            {
                return null;
            }
        }



        private void getData(string eventId, DateTime dtinizio)
        {
            try
            {

                StreamReader file = System.IO.File.OpenText(@"C:\Users\Viola\Downloads\calendar\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\Risorse.json");

                JsonSerializer serializer = new JsonSerializer();
                List<Risorsa> risorse = (List<Risorsa>)serializer.Deserialize(file, typeof(List<Risorsa>));
                StreamReader fileEventi = System.IO.File.OpenText(@"C:\Users\Viola\Downloads\calendar\testsees\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\PublicHoliday.txt");
                
                Schedulazione sche = new Schedulazione();
                Risorsa ri = new Risorsa();
                sche.publicHoliday.End_Date = sche.publicHoliday.End_Date == null ? new DateTime(9999, 12, 31) : new DateTime(sche.publicHoliday.End_Date.Year, sche.publicHoliday.End_Date.Month, sche.publicHoliday.End_Date.Day);
                sche.publicHoliday.Start_Date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);


                //dall'evento trovo userId per la risorsa


                ri = risorse.Where(t => t.id == userId).FirstOrDefault();

            }
            catch (Exception exc)
            {

            }
        }


        [HttpPost]
        public ActionResult AddDate(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {               
                    List<PublicHoliday> phss = LoadData(string.Empty);

                    PublicHoliday ph = GetDato(collection);//new PublicHoliday();
                    //ph.Desc = collection["publicHoliday.Desc"].ToString();
                    //string inizio = collection["publicHoliday.Start_Date"] + " " + collection["oraInizio"].ToString();
                    //string fine = collection["publicHoliday.End_Date"] + " " + collection["oraFine"].ToString();

                    //ph.Start_Date = Convert.ToDateTime(inizio);
                    //ph.End_Date = Convert.ToDateTime(fine);

                    //List<PublicHoliday> lista = LoadData(string.Empty);
                    //int nMax = lista.Max(t => t.Id) + 1;
                    //ph.Id = nMax;
                    //ph.ResourceId = collection["Risorsa"];
                  
                    phss.Add(ph);
                    FillList(null, phss);
                    LoadData(string.Empty);

                }
                return View("Index");
            }
            catch(Exception ee)
            {
                return View("Index"); ;
            }
        }


        private void FillList(PublicHoliday ph, List<PublicHoliday> phs)
        {
            string jsString = string.Empty;
            StreamWriter wr; 

            if (phs == null)
            {
                jsString = JsonConvert.SerializeObject(ph);
                wr = new StreamWriter(_path, append: true);
            }
            else
            {
                jsString = JsonConvert.SerializeObject(phs);
                wr = new StreamWriter(_path);
            }

            wr.WriteLine(jsString);
            wr.Close();
            wr.Dispose();
        }



        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                List<PublicHoliday> ph = LoadData(string.Empty);
                ph.Remove(ph.Where(t => t.Id == Int32.Parse(id.ToString())).Last());

                FillList(null, ph);
                LoadData(string.Empty);

                return View("Index");
            }
            catch (Exception exc)
            {
                return View("Index");
            }
        }

        //[HttpPost]
        //public ActionResult AddDate(Schedulazione sche)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //db.Entry(prodottoSet).State = EntityState.Modified;
        //            //db.SaveChanges();
        //            PublicHoliday ph = new PublicHoliday();
        //            ph.Desc = sche.publicHoliday.Desc;
        //            ph.Start_Date = Convert.ToDateTime(sche.publicHoliday.Start_Date);
        //            ph.End_Date = Convert.ToDateTime(sche.publicHoliday.End_Date);
        //            ph.Id = 10;
        //            ph.ResourceId = sche.Risorsa.First().id;

        //            string startToWrite = ph.Start_Date.Year + "-" + ph.Start_Date.Month.ToString().PadLeft(2, '0') + "-" + ph.Start_Date.Day.ToString().PadLeft(2, '0') + "T" + ph.Start_Date.Hour + ":" + ph.Start_Date.Minute + ":" + ph.Start_Date.Second;
        //            string endToWrite = ph.End_Date.Year + "-" + ph.End_Date.Month.ToString().PadLeft(2, '0') + "-" + ph.End_Date.Day.ToString().PadLeft(2, '0') + "T" + ph.End_Date.Hour + ":" + ph.End_Date.Minute + ":" + ph.End_Date.Second;

        //            string towrite = "\n\r" + ph.Id.ToString() + "," + ph.ResourceId.ToString() + "," + startToWrite + "," + endToWrite + "," + ph.Desc;                        //5,f,2018-06-07T00:30:00,2018-06-07T02:30:00,event 5
        //            StreamWriter wr = new StreamWriter(@"C:\Users\Viola\Downloads\calendar\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\PublicHoliday.txt", true);
        //            wr.WriteLine(towrite);
        //            wr.Close();
        //            /* 1,b,2018 - 04 - 07T02: 00:00,2018 - 04 - 07T07: 00:00,event 1 
        //               2,c,2018-04-07T05:00:00,2018-04-07T22:00:00,event 2 
        //               3,d,2018-04-06, 2018-04-08,event 3 
        //               4,e,2018-05-07T03:00:00,2018-05-07T08:00:00,event 4 
        //               5,f,2018-06-07T00:30:00,2018-06-07T02:30:00,event 5 */
        //            LoadData();

        //        }
        //        return View("Index");// Index();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}