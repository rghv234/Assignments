using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.entity
{
    public class Patient
    {
        private int patientId;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        public string gender;
        public string contactNumber;
        private string address;

        public Patient()
        {
        }

        public Patient(int patientId, string firstName, string lastName, DateTime dateOfBirth, string gender, string contactNumber, string address)
        {
            this.patientId = patientId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.contactNumber = contactNumber;
            this.address = address;
        }

        public int PatientId{get { return patientId; } set { patientId = value; }}
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public string Gender { get { return gender; } set { gender = value; } }
        public string ContactNumber { get { return contactNumber; } set { contactNumber = value; } }
        public string Address { get { return address; } set { address = value; } }

        public override string ToString()
        {
            return $"Patient[PatientId={patientId}, FirstName={firstName}, LastName={lastName}, DateOfBirth={dateOfBirth:yyyy-MM-dd}, Gender={gender}, ContactNumber={contactNumber}, Address={address}]";
        }
    }
}
