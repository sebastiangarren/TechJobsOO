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
        private Employer EmployerForJob;
        private Location LocationForJob;
        private CoreCompetency CoreCompetencyForJob;
        private PositionType PositionTypeForJob;


        static JobController()
        {
            jobData = JobData.GetInstance();
        }




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
                        EmployerForJob = JobProperty;
                    }
                }

                foreach (var JobProperty in jobData.Locations.ToList())
                {
                    if (newJobViewModel.Location == JobProperty.ID)
                    {
                        LocationForJob = JobProperty;
                    }
                }

                foreach (var JobProperty in jobData.CoreCompetencies.ToList())
                {
                    if (newJobViewModel.CoreCompetency == JobProperty.ID)
                    {
                        CoreCompetencyForJob = JobProperty;
                    }
                }

                foreach (var JobProperty in jobData.PositionTypes.ToList())
                {
                    if (newJobViewModel.PositionType == JobProperty.ID)
                    {
                        PositionTypeForJob = JobProperty;
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

                jobData.Jobs.Add(newJob);
                string newUrl = "/Job?id=" + newJob.ID.ToString();

                return Redirect(newUrl);
            }            


            return View(newJobViewModel);
        }
    }
}
