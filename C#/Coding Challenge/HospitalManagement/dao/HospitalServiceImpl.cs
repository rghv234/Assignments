using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.entity;
using HospitalManagement.exception;
using HospitalManagement.util;

namespace HospitalManagement.dao
{
    public class HospitalServiceImpl : IHospitalService
    {
        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "SELECT appointmentId, patientId, doctorId, appointmentDate, description FROM Appointments WHERE appointmentId = @appointmentId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Appointment
                                {
                                    AppointmentId = reader.GetInt32(0),
                                    PatientId = reader.GetInt32(1),
                                    DoctorId = reader.GetInt32(2),
                                    AppointmentDate = reader.GetDateTime(3),
                                    Description = reader.IsDBNull(4) ? null : reader.GetString(4)
                                };
                            }
                            throw new Exception("Appointment not found.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error retrieving appointment: " + ex.Message);
            }
        }

        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "SELECT appointmentId, patientId, doctorId, appointmentDate, description FROM Appointments WHERE patientId = @patientId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", patientId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                throw new PatientNumberNotFoundException($"No patient found with ID: {patientId}");
                            }
                            while (reader.Read())
                            {
                                appointments.Add(new Appointment
                                {
                                    AppointmentId = reader.GetInt32(0),
                                    PatientId = reader.GetInt32(1),
                                    DoctorId = reader.GetInt32(2),
                                    AppointmentDate = reader.GetDateTime(3),
                                    Description = reader.IsDBNull(4) ? null : reader.GetString(4)
                                });
                            }
                        }
                    }
                }
                return appointments;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error retrieving patient appointments: " + ex.Message);
            }
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "SELECT appointmentId, patientId, doctorId, appointmentDate, description FROM Appointments WHERE doctorId = @doctorId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@doctorId", doctorId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                appointments.Add(new Appointment
                                {
                                    AppointmentId = reader.GetInt32(0),
                                    PatientId = reader.GetInt32(1),
                                    DoctorId = reader.GetInt32(2),
                                    AppointmentDate = reader.GetDateTime(3),
                                    Description = reader.IsDBNull(4) ? null : reader.GetString(4)
                                });
                            }
                        }
                    }
                }
                return appointments;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error retrieving doctor appointments: " + ex.Message);
            }
        }

        public bool ScheduleAppointment(Appointment appointment)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "INSERT INTO Appointments (patientId, doctorId, appointmentDate, description) VALUES (@patientId, @doctorId, @appointmentDate, @description)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", appointment.PatientId);
                        cmd.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                        cmd.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                        cmd.Parameters.AddWithValue("@description", (object)appointment.Description ?? DBNull.Value);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key violation
                {
                    throw new PatientNumberNotFoundException("Invalid patient or doctor ID.");
                }
                throw new Exception("Error scheduling appointment: " + ex.Message);
            }
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "UPDATE Appointments SET patientId = @patientId, doctorId = @doctorId, appointmentDate = @appointmentDate, description = @description WHERE appointmentId = @appointmentId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);
                        cmd.Parameters.AddWithValue("@patientId", appointment.PatientId);
                        cmd.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                        cmd.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                        cmd.Parameters.AddWithValue("@description", (object)appointment.Description ?? DBNull.Value);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key violation
                {
                    throw new PatientNumberNotFoundException("Invalid patient or doctor ID.");
                }
                throw new Exception("Error updating appointment: " + ex.Message);
            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "DELETE FROM Appointments WHERE appointmentId = @appointmentId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error canceling appointment: " + ex.Message);
            }
        }
    }
}
