using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using HollowKnightMVC.Models;
using System.Security.Cryptography.X509Certificates;

namespace HollowKnightMVC.Controllers;

public class MobsController : Controller
{
    public string uriBase = "http://nseduardo.somee.com/HollowKnightAPI/Mobs/";

    [HttpGet]
    public ActionResult Create()
    {
        return View("Create");
    }

    [HttpGet]
    public async Task<ActionResult> IndexAsync()
    {
        try
        {
            string uriComplementar = "GetAll";
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<MobsViewModel> listaMobs = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<MobsViewModel>>(serialized));

                return View(listaMobs);
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }


    [HttpGet]
    public async Task<ActionResult> DetailsAsync(int? id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MobsViewModel p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<MobsViewModel>(serialized));
                TempData["Mensagem"] = string.Format("Personagem com Id {0} removido com sucesso!", id);
                return View(p);
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    
    [HttpPost]
    public async Task<ActionResult> CreateAsync(MobsViewModel m)
    {
        try
        {
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(m));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);

            string serialized = await response.Content.ReadAsStringAsync();


            TempData["Mensagem"] = string.Format("Boss {0} foi criado com sucesso!", m.Nome);
            return RedirectToAction("Index", "Mobs");

        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Create");
        }
    }

    [HttpGet]
    public async Task<ActionResult> EditAsync(int? id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MobsViewModel p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<MobsViewModel>(serialized));
                return View(p);
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<ActionResult> EditAsync(MobsViewModel p)
    {
        try
        {
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(p));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] =
                    string.Format("Boss {0}, dificuldade {1} atualizado com sucesso!", p.Nome, p.Dificuldade);
                return RedirectToAction("Index");
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] =
                    string.Format("Boss com ID:{0} removido com sucesso!", id);
                return RedirectToAction("Index");
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

}