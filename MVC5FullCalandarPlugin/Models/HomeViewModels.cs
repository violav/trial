using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC5FullCalandarPlugin.Models
{
    public class PublicHoliday
    {
        public PublicHoliday()
        {
            Start_Date = DateTime.Now;
            End_Date = DateTime.Now;
        }

        public int Sr { get; set; }

        [DisplayName("Oggetto")]
        public string Title { get; set; }

        [DisplayName("Nota")]
        [DataType(DataType.MultilineText)]        
        public string Desc { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Inizio")]
        public DateTime Start_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fine")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime End_Date { get; set; }

        public int Id { get; set; }
        public string ResourceId { get; set; }

        //onclick="location.href='@Url.Action("Delete", "Home", new{ id = Model.publicHoliday.Id } )'"
    }

    public class Schedulazione
    {
        public PublicHoliday publicHoliday = new PublicHoliday();
        public List<Risorsa> Risorsa = new List<Risorsa>();
    }
}
