using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Importante Llamar al modelo con el cual vamos a trabajar
using Heroes.Models;

namespace Heroes.Controllers
{
    public class HeroeController : Controller
    {
        //Conetarnos a la base de datos CESDE
        //private Heroes_JFCEntities BD = new Heroes_JFCEntities();

        //Conetarnos a la base de datos CASA
        private Heroes_JFCConnection BD = new Heroes_JFCConnection();

        

        // GET: Heroe INDEX
        public ActionResult Index()
        {

            var ListaHeroes = BD.Heroe.ToList();

            return View(ListaHeroes);
        }



        // GET: Heroe DETALLE f
        public ActionResult Detalle(int id)
        {

            var HeroeSelecionado = BD.Heroe.FirstOrDefault(x => x.IdHeroe == id);

            return View(HeroeSelecionado);
        }

        // GET: Heroe Actualizar
        public ActionResult Actualizar(int id)
        {

            var HeroeActualizar = BD.Heroe.FirstOrDefault(x => x.IdHeroe == id);

            return View(HeroeActualizar);
        }


        [HttpPost]
        // Heroe Actualizar POST
        public ActionResult Actualizar(Heroe heroeactualizado)
        {
            var HeroeActualizar = BD.Heroe.FirstOrDefault(x => x.IdHeroe == heroeactualizado.IdHeroe);
            HeroeActualizar.Nombre = heroeactualizado.Nombre;
            HeroeActualizar.Historia = heroeactualizado.Historia;
            HeroeActualizar.Poder = heroeactualizado.Poder;
            HeroeActualizar.Debilidad = heroeactualizado.Debilidad;
            HeroeActualizar.Universo = heroeactualizado.Universo;


            //si se carga una imagen que vaya y guarde la imagen
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var imagen = System.Web.HttpContext.Current.Request.Files["Imagen"];
                string logopath = Server.MapPath("~/Content/Images/");
                string filename = imagen.FileName;

                imagen.SaveAs(logopath + filename);
                HeroeActualizar.Imagen = "~/Content/Images/" + filename;
            }




            //se cofirman los cambios
            BD.SaveChanges();




            //Retorna a la primera pagina
            return RedirectToAction("Index");
        }


        [HttpGet]
        //viaja por GEt
        public ActionResult Nuevo()
        {
            
            return View();
        }

        [HttpPost]
        // Heroe Njeuvo POST
        public ActionResult Nuevo(Heroe HeroeNuevo)
        {
            //si manda una imagen que haga lo siguiente
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var imagen = System.Web.HttpContext.Current.Request.Files["Imagen"];
                string logopath = Server.MapPath("~/Content/Images/");
                string filename = imagen.FileName;

                imagen.SaveAs(logopath + filename);
                HeroeNuevo.Imagen = "~/Content/Images/" + filename;
                BD.Heroe.Add(HeroeNuevo);
                BD.SaveChanges();

            }




            return RedirectToAction("Index");
        }


        // GET: Heroe Actualizar
        public ActionResult Eliminar(int id)
        {

            var HeroeEliminar = BD.Heroe.FirstOrDefault(x => x.IdHeroe == id);
            BD.Heroe.Remove(HeroeEliminar);
            BD.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}