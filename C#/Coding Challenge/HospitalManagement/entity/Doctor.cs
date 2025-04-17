using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.entity
{
    public class Doctor
    {
        private int doctorId;
        private string firstName;
        private string lastName;
        private string specialization;
        private string contactNumber;

        public Doctor()
        {
        }

        public Doctor(int doctorId, string firstName, string lastName, string specialization, string contactNumber)
        {
            this.doctorId = doctorId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.specialization = specialization;
            this.contactNumber = contactNumber;
        }

        public int DoctorId { get { return this.doctorId; } set { doctorId = value; } }
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }
        public string Specialization { get { return this.specialization; } set { this.specialization = value; } }
        public string ContactNumber { get { return this.contactNumber; } set { this.contactNumber = value; }}

        public override string ToString() 
        {
            return $"Doctor[DoctorId={doctorId}, FirstName={firstName}, LastName={lastName}, Specialization={specialization}, ContactNumber={contactNumber}]";
        }
    }
}
