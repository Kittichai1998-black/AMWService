using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AMWService.Models
{
    public partial class OutputSP
    {
        [Key]
        public int AppointmentId { get; set; }
        public int ReturnCode { get; set; }
        public DateTime SubmittedTime { get; set; }
    }
}
