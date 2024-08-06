﻿using ElectionYousef.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ElectionYousef.Controllers
{
    public class DebatesController : Controller
    {
        private ElectionEntities db = new ElectionEntities();
        // To show the form of debates
        public ActionResult Index()
        {
            return View();
        }
        // To send the data from the view to the controller 
        [HttpPost]
        public ActionResult Index(Debate D, int Circle_ID, string Date_Of_Debate, string Topic, string First_Candidate, string First_Candidate_List, string Second_Candidate, string Second_Candidate_List)
        {
            if (ModelState.IsValid)
            {
                using (ElectionEntities db = new ElectionEntities())
                {
                    D.Circle_ID = Circle_ID;
                    D.Date_Of_Debate = DateTime.Parse(Date_Of_Debate);
                    D.Topic = Topic;
                    D.First_Candidate = First_Candidate;
                    D.First_Candidate_List = First_Candidate_List;
                    D.Second_Candidate = Second_Candidate;
                    D.Second_Candidate_List = Second_Candidate_List;

                    db.Debates.Add(D);
                    db.SaveChanges();

                    // Redirect to a confirmation page or the index to prevent resubmission
                    return RedirectToAction("Index","Debates", new { success = true });
                }
            }

            // If we got this far, something failed, redisplay form
            return View(D);
        }
        public ActionResult Admin()
        {
            return View();
        }



        // GET: Debates/Admin/5
        public ActionResult Admin(int id)
        {
            Debate debate = db.Debates.Find(id);
            if (debate == null)
            {
                return HttpNotFound();
            }
            return View(debate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin([Bind(Include = "ID,Circle_ID,Date_Of_Debate,Topic,First_Candidate,First_Candidate_List,Second_Candidate,Second_Candidate_List,Status,Zoom_link")] Debate D)
        {
            if (ModelState.IsValid)
            {
                db.Entry(D).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(D);
        }
        public ActionResult Index1()
        {
            return View(db.Debates.ToList());
        }
        // GET: Debates1
        public ActionResult IndexDebatsesHome()
        {
            return View(db.Debates.ToList());
        }
        // GET: Debates1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debate debate = db.Debates.Find(id);
            if (debate == null)
            {
                return HttpNotFound();
            }
            return View(debate);
        }

        // GET: Debates1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Debates1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Circle_ID,Date_Of_Debate,Topic,First_Candidate,First_Candidate_List,Second_Candidate,Second_Candidate_List,Status,Zoom_link")] Debate debate)
        {
            if (ModelState.IsValid)
            {
                db.Debates.Add(debate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(debate);
        }

        // GET: Debates1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debate debate = db.Debates.Find(id);
            if (debate == null)
            {
                return HttpNotFound();
            }
            return View(debate);
        }

        // POST: Debates1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Circle_ID,Date_Of_Debate,Topic,First_Candidate,First_Candidate_List,Second_Candidate,Second_Candidate_List,Status,Zoom_link")] Debate debate)
        {

            var x1 = db.Debates.Where(x => x.ID == debate.ID).FirstOrDefault();
            x1.Status = debate.Status;
            x1.Zoom_link = debate.Zoom_link;

            if (ModelState.IsValid)
            {
                db.Entry(x1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(debate);
        }

        // GET: Debates1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debate debate = db.Debates.Find(id);
            if (debate == null)
            {
                return HttpNotFound();
            }
            return View(debate);
        }

        // POST: Debates1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Debate debate = db.Debates.Find(id);
            db.Debates.Remove(debate);
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