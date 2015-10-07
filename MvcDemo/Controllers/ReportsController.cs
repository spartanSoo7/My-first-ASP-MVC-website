using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;
using System.Globalization;

namespace MvcDemo.Controllers
{
    public class ReportsController : Controller
    {
        private SFS db = new SFS();

        public ActionResult Index()
        {
            ViewBag.Message = "Student Finance Support System Reports";

            return View();
        }

        //
        // GET: /Voucher/Weekly Summery

        public ActionResult WeekSum(string datepicker = null , int VOUCHER_TYPE_ID = 0)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);
            ViewBag.VOUCHER_TYPE_ID = new SelectList(db.VOUCHER_TYPE_TABLLE, "VOUCHER_TYPE_ID", "VOUCHER_TYPE", "");

            if (VOUCHER_TYPE_ID != 0 && datepicker != null)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    
                    DateTime dateValueEnd = dateValue.AddDays(6);
                    

                    voucher_table = voucher_table.Where(
                         s => s.VOUCHER_TYPE_ID == VOUCHER_TYPE_ID &&
                              s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd
                        );

                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }

        //
        // GET: /Voucher/Monthly Summery

        public ActionResult MonthSum(string datepicker = null)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);
            

            if (datepicker != null)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddMonths(1);


                    voucher_table = voucher_table.Where(
                         s => s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd
                        );

                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }

        
        //
        // GET: /Voucher/Monthly Summery

        public ActionResult MonthSumGrantType(string datepicker = null, int VOUCHER_TYPE_ID = 0)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);
            ViewBag.VOUCHER_TYPE_ID = new SelectList(db.VOUCHER_TYPE_TABLLE, "VOUCHER_TYPE_ID", "VOUCHER_TYPE", "");

            if (VOUCHER_TYPE_ID != 0 && datepicker != null)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddMonths(1);


                    voucher_table = voucher_table.Where(
                         s => s.VOUCHER_TYPE_ID == VOUCHER_TYPE_ID &&
                              s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd
                        );
                    voucher_table = voucher_table.OrderByDescending(s => s.VOUCHER_TYPE_TABLLE.VOUCHER_TYPE);
                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }

        //
        // GET: /Voucher/Monthly Summery by campus

        public ActionResult MonthSumCampus(string datepicker = null, int CAMPUS_ID = 0)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE).Include(v => v.STUDENT.AVAILABLE_COURSE.CAMPUS_TABLE);
            ViewBag.CAMPUS_ID = new SelectList(db.CAMPUS_TABLE, "CAMPUS_ID", "CAMPUS_NAME", "");
            
            if (datepicker != null && CAMPUS_ID != 0)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddMonths(1);


                    voucher_table = voucher_table.Where(
                         s => s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd &&
                              s.STUDENT.AVAILABLE_COURSE.CAMPUS_ID == CAMPUS_ID
                        );
                    voucher_table = voucher_table.OrderByDescending(s => s.STUDENT.AVAILABLE_COURSE.CAMPUS_TABLE.CAMPUS_NAME);
                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }
        
        //
        // GET: /Voucher/Monthly Summery by campus and voucher type

        public ActionResult MonthSumCampusGrantType(string datepicker = null, int VOUCHER_TYPE_ID = 0, int CAMPUS_ID = 0)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE).Include(v => v.STUDENT.AVAILABLE_COURSE.CAMPUS_TABLE);
            ViewBag.VOUCHER_TYPE_ID = new SelectList(db.VOUCHER_TYPE_TABLLE, "VOUCHER_TYPE_ID", "VOUCHER_TYPE", "");
            ViewBag.CAMPUS_ID = new SelectList(db.CAMPUS_TABLE, "CAMPUS_ID", "CAMPUS_NAME", "");
            
            if (VOUCHER_TYPE_ID != 0 && datepicker != null && CAMPUS_ID != 0)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddMonths(1);


                    voucher_table = voucher_table.Where(
                         s => s.VOUCHER_TYPE_ID == VOUCHER_TYPE_ID &&
                              s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd &&
                              s.STUDENT.AVAILABLE_COURSE.CAMPUS_ID == CAMPUS_ID
                        );
                    voucher_table = voucher_table.OrderByDescending(s => s.STUDENT.AVAILABLE_COURSE.CAMPUS_TABLE.CAMPUS_NAME);

                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }

        //
        // GET: /Voucher/Monthly Summery by campus and voucher type

        public ActionResult MonthSumCampusGrant(string datepicker = null, int CAMPUS_ID = 0)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE).Include(v => v.STUDENT.AVAILABLE_COURSE.CAMPUS_TABLE);
            ViewBag.CAMPUS_ID = new SelectList(db.CAMPUS_TABLE, "CAMPUS_ID", "CAMPUS_NAME", "");

            if (datepicker != null && CAMPUS_ID != 0)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddMonths(1);


                    voucher_table = voucher_table.Where(
                         s => s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd &&
                              s.STUDENT.AVAILABLE_COURSE.CAMPUS_ID == CAMPUS_ID
                        );
                    voucher_table = voucher_table.OrderByDescending(s => s.VOUCHER_TYPE_TABLLE.VOUCHER_TYPE);

                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }

        //
        // GET: /Voucher/Monthly Summery by fauclty

        public ActionResult MonthSumFaculty(string datepicker = null)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE).Include(v => v.STUDENT.AVAILABLE_COURSE.FACULTY_TABLE);

            if (datepicker != null)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddMonths(1);


                    voucher_table = voucher_table.Where(
                         s => s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd
                        );

                    voucher_table = voucher_table.OrderByDescending(s => s.STUDENT.AVAILABLE_COURSE.FACULTY_TABLE.FACULTY_NAME);

                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }


        //
        // GET: /Voucher/Monthly Summery by fauclty showing grant types issued

        public ActionResult MonthSumFacultyGrant(string datepicker = null, int FACULTY_ID = 0)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE).Include(v => v.STUDENT.AVAILABLE_COURSE.FACULTY_TABLE);
            ViewBag.FACULTY_ID = new SelectList(db.FACULTY_TABLE, "FACULTY_ID", "Faculty_NAME", "");

            if (datepicker != null && FACULTY_ID != 0)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddMonths(1);


                    voucher_table = voucher_table.Where(
                         s => s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd &&
                              s.STUDENT.AVAILABLE_COURSE.FACULTY_ID == FACULTY_ID
                        );
                    voucher_table = voucher_table.OrderByDescending(s => s.STUDENT.AVAILABLE_COURSE.FACULTY_TABLE.FACULTY_NAME);
                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }


        //
        // GET: /Voucher/Yearly Summery

        public ActionResult YearlySum(string datepicker = null)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);


            if (datepicker != null)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddYears(1);


                    voucher_table = voucher_table.Where(
                         s => s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd
                        );

                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }



        //
        // GET: /Voucher/daily Summery

        public ActionResult DaySum(string datepicker = null)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);


            if (datepicker != null)
            {
                DateTime dateValue;
                string[] formats = { "MM/dd/yyyy" };
                bool date = false;
                try
                {
                    var dateTime = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    date = true;
                }
                catch
                {
                    date = false;
                }
                if (date == true)
                {
                    dateValue = DateTime.ParseExact(datepicker, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    DateTime dateValueEnd = dateValue.AddDays(1);


                    voucher_table = voucher_table.Where(
                         s => s.DATE_OF_ISSUE >= dateValue && s.DATE_OF_ISSUE < dateValueEnd
                        );

                }
                else
                {
                    TempData["dateFail"] = "Failed to convert your input to a date";
                }


            }

            return View(voucher_table.ToList());
        }


        //
        // GET: /Voucher/Age Summery

        public ActionResult Age(/*int ageInputMin = 0, int ageInputMax = 0*/)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);

            voucher_table = voucher_table.OrderBy(s => s.STUDENT.DOB);

            //ATTEMPT TO SHOW DATA IN AN AGE RANGE 
          /*  if (ageInputMin != 0 && ageInputMin < 120 && ageInputMax != 0 && ageInputMax < 120)
            {
                DateTime today = DateTime.Today;

                

                DateTime min = today.AddYears(-(ageInputMax + 1));
                DateTime max = today.AddYears(-ageInputMin);



                    voucher_table = from e in voucher_table
                                //get the difference in years since the birthdate
            let years = DateTime.Now.Year - DateTime.Parse(e.STUDENT.DOB).Year
            //get the date of the birthday this year
            let birthdayThisYear = DateTime.Parse(e.STUDENT.DOB).AddYears(years)
            select new
            {
                //if the birthday hasn't passed yet this year we need years - 1
                Age = birthdayThisYear > DateTime.Now ? years - 1 : years
            };

                    voucher_table = voucher_table.Where(
                         s => s.Age >= ageInputMin || s.Age <= ageInputMax
                        );

            }
            else if(ageInputMin >= 120 || ageInputMax >= 120)
            {
                TempData["inputFail"] = "Sorry but you cannot view students older than 120 years old";
            }
            else
            {
                TempData["inputFail"] = "Input not recognised as a number";
            }*/

            return View(voucher_table.ToList());
        }

        //
        // GET: /Voucher/Age Summery

        public ActionResult Gender(String gender = null)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE);

            voucher_table = voucher_table.OrderBy(s => s.STUDENT.DOB);

            if (gender != null)
            {
                      voucher_table = voucher_table.Where(
                           s => s.STUDENT.GENDER == gender
                          );

            }
            else
             {
                  TempData["inputFail"] = "Input not recognised as a gender";
             }

            return View(voucher_table.ToList());
        }

        //
        // GET: /Voucher/ETHINCITY

        public ActionResult Ethinicity(int ETHINICITY_ID = 0)
        {
            var voucher_table = db.VOUCHER_TABLE.Include(v => v.STUDENT).Include(v => v.VOUCHER_TYPE_TABLLE).Include(v => v.STUDENT.ETHINICITY_TABLE);
            ViewBag.ETHINICITY_ID = new SelectList(db.ETHINICITY_TABLE, "ETHINICITY_ID", "ETHINICITY", "");

            if (ETHINICITY_ID != 0)
            {
                

                    voucher_table = voucher_table.Where(
                         s => s.STUDENT.ETHINICITY_ID == ETHINICITY_ID
                        );

                    voucher_table = voucher_table.OrderByDescending(s => s.STUDENT.AVAILABLE_COURSE.FACULTY_TABLE.FACULTY_NAME);

            }
            else
            {
               TempData["inputFail"] = "Input was no recognised as an ethinicity";
            }


  

            return View(voucher_table.ToList());
        }

    }
}