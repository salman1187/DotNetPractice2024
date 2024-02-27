using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalOOPLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital h = new Hospital();

            Doctor d1 = new Doctor();
            Doctor d2 = new Doctor();
            ConsultantDoctor c1 = new ConsultantDoctor();
            Doctor d3 = c1;
            d1.Speciality = "Orthopaedic";
            d2.Speciality = "Cardiologist";
            d3.Speciality = "Neurologist";
            Nurse n1 = new Nurse();
            Nurse n2 = new Nurse();
            
            Employee e1 = d1;
            Employee e2 = d2;
            Employee e3 = d3;
            Employee e4 = n1;
            Employee e5 = n2;

            h.Employees.Add(e1);
            h.Employees.Add(e2);
            h.Employees.Add(e3);
            h.Employees.Add(e4);
            h.Employees.Add(e5);

            Patient p1 = new Patient();
            Patient p2 = new Patient();

            Ward w1 = new Ward();
            Ward w2 = new Ward();
            w1.TheWardType = WardType.SurgicalUnit;
            w2.TheWardType = WardType.General;
            w1.BasicCost = 5000;
            w2.BasicCost = 1000;
            w1.Patients.Add(p1);
            w2.Patients.Add(p2);

            h.Wards.Add(w1);
            h.Wards.Add(w2);

            Console.WriteLine($"total patients: {h.GetTotalPatients()}");
            Console.WriteLine($"total patients by ward: {h.GetTotalPatientsByWard(w2)}");
            Console.WriteLine($"total doctors: {h.GetTotalDoctors()}");
            Console.WriteLine($"total consultants: {h.GetTotalConsultants()}");
            Console.WriteLine($"get ward cost by type: {h.GetWardCostByType(WardType.General)}");
            List < Doctor > doclist = h.GetDoctorsBySpecialization("Orthopaedic");
            Console.WriteLine("doc by specialization: " + doclist[0].Speciality);

        }
    }
    class Hospital
    {
        public string Name { get; set; }
        public long Phone { get; set; } 
        public string Address { get; set; }
        public List<Ward> Wards { get; set; } = new List<Ward>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public int GetTotalPatients()
        {
            int total = 0;
            foreach(Ward w in  Wards)
                total += w.Patients.Count;
            
            return total;
        }
        public int GetTotalPatientsByWard(Ward ward)
        {
            int total = 0;
            foreach(Ward w in Wards)
                if(w.TheWardType == ward.TheWardType)
                    total += w.Patients.Count;

            return total;
        }
        public int GetTotalDoctors()
        {
            int total = 0;
            foreach (Employee e in Employees)
                if (e is Doctor)
                    total++;

            return total;
        }
        public List<Doctor> GetDoctorsBySpecialization(string spec)
        {
            List<Doctor> doclist = new List<Doctor>();

            foreach (Employee e in Employees)
                if (e is Doctor d)
                    if(d.Speciality.Equals(spec))
                        doclist.Add(d);

            return doclist;
        }
        public int GetTotalConsultants()
        {
            int total = 0;
            foreach (Employee e in Employees)
                if (e is Doctor d)
                    if (d is ConsultantDoctor)
                        total++;

            return total;
        }
        public double GetWardCostByType(WardType wardtype)
        {
            double total = 0;

            foreach (Ward ward in Wards)
                if (ward.TheWardType == wardtype)
                    total += ward.GetWardCost();

            return total;
        }
    }
    class Ward
    {
        public string WardName { get; set; }
        public WardType TheWardType { get; set; }
        public double BasicCost { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public double GetWardCost()
        {
            return WardCostCalculator.FindWardCost(BasicCost, TheWardType);
        }
    }
    class WardCostCalculator
    {
        public static double FindWardCost(double basic, WardType wardtype)
        {
            double total = basic;
            switch (wardtype)
            {
                case WardType.IntensiveCare:
                    total += total * 0.1;
                    break;
                case WardType.General:
                    total += total * 0.2;
                    break;
                case WardType.Pediatric:
                    total += total * 0.25;
                    break;
                case WardType.SurgicalUnit:
                    total += total * 0.4;
                    break;
            }
            return total;
        }
    }
    class Employee : Person
    {
        public DateTime DateOfJoin { get; set; }
        public string Education { get; set; }
    }
    class Patient : Person
    {
        public DateTime AdmittedDate { get; set; }
        public List<string> Allergies { get; set; } 
        public string PatientName { get; set; }
        public string Age { get; set; }
    }
    enum WardType
    {
        IntensiveCare,
        General,
        Pediatric,
        SurgicalUnit
    }
    class Doctor : Employee
    {
        public string Speciality { get; set; }
    }
    class Receptionist : Employee
    {

    }
    class Nurse : Employee
    {

    }
    class ConsultantDoctor : Doctor
    {

    }
    class InternalDoctor : Doctor
    {

    }
    class Person
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResidentialAddress { get; set; }
        public string Gender { get; set; }
        public string phone { get; set; }
    }
}
