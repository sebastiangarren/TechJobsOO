using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using System.ComponentModel.DataAnnotations;
using TechJobs.Models;
using System;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;


        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        public PositionType PositionTypeForJob { get; set; }
        public CoreCompetency CoreCompetencyForJob { get; set; }
        public Location LocationForJob { get; set; }
        public Employer EmployerForJob { get; set; }




        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
       
            Job job = jobData.Find(id);

            return View(job);
        }

 
        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {

            
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.


            if (ModelState.IsValid)
            {
                

                foreach (var JobProperty in jobData.Employers.ToList())
                {
                    if (newJobViewModel.EmployerID == JobProperty.ID)
                    {
                        Employer EmployerForJob = JobProperty;
                    }
                }

                foreach (var JobProperty in jobData.Locations.ToList())
                {
                    if (newJobViewModel.Location == JobProperty.ID)
                    {
                        Location LocationForJob = JobProperty;
                    }
                }

                foreach (var JobProperty in jobData.CoreCompetencies.ToList())
                {
                    if (newJobViewModel.CoreCompetency == JobProperty.ID)
                    {
                        CoreCompetency CoreCompetencyForJob = JobProperty;
                    }
                }

                foreach (var JobProperty in jobData.PositionTypes.ToList())
                {
                    if (newJobViewModel.PositionType == JobProperty.ID)
                    {
                        PositionType PositionTypeForJob = JobProperty;
                    }
                }

                
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = EmployerForJob,
                    Location = LocationForJob,
                    CoreCompetency = CoreCompetencyForJob,
                    PositionType = PositionTypeForJob,
                };

                

                return RedirectToAction("Job", new { id = newJob.ID});
            }            


            return View(newJobViewModel);
        }
    }
}
