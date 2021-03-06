﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionInventario.Models;

namespace GestionInventario.Controllers
{
    public class ComprasController : Controller
    {
        private DB_A41D6A_GestionInventarioEntities db = new DB_A41D6A_GestionInventarioEntities();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Lote).Include(c => c.Kardex);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // GET: Compras/Create
        public ActionResult Create(int? idlote)
        {
            if (idlote == null)
            {
                ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex");
                ViewBag.idLote = new SelectList(from l in db.Lote where l.idLote == null select l, "idLote", "idLote");
                return View();
            }


            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex");
            ViewBag.idLote = new SelectList(from l in db.Lote where l.idLote == idlote select l, "idLote", "idLote");

            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCompra,cantidad,precioCompra,idLote,idKardex,fecha")] Compras compras)
        {
            Kardex kar = new Kardex();
            Inventario inv = new Inventario();
            Detalle det = new Detalle();

            det.idLote = compras.idLote;
            det.idInventario = inv.idInventario;        

            if (ModelState.IsValid)
            {
                db.Compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", compras.idLote);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", compras.idKardex);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", compras.idLote);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", compras.idKardex);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCompra,cantidad,precioCompra,idLote,idKardex,fecha")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idLote = new SelectList(db.Lote, "idLote", "idLote", compras.idLote);
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex", compras.idKardex);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
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

        public ActionResult Prueba()
        {
            ViewBag.idKardex = new SelectList(db.Kardex, "idKardex", "idKardex");
            return View();
        }
    }
}
