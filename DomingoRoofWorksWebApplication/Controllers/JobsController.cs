using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DomingoRoofWorksWebApplication.Models;

namespace DomingoRoofWorksWebApplication.Controllers
{
    public class JobsController : Controller
    {
        string mm;
        private DomingoRoofWorksEntities db = new DomingoRoofWorksEntities();

        // GET: Jobs
        public ActionResult CustomerInvoiceIndex()
        {
            var jobs = db.Jobs.Include(j => j.Customer).Include(j => j.Job_Type);
            return View(jobs.ToList());
        }
        public ActionResult JobIndex()
        {
            var jobs = db.Jobs.Include(j => j.Customer).Include(j => j.Job_Type);
            return View(jobs.ToList());
        }
        public ActionResult EmployeesIndex()
        {
            return View(db.Employees.ToList());
        }
        public ActionResult CustomerIndex()
        {
            var Customer = db.Customers.Include(j => j.Address);
            return View(Customer.ToList());
        }
        public ActionResult Job_TypeIndex()
        {
            return View(db.Job_Type.ToList());
        }

        //----------------------------------------------------EmployeeDetails----------------------------------------------------------
        // GET: Employees/Details/5
        public ActionResult EmployeeDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        //----------------------------------------------------EmployeeDetails----------------------------------------------------------

        //----------------------------------------------------JobDetails----------------------------------------------------------
        public ActionResult JobDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        //----------------------------------------------------JobDetails----------------------------------------------------------


