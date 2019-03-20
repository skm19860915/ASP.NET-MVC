using System;

namespace sharemycoach.Models
{
    public class RentalModel
    {
        public Guid? Location { get; set; }
        public string LeaveOn { get; set; }
        public string ReturnOn { get; set; }
        public string Destination { get; set; }
        public Guid? Vehicle { get; set; }
        public bool? IsOwnerRenting { get; set; }
    }
}