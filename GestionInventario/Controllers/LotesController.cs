using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GestionInventario.Models;

namespace GestionInventario.Controllers
{
    public class LotesController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities db = new DB_A41D6A_GestionInventarioEntities();

        public class Lotes{
            public string numLote { get; set;}
            public string cantidadLote { get; set; }
            public string fechaVencimiento { get; set; }
            public string idMedicamento { get; set; }
        }

        // GET: Lotes
        public ActionResult Index()
        {
            var lote = db.Lote.Include(l => l.Medicamento);
            return View(lote.ToList());
        }

        // GET: Lotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            return View(lote);
        }

        // GET: Lotes/Create
        public ActionResult Create(int? id)
        {
            Medicamento medicamento = new Medicamento();
            var kar = from k in db.Kardex
                      where k.idKardex == id
                      select k;
            int idMedicamento = 0;

            foreach (Kardex kard in kar)
            {
                if (kard.idKardex == id)
                {
                    idMedicamento = kard.idMedicamento;
                    break;
                }
            }

            var med = from m in db.Medicamento
                      where m.idMedicamento == idMedicamento
                      select m;

            foreach (Medicamento medi in med)
            {
                medicamento = medi;
                break;
            }

            ViewBag.idMedicamento = new SelectList(from m in db.Medicamento where m.idMedicamento == idMedicamento select m , "idMedicamento", "nombreMedicamento");

            return View();
        }

        // POST: Lotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLote,numLote,cantidadLote,fechaVencimiento,idMedicamento")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                db.Lote.Add(lote);
                db.SaveChanges();
                return RedirectToAction("Create","Compras");
            }

            ViewBag.idMedicamento = new SelectList(db.Medicamento, "idMedicamento", "nombreMedicamento", lote.idMedicamento);
            return Json(lote.idLote);
        }

        [HttpGet]
        public ActionResult Prueba()
        {
            ViewBag.idMedicamento = new SelectList(db.Medicamento, "idMedicamento", "nombreMedicamento");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prueba([Bind(Include = "idLote,numLote,cantidadLote,fechaVencimiento,idMedicamento")] Lote lote)
        {
            return Json(lote.idMedicamento);
        }

        // GET: Lotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMedicamento = new SelectList(db.Medicamento, "idMedicamento", "nombreMedicamento", lote.idMedicamento);
            return View(lote);
        }

        // POST: Lotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLote,numLote,cantidadLote,fechaVencimiento,idMedicamento")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMedicamento = new SelectList(db.Medicamento, "idMedicamento", "nombreMedicamento", lote.idMedicamento);
            return View(lote);
        }

        // GET: Lotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            return View(lote);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lote lote = db.Lote.Find(id);
            db.Lote.Remove(lote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
