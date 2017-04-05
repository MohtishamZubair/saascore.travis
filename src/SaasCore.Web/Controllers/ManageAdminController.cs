using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaasCore.Web.Services;

namespace SaasCore.Web.Controllers
{
    public class ManageAdminController : Controller
    {
        readonly ISaasManager _saasManager;

        public ManageAdminController(ISaasManager saasManager)
        {
            _saasManager = saasManager;
        }
        // GET: ManageAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Applications()
        {
            return  Json("ByooQoof");
        }







        // GET: ManageAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManageAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManageAdmin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}