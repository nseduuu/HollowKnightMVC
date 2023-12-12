using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using RpgMvc.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using HollowKnightMVC.Models;

namespace HollowKnightMVC.Controllers;

public class MobsController : Controller
{
    public string uriBase = "http://nseduuu.somee.com/HollowKnightAPI/Mobs/";

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
}