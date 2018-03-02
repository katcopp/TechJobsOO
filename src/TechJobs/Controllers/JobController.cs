using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models; //added
using TechJobs.ViewModels;

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

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            Job someJob = jobData.Find(id);
            return View(someJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            //TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail(Index) action / view for the new Job.

             if (ModelState.IsValid)
                {

                Employer newEmployer = jobData.Employers.Find(newJobViewModel.EmployerID);
                Location newLocation = jobData.Locations.Find(newJobViewModel.LocationID);
                CoreCompetency newCoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                PositionType newPositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);

                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Location = newLocation,
                    Employer = newEmployer,
                    CoreCompetency = newCoreCompetency,
                    PositionType = newPositionType
                };

                jobData.Jobs.Add(newJob);



                    return Redirect(string.Format("/Job?ID={0}", newJob.ID));
                }


            return View(newJobViewModel);
        }
    }
}
