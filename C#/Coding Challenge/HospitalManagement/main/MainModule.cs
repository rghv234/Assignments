using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.entity;
using HospitalManagement.dao;
using HospitalManagement.exception;

namespace HospitalManagement
{
    public class MainModule
    {
        private static readonly IHospitalService hospitalService = new HospitalServiceImpl();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nHospital Management System");
                Console.WriteLine("1. Get Appointment by ID");
                Console.WriteLine("2. Get Appointments for Patient");
                Console.WriteLine("3. Get Appointments for Doctor");
                Console.WriteLine("4. Schedule Appointment");
                Console.WriteLine("5. Update Appointment");
                Console.WriteLine("6. Cancel Appointment");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Appointment ID: ");
                            int appointmentId = int.Parse(Console.ReadLine());
                            Appointment appointment = hospitalService.GetAppointmentById(appointmentId);
                            Console.WriteLine(appointment);
                            break;

                        case "2":
                            Console.Write("Enter Patient ID: ");
                            int patientId = int.Parse(Console.ReadLine());
                            List<Appointment> patientAppointments = hospitalService.GetAppointmentsForPatient(patientId);
                            foreach (var appt in patientAppointments)
                            {
                                Console.WriteLine(appt);
                            }
                            break;

                        case "3":
                            Console.Write("Enter Doctor ID: ");
                            int doctorId = int.Parse(Console.ReadLine());
                            List<Appointment> doctorAppointments = hospitalService.GetAppointmentsForDoctor(doctorId);
                            foreach (var appt in doctorAppointments)
                            {
                                Console.WriteLine(appt);
                            }
                            break;

                        case "4":
                            Console.Write("Enter Patient ID: ");
                            int newPatientId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Doctor ID: ");
                            int newDoctorId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Appointment Date (yyyy-MM-dd): ");
                            DateTime appointmentDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Description: ");
                            string description = Console.ReadLine();
                            Appointment newAppointment = new Appointment
                            {
                                PatientId = newPatientId,
                                DoctorId = newDoctorId,
                                AppointmentDate = appointmentDate,
                                Description = description
                            };
                            bool scheduled = hospitalService.ScheduleAppointment(newAppointment);
                            Console.WriteLine(scheduled ? "Appointment scheduled successfully." : "Failed to schedule appointment.");
                            break;

                        case "5":
                            Console.Write("Enter Appointment ID: ");
                            int updateAppointmentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter New Patient ID: ");
                            int updatePatientId = int.Parse(Console.ReadLine());
                            Console.Write("Enter New Doctor ID: ");
                            int updateDoctorId = int.Parse(Console.ReadLine());
                            Console.Write("Enter New Appointment Date (yyyy-MM-dd): ");
                            DateTime updateAppointmentDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter New Description: ");
                            string updateDescription = Console.ReadLine();
                            Appointment updatedAppointment = new Appointment
                            {
                                AppointmentId = updateAppointmentId,
                                PatientId = updatePatientId,
                                DoctorId = updateDoctorId,
                                AppointmentDate = updateAppointmentDate,
                                Description = updateDescription
                            };
                            bool updated = hospitalService.UpdateAppointment(updatedAppointment);
                            Console.WriteLine(updated ? "Appointment updated successfully." : "Failed to update appointment.");
                            break;

                        case "6":
                            Console.Write("Enter Appointment ID: ");
                            int cancelAppointmentId = int.Parse(Console.ReadLine());
                            bool canceled = hospitalService.CancelAppointment(cancelAppointmentId);
                            Console.WriteLine(canceled ? "Appointment canceled successfully." : "Failed to cancel appointment.");
                            break;

                        case "7":
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (PatientNumberNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
