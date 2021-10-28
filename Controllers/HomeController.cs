using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonellerUoW.Models;
using PersonellerUoW.Models.Entities;
using PersonellerUoW.Services;
using PersonellerUoW.Services.Interfaces;

namespace PersonellerUoW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UnitOfWork unitOfWork;
        private Context context;
        public HomeController(ILogger<HomeController> logger,Context _context)
        {
           
            _logger = logger;
            context = _context;
            unitOfWork = new UnitOfWork(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ListPersonel()
        { 
            var personels = unitOfWork.Personel.GetAll(filter:null,orderBy:null,x=> x.Country,x=> x.City);
            return View(personels);
        }
        public IActionResult SavePersonel()
        {
            List<Cografya> cografyas = unitOfWork.Cografya.GetAll().ToList();
            ViewBag.Countries = cografyas.Where(x => x.UstID == 0).ToList();
            ViewBag.Cities = cografyas.Where(x => x.UstID != 0).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult SavePersonel(PersonelViewModel personelView)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    List<Cografya> cografyas = unitOfWork.Cografya.GetAll().ToList();
                    ViewBag.Countries = cografyas.Where(x => x.UstID == 0).ToList();
                    ViewBag.Cities = cografyas.Where(x => x.UstID != 0).ToList();
                    return View("SavePersonel", personelView);

                }
                else
                {
                    unitOfWork.CreateTransaction();
                    if (personelView.Media != null)
                    {
                        string imageName = unitOfWork.Media.SaveMediaToDisk(personelView);
                        Personel personel = new Personel
                        {
                            Name = personelView.Name,
                            Surname = personelView.Surname,
                            City = unitOfWork.Cografya.Get(personelView.City.ID),
                            Country = unitOfWork.Cografya.Get(personelView.Country.ID),
                            DateOfBirth = personelView.DateOfBirth,
                            Gender = personelView.Gender
                        };
                        personel.Media = new Medya
                        {
                            MediaName = imageName,
                            MediaURL = $"~/media/{imageName}"
                        };
                        unitOfWork.Personel.Add(personel);
                        unitOfWork.Complete();
                        unitOfWork.Commit();
                        return RedirectToAction("ListPersonel");
                    }
                    else
                    {
                        Personel personel = new Personel
                        {
                            Name = personelView.Name,
                            Surname = personelView.Surname,
                            City = unitOfWork.Cografya.Get(personelView.City.ID),
                            Country = unitOfWork.Cografya.Get(personelView.Country.ID),
                            DateOfBirth = personelView.DateOfBirth,
                            Gender = personelView.Gender
                        };
                        unitOfWork.Personel.Add(personel);
                        unitOfWork.Complete();
                        unitOfWork.Commit();
                        return RedirectToAction("ListPersonel");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(SavePersonel));
                unitOfWork.Rollback();
                return BadRequest();
            }
        }

        public IActionResult UpdatePersonel(int id)
        {
            List<Cografya> cografyas = unitOfWork.Cografya.GetAll().ToList();
            ViewBag.Countries = cografyas.Where(x => x.UstID == 0).ToList();
            ViewBag.Cities = cografyas.Where(x => x.UstID != 0).ToList();
            Personel personel = unitOfWork.Personel.Find(filter: x => x.PersonelID == id,x => x.City,x=> x.Country,x=> x.Media).FirstOrDefault();
           PersonelViewModel personelView = new PersonelViewModel
           {
                PersonelID = personel.PersonelID,
                Name = personel.Name,
                Surname = personel.Surname,
                DateOfBirth = personel.DateOfBirth,
                Gender = personel.Gender,
                MediaUrl = personel.Media != null ? personel.Media.MediaURL : null,
                City = personel.City,
                Country = personel.Country

            };
            return View(personelView);
        }
        [HttpPost]
        public IActionResult UpdatePersonel(PersonelViewModel personelView,int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("UpdatePersonel", personelView);
                }
                else
                {
                    unitOfWork.CreateTransaction();
                    personelView.PersonelID = id;
                    Personel updatedPersonel = unitOfWork.Personel.Get(personelView.PersonelID);
                    updatedPersonel.Name = personelView.Name;
                    updatedPersonel.Surname = personelView.Surname;
                    updatedPersonel.City = unitOfWork.Cografya.Get(personelView.City.ID);
                    updatedPersonel.Country = unitOfWork.Cografya.Get(personelView.Country.ID);
                    updatedPersonel.DateOfBirth = personelView.DateOfBirth;
                    updatedPersonel.Gender = personelView.Gender;
                    if (personelView.Media != null)
                    {
                        string imageName = unitOfWork.Media.SaveMediaToDisk(personelView);
                        updatedPersonel.Media = new Medya
                        {
                            MediaName = imageName,
                            MediaURL = $"~/media/{imageName}"
                        };
                    }
                    unitOfWork.Complete();
                    unitOfWork.Commit();
                    return RedirectToAction("ListPersonel");
                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(UpdatePersonel));
                unitOfWork.Rollback();
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult DeletePersonel(int id)
        {
            try
            {
                Personel personel = unitOfWork.Personel.Get(id);
                unitOfWork.CreateTransaction();
                unitOfWork.Personel.Remove(personel);
                unitOfWork.Complete();
                unitOfWork.Commit();
                return RedirectToAction("ListPersonel");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(DeletePersonel));
                unitOfWork.Rollback();
                return BadRequest();
            }
        }
    }
}