        //----------------------------------------------------EditCustomer----------------------------------------------------------
        // GET: Customers/Edit/5
        public ActionResult EditCustomer(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Address_ID = new SelectList(db.Addresses, "Address_ID", "Street", customer.Address_ID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer([Bind(Include = "Customer_ID,Address_ID,C_FirstName,C_LastName")] Customer customer, [Bind(Include = "Address_ID,Street,city,PostalCode")] Address address)
        {
            address.Address_ID = customer.Address_ID;
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerIndex");
            }
            ViewBag.Address_ID = new SelectList(db.Addresses, "Address_ID", "Street", customer.Address_ID);
            return View(customer);
        }
        //----------------------------------------------------EditCustomer----------------------------------------------------------

        //----------------------------------------------------EditEmployees----------------------------------------------------------
        // GET: Employees/Edit/5
        public ActionResult EditEmployees(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployees([Bind(Include = "Emp_ID,E_FirstName,E_LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        //----------------------------------------------------EditEmployees----------------------------------------------------------

        //----------------------------------------------------EditJob_Type----------------------------------------------------------
        // GET: Job_Type/Edit/5
        public ActionResult EditJob_Type(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Type job_Type = db.Job_Type.Find(id);
            if (job_Type == null)
            {
                return HttpNotFound();
            }
            return View(job_Type);
        }

        // POST: Job_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJob_Type([Bind(Include = "JobType_ID,JobTypeDesc,DailyRate")] Job_Type job_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Job_TypeIndex");
            }
            return View(job_Type);
        }
        //----------------------------------------------------EditJob_Type----------------------------------------------------------
        
        //----------------------------------------------------CUSTOMER_INVOICE----------------------------------------------------------
        public ActionResult CustomersInvoice(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        //----------------------------------------------------CUSTOMER_INVOICE----------------------------------------------------------

        //----------------------------------------------------CreateMaterials----------------------------------------------------------
        public ActionResult CreateMaterials()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMaterials([Bind(Include = "MaterialDesc")] Material material)
        {
            var ff = db.Materials.OrderByDescending(t => t.Material_ID).FirstOrDefault();
            string dd = (Convert.ToInt32(ff.Material_ID.Substring(3, 3)) + 1).ToString();
            int Display = dd.Length;
            if (Display == 1)
            {
                material.Material_ID = "M_00" + dd;
            }
            else
            {
                if (Display == 2)
                {

                    material.Material_ID = "M_0" + dd;
                }
                else
                {

                    material.Material_ID = "M_" + dd;
                }
            }

            if (ModelState.IsValid)
            {
                db.Materials.Add(material);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(material);
        }
        //----------------------------------------------------CreateMaterials----------------------------------------------------------

        //----------------------------------------------------CreateCustomer----------------------------------------------------------
        // GET: Customers/Create
        public ActionResult CreateCustomer()
        {
            ViewBag.Address_ID = new SelectList(db.Addresses, "Address_ID", "Street");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer([Bind(Include = "C_FirstName,C_LastName")] Customer customer, [Bind(Include = "Street,city,PostalCode")] Address address)
        {
            var ss = db.Addresses.OrderByDescending(t => t.Address_ID).FirstOrDefault();
            string ww = (Convert.ToInt32(ss.Address_ID.Substring(3, 3)) + 1).ToString();
            int Display2 = ww.Length;
            if (Display2 == 1)
            {
                address.Address_ID = "A_00" + ww;
            }
            else
            {
                if (Display2 == 2)
                {
                    address.Address_ID = "A_0" + ww;
                }
                else
                {
                    address.Address_ID = "A_" + ww;
                }
            }

            var ff = db.Customers.OrderByDescending(t => t.Customer_ID).FirstOrDefault();
            string dd = (Convert.ToInt32(ff.Customer_ID.Substring(3, 3)) + 1).ToString();
            int Display = dd.Length;
            if (Display == 1)
            {
                customer.Customer_ID = "C_00" + dd;
                customer.Address_ID = "A_00" + ww;
            }
            else
            {
                if (Display == 2) 
                {
                    customer.Customer_ID = "C_0" + dd;
                    customer.Address_ID = "A_0" + ww;
                }
                else
                {
                    customer.Customer_ID = "C_" + dd;
                    customer.Address_ID = "A_" + ww;
                }
            }

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.Address_ID = new SelectList(db.Addresses, "Address_ID", "Street", customer.Address_ID);
            return View(customer);
        }
        //----------------------------------------------------CreateCustomer----------------------------------------------------------

        //----------------------------------------------------CreateEmployees----------------------------------------------------------
        // GET: Employees/Create
        public ActionResult CreateEmployees()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployees([Bind(Include = "E_FirstName,E_LastName")] Employee employee)
        {
            var ss = db.Employees.OrderByDescending(t => t.Emp_ID).FirstOrDefault();
            string dd = (Convert.ToInt32(ss.Emp_ID.Substring(3, 3)) + 1).ToString();
            int Display = dd.Length;
            if (Display == 1)
            {
                employee.Emp_ID = "EMP" + dd;
            }
            else
            {
                if (Display == 2)
                {

                    employee.Emp_ID = "EMP" + dd;
                }
                else
                {

                    employee.Emp_ID = "EMP" + dd;
                }
            }

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(employee);
        }
        //----------------------------------------------------CreateEmployees----------------------------------------------------------

        //----------------------------------------------------CreateJob_Type----------------------------------------------------------
        // GET: Job_Type/Create
        public ActionResult CreateJob_Type()
        {
            return View();
        }

        // POST: Job_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJob_Type([Bind(Include = "JobTypeDesc,DailyRate")] Job_Type job_Type)
        {
            var ss = db.Job_Type.OrderByDescending(t => t.JobType_ID).FirstOrDefault();
            string dd = (Convert.ToInt32(ss.JobType_ID.Substring(3, 3)) + 1).ToString();
            int Display = dd.Length;
            if (Display == 1)
            {
                job_Type.JobType_ID = "JT_00" + dd;
            }
            else
            {
                if (Display == 2)
                {
                    job_Type.JobType_ID = "JT_0" + dd;
                }
                else
                {
                    job_Type.JobType_ID = "JT_" + dd;
                }
            }
            if (ModelState.IsValid)
            {
                db.Job_Type.Add(job_Type);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(job_Type);
        }
        //----------------------------------------------------CreateJob_Type----------------------------------------------------------

        //----------------------------------------------------CreateJob----------------------------------------------------------
        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.JobType_ID = new SelectList(db.Job_Type, "JobType_ID", "JobTypeDesc");
            ViewBag.Material_ID = new SelectList(db.Materials, "Material_ID", "MaterialDesc");
            ViewBag.Emp_ID = new SelectList(db.Employees, "Emp_ID", "E_FirstName");
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "C_FirstName");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_ID,JobType_ID,NoOfDays")] Job job , string[] Emp_ID, [Bind(Include = "Material_ID,Quantity")] Job_Material job_Materials)
        {
            var FF = db.Jobs.OrderByDescending(r => r.Job_Card_No).FirstOrDefault();
            job.Job_Card_No = (Convert.ToInt32(FF.Job_Card_No) + 1).ToString();
            Employee_Job[] ej = new Employee_Job[Emp_ID.Length];
            string jj;
            var ss = db.Employee_Job.OrderByDescending(t => t.EmployeesJob_ID).FirstOrDefault();
            string dd = (Convert.ToInt32(ss.EmployeesJob_ID.Substring(3, 3)) + 1).ToString();
            int Display = dd.Length;
            if (Display == 1)
            {
                jj = "EJ_00" + dd;
            }
            else
            {
                if (Display == 2)
                {
                    jj = "EJ_0" + dd;
                }
                else
                {
                    jj = "EJ_" + dd;
                }
            }
            for (int i = 0; i < Emp_ID.Length; i++)
            {
                ej[i] = new Employee_Job(jj, Emp_ID[i], job.Job_Card_No);
                string ll = (Convert.ToInt32(dd) + 1).ToString();
                Display = ll.Length;
                if (Display == 1)
                {
                    jj = "EJ_00" + ll;
                }
                else
                {
                    if (Display == 2)
                    {
                        jj = "EJ_0" + ll;
                    }
                    else
                    {
                        jj = "EJ_" + ll;
                    }
                }
                dd = ll;
            }

            var pp = db.Job_Material.OrderByDescending(t => t.JobMaterials_ID).FirstOrDefault();
            string kk = (Convert.ToInt32(pp.JobMaterials_ID.Substring(3, 3)) + 1).ToString();
            int Display2 = kk.Length;
            if (Display2 == 1)
            {
                job_Materials.JobMaterials_ID = "JM_00" + kk;
            }
            else
            {
                if (Display2 == 2)
                {
                    job_Materials.JobMaterials_ID = "JM_0" + kk;
                }
                else
                {
                    job_Materials.JobMaterials_ID = "JM_" + kk;
                }
            }

            job_Materials.Job_Card_No = job.Job_Card_No;

            Customer customer = new Customer();
            
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                foreach (var sc in ej)
                {
                    db.Employee_Job.Add(sc);
                }
                db.Job_Material.Add(job_Materials);
                db.SaveChanges();
                return RedirectToAction("JobIndex");
            }

            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Address_ID", job.Customer_ID);
            ViewBag.JobType_ID = new SelectList(db.Job_Type, "JobType_ID", "JobTypeDesc", job.JobType_ID);
            return View(job);
        }
        //----------------------------------------------------CreateJob----------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
