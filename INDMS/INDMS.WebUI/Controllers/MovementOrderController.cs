using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    [ExceptionHandler]
    public class MovementOrderController : Controller
    {
        private INDMSEntities db = new INDMSEntities();

        // GET: MovementOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult New(MovementOrderViewModel m)
        {
            if (!string.IsNullOrEmpty(m.MovementOrder.OrderNo))
            {
                if (!string.IsNullOrEmpty(m.MovementOrder.Subject))
                {
                    if (!string.IsNullOrEmpty(m.MovementOrder.InspectorName))
                    {
                        if (!string.IsNullOrEmpty(m.MovementOrder.FirmName))
                        {
                            //Onward 
                            if (!string.IsNullOrEmpty(m.OnwordDateAndTime))
                            {
                                string[] OnwardDateTime = m.OnwordDateAndTime.Split('-');
                                m.MovementOrder.OnwardStartDate = Convert.ToDateTime(OnwardDateTime[0].Trim());
                                m.MovementOrder.OnwardEndDate = Convert.ToDateTime(OnwardDateTime[1].Trim());

                                if (!string.IsNullOrEmpty(m.MovementOrder.OnwordFrom))
                                {
                                    if (!string.IsNullOrEmpty(m.MovementOrder.OnwordTo))
                                    {
                                        if (!string.IsNullOrEmpty(m.MovementOrder.OnwordModeOfTravel))
                                        {
                                            if (m.MovementOrder.OnwordTo == "OTHER")
                                            {
                                                if (!string.IsNullOrEmpty(m.OnwardModeOfTravel))
                                                {
                                                    //Return
                                                    if (!string.IsNullOrEmpty(m.ReturnDateAndTime))
                                                    {
                                                        string[] ReturnDateTime = m.ReturnDateAndTime.Split('-');
                                                        m.MovementOrder.ReturnStartDate = Convert.ToDateTime(ReturnDateTime[0].Trim());
                                                        m.MovementOrder.ReturnEndDate = Convert.ToDateTime(ReturnDateTime[1].Trim());

                                                        if (!string.IsNullOrEmpty(m.MovementOrder.ReturnFrom))
                                                        {
                                                            if (!string.IsNullOrEmpty(m.MovementOrder.ReturnTo))
                                                            {
                                                                if (!string.IsNullOrEmpty(m.MovementOrder.ReturnModeOfTravel))
                                                                {
                                                                    if (m.MovementOrder.ReturnTo == "OTHER")
                                                                    {
                                                                        if (!string.IsNullOrEmpty(m.ReturnModeOfTravel))
                                                                        {
                                                                            if (!string.IsNullOrEmpty(m.MovementOrder.SigningAuthority))
                                                                            {

                                                                                m.MovementOrder.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                                                                

                                                                                db.MovementOrders.Add(m.MovementOrder);
                                                                                db.SaveChanges();

                                                                            }
                                                                            else
                                                                            {
                                                                                TempData["Error"] = "Please select Signing Authority";
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            TempData["Error"] = "Please Enter Return - Mode Of Travel.";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        TempData["Error"] = "Please Enter Return - Mode Of Travel.";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    TempData["Error"] = "Please select Return - Mode Of Travel.";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                TempData["Error"] = "Please select Return - To.";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            TempData["Error"] = "Please select Return - From.";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        TempData["Error"] = "Please enter Return Date & Time.";
                                                    }
                                                }
                                                else
                                                {
                                                    TempData["Error"] = "Please Enter Onward - Mode Of Travel.";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            TempData["Error"] = "Please select Onward - Mode Of Travel.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "Please select Onward - To.";
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please select Onward - From.";
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please enter Onward Date & Time.";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Select valid Firm Name";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please Select valid Inspector Name.";
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter valid Subject.";
                }
            }
            else
            {
                TempData["Error"] = "Please enter Valid Order Number";
            }

            return View(m);
        }
    }
}